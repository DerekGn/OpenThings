﻿/*
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
    /// A float <see cref="BaseMessageRecordData"/> type
    /// </summary>
    public class MessageRecordDataFloat : BaseMessageRecordData
    {
        /// <summary>
        /// Create an instance of a <see cref="MessageRecordDataFloat"/>
        /// </summary>
        /// <param name="recordType"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public MessageRecordDataFloat(RecordType recordType, float value) : base(recordType)
        {
            if (!IsFloat(recordType))
            {
                throw new ArgumentOutOfRangeException(nameof(recordType));
            }

            if (!IsSignedFloat(recordType) && value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(recordType),
                    $"{nameof(recordType)}: {recordType} {nameof(Value)}:{value} must be positive");
            }

            Value = value;
        }

        /// <summary>
        /// Create an instance of a <see cref="MessageRecordDataFloat"/>
        /// </summary>
        /// <param name="recordType">The <see cref="RecordType"/></param>
        /// <param name="bytes">The bytes to decode</param>
        internal MessageRecordDataFloat(RecordType recordType,
            List<byte> bytes) : base(recordType)
        {
            if (!IsFloat(recordType))
            {
                throw new ArgumentOutOfRangeException(nameof(recordType));
            }

            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            uint unpacked = UnPackUInt(bytes);

            if (IsSigned(recordType))
            {
                int s = (int)unpacked;

                if ((bytes[0] & 0x80) == 0x80)
                {
                    long mask = GenerateMask(bytes.Count);

                    s = (int)-(((~unpacked) & mask) + 1);
                }

                Value = (float)(s /
                         Math.Pow(2, GetRecordTypeBits(recordType)));
            }
            else
            {
                Value = (float)(unpacked /
                     Math.Pow(2, GetRecordTypeBits(recordType)));
            }
        }

        /// <summary>
        /// The value of the <see cref="MessageRecordDataFloat"/>
        /// </summary>
        public float Value { get; }

        /// <summary>
        /// Determines if a <see cref="RecordType"/> is a float
        /// </summary>
        /// <param name="recordType">The record type to check</param>
        /// <returns>True if the <paramref name="recordType"/> is a float type</returns>
        public static bool IsFloat(RecordType recordType)
        {
            bool result = false;

            switch (recordType)
            {
                case RecordType.UnsignedX4:
                case RecordType.UnsignedX8:
                case RecordType.UnsignedX12:
                case RecordType.UnsignedX16:
                case RecordType.UnsignedX20:
                case RecordType.UnsignedX24:
                    result = true;
                    break;
                case RecordType.SignedX8:
                case RecordType.SignedX16:
                case RecordType.SignedX24:
                    result = true;
                    break;
                case RecordType.Float:
                    result = true;
                    break;
                default:
                    break;
            }

            return result;
        }

        internal override IList<byte> EncodeValue()
        {
            List<byte> result;

            uint encoded = EncodeFloatToInt(RecordType, Value);

            if (IsSignedFloat(RecordType))
            {
                result = new List<byte>();

                if (Value < 0)  // pack signed
                {
                    result = BitConverter
                        .GetBytes(encoded)
                        .Reverse()
                        .SkipWhile((v) => v == 0xFF)
                        .ToList();
                }
                else  // pack unsigned
                {
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
                    .GetBytes(encoded)
                    .Reverse()
                    .SkipWhile((v) => v == 0)
                    .ToList();
            }

            return result;
        }

        private static bool IsSignedFloat(RecordType recordType)
        {
            return
                recordType == RecordType.SignedX8 ||
                recordType == RecordType.SignedX16 ||
                recordType == RecordType.SignedX24 ||
                recordType == RecordType.Float;
        }

        private static uint EncodeFloatToInt(RecordType recordType, float value)
        {
            double encode = value;
            encode *= Math.Pow(2, GetEncodingBits(recordType));
            encode = Math.Round(encode);
            uint encoded = (uint)encode;
            return encoded;
        }

        private static uint GetEncodingBits(RecordType recordType)
        {
            uint result = 0;

            switch (recordType)
            {
                case RecordType.UnsignedX4:
                    result = 4;
                    break;
                case RecordType.UnsignedX8:
                case RecordType.SignedX8:
                    result = 8;
                    break;
                case RecordType.UnsignedX12:
                    result = 12;
                    break;
                case RecordType.UnsignedX16:
                case RecordType.SignedX16:
                    result = 16;
                    break;
                case RecordType.UnsignedX20:
                    result = 20;
                    break;
                case RecordType.UnsignedX24:
                case RecordType.SignedX24:
                    result = 24;
                    break;
                default:
                    break;
            }

            return result;
        }

        private static bool IsSigned(RecordType recordType)
        {
            return
                recordType == RecordType.SignedX8 ||
                recordType == RecordType.SignedX16 ||
                recordType == RecordType.SignedX24;
        }

        private static uint GetRecordTypeBits(RecordType recordType)
        {
            uint result = 0;

            switch (recordType)
            {
                case RecordType.UnsignedX4:
                    result = 4;
                    break;
                case RecordType.UnsignedX8:
                case RecordType.SignedX8:
                    result = 8;
                    break;
                case RecordType.UnsignedX12:
                    result = 12;
                    break;
                case RecordType.UnsignedX16:
                case RecordType.SignedX16:
                    result = 16;
                    break;
                case RecordType.UnsignedX20:
                    result = 20;
                    break;
                case RecordType.UnsignedX24:
                case RecordType.SignedX24:
                    result = 24;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
