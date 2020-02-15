using System;

namespace SmartPlaylist.Contracts
{
    [Serializable]
    public class RuleTreeNodeDto
    {
        public string Id { get; set; }
        public bool IsRoot { get; set; }
        public bool IsExpanded { get; set; }
        public int Level { get; set; }
        public string[] Children { get; set; }

        public RuleOrRuleGroupDto Data { get; set; }
    }
}