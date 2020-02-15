using System.Linq;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Operator.Operators.StringOperators
{
    public class IsStringOperator : StringOperatorBase
    {
        public override string Name => "is";


        public override bool CompareStrings(StringValue itemValue, StringValue value)
        {
            return itemValue.Equals(value);
        }

        public override bool CompareArrayToString(ArrayValue<StringValue> itemValue, StringValue value)
        {
            return itemValue.Values.Any(x => x.Equals(value));
        }
    }
}