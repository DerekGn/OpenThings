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

using System;
using Xunit;

namespace OpenThings.UnitTests
{
    public class MessageTests
    {
        private readonly DefaultParameters _parameters;

        public MessageTests()
        {
            _parameters = new DefaultParameters();
        }

        [Fact]
        public void TestNullMessageHeader()
        {
            // Arrange

            // Act
            Action action = () => new Message(null);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() => action());

            Assert.NotNull(exception);
            Assert.Equal("Value cannot be null. (Parameter 'messageHeader')", exception.Message);
        }

        [Fact]
        public void TestToString()
        {
            // Arrange
            var message = new Message(new MessageHeader(0, 0, 0, 0));

            message.Records.Add(
                new MessageRecord(
                    _parameters.GetParameter(ParameterIdentifiers.WaterPressure),
                    new MessageRecordDataInt(0)));
            // Act
            var result = message.ToString();

            // Assert

            Assert.Equal(
                $"Header->{Environment.NewLine}" +
                $"Length: [0x00] " +
                $"ManufacturerId: [0x00] " +
                $"ProductId: [0x00] " +
                $"Pip: [0x0000] " +
                $"SensorId: [0x00000000]{Environment.NewLine}" +
                $"Records->{Environment.NewLine}" +
                $"Parameter:[Identifier: [0x78] Label: [WaterPressure] Units: [Pa]] Data: [Record Type: [SignedX0] Value: [0x00000000]]{Environment.NewLine}", result);
        }
    }
}