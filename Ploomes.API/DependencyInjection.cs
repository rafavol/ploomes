using Ploomes.API.Infra.UnitOfWork;
using Ploomes.API.Mapper;
using Ploomes.API.Services.Authentication;
using Ploomes.API.Services.Authentication.Interfaces;

namespace Ploomes.API
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserMapper, UserMapper>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
