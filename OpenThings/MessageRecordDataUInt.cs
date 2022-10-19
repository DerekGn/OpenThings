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

using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenThings
{
    /// <summary>
    /// An unsigned integer <see cref="BaseMessageRecordData"/> type
    /// </summary>
    public class MessageRecordDataUInt : BaseMessageRecordData
    {
        /// <summary>
        /// Create an instance of a <see cref="MessageRecordDataString"/>
        /// </summary>
        /// <param name="value">The value to encode</param>
        public MessageRecordDataUInt(uint value) : base(RecordType.UnsignedX0)
        {
            Value = value;
        }

        /// <summary>
        /// Create an instance of a <see cref="MessageRecordDataUInt"/>
        /// </summary>
        /// <param name="bytes">The bytes to decode</param>
        public MessageRecordDataUInt(List<byte> bytes) : base(RecordType.UnsignedX0)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            Value = UnPackUInt(bytes);
        }

        /// <summary>
        /// The value of the <see cref="MessageRecordDataString"/>
        /// </summary>
        public uint Value { get; }

        /// <summary>
        /// Convert the <see cref="MessageRecordDataUInt"/> to a string representation
        /// </summary>
        /// <returns>A string representation of the <see cref="MessageRecordDataUInt"/></returns>
        public override string ToString()
        {
            return $"{base.ToString()} Value: [0x{Value:X8}]";
        }

        /// <summary>
        /// Determine if a <see cref="RecordType"/> is an unsigned integer type.
        /// </summary>
        /// <param name="recordType">The <see cref="RecordType"/> to check</param>
        /// <returns>True if the <paramref name="recordType"/> is a uint type</returns>
        public static bool IsUInt(RecordType recordType)
        {
            return recordType == RecordType.UnsignedX0;
        }

        internal override IList<byte> EncodeValue()
        {
            return BitConverter
                .GetBytes(Value)
                .Reverse()
                .SkipWhile((v) => v == 0)
                .ToList();
        }
    }
}
