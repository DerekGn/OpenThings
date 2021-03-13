using FluentAssertions;
using OpenThings;
using System;
using System.Collections.Generic;
using Xunit;

namespace Openthings.UnitTests
{
    public class DecodeTests
    {
        [Fact]
        public void TestMessageInvalidBufferLenght()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 0x00 };

            // Act
            Action action = () => new Message(payload, new List<PidMap>() { new PidMap(0x4, 242) });

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
            Action action = () => new Message(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            OpenThingsException exception = Assert.Throws<OpenThingsException>(action);
            Assert.Equal("Invalid Header length [0] too short", exception.Message);
        }

        [Fact]
        public void TestMalformedMessage()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 0x03, 0xA0, 0x43, 0x61, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            // Act
            Action action = () => new Message(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            OpenThingsException exception = Assert.Throws<OpenThingsException>(action);
            Assert.Equal("Message too short [4]", exception.Message);
        }

        [Fact]
        public void TestMessageValid()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 0x12, 0x55, 0x01, 0x00, 0x00, 0xAD, 0x00, 0x28, 0x74, 0x02, 0x7E, 0x58, 0x74, 0x02, 0x94, 0x63, 0x00, 0x52, 0x8B, 0xCA };

            // Act
            var message = new Message(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            message.Header.ManufacturerId.Should().Be(0x55);
        }

        [Fact]
        public void TestMessageValidJoin()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 0x0c, 0x04, 0x0c, 0x33, 0xf2, 0x99, 0xcd, 0x84, 0x9b, 0x3c, 0x4a, 0xd4, 0xa7, 0x00 };

            // Act
            var message = new Message(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            message.Header.ManufacturerId.Should().Be(4);
        }

        [Fact]
        public void TestTempHumdityMessageValid()
        {
            // Arrange
            List<byte> payload = new List<byte>() { 0x12, 0x55, 0x01, 0x00, 0x00, 0xAD, 0x00, 0x28, 0x74, 0x02, 0x2E, 0x59, 0x74, 0x02, 0x88, 0x63, 0x00, 0xCB, 0xBE, 0x00 };

            // Act
            var message = new Message(payload, new List<PidMap>() { new PidMap(0x4, 242) });

            // Assert
            message.Header.ManufacturerId.Should().Be(0x55);
        }
    }
}
