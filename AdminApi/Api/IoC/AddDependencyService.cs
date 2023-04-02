using Application.IoC;
using Repository.IoC;

namespace AdminApi.IoC
{
    public static class AddDependencyService
    {
        public static IServiceCollection AddDependencyServiceExtension(this IServiceCollection services)
        {
            services.AddAppServiceExtension();
            services.AddRepositoriesExtension();
            return services;
        }
    }
}
