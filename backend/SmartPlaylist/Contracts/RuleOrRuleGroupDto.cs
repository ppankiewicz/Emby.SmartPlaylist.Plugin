using System;

namespace SmartPlaylist.Contracts
{
    [Serializable]
    public class RuleOrRuleGroupDto
    {
        public string Id { get; set; }
        public string Kind { get; set; }
        public RuleCriteriaValueDto Criteria { get; set; }

        public string MatchMode { get; set; }
    }
}