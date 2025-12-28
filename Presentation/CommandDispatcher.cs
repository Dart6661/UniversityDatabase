using Core.Facade.Interfaces;
using Presentation.CommandHandlers;

namespace Presentation
{
    public enum EntityType
    {
        Student,
        Curator,
        Group
    }

    public class CommandDispatcher(IUniversity university)
    {
        private readonly StudentHandler _students = new(university);
        private readonly GroupHandler _groups = new(university);
        private readonly CuratorHandler _curators = new(university);

        public async Task ExecuteAsync(string input)
        {
            try
            {
                var ctx = Parse(input);
                switch (ctx.Entity)
                {
                    case EntityType.Student:
                        await _students.Handle(ctx);
                        break;
                    case EntityType.Group:
                        await _groups.Handle(ctx);
                        break;
                    case EntityType.Curator:
                        await _curators.Handle(ctx);
                        break;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private static CommandContext Parse(string input)
        {
            var splitInput = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (splitInput.Length < 3 || splitInput[0] != "univ")
                throw new Exception("format: univ <entity> <command> [--arg value]");

            var entity = splitInput[1].ToLower() switch
            {
                "student" => EntityType.Student,
                "group" => EntityType.Group,
                "curator" => EntityType.Curator,
                _ => throw new Exception("unknown entity")
            };

            Enum command = entity switch
            {
                EntityType.Student => ParseEnum<StudentCommand>(splitInput[2]),
                EntityType.Group => ParseEnum<GroupCommand>(splitInput[2]),
                EntityType.Curator => ParseEnum<CuratorCommand>(splitInput[2]),
                _ => throw new Exception("invalid entity")
            };

            var args = new Dictionary<string, string>();
            for (int i = 3; i < splitInput.Length; i += 2)
            {
                if (!splitInput[i].StartsWith("--") || i + 1 >= splitInput.Length) throw new Exception("invalid arguments");
                args[splitInput[i][2..]] = splitInput[i + 1];
            }

            return new CommandContext
            {
                Entity = entity,
                Command = command,
                Args = args
            };
        }

        private static T ParseEnum<T>(string value) where T : struct
        {
            if (!Enum.TryParse<T>(value, true, out var result))
                throw new Exception($"unknown command '{value}'");
            return result;
        }
    }

}
