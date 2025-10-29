using AutoMapper;
using TestTask.Application.DTO;
using TestTask.Domain.Models;

namespace TestTask.Application.Mapping
{
    public class DogsMapping : Profile
    {
        public DogsMapping()
        {
            CreateMap<DogEntity, ResponseDogDTO>();
            CreateMap<CreateDogDTO, DogEntity>();
            CreateMap<CreateDogDTO, DogEntity>()
            .ConstructUsing(dto => new DogEntity(dto.Name, dto.Color, dto.TailLength,dto.Weight));
        }
    }
}
