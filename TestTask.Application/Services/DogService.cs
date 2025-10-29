using TestTask.Domain.Models;
using TestTask.Domain.Interfaces;


namespace TestTask.Application.Services
{
    public class DogService : IDogsService
    {
        private readonly IDogsRepository _dogRepository;
        public DogService(IDogsRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }
        public async Task<DogEntity> CreateDogAsync(DogEntity Dog) => await _dogRepository.CreateAsync(Dog);
        public async Task<List<DogEntity>> GetAllDogsAsync(
            string? attribute,
            string? order,
            int pageNumber,
            int pageSize) => await _dogRepository.GetAllAsync(attribute, order, pageNumber, pageSize);
    }
}
