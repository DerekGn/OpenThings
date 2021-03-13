/*
* MIT License
*
* Copyright (c) 2021 Derek Goslin
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenThings
{
    public class Message
    {
        private static ushort random;

        public Message(IList<byte> buffer, IList<PidMap> pidMaps)
        {
            if(buffer == null || buffer.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(buffer));
            }

            if (pidMaps == null || pidMaps.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pidMaps));
            }

            var payload = ValidateAndExtractPayload(buffer);

            Header = new MessageHeader(payload);

            var body = payload.Skip(5).ToList();

            if (Header.EncryptPip != 0)
            {
                var pidMap = pidMaps.FirstOrDefault(_ => _.ManufacturerId == Header.ManufacturerId);

                if (pidMap == null)
                {
                    throw new OpenThingsException($"No [{nameof(PidMap)}] found for manufacture id [0x{Header.ManufacturerId:X}]");
                }

                body = Decrypt(body, pidMap.Pid, Header.EncryptPip);

                Header.SetSensorId(body.Take(3).ToList());
            }
            else
            {
                Header.SetSensorId(payload.Skip(5).Take(3).ToList());
            }

            Crc16Ccitt crc16Ccitt = new Crc16Ccitt(0);

            ushort crcActual = (ushort)((body.Skip(body.Count - 2).First() << 8) + body.Skip(body.Count - 1).First());
            ushort crcExpected = crc16Ccitt.ComputeChecksum(body.Take(body.Count - 2).ToArray());

            if (crcActual != crcExpected)
                throw new OpenThingsException($"Invalid Crc Expected: [0x{crcExpected:X4}] Actual: [0x{crcActual:X4}]");

            Footer = new MessagerFooter(crcActual);

            Records = new List<MessageRecord>();

            ParseRecords(body.Skip(3).Take(body.Count - 6).ToList());
        }

        public MessageHeader Header { get; private set; }

        public IList<MessageRecord> Records { get; private set; }

        public MessagerFooter Footer { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Header->");
            sb.AppendLine(Header.ToString());
            sb.AppendLine("Records->");

            foreach (var r in Records)
            {
                sb.AppendLine(r.ToString());
            }

            sb.AppendLine("Footer->");
            sb.AppendLine(Footer.ToString());

            return sb.ToString();
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

        private void ParseRecords(IList<byte> recordBytes)
        {
            int i = 0;

            while (i < recordBytes.Count)
            {
                var parameter = Parameter.Parameters.FirstOrDefault(p => p.Identifier == (ParameterIdentifiers)(recordBytes[i] & 0x7F));

                if (parameter != null)
                {
                    var record = new MessageRecord(parameter, recordBytes.Skip(i + 1).ToList());

                    Records.Add(record);

                    i += record.Length + 2;
                }
                else
                {
                    throw new OpenThingsException($"Unable to find parameter id [0x{recordBytes[0]:X}]");
                }
            };


            //while (i < recordBytes.Count)
            //{
            //    var parameter = Parameter.Parameters.FirstOrDefault(p => p.Identifier == (ParameterIdentifiers)(recordBytes[i] & 0x7F));

            //    if (parameter != null)
            //    {
            //        var record = new MessageRecord(parameter, recordBytes.Skip(1).ToList());

            //        Records.Add(record);

            //        i += record.Length + 2;

            //        if (recordBytes[i] == 0)
            //        {
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        throw new OpenThingsException($"Unable to find parameter id [0x{recordBytes[0]:X}]");
            //    }
            //}
        }

        private IList<byte> ValidateAndExtractPayload(IList<byte> buffer)
        {
            List<byte> payload = new List<byte>();

            if (buffer.Count < 10)
                throw new OpenThingsException($"Invalid buffer length [{buffer.Count}] too short");

            if ((buffer[0] & 0x7F) == 0)
            {
                throw new OpenThingsException($"Invalid Header length [{buffer[0]}] too short");
            }

            var expected = (buffer[0] & 0x7F) + 1;

            if (expected > buffer.Count)
            {
                throw new OpenThingsException($"Not enough bytes in buffer expected [{expected}] actual [{buffer.Count}]");
            }

            if(expected < 10)
            {
                throw new OpenThingsException($"Message too short [{expected}]");
            }

            payload.AddRange(buffer.Take(buffer[0] + 1));

            return payload;
        }
    }
}
