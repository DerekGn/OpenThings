using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OpenThings.UnitTests
{
    public class DecodeTests
    {
        private OpenThingsDecoder _decoder;

        public DecodeTests()
        {
            _decoder = new OpenThingsDecoder();
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
            List<byte> payload = new List<byte>() { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            // Act
            Action action = () => _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            OpenThingsException exception = Assert.Throws<OpenThingsException>(action);
            Assert.Equal("Invalid OpenThings Header length [0]", exception.Message);
        }

        [Fact]
        public void TestMalformedMessage()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 0x03, 0xA0, 0x43, 0x61, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            // Act
            Action action = () => _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            OpenThingsException exception = Assert.Throws<OpenThingsException>(action);
            Assert.Equal("Invalid OpenThings Header length [3]", exception.Message);
        }

        [Fact]
        public void TestMessageValid()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 0x12, 0x55, 0x01, 0x00, 0x00, 0xAD, 0x00, 0x28, 0x74, 0x02, 0x7E, 0x58, 0x74, 0x02, 0x94, 0x63, 0x00, 0x52, 0x8B, 0xCA };

            // Act
            var message = _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            message.Header.Should().NotBeNull();
            message.Header.ManufacturerId.Should().Be(0x55);
            message.Header.ProductId.Should().Be(1);
            message.Header.SensorId.Should().Be(0xAD0028);
            message.Records.Should().NotBeNull().And.NotBeEmpty();
            message.Records.Should().HaveCount(2);
            message.Records.Take(1).First().Parameter.Should().NotBeNull();
            message.Records.Take(1).First().Parameter.Identifier.Should().Be(ParameterIdentifier.Temperature);
            message.Records.Skip(1).Take(1).First().Parameter.Should().NotBeNull();
            message.Records.Skip(1).Take(1).First().Parameter.Identifier.Should().Be(ParameterIdentifier.Temperature);
        }

        [Fact]
        public void TestMessageValidJoin()
        {
            //Arrange
            List<byte> payload = new List<byte>() { 0x0c, 0x04, 0x0c, 0x33, 0xf2, 0x99, 0xcd, 0x84, 0x9b, 0x3c, 0x4a, 0xd4, 0xa7, 0x00 };

            // Act
            var message = _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            message.Header.ManufacturerId.Should().Be(4);
        }

        [Fact]
        public void TestTempHumdityMessageValid()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 0x12, 0x55, 0x01, 0x00, 0x00, 0xAD, 0x00, 0x28, 0x74, 0x02, 0x2E, 0x59, 0x74, 0x02, 0x88, 0x63, 0x00, 0xCB, 0xBE, 0x00 };

            // Act
            var message = _decoder.Decode(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            message.Header.ManufacturerId.Should().Be(0x55);
        }
    }
}
