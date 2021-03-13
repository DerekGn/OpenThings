
namespace OpenThings
{
    public class PidMap
    {
        public PidMap(byte manufacturerId, byte pid)
        {
            ManufacturerId = manufacturerId;
            Pid = pid;
        }

        public byte ManufacturerId { get; private set; }

        public byte Pid { get; private set; }
    }
}
