namespace PasswordValidation;

public interface IRule
{
    public bool IsValid(string password);
    string GetErrorMessage();
}

public class LengthRule : IRule
{
    private readonly int _length;

    public LengthRule(int length)
    {
        _length = length;
    }

    public bool IsValid(string password) => password.Length > _length;

    public string GetErrorMessage() => ErrorMessages.LengthError;
}

public class UpperCaseRule : IRule
{
    public bool IsValid(string password) => password.Any(char.IsUpper);
    public string GetErrorMessage() => ErrorMessages.UpperCaseError;
}

public class LowerCaseRule : IRule
{
    public bool IsValid(string password) => password.Any(char.IsLower);
    public string GetErrorMessage() => ErrorMessages.LowerCaseError;
}

public class IncludesCharacterRule : IRule
{
    private readonly char _character;

    public IncludesCharacterRule(char character)
    {
        _character = character;
    }

    public bool IsValid(string password) => password.Any(x => x.Equals(_character));
    public string GetErrorMessage() => ErrorMessages.IncludesCharacterError;
}

public class IncludesNumberRule : IRule
{
    public bool IsValid(string password) => password.Any(char.IsNumber);
    public string GetErrorMessage() => ErrorMessages.IncludesNumberError;
}