namespace SmartPlaylist.Domain.Rule
{
    public class Rule : RuleBase
    {
        public static string Kind = "rule";

        public Rule(string id, RuleCriteriaValue criteria) : base(id)
        {
            Criteria = criteria;
        }

        public RuleCriteriaValue Criteria { get; }


        public override bool IsMatch(UserItem item)
        {
            return Criteria.IsMatch(item);
        }
    }
}