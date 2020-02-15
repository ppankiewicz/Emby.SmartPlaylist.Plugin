using System.Linq;

namespace SmartPlaylist.Domain.Rule
{
    public class RuleGroup : RuleBase
    {
        public static string Kind = "ruleGroup";

        public RuleGroup(string id, RuleBase[] children, RuleGroupMatchMode matchMode) : base(id)
        {
            Children = children;
            MatchMode = matchMode;
        }

        public RuleBase[] Children { get; }
        public RuleGroupMatchMode MatchMode { get; }

        public override bool IsMatch(UserItem item)
        {
            switch (MatchMode)
            {
                case RuleGroupMatchMode.All:
                    return Children.All(x => x.IsMatch(item));
                case RuleGroupMatchMode.Any:
                    return Children.Any(x => x.IsMatch(item));
                default:
                    return false;
            }
        }
    }
}