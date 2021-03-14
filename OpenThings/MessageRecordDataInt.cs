
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenThings
{
    public class MessageRecordDataInt : BaseMessageRecordData
    {
        public MessageRecordDataInt(RecordType recordType, int length, int value) : base(recordType, length)
        {
            if (length > 4)
                throw new ArgumentOutOfRangeException(nameof(length));

            AssertRecordType(recordType);
            Value = value;
        }

        public int Value { get; }

        internal override IList<byte> GetValueByes()
        {
            return BitConverter.GetBytes(Value).Take(Length).Reverse().ToList();
        }

        private void AssertRecordType(RecordType recordType)
        {
            bool valid = false;
            switch (recordType)
            {
                case RecordType.SignedX0:
                case RecordType.SignedX8:
                case RecordType.SignedX12:
                case RecordType.SignedX16:
                case RecordType.SignedX24:
                    valid = true;
                    break;
            }

            if (!valid)
                throw new ArgumentOutOfRangeException(nameof(recordType));
        }
    }
}
