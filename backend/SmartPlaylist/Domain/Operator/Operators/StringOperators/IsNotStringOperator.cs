using System.Linq;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Operator.Operators.StringOperators
{
    public class IsNotStringOperator : IsStringOperator
    {
        public override string Name => "is not";

        public override bool CompareStrings(StringValue itemValue, StringValue value)
        {
            return !base.CompareStrings(itemValue, value);
        }

        public override bool CompareArrayToString(ArrayValue<StringValue> itemValue, StringValue value)
        {
            return itemValue.Values.All(x => !Compare(x, value));
        }
    }
}