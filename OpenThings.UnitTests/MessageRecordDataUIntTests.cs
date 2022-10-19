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

using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace OpenThings.UnitTests
{
    public class MessageRecordDataUIntTests
    {
        [Fact]
        public void TestEncodeZero()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataUInt(0);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x00 });
        }

        [Fact]
        public void TestEncodeByteUpper()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataUInt(255);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x01, 0xFF });
        }

        [Fact]
        public void TestEncodeShortLower()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataUInt(256);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x02, 0x01, 0x00 });
        }

        [Fact]
        public void TestEncodeShortUpper()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataUInt(65535);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x02, 0xFF, 0xFF });
        }

        [Fact]
        public void TestEncodeUIntLower()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataUInt(65536);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x03, 0x01, 0x00, 0x00 });
        }

        [Fact]
        public void TestEncodeUIntUpper()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataUInt(65535);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x02, 0xFF, 0xFF });
        }

        [Fact]
        public void TestToString()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataUInt(0xAA55);

            // Act
            var result = messageRecordData.ToString();

            // Assert
            result.Should().Be("Record Type: [UnsignedX0] Value: [0x0000AA55]");
        }

        [Fact]
        public void TestValue()
        {
            // Arrange

            // Act
            var messageRecordData = new MessageRecordDataUInt(0xAA55);

            // Assert
            messageRecordData.Value.Should().Be(0xAA55);
        }
    }
}