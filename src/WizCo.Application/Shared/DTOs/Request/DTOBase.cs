using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace WizCo.Application.Shared.DTOs.Request;

public abstract class DTOBase
{
    public bool IsValid { get; private set; }

    [JsonIgnore]
    public ValidationResult ValidationResult { get; private set; }

    public DTOBase()
    {
        ValidationResult = new ValidationResult();
    }

    public async Task ValidateAsync<T>(AbstractValidator<T> validator) where T : DTOBase
    {
        ValidationResult = await validator.ValidateAsync(this as T);
        IsValid = ValidationResult.IsValid;
    }
}
