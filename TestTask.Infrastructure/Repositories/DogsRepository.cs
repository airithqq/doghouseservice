using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Models;
using TestTask.Infrastructure.Data;
using TestTask.Infrastructure.Interfaces;

namespace TestTask.Infrastructure.Repositories
{
     public class DogsRepository : IDogsRepository
    {
        private readonly TestTaskDbContext _context;
        public DogsRepository(TestTaskDbContext context)
        {
            _context= context;
        }

        public async Task<DogEntity> CreateAsync(DogEntity dog)
        {
            _context.DogEntities.Add(dog);
            await _context.SaveChangesAsync();
            return dog;
        }

        public async Task<List<DogEntity>> GetAllAsync(string? attribute, string? order)
        {
            var query = _context.DogEntities.AsQueryable();
            var sortMap = new Dictionary<string, Func<IQueryable<DogEntity>, IQueryable<DogEntity>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["taillength_asc"] = t => t.OrderBy(x => x.TailLength),
                ["taillength_desc"] = t => t.OrderByDescending(x => x.TailLength),
                ["weight_asc"] = w => w.OrderBy(x => x.Weight),
                ["weight_desc"] = w => w.OrderByDescending(x => x.Weight),
            };
            var key = $"{attribute}_{order}";
            query = sortMap.TryGetValue(key, out var sorter) ? sorter(query) : query.OrderBy(x => x.Id);
            return await query.ToListAsync();
        }

    }
}
