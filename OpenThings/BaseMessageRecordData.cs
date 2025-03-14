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

namespace OpenThings
{
    /// <summary>
    /// A base message record data type
    /// </summary>
    public abstract class BaseMessageRecordData
    {
        internal const int MaxRecordLength = 16;

        /// <summary>
        /// Create an instance of a <see cref="BaseMessageRecordData"/>
        /// </summary>
        /// <param name="recordType">The <see cref="RecordType"/></param>
        protected BaseMessageRecordData(RecordType recordType)
        {
            RecordType = recordType;
        }

        /// <summary>
        /// The record type
        /// </summary>
        public RecordType RecordType { get; }

        /// <summary>
        /// Encode the <see cref="BaseMessageRecordData"/> derived data bytes
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="byte"/></returns>
        public IEnumerable<byte> Encode()
        {
            var encoded = EncodeValue();

            List<byte> bytes = new List<byte>
            {
                (byte)(((byte)RecordType << 4) | (byte)encoded.Count)
            };

            bytes.AddRange(encoded);

            return bytes;
        }

        /// <summary>
        /// Cast to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/>"/></typeparam>
        /// <returns>The <see cref="BaseMessageRecordData"/> derived type cast to <typeparamref name="T"/></returns>
        public T As<T>() where T : BaseMessageRecordData
        {
            return (T)this;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Record Type: [{RecordType}]";
        }

        internal static long GenerateMask(int byteCount)
        {
            long result = 0;

            for (int i = 0; i < byteCount; i++)
            {
                if (result > 0)
                {
                    result <<= 8;
                }
                result += 0xFF;
            }

            return result;
        }

        internal static uint UnPackUInt(List<byte> bytes)
        {
            uint result = 0;

            for (int i = 0; i < bytes.Count; i++)
            {
                result <<= 8;
                result += bytes[i];
            }

            return result;
        }

        /// <summary>
        /// Encode the instance message record data value
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="byte"/> values that encode the message record value</returns>
        internal abstract IList<byte> EncodeValue();
    }
}