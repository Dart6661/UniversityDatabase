using Core.Facade.Interfaces;

namespace Presentation.CommandHandlers
{
    public enum GroupCommand
    {
        create,
        get,
        get_all,
        get_students,
        get_size,
        update,
        delete
    }

    public class GroupHandler(IUniversity u)
    {
        private readonly IUniversity _u = u;

        public async Task Handle(CommandContext ctx)
        {
            switch ((GroupCommand)ctx.Command)
            {
                case GroupCommand.create: await Create(ctx); break;
                case GroupCommand.get: await Get(ctx); break;
                case GroupCommand.get_all: await GetAll(); break;
                case GroupCommand.get_students: await GetStudents(ctx); break;
                case GroupCommand.get_size: await GetGroupSize(ctx); break;
                case GroupCommand.update: await Update(ctx); break;
                case GroupCommand.delete: await Delete(ctx); break;
                default: throw new Exception("unknown group command");
            }
        }

        private async Task Create(CommandContext ctx)
        {
            DateTime? creationDate = null;
            if (ctx.Args.TryGetValue("creation-date", out var d))
                creationDate = DateTime.Parse(d).ToUniversalTime();

            await _u.CreateGroupAsync(
                ctx.Args["name"],
                creationDate,
                ctx.Args["curator-name"],
                ctx.Args["curator-email"]
            );

            Console.WriteLine("group created");
        }

        private async Task Get(CommandContext ctx)
        {
            var g = await _u.GetGroupAsync(int.Parse(ctx.Args["id"]));
            Console.WriteLine($"id: {g.Id}");
            Console.WriteLine($"name: {g.Name}");
            Console.WriteLine($"created: {g.CreationDate}");
            Console.WriteLine($"curator: {g.Curator.Name}");
        }

        private async Task GetAll()
        {
            foreach (var g in await _u.GetAllGroupsAsync())
                Console.WriteLine($"{g.Id}: {g.Name}");
        }

        private async Task GetStudents(CommandContext ctx)
        {
            foreach (var s in await _u.GetStudentsOfGroupAsync(int.Parse(ctx.Args["id"])))
                Console.WriteLine($"{s.Id}: {s.Name}, {s.Age}");
        }

        private async Task GetGroupSize(CommandContext ctx)
        {
            var size = await _u.GetGroupSizeAsync(int.Parse(ctx.Args["id"]));
            Console.WriteLine($"group size: {size}");
        }

        private async Task Update(CommandContext ctx)
        {
            var id = int.Parse(ctx.Args["id"]);
            ctx.Args.TryGetValue("name", out var name);

            DateTime? creationDate = null;
            if (ctx.Args.TryGetValue("creation-date", out var d))
                creationDate = DateTime.Parse(d);

            if (name == null && creationDate == null)
                throw new Exception("nothing to update");

            await _u.UpdateGroupAsync(id, name, creationDate);
            Console.WriteLine("group updated");
        }

        private async Task Delete(CommandContext ctx)
        {
            await _u.DeleteGroupAsync(int.Parse(ctx.Args["id"]));
            Console.WriteLine("group deleted");
        }
    }

}
