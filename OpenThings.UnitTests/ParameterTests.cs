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
    public class ParameterTests
    {
        [Fact]
        public void TestValid()
        {
            // Arrange

            // Act
            var parameter = new Parameter(OpenThingsParameter.BatteryLevel);

            // Assert
            parameter.Units.Should().Be("V");
        }

        [Fact]
        public void TestValidNoUnits()
        {
            // Arrange

            // Act
            var parameter = new Parameter(OpenThingsParameter.AbsoluteActiveEnergy);

            // Assert
            parameter.Units.Should().Be(string.Empty);
        }

        [Fact]
        public void TestToString()
        {
            // Arrange

            // Act
            var parameter = new Parameter(OpenThingsParameter.BatteryLevel);

            // Assert
            parameter.ToString().Should().Be("Identifier: [BatteryLevel] Units: [V]");
        }

        [Fact]
        public void TestGetParameter()
        {
            // Arrange
            
            // Act
            var parameter = Parameter.GetParameter(0x21);

            // Assert
            parameter.Should().NotBeNull();
        }

        [Fact]
        public void TestGetParameterNotFound()
        {
            // Arrange

            // Act
            Action action = () => { var parameter = Parameter.GetParameter(20); };

            // Assert
            action.Should().Throw<OpenThingsException>().WithMessage("Unable to find parameter id [0x14]");
        }
    }
}
