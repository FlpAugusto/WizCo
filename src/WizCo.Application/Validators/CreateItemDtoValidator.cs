using FluentValidation;
using WizCo.Application.Shared.DTOs.Request;

namespace WizCo.Application.Validators
{
    public class CreateItemDtoValidator : AbstractValidator<CreateItemDto>
    {
        public CreateItemDtoValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty()
                    .WithMessage("O nome do produto é obrigatório.")
                .MaximumLength(200)
                .   WithMessage("O nome do produto não pode ter mais de 200 caracteres.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                    .WithMessage("A quantidade deve ser maior que zero.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                    .WithMessage("O preço unitário deve ser maior que zero.");
        }
    }
}
