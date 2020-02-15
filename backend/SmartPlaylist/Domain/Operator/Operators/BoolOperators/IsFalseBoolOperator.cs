using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Operator.Operators.BoolOperators
{
    public class IsFalseBoolOperator : IsTrueBoolOperator
    {
        public override string Name => "is false";

        public override bool Compare(Value itemValue, Value value)
        {
            return !base.Compare(itemValue, value);
        }
    }
}