namespace PasswordValidation;

public interface IValidation
{
    bool Validate(string password);
}

public class ValidationOne : IValidation
{
    public bool Validate(string password) =>
        password.Length > 8 &&
        password.Any(char.IsUpper) &&
        password.Any(char.IsLower) &&
        password.Any(char.IsNumber) &&
        password.Any(x => x.Equals('_'));
}

public class ValidationTwo : IValidation
{
    public bool Validate(string password) =>
        password.Length > 6 &&
        password.Any(char.IsUpper) &&
        password.Any(char.IsLower) &&
        password.Any(char.IsNumber);
}

public class ValidationThree : IValidation
{
    public bool Validate(string password) =>
        password.Length > 16 &&
        password.Any(char.IsUpper) &&
        password.Any(char.IsLower) &&
        password.Any(x => x.Equals('_'));
}

public class ValidationFour : IValidation
{
    private readonly RuleValidator _ruleValidator;
    
    public ValidationFour()
    {
        _ruleValidator = new RuleValidatorBuilder()
            .With(new LenghtValidationRule(8))
            .With(new UpperCaseRule())
            .Build();
    }
    
    public bool Validate(string password) => _ruleValidator.Validate(password);
}