/*
* MIT License
*
* Copyright (c) 2022 Derek Goslin
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

using OpenThings.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenThings
{
    /// <summary>
    /// An <see cref="IOpenThingsDecoder"/> type
    /// </summary>
    public class OpenThingsDecoder : IOpenThingsDecoder
    {
        private readonly IParameters _parameters;
        private static ushort random;

        /// <summary>
        /// Create an instance of a <see cref="OpenThingsDecoder"/>
        /// </summary>
        public OpenThingsDecoder(IParameters parameters)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        /// <summary>
        /// Decode a <see cref="IList{T}"/> of bytes representing the OpenThings message payload
        /// </summary>
        /// <param name="payload">The OpenThings payload message bytes</param>
        /// <param name="pidMaps">A mapping of PID to manufacture Ids to enable linear shift encryption decoding</param>
        /// <returns>A decode <see cref="Message"/></returns>
        public Message Decode(IList<byte> payload, IList<PidMap> pidMaps)
        {
            Crc16Ccitt crc16Ccitt = new Crc16Ccitt(0);

            if (payload == null || payload.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(payload));
            }

            if (payload.Count < 11)
            {
                throw new OpenThingsException($"Invalid buffer length [{payload.Count}] too short");
            }

            if (payload[0] < 11)
            {
                throw new OpenThingsException($"Invalid OpenThings Header length [{payload[0]}]");
            }

            if (payload[0] > payload.Count)
            {
                throw new OpenThingsException($"Invalid OpenThings Header length [{payload[0]}] Buffer Length: [{payload.Count}]");
            }

            var pip = BitConverter.ToUInt16(payload.Skip(3).Take(2).Reverse().ToArray(), 0);

            var header = new MessageHeader(payload[1], payload[2], pip, 0x0);

            var body = payload.Take(payload[0] + 1).Skip(5).ToList();

            if (pip != 0)
            {
                var pidMap = pidMaps.FirstOrDefault(_ => _.ManufacturerId == header.ManufacturerId) ?? 
                    throw new OpenThingsException($"No [{nameof(PidMap)}] found for manufacture id [0x{header.ManufacturerId:X}]");
                
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
            {
                throw new OpenThingsException($"Invalid Crc Expected: [0x{crcExpected:X4}] Actual: [0x{crcActual:X4}]");
            }

            var message = new Message(header);

            ParseMessageRecords(message, body.Skip(3).Take(body.Count - 6).ToList());

            return message;
        }

        private void ParseMessageRecords(Message message, List<byte> recordBytes)
        {
            int i = 0;

            while (i < recordBytes.Count)
            {
                var parameter = _parameters.GetParameter(recordBytes[i]);

                var recordType = (RecordType)(recordBytes[i + 1] >> 4);

                var length = (recordBytes[i + 1] & 0x0F);

                message.Records.Add(new MessageRecord(parameter, MapMessageRecordData(recordType, recordBytes.Skip(i + 2).Take(length).ToList())));

                i += length + 2;
            }
        }

        private static BaseMessageRecordData MapMessageRecordData(RecordType recordType, List<byte> bytes)
        {
            BaseMessageRecordData result = null;

            if (MessageRecordDataInt.IsInt(recordType))
            {
                result = new MessageRecordDataInt(bytes);
            }
            else if (MessageRecordDataUInt.IsUInt(recordType))
            {
                result = new MessageRecordDataUInt(bytes);
            }
            else if (MessageRecordDataFloat.IsFloat(recordType))
            {
                result = new MessageRecordDataFloat(recordType, bytes);
            }
            else if (MessageRecordDataString.IsString(recordType))
            {
                result = new MessageRecordDataString(bytes);
            }

            return result;
        }

        private static List<byte> Decrypt(IList<byte> payload, byte encyptionId, ushort pip)
        {
            List<byte> decrypted = new List<byte>();

            Seed(encyptionId, pip);

            foreach (var b in payload)
            {
                decrypted.Add(EncryptDecrypt(b));
            }
            return decrypted;
        }

        private static byte EncryptDecrypt(byte dat)
        {
            for (int i = 0; i < 5; ++i)
            {
                random = (ushort)(((random & 1) > 0) ? ((random >> 1) ^ 62965U) : (random >> 1));
            }

            return (byte)(random ^ dat ^ 90U);
        }

        private static void Seed(byte encryptionId, ushort pip)
        {
            random = (ushort)((encryptionId << 8) ^ pip);
        }
    }
}
