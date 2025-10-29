using AutoMapper;
using Xunit;
using TestTask.Application.Mapping;
using TestTask.Application.DTO;
using TestTask.Domain.Models;

namespace TestTask.Tests.Mapping
{
    public class DogsMappingTests
    {
        private readonly IMapper _mapper;

        public DogsMappingTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DogsMapping>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_CreateDogDTO_To_DogEntity_Correctly()
        {
            var dto = new CreateDogDTO
            {
                Name = "Buddy",
                Color = "Brown",
                TailLength = 15,
                Weight = 25
            };
            var entity = _mapper.Map<DogEntity>(dto);

            Assert.Equal(dto.Name, entity.Name);
            Assert.Equal(dto.Color, entity.Color);
            Assert.Equal(dto.TailLength, entity.TailLength);
            Assert.Equal(dto.Weight, entity.Weight);
        }

        [Fact]
        public void Should_Map_DogEntity_To_ResponseDogDTO_Correctly()
        {
            var entity = new DogEntity("Charlie", "Black", 12, 20);
            var dto = _mapper.Map<ResponseDogDTO>(entity);

            Assert.Equal(entity.Name, dto.Name);
            Assert.Equal(entity.Color, dto.Color);
            Assert.Equal(entity.TailLength, dto.TailLength);
            Assert.Equal(entity.Weight, dto.Weight);
        }
    }
}
