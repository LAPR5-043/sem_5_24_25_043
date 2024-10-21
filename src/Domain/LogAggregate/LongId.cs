using src.Domain.Shared;

public class LongId : EntityId,  IComparable<LongId>
{
    public long Value { get; private set; }

    public LongId(long value) : base (value)
    {
        Value = value;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        LongId other = (LongId)obj;
        return Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    protected override object createFromString(string text)
    {
       return new LongId(long.Parse(text));
    }

    public override string AsString()
    {
        return Value.ToString();
    }
    public int CompareTo(LongId other)
    {
        if (other == null) return 1;
        return Value.CompareTo(other.Value);
    }
}