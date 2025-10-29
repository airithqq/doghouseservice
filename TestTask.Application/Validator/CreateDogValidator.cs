using FluentValidation;
using TestTask.Application.DTO;
using TestTask.Infrastructure.Persistence;

namespace TestTask.Application.Validator
{
    public class CreateDogValidator : AbstractValidator<CreateDogDTO>
    {
        private readonly DogDbContext _context;
        public CreateDogValidator(DogDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name of a dog is required")
                .Length(3, 50).WithMessage("Name should be between 3 and 50 characters")
                .Must(name =>
                {
                    return !_context.DogEntities.Any(d => string.Equals(d.Name, name, StringComparison.OrdinalIgnoreCase));
                })
                .WithMessage("A dog with this name already exists.");

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("Color of a dog is required")
                .Length(3, 50).WithMessage("Color should be between 3 and 50 characters");

            RuleFor(x => x.TailLength)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Enter a positive integer for dog`s tail length");

            RuleFor(x => x.Weight)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Enter a positive integer for dog`s weight");
        }
    }
}
