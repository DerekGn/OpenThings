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
using Xunit;

namespace OpenThings.UnitTests
{
    public class MessageRecordTests
    {
        [Fact]
        public void TestMessageRecordNullData()
        {
            // Arrange

            // Act
            Action action = () => new MessageRecord(new Parameter(OpenThingsParameter.AbsoluteActiveEnergy), null);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'data')");
        }

        [Fact]
        public void TestMessageRecordNullParameter()
        {
            // Arrange

            // Act
            Action action = () => new MessageRecord(null, null);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'parameter')");
        }

        [Fact]
        public void TestToString()
        {
            // Arrange

            // Act
            var messageRecord = new MessageRecord(
                new Parameter(OpenThingsParameter.ReactivePower),
                new MessageRecordDataInt(0));

            // Assert
            messageRecord
                .ToString()
                .Should()
                .Be("Parameter:[Identifier: [ReactivePower] Units: [VAR]] Data: [Record Type: [SignedX0] Value: [0x00000000]]");
        }
    }
}
