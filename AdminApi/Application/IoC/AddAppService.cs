using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.IoC
{
    public static class AddAppService
    {
        public static IServiceCollection AddAppServiceExtension(this IServiceCollection appService)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();

            types.Where(type => type.IsInterface).ToList()
                .ForEach(interfac => types.Where(type => type.GetInterfaces().Contains(interfac)).ToList()
                .ForEach(implementation => appService.AddScoped(interfac, implementation)));

            return appService;
        }
    }
}
