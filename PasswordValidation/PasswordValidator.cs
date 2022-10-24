namespace PasswordValidation;

public class PasswordValidator
{
    private readonly IValidationRules _validationRule;

    public PasswordValidator(IValidationRules validationRule)
    {
        _validationRule = validationRule;
    }

    public bool ValidatePassword(string password)
    {
        return _validationRule.IsValid(password);
    }
}