using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Operator.Operators.DateOperators
{
    public class IsNotInTheLastOperator : IsInTheLastOperator
    {
        public override string Name => "is not in the last";

        public override bool Compare(Value itemValue, Value value)
        {
            return !base.Compare(itemValue, value);
        }
    }
}