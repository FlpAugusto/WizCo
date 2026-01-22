using FluentValidation;
using WizCo.Application.Shared.DTOs.Request;

namespace WizCo.Application.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.ClientName)
                .NotEmpty()
                    .WithMessage("O nome do cliente é obrigatório.")
                .MaximumLength(200)
                    .WithMessage("O nome do cliente não pode ter mais de 200 caracteres.");

            RuleFor(x => x.Itens)
                .NotEmpty()
                    .WithMessage("O pedido deve conter ao menos um item.");

            RuleForEach(x => x.Itens)
                .SetValidator(new CreateItemDtoValidator());
        }
    }
}
