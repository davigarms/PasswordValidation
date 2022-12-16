namespace PasswordValidation;

public interface IRule
{
    public bool IsValid(string password);
    string GetErrorMessage();
}

public class UpperCaseRule : IRule
{
    private const string ErrorMessage = "Uppercase error";
    
    public bool IsValid(string password) => password.Any(char.IsUpper);
    public string GetErrorMessage() => ErrorMessage;
}

public class LengthRule : IRule
{
    private readonly int _length;
    private const string ErrorMessage = "Length error";

    public LengthRule(int length)
    {
        _length = length;
    }

    public bool IsValid(string password) => password.Length > _length;

    public string GetErrorMessage() => ErrorMessage;
}

public class LowerCaseRule : IRule
{
    private const string ErrorMessage = "Lowercase error";
    public bool IsValid(string password) => password.Any(char.IsLower);
    public string GetErrorMessage() => ErrorMessage;
}

public class IncludesCharacterRule : IRule
{
    private readonly char _character;

    public IncludesCharacterRule(char character)
    {
        _character = character;
    }

    public bool IsValid(string password) => password.Any(x => x.Equals(_character));
    public string GetErrorMessage() => "Character error";
}

public class IncludesNumberRule : IRule
{
    public bool IsValid(string password) => password.Any(char.IsNumber);
    public string GetErrorMessage() => "Number error";
}