using System;
using System.Linq;
using System.Threading.Tasks;
using SmartPlaylist.Contracts;
using SmartPlaylist.Domain;
using SmartPlaylist.Domain.CriteriaDefinition;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Services.SmartPlaylist
{
    public class CleanupOldCriteriaDecorator : ISmartPlaylistStore
    {
        private readonly ISmartPlaylistStore _decorated;

        public CleanupOldCriteriaDecorator(ISmartPlaylistStore decorated)
        {
            _decorated = decorated;
        }

        public async Task<SmartPlaylistDto> GetSmartPlaylistAsync(Guid smartPlaylistId)
        {
            var dto = await _decorated.GetSmartPlaylistAsync(smartPlaylistId).ConfigureAwait(false);

            CleanupSmartPlaylist(dto);

            return dto;
        }

        public async Task<SmartPlaylistDto[]> LoadPlaylistsAsync(Guid userId)
        {
            var smartPlaylists = await _decorated.LoadPlaylistsAsync(userId).ConfigureAwait(false);

            smartPlaylists.ToList().ForEach(CleanupSmartPlaylist);

            return smartPlaylists;
        }

        public async Task<SmartPlaylistDto[]> GetAllSmartPlaylistsAsync()
        {
            var smartPlaylists = await _decorated.GetAllSmartPlaylistsAsync().ConfigureAwait(false);

            smartPlaylists.ToList().ForEach(CleanupSmartPlaylist);

            return smartPlaylists;
        }

        public void Save(SmartPlaylistDto smartPlaylist)
        {
            _decorated.Save(smartPlaylist);
        }

        public void Delete(Guid userId, string smartPlaylistId)
        {
            _decorated.Delete(userId, smartPlaylistId);
        }

        private void CleanupSmartPlaylist(SmartPlaylistDto dto)
        {
            var limitChanged = CleanupOldLimit(dto);
            var criteriaChanged = CleanupOldCriteria(dto);

            if (limitChanged || criteriaChanged) _decorated.Save(dto);
        }

        private static bool CleanupOldCriteria(SmartPlaylistDto dto)
        {
            var changed = false;
            var rulesWithCriteria = dto.RulesTree.Where(x => x.Data?.Criteria != null).ToArray();
            foreach (var rule in rulesWithCriteria)
            {
                var criteria = rule.Data.Criteria;
                var criteriaDefinition = DefinedCriteriaDefinitions.All.FirstOrDefault(x => x.Name == criteria.Name);
                if (criteriaDefinition == null)
                {
                    ChangeCriteriaDefinition(criteria);

                    changed = true;
                }
                else if (IsOldListValue(criteria.Value, criteriaDefinition.Values))
                {
                    criteria.Value = criteriaDefinition.Values.First();
                    changed = true;
                }
            }

            return changed;
        }

        private static void ChangeCriteriaDefinition(RuleCriteriaValueDto criteria)
        {
            var criteriaDefinition = GetFirstBestMatchedCriteriaDef(criteria);
            var criteriaDefOperator = criteriaDefinition.Type.Operators.First();
            criteria.Name = criteriaDefinition.Name;
            if (criteria.Operator.Type != criteriaDefOperator.Type)
            {
                criteria.Operator = criteriaDefOperator.ToDto();
                criteria.Value = criteriaDefOperator.DefaultValue;
            }
        }

        private static CriteriaDefinition GetFirstBestMatchedCriteriaDef(RuleCriteriaValueDto criteria)
        {
            return DefinedCriteriaDefinitions.All.FirstOrDefault(x => x.Type.Name == criteria.Operator.Type) ??
                   DefinedCriteriaDefinitions.All.First();
        }

        private static bool CleanupOldLimit(SmartPlaylistDto dto)
        {
            var changed = false;
            if (dto.Limit.HasLimit && !DefinedLimitOrders.AllNames.Contains(dto.Limit.OrderBy))
            {
                dto.Limit.OrderBy = DefinedLimitOrders.AllNames.First();
                changed = true;
            }

            return changed;
        }

        private static bool IsOldListValue(object currentValue, Value[] availableValues)
        {
            return currentValue is ListValue listValue && availableValues.Any() && !availableValues.Contains(listValue);
        }

        public bool Exists(Guid userId, string smartPlaylistId)
        {
            return _decorated.Exists(userId, smartPlaylistId);
        }
    }
}