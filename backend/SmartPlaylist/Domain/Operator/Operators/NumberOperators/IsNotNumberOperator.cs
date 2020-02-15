using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Operator.Operators.NumberOperators
{
    public class IsNotNumberOperator : IsNumberOperator
    {
        public override string Name => "is not";

        public override bool Compare(Value itemValue, Value value)
        {
            return !base.Compare(itemValue, value);
        }
    }
}