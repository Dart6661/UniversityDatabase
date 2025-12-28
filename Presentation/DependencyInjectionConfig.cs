using Microsoft.Extensions.DependencyInjection;
using Core.Facade;
using Core.Facade.Interfaces;
using Core.Repositories.Interfaces;
using Core.Services;
using Core.Services.Interfaces;
using Core.UnitOfWork.Interfaces;
using DatabaseContext.Context;
using DatabaseContext.Repositories;
using DatabaseContext.UnitOfWork;

namespace Presentation
{
    public static class DependencyInjectionConfig
    {
        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddScoped(_ => new UnivContext(DbContextOptionsFactory.Create<UnivContext>()));

            services.AddScoped<IUnitOfWork, DbUnitOfWork>();

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICuratorRepository, CuratorRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICuratorService, CuratorService>();
            services.AddScoped<IGroupService, GroupService>();

            services.AddScoped<IUniversity, University>();

            return services.BuildServiceProvider();
        }
    }
}
