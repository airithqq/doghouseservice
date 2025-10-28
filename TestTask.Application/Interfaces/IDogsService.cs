using TestTask.Application.DTO;
using TestTask.Domain.Models;

namespace TestTask.Application.Interfaces
{
    public interface IDogsService
    {
        public Task<ResponseDogDTO> CreateDogAsync(CreateDogDTO dto);
        public Task<List<ResponseDogDTO>> GetAllDogsAsync(string? attribute, string? order);
    }
}
