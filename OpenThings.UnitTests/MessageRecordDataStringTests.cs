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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OpenThings.UnitTests
{
    public class MessageRecordDataStringTests
    {
        private const string Expected = "ABCD";

        [Fact]
        public void TestDecode()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataString(Encoding.ASCII.GetBytes(Expected).ToList());

            // Act
            var result = messageRecordData.Value;

            // Assert
            Assert.Equal(Expected, result);
        }

        [Fact]
        public void TestEmpty()
        {
            // Arrange

            // Act
            Action action = () => { new MessageRecordDataString(""); };

            // Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => action());

            Assert.NotNull(exception);
            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'value')", exception.Message);
        }

        [Fact]
        public void TestEmptyBytes()
        {
            // Arrange

            // Act
            Action action = () => new MessageRecordDataString(bytes: null);

            // Assert
            Assert.Throws<ArgumentNullException>(() => action());
        }

        [Fact]
        public void TestEncode()
        {
            // Arrange
            var messageRecordData = new MessageRecordDataString("".PadRight(5, 'X'));

            // Act
            var result = messageRecordData.Encode();

            // Assert
            Assert.Equal(new List<byte>() { 0x75, 0x58, 0x58, 0x58, 0x58, 0x58 }, result);
        }

        [Fact]
        public void TestMaxLength()
        {
            // Arrange
            
            // Act
            Action action = () => { new MessageRecordDataString("".PadRight(16, 'X')); };

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => action());
        }
    }
}
