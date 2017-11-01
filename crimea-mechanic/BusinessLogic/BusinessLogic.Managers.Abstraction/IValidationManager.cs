using BusinessLogic.Objects.User;
using Common.Validation;

namespace BusinessLogic.Managers.Abstraction
{
    public interface IValidationManager
    {
        ValidationResult ValidateRegistrationDto(RegistrationDto dto);
    }
}
