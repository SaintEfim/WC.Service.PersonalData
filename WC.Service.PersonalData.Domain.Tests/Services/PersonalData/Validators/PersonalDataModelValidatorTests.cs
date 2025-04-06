using FluentValidation;
using FluentValidation.TestHelper;
using WC.Service.PersonalData.Domain.Models;
using WC.Service.PersonalData.Domain.Services.PersonalData.Validators;
using WC.Service.PersonalData.Shared.Models;

namespace WC.Service.PersonalData.Domain.Tests.Services.PersonalData.Validators;

public class PersonalDataModelValidatorTests
{
    private static async Task Check_Main_Data(
        Func<PersonalDataModel> newModelFunc,
        Action<TestValidationResult<PersonalDataModel>> checkResult)
    {
        var validator = new PersonalDataModelValidator();
        var context = new ValidationContext<PersonalDataModel>(newModelFunc());
        var result = await validator.TestValidateAsync(context);
        checkResult(result);
    }

    [Fact]
    public Task PersonalData_Positive_Model_Validator()
    {
        return Check_Main_Data(NewModelFunc,
            r => r.ShouldNotHaveAnyValidationErrors());

        PersonalDataModel NewModelFunc()
        {
            return PersonalData.PersonalDataModel();
        }
    }

    [Fact]
    public Task PersonalData_Negative_EmployeeId_Empty()
    {
        return Check_Main_Data(NewModelFunc,
            r => r.ShouldHaveAnyValidationError()
                .WithErrorCode("NotEmptyValidator")
                .When(x => x.PropertyName == nameof(PersonalDataModel.EmployeeId))
                .Only());

        PersonalDataModel NewModelFunc()
        {
            var data = PersonalData.PersonalDataModel();
            data.EmployeeId = Guid.Empty;
            return data;
        }
    }

    [Fact]
    public Task PersonalData_Negative_Email_Empty()
    {
        return Check_Main_Data(NewModelFunc,
            r => r.ShouldHaveAnyValidationError()
                .WithErrorCode("NotEmptyValidator")
                .When(x => x.PropertyName == "Email." + nameof(PersonalDataModel.Email))
                .Only());

        PersonalDataModel NewModelFunc()
        {
            var data = PersonalData.PersonalDataModel();
            data.Email = string.Empty;
            return data;
        }
    }

    [Fact]
    public Task PersonalData_Negative_Email_Invalid()
    {
        return Check_Main_Data(NewModelFunc,
            r => r.ShouldHaveAnyValidationError()
                .WithErrorCode("RegularExpressionValidator")
                .When(x => x.PropertyName == "Email." + nameof(PersonalDataModel.Email))
                .Only());

        PersonalDataModel NewModelFunc()
        {
            var data = PersonalData.PersonalDataModel();
            data.Email = "invalidEmail";
            return data;
        }
    }

    [Fact]
    public Task PersonalData_Negative_Password_Empty()
    {
        return Check_Main_Data(NewModelFunc,
            r => r.ShouldHaveAnyValidationError()
                .WithErrorCode("NotEmptyValidator")
                .When(x => x.PropertyName == "Password." + nameof(PersonalDataModel.Password))
                .Only());

        PersonalDataModel NewModelFunc()
        {
            var data = PersonalData.PersonalDataModel();
            data.Password = string.Empty;
            return data;
        }
    }

    [Fact]
    public Task PersonalData_Negative_Password_Invalid()
    {
        return Check_Main_Data(NewModelFunc,
            r => r.ShouldHaveAnyValidationError()
                .WithErrorCode("RegularExpressionValidator")
                .When(x => x.PropertyName == "Password." + nameof(PersonalDataModel.Password))
                .Only());

        PersonalDataModel NewModelFunc()
        {
            var data = PersonalData.PersonalDataModel();
            data.Password = "short";
            return data;
        }
    }

    [Fact]
    public Task PersonalData_Negative_Password_NoSpecialSymbol()
    {
        return Check_Main_Data(NewModelFunc,
            r => r.ShouldHaveAnyValidationError()
                .WithErrorCode("RegularExpressionValidator")
                .When(x => x.PropertyName == "Password." + nameof(PersonalDataModel.Password))
                .Only());

        PersonalDataModel NewModelFunc()
        {
            var data = PersonalData.PersonalDataModel();
            data.Password = "Abcdef12";
            return data;
        }
    }

    [Fact]
    public Task PersonalData_Negative_Password_NoDigit()
    {
        return Check_Main_Data(NewModelFunc,
            r => r.ShouldHaveAnyValidationError()
                .WithErrorCode("RegularExpressionValidator")
                .When(x => x.PropertyName == "Password." + nameof(PersonalDataModel.Password))
                .Only());

        PersonalDataModel NewModelFunc()
        {
            var data = PersonalData.PersonalDataModel();
            data.Password = "Abcdefg!";
            return data;
        }
    }

    [Fact]
    public Task PersonalData_Negative_Password_NoUpperCase()
    {
        return Check_Main_Data(NewModelFunc,
            r => r.ShouldHaveAnyValidationError()
                .WithErrorCode("RegularExpressionValidator")
                .When(x => x.PropertyName == "Password." + nameof(PersonalDataModel.Password))
                .Only());

        PersonalDataModel NewModelFunc()
        {
            var data = PersonalData.PersonalDataModel();
            data.Password = "abcdef1!";
            return data;
        }
    }

    [Fact]
    public Task PersonalData_Negative_Password_NoLowerCase()
    {
        return Check_Main_Data(NewModelFunc,
            r => r.ShouldHaveAnyValidationError()
                .WithErrorCode("RegularExpressionValidator")
                .When(x => x.PropertyName == "Password." + nameof(PersonalDataModel.Password))
                .Only());

        PersonalDataModel NewModelFunc()
        {
            var data = PersonalData.PersonalDataModel();
            data.Password = "ABCDEF1!";
            return data;
        }
    }

    [Fact]
    public Task PersonalData_Negative_Role_Invalid()
    {
        return Check_Main_Data(NewModelFunc,
            r => r.ShouldHaveAnyValidationError()
                .WithErrorCode("EnumValidator")
                .When(x => x.PropertyName == nameof(PersonalDataModel.Role))
                .Only());

        PersonalDataModel NewModelFunc()
        {
            var data = PersonalData.PersonalDataModel();
            data.Role = (UserRole) 999;
            return data;
        }
    }
}
