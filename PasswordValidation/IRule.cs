namespace PasswordValidation;

public interface IRule
{
    public bool Validate(string password);
}

public class UpperCaseRule : IRule
{
    public bool Validate(string password) => password.Any(char.IsUpper);
}

public class LenghtValidationRule : IRule
{
    private readonly int _length;

    public LenghtValidationRule(int length) => _length = length;

    public bool Validate(string password) => password.Length > _length;
}