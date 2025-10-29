using TestTask.Domain.Interfaces;
using TestTask.Application.Services;
using TestTask.Infrastructure.Repositories;
using TestTask.Application.Mapping;
using TestTask.Application.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace TestTask.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPI(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDogsRepository, DogsRepository>();
            services.AddScoped<IDogsService, DogService>();
            services.AddAutoMapper(typeof(DogsMapping).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateDogValidator>();
            return services;
        }
    }
}
