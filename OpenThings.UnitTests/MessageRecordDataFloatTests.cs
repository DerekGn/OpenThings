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
    public class MessageRecordDataFloatTests
    {
        [Theory]
        [InlineData(RecordType.UnsignedX0)]
        [InlineData(RecordType.SignedX0)]
        [InlineData(RecordType.Chars)]
        [InlineData(RecordType.Enum)]
        public void TestInvalidType(RecordType recordType)
        {
            // Arrange

            // Act
            Action action = () => new MessageRecordDataFloat(recordType, 1);

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(RecordType.UnsignedX4, -1.0f)]
        [InlineData(RecordType.UnsignedX8, -1.0f)]
        [InlineData(RecordType.UnsignedX12, -1.0f)]
        [InlineData(RecordType.UnsignedX16, -1.0f)]
        [InlineData(RecordType.UnsignedX20, -1.0f)]
        [InlineData(RecordType.UnsignedX24, -1.0f)]
        public void TestInvalidTypeUnsigned(RecordType recordType, float value)
        {
            // Arrange
            
            // Act
            Action action = () => new MessageRecordDataFloat(recordType, value);

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(RecordType.UnsignedX0)]
        [InlineData(RecordType.SignedX0)]
        [InlineData(RecordType.Chars)]
        [InlineData(RecordType.Enum)]
        public void TestInvalidTypeAlternateConstructor(RecordType recordType)
        {
            // Arrange

            // Act
            Action action = () => new MessageRecordDataFloat(recordType, new List<byte>() { });

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void TestNullBytes()
        {
            // Arrange

            // Act
            Action action = () => new MessageRecordDataFloat(RecordType.UnsignedX4, null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void TestEncodeUnsignedX4Zero()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX4, 0f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x10 });
        }

        [Fact]
        public void TestEncodeUnsignedX4()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX4, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x11, 0x12 });
        }

        [Fact]
        public void TestEncodeUnsignedX8()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX8, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x22, 0x01, 0x20 });
        }

        [Fact]
        public void TestEncodeUnsignedX12()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX12, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x32, 0x11, 0xFA });
        }

        [Fact]
        public void TestEncodeUnsignedX16()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX16, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x43, 0x01, 0x1F, 0x9B });
        }

        [Fact]
        public void TestEncodeUnsignedX20()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX20, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x53, 0x11, 0xF9, 0xAE });
        }

        [Fact]
        public void TestEncodeUnsignedX24()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX24, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x64, 0x01, 0x1F, 0x9A, 0xDE });
        }

        [Fact]
        public void TestEncodeSignedX8NegativeValue()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX8, -1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x92, 0xFE, 0xE0 });
        }

        [Fact]
        public void TestEncodeSignedX8PositiveValue()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX8, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x92, 0x01, 0x20 });
        }

        [Fact]
        public void TestEncodeSignedX16NegativeValue()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX16, -1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0xA3, 0xFE, 0xE0, 0x65 });
        }

        [Fact]
        public void TestEncodeSignedX16PositiveValue()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX16, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0xA3, 0x1, 0x1F, 0x9B });
        }

        [Fact]
        public void TestEncodeSignedX24NegativeValue()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX24, -1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0xB4, 0xFE, 0xE0, 0x65, 0x22 });
        }

        [Fact]
        public void TestEncodeSignedX24PositiveValue()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX24, 1.123456789f);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0xB4, 0x01, 0x1F, 0x9A, 0xDE });
        }

        [Fact]
        public void TestDecodeUnsignedX4()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX4, new List<byte>() { 0x12 });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(1.125f);
        }

        [Fact]
        public void TestDecodeUnsignedX8()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX8, new List<byte>() { 0x01, 0x55 });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(1.33203125f);
        }

        [Fact]
        public void TestDecodeUnsignedX12()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX12, new List<byte>() { 0x11, 0xFA });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(1.12353515625f);
        }

        [Fact]
        public void TestDecodeUnsignedX16()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX16, new List<byte>() { 0x01, 0x1F, 0x9B });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(1.1234588623046875f);
        }

        [Fact]
        public void TestDecodeUnsignedX20()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX20, new List<byte>() { 0x11, 0xF9, 0xAE });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(1.1234569549560547f);
        }

        [Fact]
        public void TestDecodeUnsignedX24()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.UnsignedX24, new List<byte>() { 0x01, 0x1F, 0x9A, 0xDE });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(1.1234567761421204f);
        }

        [Fact]
        public void TestDecodeSignedX8Positive()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX8, new List<byte>() { 0x01, 0x20 });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(1.125f);
        }

        [Fact]
        public void TestDecodeSignedX8Negative()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX8, new List<byte>() { 0xFE, 0xE0 });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(-1.125f);
        }

        [Fact]
        public void TestDecodeSignedX16Positive()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX16, new List<byte>() { 0x01, 0x1F, 0x9b });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(1.1234588623046875f);
        }

        [Fact]
        public void TestDecodeSignedX16Negative()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX16, new List<byte>() { 0xFE, 0xE0, 0x65 });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(-1.12345886f);
        }

        [Fact]
        public void TestDecodeSignedX24Positive()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX24, new List<byte>() { 0x01, 0x1F, 0x9A, 0xDE });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(1.123456789f);
        }

        [Fact]
        public void TestDecodeSignedX24Negative()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataFloat(RecordType.SignedX24, new List<byte>() { 0xFE, 0xE0, 0x65, 0x22 });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(-1.123456789f);
        }
    }
}
