using System.Linq;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Operator.Operators.StringOperators
{
    public class ContainsStringOperator : StringOperatorBase
    {
        public override string Name => "contains";
        public override Value DefaultValue => StringValue.Default;

        public override bool CompareStrings(StringValue itemValue, StringValue value)
        {
            return itemValue.Contains(value);
        }

        public override bool CompareArrayToString(ArrayValue<StringValue> itemValue, StringValue value)
        {
            return itemValue.Values.Any(x => x.Contains(value));
        }
    }
}