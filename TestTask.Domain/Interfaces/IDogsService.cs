using TestTask.Domain.Models;

namespace TestTask.Domain.Interfaces
{
    public interface IDogsService
    {
        public Task<DogEntity> CreateDogAsync(DogEntity Dog);
        public Task<List<DogEntity>> GetAllDogsAsync(string? attribute, string? order, int pageNumber, int pageSize);
    }
}
