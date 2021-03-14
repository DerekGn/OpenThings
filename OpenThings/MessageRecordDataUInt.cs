using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenThings
{
    public class MessageRecordDataUInt : BaseMessageRecordData
    {
        public MessageRecordDataUInt(RecordType recordType, int length, uint value) : base(recordType, length)
        {
            if (length > 4)
                throw new ArgumentOutOfRangeException(nameof(length));
            AssertRecordType(recordType);
            Value = value;
        }

        public uint Value { get; }

        internal override IList<byte> GetValueByes()
        {
            return BitConverter.GetBytes(Value).Take(Length).Reverse().ToList();
        }

        private void AssertRecordType(RecordType recordType)
        {
            bool valid = false;
            switch (recordType)
            {
                case RecordType.UnsignedX0:
                case RecordType.UnsignedX4:
                case RecordType.UnsignedX8:
                case RecordType.UnsignedX12:
                case RecordType.UnsignedX16:
                case RecordType.UnsignedX20:
                case RecordType.UnsignedX24:
                    valid = true;
                    break;
            }

            if (!valid)
                throw new ArgumentOutOfRangeException(nameof(recordType));
        }
    }
}
