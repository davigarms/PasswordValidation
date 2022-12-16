namespace PasswordValidation;

public interface IValidation
{
    void Validate(string password);
    List<string> GetErrors();
}

public class Validation : IValidation
{
    private readonly RuleValidator _ruleValidator;

    public Validation(RuleValidator ruleValidator)
    {
        _ruleValidator = ruleValidator;
    }

    public void Validate(string password) => _ruleValidator.Validate(password);

    public List<string> GetErrors() => _ruleValidator.GetErrors();
}