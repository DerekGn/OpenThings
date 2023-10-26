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
using OpenThings.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OpenThings.UnitTests
{
    public class DecodeTests
    {
        private readonly OpenThingsDecoder _decoder;

        public DecodeTests()
        {
            _decoder = new OpenThingsDecoder(new DefaultParameters());
        }

        [Fact]
        public void TestMalformedMessage()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 
                0x03, 0xA0, 0x43, 0x61, 
                0x00, 0x00, 0x00, 0x00, 
                0x00, 0x00, 0x00, 0x00, 
                0x00, 0x00, 0x00, 0x00, 
                0x00, 0x00 };

            // Act
            Action action = () => _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            OpenThingsException exception = Assert.Throws<OpenThingsException>(action);
            Assert.Equal("Invalid OpenThings Header length [3]", exception.Message);
        }

        [Fact]
        public void TestMessageInvalidBufferLenght()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 0x00 };

            // Act
            Action action = () => _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            OpenThingsException exception = Assert.Throws<OpenThingsException>(action);
            Assert.Equal("Invalid buffer length [1] too short", exception.Message);
        }

        [Fact]
        public void TestMessageInvalidHeaderLenght()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 
                0x00, 0x00, 0x00, 0x00, 
                0x00, 0x00, 0x00, 0x00, 
                0x00, 0x00, 0x00, 0x00 };

            // Act
            Action action = () => _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            OpenThingsException exception = Assert.Throws<OpenThingsException>(action);
            Assert.Equal("Invalid OpenThings Header length [0]", exception.Message);
        }

        [Fact]
        public void TestMessageValid()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 
                0x12, 0x55, 0x01, 0x00, 
                0x00, 0xAD, 0x00, 0x28, 
                0x74, 0x02, 0x7E, 0x58, 
                0x74, 0x02, 0x94, 0x63, 
                0x00, 0x52, 0x8B, 0xCA };

            // Act
            var message = _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            message.Header.Should().NotBeNull();
            message.Header.ManufacturerId.Should().Be(0x55);
            message.Header.ProductId.Should().Be(1);
            message.Header.SensorId.Should().Be(0xAD0028);
            message.Records.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2);
            message.Records.Take(1).First().Parameter.Should().NotBeNull();
            message.Records.Take(1).First().Parameter.Identifier.Should().Be(ParameterIdentifiers.Temperature);
            message.Records.Skip(1).Take(1).First().Parameter.Should().NotBeNull();
            message.Records.Skip(1).Take(1).First().Parameter.Identifier.Should().Be(ParameterIdentifiers.Temperature);
        }

        [Fact]
        public void TestMessageValidDecode()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 
                0x13, 0x55, 0xAA, 0x55, 
                0x55, 0x18, 0xE2, 0x3B, 
                0x97, 0x06, 0x47, 0x40, 
                0xFB, 0xE8, 0x84, 0x0E, 
                0xCD, 0x29, 0xAE };

            // Act
            var message = _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x55, 0xFF) });

            // Assert
            message.Header.Should().NotBeNull();
            message.Header.ManufacturerId.Should().Be(0x55);
        }

        [Fact]
        public void TestMessageValidJoin()
        {
            //Arrange
            List<byte> payload = new List<byte>() { 
                0x0c, 0x04, 0x0c, 0x33, 
                0xf2, 0x99, 0xcd, 0x84, 
                0x9b, 0x3c, 0x4a, 0xd4, 
                0xa7, 0x00 };

            // Act
            var message = _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            message.Header.Should().NotBeNull();
            message.Header.ManufacturerId.Should().Be(4);
            message.Header.ProductId.Should().Be(0x0C);
            message.Header.SensorId.Should().Be(0x91BD);
            message.Records.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1);
            message.Records.Take(1).First().Parameter.Should().NotBeNull();
            message.Records.Take(1).First().Parameter.Identifier.Should().Be(DefaultCommands.JoinCommand);
        }

        [Fact]
        public void TestPayloadInvalid()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 
                0xFF, 0x00, 0x00, 0x00, 
                0x00, 0x00, 0x00, 0x00, 
                0x00, 0x00, 0x00, 0x00, 
                0x00, 0x00, 0x00 };

            // Act
            Action action = () => _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            action
                .Should()
                .Throw<OpenThingsException>()
                .WithMessage("Invalid OpenThings Header length [255] Buffer Length: [15]");
        }

        [Fact]
        public void TestPayloadNull()
        {
            // Arrange

            // Act
            Action action = () => _decoder.Decode(null, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            action
                .Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage("Specified argument was out of the range of valid values. (Parameter 'payload')");
        }

        [Fact]
        public void TestTempHumdityMessageValid()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 
                0x12, 0x55, 0x01, 0x00, 
                0x00, 0xAD, 0x00, 0x28, 
                0x74, 0x02, 0x2E, 0x59, 
                0x74, 0x02, 0x88, 0x63, 
                0x00, 0xCB, 0xBE, 0x00 };

            // Act
            var message = _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            message.Header.Should().NotBeNull();
            message.Header.ManufacturerId.Should().Be(0x55);
        }

        [Fact]
        public void TestNoPid()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 
                0x13, 0x55, 0xAA, 0x55, 
                0x55, 0x18, 0xE2, 0x3B, 
                0x97, 0x06, 0x47, 0x40, 
                0xFB, 0xE8, 0x84, 0x0E, 
                0xCD, 0x29, 0xAE };

            // Act
            Action action = () => _decoder.Decode(payload, new List<PidMap>() { });

            // Assert
            action
                .Should()
                .Throw<OpenThingsException>()
                .WithMessage("No [PidMap] found for manufacture id [0x55]");    
        }

        [Fact]
        public void TestDecode()
        {
            // Arrange
            List<byte> payload = new List<byte>() {
            0x1A, 0x55, 0xAA, 0x00,
                    0x00, 0xFE, 0xED, 0xED,
                    0x74, 0x12, 0x01, 0x48,
                    0x2D, 0x74, 0x54, 0x45,
                    0x53, 0x54, 0x66, 0x01,
                    0x78, 0x4C, 0x81, 0xCE,
                    0x00, 0xF9, 0xB7 };

            // Act
            var message = _decoder.Decode(payload, new List<PidMap>() { });

            message.Header.Should().NotBeNull();
            message.Header.ManufacturerId.Should().Be(0x55);
            message.Header.ProductId.Should().Be(0xAA);
            message.Header.SensorId.Should().Be(0xFEEDED);
            message.Records.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(4);
            message.Records.Take(1).First().Parameter.Should().NotBeNull();
            message.Records.Take(1).First().Parameter.Identifier.Should().Be(ParameterIdentifiers.Temperature);
            message.Records.Take(1).First().Data.As<MessageRecordDataFloat>().Value.Should().Be(20.5f);
            message.Records.Skip(1).Take(1).First().Parameter.Should().NotBeNull();
            message.Records.Skip(1).Take(1).First().Parameter.Identifier.Should().Be(ParameterIdentifiers.Debug);
            message.Records.Skip(1).Take(1).First().Data.As<MessageRecordDataString>().Value.Should().Be("TEST");
            message.Records.Skip(2).Take(1).First().Parameter.Should().NotBeNull();
            message.Records.Skip(2).Take(1).First().Parameter.Identifier.Should().Be(ParameterIdentifiers.Frequency);
            message.Records.Skip(2).Take(1).First().Data.As<MessageRecordDataUInt>().Value.Should().Be(120);
            message.Records.Skip(3).Take(1).First().Parameter.Should().NotBeNull();
            message.Records.Skip(3).Take(1).First().Parameter.Identifier.Should().Be(ParameterIdentifiers.Level);
            message.Records.Skip(3).Take(1).First().Data.As<MessageRecordDataInt>().Value.Should().Be(-50);
        }
    }
}