using TestTask.Domain.Models;

namespace TestTask.Infrastructure.Interfaces
{
    public interface IDogsRepository
    {
        public Task<DogEntity> CreateAsync(DogEntity dog);
        public Task<List<DogEntity>> GetAllAsync(string? attribute, string? order);
    }
}
