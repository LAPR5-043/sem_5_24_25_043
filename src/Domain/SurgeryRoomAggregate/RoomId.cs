using System;
using src.Domain.Shared;


    public class RoomId : EntityId, IComparer<RoomId>
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

public int Compare(RoomId? x, RoomId? y)
{
    if (x == null && y == null)
    {
        return 0;
    }

    if (x == null)
    {
        return -1;
    }

    if (y == null)
    {
        return 1;
    }

    // Remover os primeiros dois caracteres dos valores de x e y
    int xValue = int.Parse(x.Value.Substring(2));
    int yValue = int.Parse(y.Value.Substring(2));

    return xValue.CompareTo(yValue);
}
}
