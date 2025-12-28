namespace Presentation
{
    public class CommandContext
    {
        public EntityType Entity { get; init; }
        public Enum Command { get; init; } = null!;
        public Dictionary<string, string> Args { get; init; } = [];
    }


}
