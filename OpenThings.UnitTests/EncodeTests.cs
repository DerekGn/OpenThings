using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace OpenThings.UnitTests
{
    public class EncodeTests
    {
        private OpenThingsEncoder _encoder;

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

            var parameterTemp = new Parameter(ParameterIdentifier.Temperature);
            var dataTemp = new MessageRecordDataUInt(RecordType.UnsignedX0, 2, 0xBEEF);
            var parameterHumidity = new Parameter(ParameterIdentifier.RelativeHumidity);
            var dataHumidity = new MessageRecordDataUInt(RecordType.UnsignedX0, 2, 0xBEEF);

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
                .ContainInOrder(new List<byte>() { 0x13, 0x55, 0xAA, 0x00, 0x00, 0xFE, 0xED, 0xED, 0x74, 0x02, 0xBE, 0xEF, 0x68, 0x02, 0xBE, 0xEF, 0x00, 0x70, 0xA4 });
        }

        [Fact]
        public void TestEncodeMessageWithEncoding()
        {
            // Arrange
            var messageHeader = new MessageHeader(0x55, 0xAA, 0x0000, 0xFEEDED);

            var message = new Message(messageHeader);

            var parameterTemp = new Parameter(ParameterIdentifier.Temperature);
            var dataTemp = new MessageRecordDataUInt(RecordType.UnsignedX0, 2, 0xBEEF);
            var parameterHumidity = new Parameter(ParameterIdentifier.RelativeHumidity);
            var dataHumidity = new MessageRecordDataUInt(RecordType.UnsignedX0, 2, 0xBEEF);

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
                .ContainInOrder(new List<byte>() { 0x13, 0x55, 0xAA, 0x55, 0x55, 0x18, 0xE2, 0x3B, 0x97, 0x06, 0x47, 0x40, 0xFB, 0xE8, 0x84, 0x0E, 0xCD, 0x29, 0xAE });
        }
    }
}
