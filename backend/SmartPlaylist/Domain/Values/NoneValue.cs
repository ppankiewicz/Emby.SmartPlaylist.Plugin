namespace SmartPlaylist.Domain.Values
{
    public sealed class NoneValue : Value
    {
        public override string Kind { get; } = nameof(NoneValue);
    }
}