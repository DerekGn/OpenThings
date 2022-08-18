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
using System;
using System.Collections.Generic;
using Xunit;

namespace OpenThings.UnitTests
{
    public class MessageRecordDataIntTests
    {
        [Fact]
        public void TestEncode()
        {
            // Arrange
            var messageRecordDataInt = new MessageRecordDataInt(RecordType.SignedX0, 1, 0xAA55);

            // Act
            var result = messageRecordDataInt.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x81, 0x55 });
        }

        [Fact]
        public void TestGetValueBytes()
        {
            // Arrange
            var messageRecordDataInt = new MessageRecordDataInt(RecordType.SignedX0, 1, 0xAA55);

            // Act
            var result = messageRecordDataInt.GetValueByes();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x55 });
        }

        [Fact]
        public void TestInvalidLength()
        {
            // Arrange

            // Act
            Action action = () => new MessageRecordDataInt(RecordType.SignedX0, 5, 0xAA55);

            // Assert
            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("Specified argument was out of the range of valid values. (Parameter 'length')");
        }

        [Fact]
        public void TestInvalidType()
        {
            // Arrange

            // Act
            Action action = () => new MessageRecordDataInt(RecordType.Chars, 1, 0xAA55);

            // Assert
            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("Specified argument was out of the range of valid values. (Parameter 'recordType')");
        }

        [Fact]
        public void TestToString()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataUInt(RecordType.UnsignedX0, 1, 0xAA55);

            // Act
            var result = messageRecordData.ToString();

            // Assert
            result.Should().Be("Record Type: [UnsignedX0] Length: [1] Value: [0x0000AA55]");
        }

        [Fact]
        public void TestValid()
        {
            // Arrange

            // Act
            var messageRecordDataInt = new MessageRecordDataInt(RecordType.SignedX0, 1, 0xAA55);

            // Assert
            messageRecordDataInt.Value.Should().Be(0xAA55);
        }
    }
}