using System.Collections.Generic;
using System.Linq;

namespace OpenThings
{
    public class OpenThingsEncoder : IOpenThingsEncoder
    {
        public IList<byte> Encode(Message message)
        {
            Crc16Ccitt crc16Ccitt = new Crc16Ccitt(0);

            List<byte> encoded = new List<byte>
            {
                0,
                message.Header.ManufacturerId,
                message.Header.ProductId,
                message.Header.ProductId,
                0,
                0
            };

            foreach (var record in message.Records)
            {
                encoded.Add((byte)record.Parameter.Identifier);
                encoded.AddRange(record.Data.Encode());
            }

            var crcBytes = crc16Ccitt.ComputeChecksumBytes(encoded.Skip(5).ToArray());

            encoded.Add(0);
            encoded.AddRange(crcBytes);

            encoded[0] = (byte) encoded.Count;

            return encoded;
        }

        public IList<byte> Encode(Message message, byte encryptionId, uint seed)
        {
            var encoded = Encode(message);

            return encoded;
        }
    }
}
