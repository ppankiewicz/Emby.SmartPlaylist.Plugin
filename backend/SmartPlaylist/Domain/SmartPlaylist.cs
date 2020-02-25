using System;
using System.Collections.Generic;
using System.Linq;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.TV;
using SmartPlaylist.Comparers;
using SmartPlaylist.Contracts;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Extensions;

namespace SmartPlaylist.Domain
{
    public class SmartPlaylist
    {
        private readonly SmartPlaylistDto _dto;

        public SmartPlaylist(Guid id, string name, Guid userId, RuleBase[] rules,
            SmartPlaylistLimit limit, DateTimeOffset? lastShuffleUpdate, UpdateType updateType, SmartPlaylistDto dto)
        {
            _dto = dto;
            Id = id;
            Name = name;
            UserId = userId;
            Rules = rules;
            Limit = limit;
            LastShuffleUpdate = lastShuffleUpdate;
            UpdateType = updateType;
            MediaType = MediaTypeGetter.Get(rules);
        }

        public Guid Id { get; }
        public string Name { get; }
        public Guid UserId { get; }
        public RuleBase[] Rules { get; }

        public SmartPlaylistLimit Limit { get; }

        public UpdateType UpdateType { get; }
        public DateTimeOffset? LastShuffleUpdate { get; private set; }


        public bool CanUpdatePlaylist => CheckIfCanUpdatePlaylist();

        public bool IsShuffleUpdateType => UpdateType == UpdateType.ShuffleDaily ||
                                           UpdateType == UpdateType.ShuffleMonthly ||
                                           UpdateType == UpdateType.ShuffleWeekly;

        public bool CanUpdatePlaylistWithNewItems => (IsRandomSort || !Limit.HasLimit) && !IsShuffleUpdateType;
        public bool IsRandomSort => Limit.OrderBy is RandomLimitOrder;
        public string MediaType { get; }

        private bool CheckIfCanUpdatePlaylist()
        {
            if (UpdateType == UpdateType.Manual) return false;

            if (LastShuffleUpdate.HasValue && IsShuffleUpdateType)
            {
                var now = DateTimeOffset.UtcNow;
                switch (UpdateType)
                {
                    case UpdateType.ShuffleDaily:
                        return now >= LastShuffleUpdate.Value.AddDays(1);
                    case UpdateType.ShuffleWeekly:
                        return now >= LastShuffleUpdate.Value.AddDays(7);
                    case UpdateType.ShuffleMonthly:
                        return now >= LastShuffleUpdate.Value.AddMonths(1);
                }
            }

            return true;
        }

        public IEnumerable<BaseItem> FilterPlaylistItems(UserPlaylist userPlaylist, IEnumerable<BaseItem> items)
        {
            var playlistItems = userPlaylist.GetItems();
            var newItems = FilterItems(playlistItems, items, userPlaylist.User);
            newItems = RemoveMissingEpisodes(newItems);

            if (IsShuffleUpdateType) newItems = newItems.Shuffle();

            newItems = OrderItems(newItems);
            newItems = newItems.Take(Limit.MaxItems);

            return newItems;
        }

        private static IEnumerable<BaseItem> RemoveMissingEpisodes(IEnumerable<BaseItem> items)
        {
            return items.Where(x => !(x is Episode episode && episode.IsMissingEpisode));
        }

        private IEnumerable<BaseItem> OrderItems(IEnumerable<BaseItem> playlistItems)
        {
            return Limit.OrderBy.Order(playlistItems);
        }

        private IEnumerable<BaseItem> FilterItems(IEnumerable<BaseItem> playlistItems, IEnumerable<BaseItem> newItems,
            User user)
        {
            return playlistItems.Union(newItems, new BaseItemEqualByInternalId())
                .Where(x => IsMatchRules(x, user));
        }

        private bool IsMatchRules(BaseItem item, User user)
        {
            return Rules.All(x => x.IsMatch(new UserItem(user, item)));
        }


        public void UpdateLastShuffleTime()
        {
            var lastShuffleUpdate = LastShuffleUpdate.GetValueOrDefault(DateTimeOffset.UtcNow.Date);

            switch (UpdateType)
            {
                case UpdateType.ShuffleDaily:
                    LastShuffleUpdate = lastShuffleUpdate.AddDays(1);
                    break;
                case UpdateType.ShuffleWeekly:
                    LastShuffleUpdate = lastShuffleUpdate.AddDays(7);
                    break;
                case UpdateType.ShuffleMonthly:
                    LastShuffleUpdate = lastShuffleUpdate.AddMonths(1);
                    break;
            }
        }

        public SmartPlaylistDto ToDto()
        {
            return new SmartPlaylistDto
            {
                Id = _dto.Id,
                LastShuffleUpdate = LastShuffleUpdate,
                Limit = _dto.Limit,
                Name = _dto.Name,
                RulesTree = _dto.RulesTree,
                UpdateType = _dto.UpdateType,
                UserId = _dto.UserId
            };
        }
    }
}