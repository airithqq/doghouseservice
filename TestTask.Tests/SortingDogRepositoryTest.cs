using Microsoft.EntityFrameworkCore;
using TestTask.Domain.Models;
using TestTask.Infrastructure.Persistence;
using TestTask.Infrastructure.Repositories;
using Xunit;

namespace TestTask.Tests.SortingDogRepositoryTest
{
    public class SortingDogRepositoryTest
    {
        private DogDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<DogDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DogDbContext(options);
            context.DogEntities.AddRange(new[]
            {
            new DogEntity { Id = 1, Name = "Alpha", TailLength = 5, Weight = 20 },
            new DogEntity { Id = 2, Name = "Bravo", TailLength = 10, Weight = 15 },
            new DogEntity { Id = 3, Name = "Charlie", TailLength = 7, Weight = 25 },
        });
            context.SaveChanges();
            return context;
        }

        [Theory]
        [InlineData("taillength", "asc", new[] { 1, 3, 2 })]
        [InlineData("taillength", "desc", new[] { 2, 3, 1 })]
        [InlineData("weight", "asc", new[] { 2, 1, 3 })]
        [InlineData("weight", "desc", new[] { 3, 1, 2 })]
        public async Task GetAllAsync_Should_Sort_Correctly(string attribute, string order, int[] expectedIds)
        {
            using var context = CreateContext();
            var repo = new DogsRepository(context);

            var result = await repo.GetAllAsync(attribute, order, pageNumber: 1, pageSize: 10);

            Assert.Equal(expectedIds.Length, result.Count);
            for (int i = 0; i < expectedIds.Length; i++)
            {
                Assert.Equal(expectedIds[i], result[i].Id);
            }
        }
    }
}