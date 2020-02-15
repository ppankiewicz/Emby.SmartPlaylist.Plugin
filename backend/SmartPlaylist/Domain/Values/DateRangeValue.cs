using System;

namespace SmartPlaylist.Domain.Values
{
    public class DateRangeValue : Value
    {
        public static DateRangeValue Default =
            new DateRangeValue(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddMonths(1));

        public DateRangeValue(DateTimeOffset from, DateTimeOffset to)
        {
            From = from;
            To = to;
        }

        public override string Kind => "dateRange";

        public DateTimeOffset From { get; }
        public DateTimeOffset To { get; }

        public static DateRangeValue Create(DateTimeOffset from, DateTimeOffset to)
        {
            return new DateRangeValue(from, to);
        }

        protected bool Equals(DateRangeValue other)
        {
            return From.Equals(other.From) && To.Equals(other.To);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DateRangeValue) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (From.GetHashCode() * 397) ^ To.GetHashCode();
            }
        }

        public static bool operator ==(DateRangeValue left, DateRangeValue right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DateRangeValue left, DateRangeValue right)
        {
            return !Equals(left, right);
        }
    }
}