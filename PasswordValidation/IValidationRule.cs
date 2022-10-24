namespace PasswordValidation;

public interface IValidationRule
{
    public bool IsValid(string password);
}

public class ValidationRuleOne : IValidationRule
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

public class ValidationRuleTwo : IValidationRule
{
    public bool IsValid(string password)
    {
        return password.Length > 6 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(char.IsNumber);
    }
}

public class ValidationRuleThree : IValidationRule
{
    public bool IsValid(string password)
    {
        return password.Length > 16 &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               password.Any(x => x.Equals('_'));
    }
}