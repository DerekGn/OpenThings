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
    /// A signed integer <see cref="BaseMessageRecordData"/> type
    /// </summary>
    public class MessageRecordDataInt : BaseMessageRecordData
    {
        /// <summary>
        /// Create an instance of a <see cref="MessageRecordDataString"/>
        /// </summary>
        /// <param name="value">The value to encode</param>
        public MessageRecordDataInt(int value) : base(RecordType.SignedX0)
        {
            Value = value;
        }

        /// <summary>
        /// Create an instance of a <see cref="MessageRecordDataString"/>
        /// </summary>
        /// <param name="bytes">The bytes to decode</param>
        internal MessageRecordDataInt(List<byte> bytes) : base(RecordType.SignedX0)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            uint unpacked = UnPackUInt(bytes);
            if ((bytes[0] & 0x80) == 0x80)
            {
                int mask = GenerateMask(bytes.Count);

                Value = (int)-(((~unpacked) & mask) + 1);
            }
            else
            {
                Value = (int)unpacked;
            }
        }

        /// <summary>
        /// The value of the <see cref="MessageRecordDataString"/>
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Convert the <see cref="MessageRecordDataString"/> to a string representation
        /// </summary>
        /// <returns>A string representation of the <see cref="MessageRecordDataString"/></returns>
        public override string ToString()
        {
            return $"{base.ToString()} Value: [0x{Value:X8}]";
        }

        /// <summary>
        /// Determine if a <see cref="RecordType"/> is an integer type.
        /// </summary>
        /// <param name="recordType">The <see cref="RecordType"/> to check</param>
        /// <returns>True if the <paramref name="recordType"/> is a int type</returns>
        public static bool IsInt(RecordType recordType)
        {
            return recordType == RecordType.SignedX0;
        }

        internal override IList<byte> EncodeValue()
        {
            List<byte> result = null;

            if (Value < 0)
            {
                if (Value == -1)
                {
                    result = new List<byte>() { 0xFF };
                }
                else
                {
                    long encoded = Value;

                    uint bits = GetValueBits(Value);

                    bits = (uint)(((((float)bits - 1) / 8) + 1) * 8);

                    encoded &= (long)Math.Pow(2, bits) - 1;

                    result = BitConverter
                        .GetBytes(encoded)
                        .Reverse()
                        .SkipWhile((v) => v == 0x00)
                        .ToList();
                }
            }
            else
            {
                result = BitConverter
                    .GetBytes(Value)
                    .Reverse()
                    .SkipWhile((v) => v == 0x00)
                    .ToList();
            }

            return result;
        }

        private static uint GetValueBits(int value)
        {
            if (value == -1)
            {
                return 2;
            }
            else
            {
                return GetHighestClearBit((ulong)value) + 2;
            }
        }

        private static uint GetHighestClearBit(ulong value)
        {
            ulong mask = 0x8000000000000000;
            uint bitNum = 63;

            do
            {
                if ((value & mask) == 0)
                {
                    break;
                }

                mask >>= 1;
                bitNum--;
            } while (mask != 0);

            return bitNum;
        }
    }
}