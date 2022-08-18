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

namespace OpenThings
{
    /// <summary>
    /// Computes a 16 bit CCITT CRC
    /// </summary>
    internal class Crc16Ccitt
    {
        private const ushort Polynomial = 4129;
        private readonly ushort _initialValue = 0;
        private readonly ushort[] _table = new ushort[256];

        /// <summary>
        /// Create instance of a <see cref="Crc16Ccitt"/>
        /// </summary>
        /// <param name="initialValue">The initial seed value for the CRC calculation</param>
        public Crc16Ccitt(ushort initialValue)
        {
            _initialValue = initialValue;
            ushort temp, a;

            for (int i = 0; i < _table.Length; ++i)
            {
                temp = 0;
                a = (ushort)(i << 8);
                for (int j = 0; j < 8; ++j)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                    {
                        temp = (ushort)((temp << 1) ^ Polynomial);
                    }
                    else
                    {
                        temp <<= 1;
                    }
                    a <<= 1;
                }
                _table[i] = temp;
            }
        }

        /// <summary>
        /// Compute the CRC checksum
        /// </summary>
        /// <param name="bytes">The <see cref="List{T}"/> of bytes to compute the checksum</param>
        /// <returns>The computed CCITT 16 checksum</returns>
        public ushort ComputeChecksum(IList<byte> bytes)
        {
            ushort crc = _initialValue;

            foreach (var b in bytes)
            {
                crc = (ushort)((crc << 8) ^ _table[(crc >> 8) ^ (0xff & b)]);
            }
            return crc;
        }

        /// <summary>
        /// Compute the CRC checksum
        /// </summary>
        /// <param name="bytes">The <see cref="List{T}"/> of bytes to compute the checksum</param>
        /// <returns>The computed CCITT 16 checksum</returns>
        public byte[] ComputeChecksumBytes(IList<byte> bytes)
        {
            ushort crc = ComputeChecksum(bytes);
            return BitConverter.GetBytes(crc);
        }
    }
}