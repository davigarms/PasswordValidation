namespace PasswordValidation;

public class PasswordValidator
{
    private readonly IValidationFactory _validationFactory;

    public PasswordValidator(IValidationFactory validationFactory)
    {
        _validationFactory = validationFactory;
    }

    public bool IsValid(string password) => _validationFactory.Create().IsValid(password);
}