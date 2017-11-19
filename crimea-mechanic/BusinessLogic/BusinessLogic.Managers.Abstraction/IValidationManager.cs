using BusinessLogic.Objects.Application;
using BusinessLogic.Objects.Car;
using BusinessLogic.Objects.User;
using Common.Validation;

namespace BusinessLogic.Managers.Abstraction
{
    public interface IValidationManager
    {
        ValidationResult ValidateRegistrationUserDto(RegistrationUserDto dto);
        ValidationResult ValidateRegistrationCarServiceDto(RegistrationCarServiceDto dto);
        ValidationResult ValidateUserCarDto(AddOrEditUserCarDto dto);
        ValidationResult ValidateCreateApplicationDto(CreateApplicationDto dto);
        ValidationResult ValidateEditApplicationDto(EditApplicationDto dto);
        ValidationResult ValidateAddOfferDto(AddOfferDto dto);
    }
}
