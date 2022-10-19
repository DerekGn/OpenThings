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
using System.Collections.Generic;
using Xunit;

namespace OpenThings.UnitTests
{
    public class EncodeTests
    {
        private readonly OpenThingsEncoder _encoder;

        public EncodeTests()
        {
            _encoder = new OpenThingsEncoder();
        }

        [Fact]
        public void TestEncodeMessage()
        {
            // Arrange
            var messageHeader = new MessageHeader(0x55, 0xAA, 0x0000, 0xFEEDED);

            var message = new Message(messageHeader);

            var parameterTemp = new Parameter(OpenThingsParameter.Temperature);
            var dataTemp = new MessageRecordDataUInt(0xBEEF);
            var parameterHumidity = new Parameter(OpenThingsParameter.RelativeHumidity);
            var dataHumidity = new MessageRecordDataUInt(0xBEEF);

            message.Records.Add(
                new MessageRecord(
                    parameterTemp,
                    dataTemp));

            message.Records.Add(
                new MessageRecord(
                    parameterHumidity,
                    dataHumidity));

            // Act
            var result = _encoder.Encode(message);

            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .NotBeEmpty()
                .And
                .ContainInOrder(new List<byte>() { 0x12, 0x55, 0xAA, 0x00, 0x00, 0xFE, 0xED, 0xED, 0x74, 0x02, 0xBE, 0xEF, 0x68, 0x02, 0xBE, 0xEF, 0x00, 0x70, 0xA4 });
        }

        [Fact]
        public void TestEncodeMessageIdentifyCommand()
        {
            MessageRecordDataUInt messageRecordData = new MessageRecordDataUInt(0);
            Parameter parameter = new Parameter(OpenThingsParameter.IdentifyCommand);
            MessageRecord messageRecord = new MessageRecord(parameter, messageRecordData);
            MessageHeader messageHeader = new MessageHeader(0, 0, 0, 0x00F3E379);
            Message message = new Message(messageHeader);
            message.Records.Add(messageRecord);

            // Act
            var result = _encoder.Encode(message);

            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .NotBeEmpty()
                .And
                .ContainInOrder(new List<byte>() { 0x0C, 0x00, 0x00, 0x00, 0x00, 0xF3, 0xE3, 0x79, 0xBF, 0x00, 0x00, 0xE9, 0x4E });
        }

        [Fact]
        public void TestEncodeMessageWithEncryption()
        {
            // Arrange
            var messageHeader = new MessageHeader(0x55, 0xAA, 0x0000, 0xFEEDED);

            var message = new Message(messageHeader);

            var parameterTemp = new Parameter(OpenThingsParameter.Temperature);
            var dataTemp = new MessageRecordDataUInt(0xBEEF);
            var parameterHumidity = new Parameter(OpenThingsParameter.RelativeHumidity);
            var dataHumidity = new MessageRecordDataUInt(0xBEEF);

            message.Records.Add(
                new MessageRecord(
                    parameterTemp,
                    dataTemp));

            message.Records.Add(
                new MessageRecord(
                    parameterHumidity,
                    dataHumidity));

            // Act
            var result = _encoder.Encode(message, 0xFF, 0xAA55);

            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .NotBeEmpty()
                .And
                .ContainInOrder(new List<byte>() { 0x12, 0x55, 0xAA, 0x55, 0x55, 0x18, 0xE2, 0x3B, 0x97, 0x06, 0x47, 0x40, 0xFB, 0xE8, 0x84, 0x0E, 0xCD, 0x29, 0xAE });
        }
    }
}