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

// Ignore Spelling: Ccitt

using System.Collections.Generic;
using Xunit;

namespace OpenThings.UnitTests
{
    public class Crc16CcittTests
    {
        [Fact]
        public void TestComputeChecksum()
        {
            // Arrange
            var crc16Ccitt = new Crc16Ccitt(89);

            // Act
            var result = crc16Ccitt.ComputeChecksum(new List<byte>() { 0x55, 0x66, 0xAA });

            // Assert
            Assert.Equal(46133, result);
        }

        [Fact]
        public void TestComputeChecksumBytes()
        {
            // Arrange
            var crc16Ccitt = new Crc16Ccitt(89);

            // Act
            var result = crc16Ccitt.ComputeChecksumBytes(new List<byte>() { 0x55, 0x66, 0xAA });

            // Assert
            Assert.Equal(new List<byte>() { 0x35, 0xB4 }, result);
        }
    }
}