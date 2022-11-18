namespace PasswordValidation;

public interface IValidation
{
    bool Validate(string password);
}

public class Validation : IValidation
{
    private readonly RuleValidator _ruleValidator;

    public Validation(RuleValidator ruleValidator)
    {
        _ruleValidator = ruleValidator;
    }

    public bool Validate(string password) => _ruleValidator.Validate(password);
}