namespace PasswordValidation;

public interface IRule
{
    public bool Validate(string password);
}

public class UpperCaseRule : IRule
{
    public bool Validate(string password) => password.Any(char.IsUpper);
}

public class LenghtRule : IRule
{
    private readonly int _length;

    public LenghtRule(int length)
    {
        _length = length;
    }

    public bool Validate(string password) => password.Length > _length;
}

public class LowerCaseRule : IRule
{
    public bool Validate(string password) => password.Any(char.IsLower);
}

public class IncludesCharacterValidationRule : IRule
{
    private readonly char _character;

    public IncludesCharacterValidationRule(char character)
    {
        _character = character;
    }

    public bool Validate(string password) => password.Any(x => x.Equals(_character));
}

public class IsNumberRule : IRule
{
    public bool Validate(string password) => password.Any(char.IsNumber);
}