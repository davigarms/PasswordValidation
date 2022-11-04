namespace PasswordValidation;

public class RuleValidatorBuilder
{
    private readonly RuleValidator _ruleValidator;

    public RuleValidatorBuilder()
    {
        _ruleValidator = new RuleValidator();
    }

    public RuleValidatorBuilder With(IRule rule)
    {
        _ruleValidator.Add(rule);
        return this;
    }

    public RuleValidator Build() => _ruleValidator;
}