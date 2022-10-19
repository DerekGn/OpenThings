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
using System.Text;

namespace OpenThings
{
    /// <summary>
    /// A string <see cref="BaseMessageRecordData"/> type
    /// </summary>
    public class MessageRecordDataString : BaseMessageRecordData
    {
        /// <summary>
        /// Create an instance of a <see cref="MessageRecordDataString"/>
        /// </summary>
        /// <param name="value">The value to encode</param>
        /// <exception cref="ArgumentOutOfRangeException">If the <paramref name="value"/> is out of range</exception>
        public MessageRecordDataString(string value) : base(RecordType.Chars)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            if (value.Length > MaxRecordLength - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        public MessageRecordDataString(List<byte> bytes) : base(RecordType.Chars)
        {
        }

        /// <summary>
        /// The value of the <see cref="MessageRecordDataString"/>
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Determine if a <see cref="RecordType"/> is an string type.
        /// </summary>
        /// <param name="recordType">The <see cref="RecordType"/> to check</param>
        /// <returns>True if the <paramref name="recordType"/> is a string type</returns>
        public static bool IsString(RecordType recordType)
        {
            return recordType == RecordType.Chars;
        }

        internal override IList<byte> EncodeValue()
        {
            return Encoding.ASCII.GetBytes(Value);
        }
    }
}
