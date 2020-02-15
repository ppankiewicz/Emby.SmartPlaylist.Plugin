namespace SmartPlaylist.Domain.Values
{
    public class BoolValue : Value
    {
        public override string Kind => "bool";
        public static BoolValue Default = new BoolValue(false);

        public bool Value { get; }

        public static Value Create(bool value)
        {
            return new BoolValue(value);
        }


        public BoolValue(bool value)
        {
            Value = value;
        }

        protected bool Equals(BoolValue other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BoolValue) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(BoolValue left, BoolValue right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BoolValue left, BoolValue right)
        {
            return !Equals(left, right);
        }
    }
}