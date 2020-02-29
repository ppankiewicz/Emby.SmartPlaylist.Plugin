using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Operator.Operators.ListOperators
{
    public class IsListValueOperator : OperatorGen<ListValue, ListValue>
    {
        public IsListValueOperator(): this(ListValue.Default)
        {
        }

        public IsListValueOperator(ListValue defaultListValue)
        {
            DefaultValue = defaultListValue;
        }

        public override string Name => "is";
        public override Value DefaultValue { get; }

        public override bool Compare(Value itemValue, Value value)
        {
            return Compare(itemValue as ListValue, value as ListValue);
        }

        public override bool Compare(ListValue itemValue, ListValue value)
        {
            return itemValue?.Equals(value) ?? false;
        }
    }
}