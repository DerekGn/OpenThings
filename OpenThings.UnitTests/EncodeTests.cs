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
            var messageHeader = new MessageHeader(0x55, 0xAA, 0x0000);

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
                .ContainInOrder(new List<byte>() { 0x11, 0x55, 0xAA, 0xAA, 0x00, 0x00, 0x74, 0x02, 0xBE, 0xEF, 0x68, 0x02, 0xBE, 0xEF, 0x00, 0xCA, 0x79 });
        }
    }
}
