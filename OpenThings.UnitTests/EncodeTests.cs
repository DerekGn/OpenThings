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
using System.Linq;
using Xunit;

namespace OpenThings.UnitTests
{
    public class EncodeTests
    {
        private readonly OpenThingsEncoder _encoder;
        private readonly ParameterList _parameters;

        public EncodeTests()
        {
            _encoder = new OpenThingsEncoder();
            _parameters = new ParameterList();
        }

        [Fact]
        public void TestEncodeMessage()
        {
            // Arrange
            var messageHeader = new MessageHeader(0x55, 0xAA, 0x0000, 0xFEEDED);

            var message = new Message(messageHeader);

            message.Records.Add(
                new MessageRecord(
                    _parameters.GetParameter(ParameterIdentifier.Temperature),
                    new MessageRecordDataFloat(RecordType.UnsignedX4, 20.5f)));

            message.Records.Add(
                new MessageRecord(
                    _parameters.GetParameter(ParameterIdentifier.Debug),
                    new MessageRecordDataString("TEST")));

            message.Records.Add(
                new MessageRecord(
                    _parameters.GetParameter(ParameterIdentifier.Frequency),
                    new MessageRecordDataUInt(120)));

            message.Records.Add(
                new MessageRecord(
                    _parameters.GetParameter(ParameterIdentifier.Level),
                    new MessageRecordDataInt(-50)));

            // Act
            var result = _encoder.Encode(message);

            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .NotBeEmpty()
                .And
                .Equal(new List<byte>() {
                    0x1A, 0x55, 0xAA, 0x00, 
                    0x00, 0xFE, 0xED, 0xED, 
                    0x74, 0x12, 0x01, 0x48, 
                    0x2D, 0x74, 0x54, 0x45, 
                    0x53, 0x54, 0x66, 0x01, 
                    0x78, 0x4C, 0x81, 0xCE, 
                    0x00, 0xF9, 0xB7
                });
        }

        [Fact]
        public void TestEncodeMessageIdentifyCommand()
        {
            MessageRecordDataUInt messageRecordData = new MessageRecordDataUInt(0);
            Parameter parameter = _parameters.GetParameter(ParameterIdentifier.IdentifyCommand);
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
                .Equal(new List<byte>() { 0x0C, 0x00, 0x00, 0x00, 0x00, 0xF3, 0xE3, 0x79, 0xBF, 0x00, 0x00, 0xE9, 0x4E });
        }

        [Fact]
        public void TestEncodeMessageWithEncryption()
        {
            // Arrange
            var messageHeader = new MessageHeader(0x55, 0xAA, 0x0000, 0xFEEDED);

            var message = new Message(messageHeader);

            var parameterTemp = _parameters.GetParameter(ParameterIdentifier.Temperature);
            var dataTemp = new MessageRecordDataUInt(0xBEEF);
            var parameterHumidity = _parameters.GetParameter(ParameterIdentifier.RelativeHumidity);
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
                .Equal(new List<byte>() { 0x12, 0x55, 0xAA, 0x55, 0x55, 0x18, 0xE2, 0x3B, 0x97, 0x06, 0x47, 0x40, 0xFB, 0xE8, 0x84, 0x0E, 0xCD, 0x29, 0xAE });
        }
    }
}