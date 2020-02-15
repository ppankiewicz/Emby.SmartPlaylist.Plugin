using System;
using System.Collections.Generic;
using System.Linq;
using SmartPlaylist.Contracts;
using SmartPlaylist.Domain;
using SmartPlaylist.Domain.Rule;

namespace SmartPlaylist.Adapters
{
    public static class SmartPlaylistAdapter
    {
        public static Domain.SmartPlaylist Adapt(SmartPlaylistDto dto)
        {
            return new Domain.SmartPlaylist(Guid.Parse(dto.Id), dto.Name, dto.UserId,
                new RuleBase[] {RuleAdapter.Adapt(dto.RulesTree)}, GetLimit(dto.Limit), dto.LastShuffleUpdate,
                GetUpdateType(dto.UpdateType), dto);
        }

        private static UpdateType GetUpdateType(string updateTypeName)
        {
            if (Enum.TryParse(updateTypeName, true, out UpdateType updateType)) return updateType;

            return UpdateType.Live;
        }

        private static SmartPlaylistLimit GetLimit(SmartPlaylistLimitDto dto)
        {
            if (!dto.HasLimit) return SmartPlaylistLimit.None;

            return new SmartPlaylistLimit
            {
                MaxItems = dto.MaxItems,
                OrderBy = DefinedLimitOrders.All.FirstOrDefault(x =>
                              string.Equals(x.Name, dto.OrderBy, StringComparison.CurrentCultureIgnoreCase)) ??
                          SmartPlaylistLimit.None.OrderBy
            };
        }

        public static Domain.SmartPlaylist[] Adapt(IEnumerable<SmartPlaylistDto> smartPlaylistDtos)
        {
            return smartPlaylistDtos.Select(ns =>
                Adapt(ns
                )).ToArray();
        }
    }
}