using TestTask.Application.Interfaces;
using TestTask.Application.Services;
using TestTask.Infrastructure.Interfaces;
using TestTask.Infrastructure.Repositories;
using TestTask.Application.Mapping;


namespace TestTask.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPI(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDogsRepository, DogsRepository>();
            services.AddScoped<IDogsService, DogService>();
            services.AddAutoMapper(typeof(DogsMapping).Assembly);

            return services;
        }
    }
}
