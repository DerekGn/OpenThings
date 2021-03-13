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

using OpenThings.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenThings
{
    public class MessageRecord
    {
        public MessageRecord(Parameter parameter, IList<byte> recordBytes)
        {
            Parameter = parameter;

            Length = recordBytes[0] & 0x0F;

            RecordType = (RecordType)((recordBytes[0] & 0xF0) >> 4);

            MapRecordType(Length, recordBytes.Skip(1).ToList());
        }
        public int Length { get; }
        public Parameter Parameter { get; }
        public RecordType RecordType { get; }
        public BaseRecordData Data { get; private set; }
        private void MapRecordType(int length, IList<byte> dataBytes)
        {
            int result = 0;
            int i = 0;

            switch (RecordType)
            {
                case RecordType.UnsignedX0:
                case RecordType.UnsignedX4:
                case RecordType.UnsignedX8:
                case RecordType.UnsignedX12:
                case RecordType.UnsignedX16:
                case RecordType.UnsignedX20:
                case RecordType.UnsignedX24:
                    for (i = 0; i < length; i++)
                    {
                        result <<= 8;
                        result += dataBytes[i];
                    }
                    Data = new RecordData<FixedPointData>(new FixedPointData(result, (float)(result / Math.Pow(2, DecimalPlaces(RecordType)))));
                    break;
                case RecordType.Chars:
                    Data = new RecordData<List<char>>(dataBytes.Select( _ => Convert.ToChar(_)).ToList());
                    break;
                case RecordType.SignedX0:
                case RecordType.SignedX8:
                case RecordType.SignedX12:
                case RecordType.SignedX16:
                case RecordType.SignedX24:
                    
                    for (i = 0; i < length; i++)
                    {
                        result <<= 8;
                        result += dataBytes[i];
                    }
                    if ((dataBytes[i] & 0x80) == 0x80)
                    {
                        result = -(((~result) & ((2 ^ (length * 8)) - 1)) + 1);
                    }
                    Data = new RecordData<FixedPointData>(new FixedPointData(result, (float)(result / Math.Pow(2, DecimalPlaces(RecordType)))));
                    break;
                case RecordType.Enum:
                    break;
                case RecordType.Float:
                    break;
                default:
                    break;
            }
        }
        public override string ToString()
        {
            return
                $"\tParameter->\r\n\t\t{Parameter}\r\n" +
                $"\t\tLength:\t\t[0x{Length:X2}]\r\n" +
                $"\t\tData:\t\t[{Data}]";
        }

        private int DecimalPlaces(RecordType recordType)
        {
            int result = 0;

            switch (recordType)
            {
                case RecordType.UnsignedX4:
                    result = 4;
                    break;
                case RecordType.SignedX8:
                case RecordType.UnsignedX8:
                    result = 8;
                    break;
                case RecordType.SignedX12:
                case RecordType.UnsignedX12:
                    result = 12;
                    break;
                case RecordType.SignedX16:
                case RecordType.UnsignedX16:
                    result = 16;
                    break;
                case RecordType.UnsignedX20:
                    result = 20;
                    break;
                case RecordType.SignedX24:
                case RecordType.UnsignedX24:
                    result = 24;
                    break;
            }

            return result;
        }
    }
}
