using Microsoft.EntityFrameworkCore;
using TestTask.API;
using TestTask.API.Filters;
using TestTask.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TestTaskDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TestTaskConnectionString")));
builder.Services.AddAPI(builder.Configuration);
builder.Services.AddSwaggerGen(a =>
a.ParameterFilter<DogAttributeFilter>());
builder.Services.AddSwaggerGen(o =>
o.ParameterFilter<DogOrderFilter>());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
