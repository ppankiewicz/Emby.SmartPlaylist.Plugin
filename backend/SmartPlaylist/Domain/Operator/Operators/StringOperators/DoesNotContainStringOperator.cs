using System.Linq;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Operator.Operators.StringOperators
{
    public class DoesNotContainStringOperator : ContainsStringOperator
    {
        public override string Name => "does not contain";

        public override bool CompareStrings(StringValue itemValue, StringValue value)
        {
            return !base.CompareStrings(itemValue, value);
        }

        public override bool CompareArrayToString(ArrayValue<StringValue> itemValue, StringValue value)
        {
            return itemValue.Values.All(x => !x.Contains(value));
        }
    }
}