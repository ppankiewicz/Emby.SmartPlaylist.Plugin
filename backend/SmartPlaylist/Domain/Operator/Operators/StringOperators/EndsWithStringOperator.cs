using System.Linq;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Operator.Operators.StringOperators
{
    public class EndsWithStringOperator : StringOperatorBase
    {
        public override string Name => "ends with";

        public override bool CompareStrings(StringValue itemValue, StringValue value)
        {
            return itemValue.EndsWith(value);
        }

        public override bool CompareArrayToString(ArrayValue<StringValue> itemValue, StringValue value)
        {
            return itemValue.Values.Any(x => x.EndsWith(value));
        }
    }
}