using System.Linq;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.Rule
{
    public class RuleCriteriaValue
    {
        public RuleCriteriaValue(Value value, Operator.Operator @operator,
            CriteriaDefinition.CriteriaDefinition criteriaDefinition)
        {
            Value = value;
            Operator = @operator;
            CriteriaDefinition = criteriaDefinition;
        }

        public Value Value { get; }

        public Operator.Operator Operator { get; }

        public CriteriaDefinition.CriteriaDefinition CriteriaDefinition { get; }

        public bool IsMatch(UserItem item)
        {
            var itemValue = CriteriaDefinition.GetValue(item);
            return Operator.CanCompare(itemValue, Value) && Operator.Compare(itemValue, Value);
        }
    }
}