using System.Linq;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Operator.Operators.StringOperators
{
    public class StartsWithStringOperator : StringOperatorBase
    {
        public override string Name => "starts with";


        public override bool CompareStrings(StringValue itemValue, StringValue value)
        {
            return itemValue.StartsWith(value);
        }

        public override bool CompareArrayToString(ArrayValue<StringValue> itemValue, StringValue value)
        {
            return itemValue.Values.Any(x => x.StartsWith(value));
        }
    }
}