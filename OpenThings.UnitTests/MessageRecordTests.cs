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
    public class MessageRecordTests
    {
        private readonly DefaultParameters _parameters;

        public MessageRecordTests()
        {
            _parameters = new DefaultParameters();
        }

        [Fact]
        public void TestMessageRecordNullData()
        {
            // Arrange

            // Act
            Action action = () => new MessageRecord(_parameters.GetParameter(ParameterIdentifiers.AirPressure), null);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() => action());

            Assert.NotNull(exception);
            Assert.Equal("Value cannot be null. (Parameter 'data')", exception.Message);
        }

        [Fact]
        public void TestMessageRecordNullParameter()
        {
            // Arrange

            // Act
            Action action = () => new MessageRecord(null, null);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() => action());

            Assert.NotNull(exception);
            Assert.Equal("Value cannot be null. (Parameter 'parameter')", exception.Message);
        }

        [Fact]
        public void TestToString()
        {
            // Arrange

            // Act
            var messageRecord = new MessageRecord(
                _parameters.GetParameter(ParameterIdentifiers.ReactivePower),
                new MessageRecordDataInt(0));

            // Assert
            Assert.NotNull(messageRecord);
            Assert.Equal("Parameter:[Identifier: [0x71] Label: [ReactivePower] Units: [VAR]] Data: [Record Type: [SignedX0] Value: [0x00000000]]",
                messageRecord.ToString());
        }
    }
}