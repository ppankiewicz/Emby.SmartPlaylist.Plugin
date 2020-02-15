namespace SmartPlaylist.Domain.Values
{
    public class NumberRangeValue : Value
    {
        public static NumberRangeValue Default = new NumberRangeValue(0, 0);

        public NumberRangeValue(int from, int to)
        {
            From = from;
            To = to;
        }

        public override string Kind => "numberRange";
        public int From { get; }
        public int To { get; }

        public static NumberRangeValue Create(int from, int to)
        {
            return new NumberRangeValue(from, to);
        }

        protected bool Equals(NumberRangeValue other)
        {
            return From == other.From && To == other.To;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NumberRangeValue) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (From * 397) ^ To;
            }
        }

        public static bool operator ==(NumberRangeValue left, NumberRangeValue right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NumberRangeValue left, NumberRangeValue right)
        {
            return !Equals(left, right);
        }
    }
}