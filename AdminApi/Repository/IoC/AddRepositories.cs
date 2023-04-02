using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Repository.IoC
{
    public static class AddRepositories
    {
        public static IServiceCollection AddRepositoriesExtension(this IServiceCollection appService)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();

            types.Where(type => type.IsInterface).ToList()
                .ForEach(interfac => types.Where(type => type.GetInterfaces().Contains(interfac)).ToList()
                .ForEach(implementation => appService.AddScoped(interfac, implementation)));

            return appService;
        }
    }
}
