using System.Collections.Generic;

namespace OpenThings
{
    public interface IOpenThingsEncoder
    {
        IList<byte> Encode(Message message);

        IList<byte> Encode(Message message, byte encryptionId, uint seed);
    }
}