namespace PasswordValidation;

public interface IValidationRules
{
    public bool IsValid(string password);
}

public class ValidationRuleOne : IValidationRules
{
    public bool IsValid(string password)
    {
        return password.Length > 8 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(char.IsNumber) &&
               password.Any(x => x.Equals('_'));
    }
}

public class ValidationRuleTwo : IValidationRules
{
    public bool IsValid(string password)
    {
        return password.Length > 6 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(char.IsNumber);
    }
}

public class ValidationRuleThree : IValidationRules
{
    public bool IsValid(string password)
    {
        return password.Length > 16 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(x => x.Equals('_'));
    }
}