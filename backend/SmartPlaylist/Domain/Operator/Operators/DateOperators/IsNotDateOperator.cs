using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Operator.Operators.DateOperators
{
    public class IsNotDateOperator : IsDateOperator
    {
        public override string Name => "is not";

        public override bool Compare(Value itemValue, Value value)
        {
            return !base.Compare(itemValue, value);
        }
    }
}