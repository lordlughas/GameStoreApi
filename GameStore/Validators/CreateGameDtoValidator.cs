using FluentValidation;
using GameStore.Dtos;

namespace GameStore.Validator;

public class CreateGameDtoValidator : AbstractValidator<CreateGameDto>
{
    public CreateGameDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Genre).NotEmpty();
    }
}
