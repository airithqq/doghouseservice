using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
using TestTask.API;
using TestTask.API.Filters;
using TestTask.Infrastructure.Persistence;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DogDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("TestTaskConnectionString"),
        b => b.MigrationsAssembly("TestTask.Infrastructure")));

        builder.Services.AddAPI(builder.Configuration);
        builder.Services.AddSwaggerGen(a =>
        a.ParameterFilter<DogAttributeFilter>());
        builder.Services.AddSwaggerGen(o =>
        o.ParameterFilter<DogOrderFilter>());

        builder.Services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("default", opt =>
            {
                opt.PermitLimit = 10;
                opt.Window = TimeSpan.FromSeconds(1);
                opt.QueueLimit = 0;
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });

            options.OnRejected = (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                return new ValueTask();
            };
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseRateLimiter();
        app.MapControllers().RequireRateLimiting("default");
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.Run();
    }
}
