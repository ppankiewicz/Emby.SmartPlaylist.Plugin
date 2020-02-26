using SmartPlaylist.Domain.Operator;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition
{
    public abstract class CriteriaDefinitionType
    {
        public abstract string Name { get; }
        public abstract Operator.Operator[] Operators { get; }
    }

    public class StringDefinitionType : CriteriaDefinitionType
    {
        public static readonly StringDefinitionType Instance = new StringDefinitionType();

        public override string Name => StringValue.Default.Kind;

        public override Operator.Operator[] Operators => DefinedOperators.StringOperators;
    }

    public class NumberDefinitionType : CriteriaDefinitionType
    {
        public static readonly NumberDefinitionType Instance = new NumberDefinitionType();
        public override string Name => NumberValue.Default.Kind;

        public override Operator.Operator[] Operators => DefinedOperators.NumberOperators;
    }


    public class DateDefinitionType : CriteriaDefinitionType
    {
        public static readonly DateDefinitionType Instance = new DateDefinitionType();

        public override string Name => DateValue.Default.Kind;

        public override Operator.Operator[] Operators => DefinedOperators.DateOperators;
    }

    public class ListValueDefinitionType : CriteriaDefinitionType
    {
        public static readonly CriteriaDefinitionType Instance = new ListValueDefinitionType();
        public override string Name => ListValue.Default.Kind;

        public override Operator.Operator[] Operators => DefinedOperators.ListValueOperators;
    }

    public class BoolValueDefinitionType : CriteriaDefinitionType
    {
        public static readonly CriteriaDefinitionType Instance = new BoolValueDefinitionType();
        public override string Name => BoolValue.Default.Kind;

        public override Operator.Operator[] Operators => DefinedOperators.BoolValueOperators;
    }
}