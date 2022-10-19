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
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Xunit;

namespace OpenThings.UnitTests
{
    public class MessageRecordDataFloatTests
    {
        [Theory]
        [InlineData(RecordType.UnsignedX4, -1.0f)]
        [InlineData(RecordType.UnsignedX8, -1.0f)]
        [InlineData(RecordType.UnsignedX12, -1.0f)]
        [InlineData(RecordType.UnsignedX16, -1.0f)]
        [InlineData(RecordType.UnsignedX20, -1.0f)]
        [InlineData(RecordType.UnsignedX24, -1.0f)]
        public void TestUnsignedSignedValue(RecordType recordType, float value)
        {
            // Arrange
            
            // Act
            Action action = () => new MessageRecordDataFloat(recordType, value);

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void TestUnsignedX4Zero()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX4, 0f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x10 });
        }

        [Fact]
        public void TestUnsignedX4()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX4, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x11, 0x12 });
        }

        [Fact]
        public void TestUnsignedX8()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX8, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x22, 0x01, 0x20 });
        }

        [Fact]
        public void TestUnsignedX12()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX12, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x32, 0x11, 0xFA });
        }

        [Fact]
        public void TestUnsignedX16()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX16, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x43, 0x01, 0x1F, 0x9B });
        }

        [Fact]
        public void TestUnsignedX20()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX20, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x53, 0x11, 0xF9, 0xAE });
        }

        [Fact]
        public void TestUnsignedX24()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX24, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x64, 0x01, 0x1F, 0x9A, 0xDE });
        }

        [Fact]
        public void TestSignedX8NegativeValue()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX8, -1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x92, 0xFE, 0xE0 });
        }

        [Fact]
        public void TestSignedX8PositiveValue()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX8, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x92, 0x01, 0x20 });
        }

        [Fact]
        public void TestSignedX16NegativeValue()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX16, -1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0xA3, 0xFE, 0xE0, 0x65 });
        }

        [Fact]
        public void TestSignedX16PositiveValue()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX16, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0xA3, 0x1, 0x1F, 0x9B });
        }

        [Fact]
        public void TestSignedX24NegativeValue()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX24, -1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0xB4, 0xFE, 0xE0, 0x65, 0x22 });
        }

        [Fact]
        public void TestSignedX24PositiveValue()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX24, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0xB4, 0x01, 0x1F, 0x9A, 0xDE });
        }
    }
}
