using System;
using src.Domain.Shared;


    public class RoomId : EntityId
    {
        public string Value { get; }

        public RoomId(string value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Room ID cannot be null or empty.", nameof(value));
            }

            Value = value;
        }

    protected override object createFromString(string text)
    {
        return new String(text);
    }

    public override string AsString()
    {
        return Value;
    }
}
