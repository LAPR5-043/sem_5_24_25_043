using System;
using src.Domain.Shared;

/// <summary>
/// Represents an id of a staff member
/// </summary>
public class StaffID : EntityId , IComparable<StaffID>
{
    /// <summary>
    /// The staff id
    /// </summary>
    public string id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StaffID"/> class
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="ArgumentException"></exception>
    public StaffID(string id) : base(id)
    {
        this.id = id;
    }

    public StaffID() : base(null)
    {
    }

    /// <summary>
    /// Compares two staff id's for equality
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        StaffID staffID = (StaffID)obj;
        return id == staffID.id;
    }

    /// <summary>
    /// Returns the hash code for the staff id
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return id.GetHashCode();
    }

    /// <summary>
    /// Returns the string representation of the staff id
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return id;
    }

    protected override object createFromString(string text)
    {
        return new String(text);
    }

    public override string AsString()
    {
        return id;
    }

    public int CompareTo(StaffID? other)
    {
        if (other == null)
        {
            return 1;
        }

        return id.CompareTo(other.id);
    }
}