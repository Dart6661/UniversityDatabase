using Core.Facade.Interfaces;

namespace Presentation.CommandHandlers
{
    public enum CuratorCommand
    {
        create,
        get, 
        get_all,
        get_students_average_age,
        update,
        delete
    }

    public class CuratorHandler(IUniversity u)
    {
        private readonly IUniversity _u = u;

        public async Task Handle(CommandContext ctx)
        {
            switch ((CuratorCommand)ctx.Command)
            {
                case CuratorCommand.create: await Create(ctx); break;
                case CuratorCommand.get: await Get(ctx); break;
                case CuratorCommand.get_all: await GetAll(); break;
                case CuratorCommand.get_students_average_age: await GetStudentsAverageAge(ctx); break;
                case CuratorCommand.update: await Update(ctx); break;
                case CuratorCommand.delete: await Delete(ctx); break;
                default: throw new Exception("unknown curator command");
            }
        }

        private async Task Create(CommandContext ctx)
        {
            DateTime? creationDate = null;
            if (ctx.Args.TryGetValue("group-creation-date", out var d))
                creationDate = DateTime.Parse(d);

            await _u.CreateCuratorAsync(
                ctx.Args["name"],
                ctx.Args["email"],
                ctx.Args["group-name"],
                creationDate
            );

            Console.WriteLine("curator created");
        }

        private async Task Get(CommandContext ctx)
        {
            var c = await _u.GetCuratorAsync(int.Parse(ctx.Args["id"]));
            Console.WriteLine($"id: {c.Id}");
            Console.WriteLine($"name: {c.Name}");
            Console.WriteLine($"email: {c.Email}");
            Console.WriteLine($"group id: {c.GroupId}");
        }

        private async Task GetAll()
        {
            foreach (var c in await _u.GetAllCuratorsAsync())
                Console.WriteLine($"{c.Id}: {c.Name} ({c.Email})");
        }

        private async Task GetStudentsAverageAge(CommandContext ctx)
        {
            var avg = await _u.GetStudentsAverageAgeAsync(int.Parse(ctx.Args["id"]));
            Console.WriteLine($"average students age: {avg}");
        }

        private async Task Update(CommandContext ctx)
        {
            var id = int.Parse(ctx.Args["id"]);
            ctx.Args.TryGetValue("name", out var name);
            ctx.Args.TryGetValue("email", out var email);

            if (name == null && email == null)
                throw new Exception("nothing to update");

            await _u.UpdateCuratorAsync(id, name, email);
            Console.WriteLine("curator updated");
        }

        private async Task Delete(CommandContext ctx)
        {
            await _u.DeleteCuratorAsync(int.Parse(ctx.Args["id"]));
            Console.WriteLine("curator deleted");
        }
    }

}
