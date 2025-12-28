using Core.Facade.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    internal class Program
    {
        static async Task Main()
        {
            Console.WriteLine("start");
            ServiceProvider serviceProvider = DependencyInjectionConfig.ConfigureServices();

            while (true)
            {
                try
                {
                    string? input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input)) break;

                    using var scope = serviceProvider.CreateScope();
                    var university = scope.ServiceProvider.GetRequiredService<IUniversity>();
                    var commandDispatcher = new CommandDispatcher(university);
                    await commandDispatcher.ExecuteAsync(input);
                    Console.WriteLine();
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }

            }

            Console.WriteLine("end");
        }
    }
}