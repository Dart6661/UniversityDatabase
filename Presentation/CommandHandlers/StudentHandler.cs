using Core.Facade.Interfaces;

namespace Presentation.CommandHandlers
{
    public enum StudentCommand
    {
        create,
        get,
        get_all,
        get_curator_name,
        update, 
        delete
    }

    public class StudentHandler(IUniversity u)
    {
        private readonly IUniversity _u = u;

        public async Task Handle(CommandContext ctx)
        {
            switch ((StudentCommand)ctx.Command)
            {
                case StudentCommand.create: await Create(ctx); break;
                case StudentCommand.get: await Get(ctx); break;
                case StudentCommand.get_all: await GetAll(); break;
                case StudentCommand.get_curator_name: await GetCuratorName(ctx); break;
                case StudentCommand.update: await Update(ctx); break;
                case StudentCommand.delete: await Delete(ctx); break;
                default: throw new Exception("unknown student command");
            }
        }

        private async Task Create(CommandContext ctx)
        {
            await _u.CreateStudentAsync(
                ctx.Args["name"],
                int.Parse(ctx.Args["age"]),
                int.Parse(ctx.Args["group-id"])
            );
            Console.WriteLine("student created");
        }

        private async Task Get(CommandContext ctx)
        {
            var s = await _u.GetStudentAsync(int.Parse(ctx.Args["id"]));
            Console.WriteLine($"id: {s.Id}");
            Console.WriteLine($"name: {s.Name}");
            Console.WriteLine($"age: {s.Age}");
            Console.WriteLine($"group id: {s.GroupId}");
        }

        private async Task GetAll()
        {
            foreach (var s in await _u.GetAllStudentsAsync())
                Console.WriteLine($"{s.Id}: {s.Name}, {s.Age}, group {s.GroupId}");
        }

        private async Task GetCuratorName(CommandContext ctx)
        {
            var name = await _u.GetCuratorNameAsync(int.Parse(ctx.Args["id"]));
            Console.WriteLine($"curator name: {name}");
        }

        private async Task Update(CommandContext ctx)
        {
            var id = int.Parse(ctx.Args["id"]);
            ctx.Args.TryGetValue("name", out var name);
            ctx.Args.TryGetValue("age", out var ageStr);
            ctx.Args.TryGetValue("group-id", out var groupStr);

            int? age = ageStr != null ? int.Parse(ageStr) : null;
            int? groupId = groupStr != null ? int.Parse(groupStr) : null;

            if (name == null && age == null && groupId == null) throw new Exception("nothing to update");

            await _u.UpdateStudentAsync(id, name, age, groupId);
            Console.WriteLine("student updated");
        }

        private async Task Delete(CommandContext ctx)
        {
            await _u.DeleteStudentAsync(int.Parse(ctx.Args["id"]));
            Console.WriteLine("student deleted");
        }
    }
}
