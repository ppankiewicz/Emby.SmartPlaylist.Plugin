using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Operator.Operators.ListOperators
{
    public class IsNotListValueOperator : IsListValueOperator
    {
        public IsNotListValueOperator():this(ListValue.Default)
        {
        }

        public IsNotListValueOperator(ListValue defaultListValue):base(defaultListValue)
        {
        }

        public override string Name => "is not";

        public override bool Compare(Value itemValue, Value value)
        {
            return !base.Compare(itemValue, value);
        }
    }
}