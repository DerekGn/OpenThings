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

using System.Collections.Generic;
using System.Linq;

namespace OpenThings
{
    /// <summary>
    /// An <see cref="IOpenThingsEncoder"/> type
    /// </summary>
    public class OpenThingsEncoder : IOpenThingsEncoder
    {
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
                0,
                message.Header.ManufacturerId,
                message.Header.ProductId,
                message.Header.ProductId,
                0,
                0
            };

            foreach (var record in message.Records)
            {
                encoded.Add((byte)record.Parameter.Identifier);
                encoded.AddRange(record.Data.Encode());
            }

            var crcBytes = crc16Ccitt.ComputeChecksumBytes(encoded.Skip(5).ToArray());

            encoded.Add(0);
            encoded.AddRange(crcBytes);

            encoded[0] = (byte) encoded.Count;

            return encoded;
        }

        /// <summary>
        /// Encode an OpenThings <see cref="Message"/>
        /// </summary>
        /// <param name="message">The <see cref="Message"/> message to encode</param>
        /// <param name="encryptionId">The encryption Id</param>
        /// <param name="seed">The random seed for encrypting the message</param>
        /// <returns>A <see cref="IList{T}"/> of the encoded OpentThings message bytes</returns>
        public IList<byte> Encode(Message message, byte encryptionId, uint seed)
        {
            var encoded = Encode(message);

            return encoded;
        }
    }
}
