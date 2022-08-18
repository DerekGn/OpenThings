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
        /// Create an instance of a <see cref="MessageRecordDataInt"/>
        /// </summary>
        /// <param name="recordType">The <see cref="RecordType"/></param>
        /// <param name="length">The length in bytes of the message record when encoded</param>
        /// <param name="value">The value to encode</param>
        public MessageRecordDataUInt(RecordType recordType, int length, uint value) : base(recordType, length)
        {
            if (length > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            AssertRecordType(recordType);

            Value = value;
        }

        /// <summary>
        /// The value of the <see cref="MessageRecordDataInt"/>
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

        internal override IList<byte> GetValueByes()
        {
            return BitConverter.GetBytes(Value).Take(Length).Reverse().ToList();
        }

        private void AssertRecordType(RecordType recordType)
        {
            bool valid = false;
            switch (recordType)
            {
                case RecordType.UnsignedX0:
                case RecordType.UnsignedX4:
                case RecordType.UnsignedX8:
                case RecordType.UnsignedX12:
                case RecordType.UnsignedX16:
                case RecordType.UnsignedX20:
                case RecordType.UnsignedX24:
                    valid = true;
                    break;
            }

            if (!valid)
                throw new ArgumentOutOfRangeException(nameof(recordType));
        }
    }
}
