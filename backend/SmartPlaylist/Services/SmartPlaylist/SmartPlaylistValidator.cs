using System;
using System.Collections.Generic;
using System.Linq;
using SmartPlaylist.Contracts;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Services.SmartPlaylist
{
    public class SmartPlaylistValidator
    {
        public void Validate(SmartPlaylistDto smartPlaylist)
        {
            var errorProps = new List<string>();
            if (!ValidateName(smartPlaylist)) errorProps.Add("SmartPlaylistName");

            if (!ValidateCriteriaValue(smartPlaylist)) errorProps.Add("RuleCriteriaValue");

            if (!ValidateMaxItemsLimit(smartPlaylist)) errorProps.Add("MaxItemsLimit");

            if (errorProps.Any())
                throw new Exception(
                    $"Validation of {nameof(SmartPlaylistDto)} failed for: {string.Join(",", errorProps)}");
        }

        private static bool ValidateMaxItemsLimit(SmartPlaylistDto smartPlaylist)
        {
            return (smartPlaylist.Limit.HasLimit && smartPlaylist.Limit.MaxItems >= 1) || !smartPlaylist.Limit.HasLimit;
        }

        private static bool ValidateCriteriaValue(SmartPlaylistDto smartPlaylist)
        {
            return smartPlaylist.RulesTree.Select(x => x.Data)
                .Where(x => x.Criteria != null).All(x => !((Value) x.Criteria.Value).IsNone);
        }

        private static bool ValidateName(SmartPlaylistDto smartPlaylist)
        {
            return !string.IsNullOrWhiteSpace(smartPlaylist.Name);
        }
    }
}