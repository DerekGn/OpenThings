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

using System.Collections.Generic;
using System.Linq;

namespace OpenThings
{
    /// <summary>
    /// An <see cref="IOpenThingsEncoder"/> type
    /// </summary>
    public class OpenThingsEncoder : IOpenThingsEncoder
    {
        private static ushort random;

        /// <summary>
        /// Encode an OpenThings <see cref="Message"/>
        /// </summary>
        /// <param name="message">The <see cref="Message"/> message to encode</param>
        /// <returns>A <see cref="IList{T}"/> of the encoded OpentThings message bytes</returns>
        public IList<byte> Encode(Message message)
        {
            Crc16Ccitt crc16Ccitt = new Crc16Ccitt(0);

            List<byte> encoded = new List<byte>
            {
                0, // Length
                message.Header.ManufacturerId,
                message.Header.ProductId,
                0, // Pip Msb
                0, // Pip Lsb
                (byte) (message.Header.SensorId >> 16),
                (byte) ((message.Header.SensorId & 0xFF00) >> 8),
                (byte) (message.Header.SensorId & 0xFF)
            };

            foreach (var record in message.Records)
            {
                encoded.Add((byte)record.Parameter.Identifier);
                encoded.AddRange(record.Data.Encode());
            }

            encoded.Add(0);

            var crcBytes = crc16Ccitt.ComputeChecksumBytes(encoded.Skip(5).ToArray());

            encoded.AddRange(crcBytes.Reverse());

            encoded[0] = (byte)(encoded.Count - 1);

            return encoded;
        }

        /// <summary>
        /// Encode an OpenThings <see cref="Message"/>
        /// </summary>
        /// <param name="message">The <see cref="Message"/> message to encode</param>
        /// <param name="encryptionId">The encryption Id</param>
        /// <param name="seed">The random seed for encrypting the message</param>
        /// <returns>A <see cref="IList{T}"/> of the encoded OpentThings message bytes</returns>
        public IList<byte> Encode(Message message, byte encryptionId, ushort seed)
        {
            var encoded = Encode(message);

            Encrypt(encoded, encryptionId, seed);

            return encoded;
        }

        private void Encrypt(IList<byte> encoded, byte encryptionId, ushort seed)
        {
            RandomiseSeed(seed);

            ushort messagePip = GeneratePip(encryptionId);

            encoded[3] = (byte) (messagePip >> 8);
            encoded[4] = (byte) (messagePip & 0xFF);

            for (int i = 5; i < encoded.Count; ++i)
            {
                encoded[i] = EncryptDecrypt(encoded[i]);
            }
        }

        private static byte EncryptDecrypt(byte b)
        {
            int i;
            
            for (i = 0; i < 5; ++i) 
            { 
                random = (random & 1) > 0 ? (ushort)((random >> 1) ^ 62965U) : (ushort)(random >> 1); 
            }
            
            return (byte)(random ^ b ^ 90U);
        }

        private static void RandomiseSeed(ushort seed)
        {
            random = (ushort)(random ^ seed);
            
            if (random == 0)
            {
                random = 1;
            }
        }

        ushort GeneratePip(byte encryptionId)
        {
            return (ushort)(random ^ (encryptionId << 8));
        }
    }
}
