using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Models;

namespace TestTask.Infrastructure.Data
{
    public class TestTaskDbContext : DbContext
    {
        public TestTaskDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DogEntity> DogEntities { get; set; }
    }
}
