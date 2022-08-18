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
    public class MessageHeaderTests
    {
        [Fact]
        public void TestInvalidManufacturer()
        {
            // Arrange

            // Act
            Action action = () => new MessageHeader(0x81, 0, 0, 0);

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Specified argument was out of the range of valid values. (Parameter 'manufacturerId')");
        }

        [Fact]
        public void TestInvalidSensorId()
        {
            // Arrange

            // Act
            Action action = () => new MessageHeader(0x10, 0, 0, uint.MaxValue);

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Specified argument was out of the range of valid values. (Parameter 'sensorId')");
        }

        [Fact]
        public void TestSetSensorId()
        {
            // Arrange
            var messageHeader = new MessageHeader(0x10, 0, 0, 0);

            // Act
            messageHeader.SetSensorId(new List<byte>() { 0xFE, 0xED, 0xAA });

            // Assert
            messageHeader.SensorId.Should().Be(0xFEEDAA);
        }

        [Fact]
        public void TestSetSensorIdInvalidCountValue()
        {
            // Arrange
            var messageHeader = new MessageHeader(0x10, 0, 0, 0);

            // Act
            Action action = () => messageHeader.SetSensorId(new List<byte>() { });

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Specified argument was out of the range of valid values. (Parameter 'sensorId')");
        }

        [Fact]
        public void TestSetSensorIdNullValue()
        {
            // Arrange
            var messageHeader = new MessageHeader(0x10, 0, 0, 0);

            // Act
            Action action = () => messageHeader.SetSensorId(null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void TestToString()
        {
            // Arrange
            var messageHeader = new MessageHeader(0x10, 0, 0, 0);

            // Act
            var result = messageHeader.ToString();

            // Assert
            result
                .Should()
                .Be("Length: [0x00] ManufacturerId: [0x10] ProductId: [0x00] Pip: [0x0000] SensorId: [0x00000000]");
        }
    }
}