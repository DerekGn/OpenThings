using System.Collections.Generic;

namespace OpenThings
{
    public interface IOpenThingsDecoder
    {
        Message Decode(IList<byte> payload, IList<PidMap> pidMaps);
    }
}