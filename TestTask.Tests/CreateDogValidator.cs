using Xunit;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Validator;
using TestTask.Application.DTO;
using TestTask.Infrastructure.Persistence;
using TestTask.Domain.Models;

namespace TestTask.Tests.Validator
{
    public class CreateDogValidatorTests
    {
        private readonly DogDbContext _context;
        private readonly CreateDogValidator _validator;

        public CreateDogValidatorTests()
        {
            var options = new DbContextOptionsBuilder<DogDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            _context = new DogDbContext(options);

            _context.DogEntities.Add(new DogEntity("Buddy", "Brown", 10, 20));
            _context.SaveChanges();

            _validator = new CreateDogValidator(_context);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new CreateDogDTO { Name = "", Color = "White", TailLength = 5, Weight = 10 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Too_Short()
        {
            var model = new CreateDogDTO { Name = "Bo", Color = "Black", TailLength = 5, Weight = 10 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Not_Unique()
        {
            var model = new CreateDogDTO { Name = "Buddy", Color = "White", TailLength = 5, Weight = 10 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorMessage("A dog with this name already exists.");
        }

        [Fact]
        public void Should_Have_Error_When_Color_Is_Empty()
        {
            var model = new CreateDogDTO { Name = "Charlie", Color = "", TailLength = 5, Weight = 10 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Color);
        }

        [Fact]
        public void Should_Have_Error_When_TailLength_Is_NonPositive()
        {
            var model = new CreateDogDTO { Name = "Rocky", Color = "Brown", TailLength = 0, Weight = 10 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.TailLength);
        }

        [Fact]
        public void Should_Have_Error_When_Weight_Is_NonPositive()
        {
            var model = new CreateDogDTO { Name = "Bella", Color = "Black", TailLength = 5, Weight = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Weight);
        }

        [Fact]
        public void Should_Not_Have_Errors_For_Valid_Model()
        {
            var model = new CreateDogDTO { Name = "Lucky", Color = "White", TailLength = 7, Weight = 15 };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
