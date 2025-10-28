using TestTask.Application.Interfaces;
using TestTask.Domain.Models;
using TestTask.Infrastructure.Interfaces;
using TestTask.Application.DTO;
using AutoMapper;

namespace TestTask.Application.Services
{
    public class DogService : IDogsService
    {
        private readonly IDogsRepository _dogRepository;
        private readonly IMapper _mapper;
        public DogService(IDogsRepository dogRepository, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDogDTO> CreateDogAsync(CreateDogDTO dto)
        {
            var dog = _mapper.Map<DogEntity>(dto);
            var createdDog = await _dogRepository.CreateAsync(dog);
            return _mapper.Map<ResponseDogDTO>(createdDog);
        }

        public async Task<List<ResponseDogDTO>> GetAllDogsAsync(string? attribute, string? order)
        {
            var dogs = await _dogRepository.GetAllAsync(attribute, order);
            var response = _mapper.Map<List<ResponseDogDTO>>(dogs);
            return response;    
        }
        


    }
}
