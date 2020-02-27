using System.Linq;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Entities;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class ParentalRatingCriteriaDefinition : CriteriaDefinition
    {
        private static readonly ParentalRating[] ParentalRatings = BaseItem.LocalizationManager.GetParentalRatings();

        private static readonly ListValue[] ParentalRatingsListValues = ParentalRatings
            .Select(x => ListValue.Create(x.Name, x.Value)).OrderBy(x=>x.NumValue).ToArray();

        public override string Name => "Parental rating";
        public override CriteriaDefinitionType Type => new ComparableListValueDefinitionType(ParentalRatingsListValues.First());

        public override Value[] Values { get; } = ParentalRatingsListValues
            .Cast<Value>()
            .ToArray();

        public override Value GetValue(UserItem item)
        {
            var ratingListValue =
                ParentalRatingsListValues.FirstOrDefault(x =>
                    x.NumValue.Equals(item.Item.InheritedParentalRatingValue));

            if (ratingListValue != null) return ratingListValue;

            return Value.None;
        }
    }
}