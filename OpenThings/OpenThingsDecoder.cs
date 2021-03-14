using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenThings
{
    public class OpenThingsDecoder : IOpenThingsDecoder
    {
        private static ushort random;

        public Message Decode(IList<byte> payload, IList<PidMap> pidMaps)
        {
            Crc16Ccitt crc16Ccitt = new Crc16Ccitt(0);

            if (payload == null || payload.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(payload));
            }

            if (payload.Count < 11)
                throw new OpenThingsException($"Invalid buffer length [{payload.Count}] too short");

            if (payload[0] < 11)
                throw new OpenThingsException($"Invalid OpenThings Header length [{payload[0]}]");

            if (payload[0] > payload.Count)
                throw new OpenThingsException($"Invalid OpenThings Header length [{payload[0]}] Buffer Length: [{payload.Count}]");

            var pip = BitConverter.ToUInt16(payload.Skip(3).Take(2).Reverse().ToArray(), 0);

            var header = new MessageHeader(payload[1], payload[2], pip);

            var body = payload.Take(payload[0] + 1).Skip(5).ToList();

            if (pip != 0)
            {
                var pidMap = pidMaps.FirstOrDefault(_ => _.ManufacturerId == header.ManufacturerId);

                if (pidMap == null)
                {
                    throw new OpenThingsException($"No [{nameof(PidMap)}] found for manufacture id [0x{header.ManufacturerId:X}]");
                }

                body = Decrypt(body, pidMap.Pid, header.Pip);

                header.SetSensorId(body.Take(3).ToList());
            }
            else
            {
                header.SetSensorId(body.Take(3).ToList());
            }

            ushort crcActual = (ushort)((body.Skip(body.Count - 2).First() << 8) + body.Skip(body.Count - 1).First());
            ushort crcExpected = crc16Ccitt.ComputeChecksum(body.Take(body.Count - 2).ToArray());

            if (crcActual != crcExpected)
                throw new OpenThingsException($"Invalid Crc Expected: [0x{crcExpected:X4}] Actual: [0x{crcActual:X4}]");

            var message = new Message(header);

            ParseMessageRecords(message, body.Skip(3).Take(body.Count - 6).ToList());

            return message;
        }

        private void ParseMessageRecords(Message message, List<byte> recordBytes)
        {
            int i = 0;

            while (i < recordBytes.Count)
            {
                var parameter = Parameter.GetParameter(recordBytes[i]);

                var recordType = (RecordType) (recordBytes[i + 1] >> 4);

                var length = (recordBytes[i + 1] & 0xFF);

                message.Records.Add(new MessageRecord(parameter, MapMessageRecordData(recordType, length, recordBytes.Skip(i + 2).Take(length).ToList())));

                i += length + 2;
            };
        }

        private BaseMessageRecordData MapMessageRecordData(RecordType recordType, int length, List<byte> dataBytes)
        {
            switch (recordType)
            {
                case RecordType.UnsignedX0:
                case RecordType.UnsignedX4:
                case RecordType.UnsignedX8:
                case RecordType.UnsignedX12:
                case RecordType.UnsignedX16:
                case RecordType.UnsignedX20:
                case RecordType.UnsignedX24:
                    uint unsigned = 0;
                    for (int i = 0; i < length; i++)
                    {
                        unsigned <<= 8;
                        unsigned += dataBytes[i];
                    }
                    return new MessageRecordDataUInt(recordType, length, unsigned );
                case RecordType.SignedX0:
                case RecordType.SignedX8:
                case RecordType.SignedX12:
                case RecordType.SignedX16:
                case RecordType.SignedX24:
                    int signed = 0;
                    for (int i = 0; i < length; i++)
                    {
                        signed <<= 8;
                        signed += dataBytes[i];
                    }
                    return new MessageRecordDataInt(recordType, length, signed);
                default:
                    throw new ArgumentOutOfRangeException(nameof(recordType));
            }
        }

        private List<byte> Decrypt(IList<byte> payload, byte encyptionId, ushort pip)
        {
            List<byte> decrypted = new List<byte>();

            Seed(encyptionId, pip);

            foreach (var b in payload)
            {
                decrypted.Add(EncryptDecrypt(b));
            }
            return decrypted;
        }

        private byte EncryptDecrypt(byte dat)
        {
            for (int i = 0; i < 5; ++i)
            {
                random = (ushort)(((random & 1) > 0) ? ((random >> 1) ^ 62965U) : (random >> 1));
            }

            return (byte)(random ^ dat ^ 90U);
        }

        private void Seed(byte encryptionId, ushort pip)
        {
            random = (ushort)((encryptionId << 8) ^ pip);
        }
    }
}
