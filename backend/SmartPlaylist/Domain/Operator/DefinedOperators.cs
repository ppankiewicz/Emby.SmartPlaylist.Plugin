using System.Linq;
using SmartPlaylist.Domain.Values;
using SmartPlaylist.Extensions;

namespace SmartPlaylist.Domain.Operator
{
    public static class DefinedOperators
    {
        public static Operator[] All = typeof(Operator).Assembly.FindAndCreateDerivedTypes<Operator>().ToArray();

        public static Operator[] StringOperators =
            All.Where(x => x.Type == StringValue.Default.Kind).ToArray();

        public static Operator[] NumberOperators =
            All.Where(x => x.Type == NumberValue.Default.Kind || x.Type == NumberRangeValue.Default.Kind).ToArray();

        public static Operator[] DateOperators = All.Where(x =>x.Type == DateRangeValue.Default.Kind || x.Type == DateValue.Default.Kind || x.Type == LastPeriodValue.Default.Kind).ToArray();
        public static Operator[] ListValueOperators = All.Where(x => x.Type == ListValue.Default.Kind).ToArray();
        public static Operator[] BoolValueOperators = All.Where(x => x.Type == BoolValue.Default.Kind).ToArray();
    }
}