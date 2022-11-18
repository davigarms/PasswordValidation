namespace PasswordValidation;

public class PasswordValidator
{
    private readonly IValidation _validation;

    public PasswordValidator(IValidationFactory validationFactory)
    {
        _validation = validationFactory.Create();
    }

    public bool IsValid(string password) => _validation.Validate(password);
}