using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Models;

namespace TestTask.Infrastructure.Persistence
{
    public class DogDbContext : DbContext
    {
        public DogDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<DogEntity> DogEntities { get; set; }
    }
}
