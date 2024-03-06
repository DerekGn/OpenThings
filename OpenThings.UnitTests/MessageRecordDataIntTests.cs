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

using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace OpenThings.UnitTests
{
    public class MessageRecordDataIntTests
    {
        [Fact]
        public void TestDecodeByteIntLower()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(new List<byte>() { 0x80, 0x00, 0x00, 0x00 });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(-2147483648);
        }

        [Fact]
        public void TestDecodeByteIntUpper()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(new List<byte>() { 0x7F, 0xFF, 0xFF, 0xFF });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(2147483647);
        }

        [Fact]
        public void TestDecodeByteLower()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(new List<byte>() { 0x80 });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(-128);
        }

        [Fact]
        public void TestDecodeByteShortLower()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(new List<byte>() { 0x80, 0x00 });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(-32768);
        }

        [Fact]
        public void TestDecodeByteShortUpper()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(new List<byte>() { 0x7F, 0xFF });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(32767);
        }

        [Fact]
        public void TestDecodeByteUpper()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(new List<byte>() { 0x7F });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(127);
        }

        [Fact]
        public void TestDecodeEmpty()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(new List<byte>() { });

            // Act
            var result = messageRecordData.Value;

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void TestEncode2147483647()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(2147483647);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x84, 0x7F, 0xFF, 0xFF, 0xFF });
        }

        [Fact]
        public void TestEncode255()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(255);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x81, 0xFF });
        }

        [Fact]
        public void TestEncode256()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(256);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x82, 0x01, 0x00 });
        }

        [Fact]
        public void TestEncode32767()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(32767);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x82, 0x7F, 0xFF });
        }

        [Fact]
        public void TestEncode32768()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(32768);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x82, 0x80, 0x00 });
        }

        [Fact]
        public void TestEncodeMinus127()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(-127);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x81, 0x81 });
        }

        [Fact]
        public void TestEncodeMinus128()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(-128);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x81, 0x80 });
        }

        [Fact]
        public void TestEncodeMinus129()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(-129);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x82, 0xFF, 0x7F });
        }

        [Fact]
        public void TestEncodeMinus2147483648()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(-2147483648);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x84, 0x80, 0x00, 0x00, 0x00 });
        }

        [Fact]
        public void TestEncodeMinus32767()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(-32767);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x82, 0x80, 0x01 });
        }

        [Fact]
        public void TestEncodeMinus32768()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(-32768);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x82, 0x80, 0x00 });
        }

        [Fact]
        public void TestEncodeMinusOne()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(-1);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x81, 0xFF });
        }

        [Fact]
        public void TestEncodeMinusTwo()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(-2);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x81, 0xFE });
        }

        [Fact]
        public void TestEncodeOne()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(1);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x81, 0x01 });
        }

        [Fact]
        public void TestEncodeZero()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(0);

            // Act
            var result = messageRecordData.Encode();

            // Assert
            result.Should().BeEquivalentTo(new List<byte>() { 0x80 });
        }

        [Fact]
        public void TestNullBytes()
        {
            // Arrange

            // Act
            Action action = () => new MessageRecordDataInt(null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void TestToString()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataInt(0xAA55);

            // Act
            var result = messageRecordData.ToString();

            // Assert
            result.Should().Be("Record Type: [SignedX0] Value: [0x0000AA55]");
        }

        [Fact]
        public void TestValue()
        {
            // Arrange

            // Act
            var messageRecordData = new MessageRecordDataInt(0xAA55);

            // Assert
            messageRecordData.Value.Should().Be(0xAA55);
        }
    }
}