namespace PasswordValidation;

public class RuleValidator
{
    private readonly List<IRule> _rules;
    private List<string> _errors;

    public RuleValidator()
    {
        _rules = new List<IRule>();
        _errors = new List<string>();
    }

    public void Validate(string password)
    {
        foreach (var rule in _rules.Where(rule => !rule.IsValid(password)))
        {
            _errors.Add(rule.GetErrorMessage());
        }
    }

    public int RuleCount => _rules.Count;

    public void Add(IRule rule) => _rules.Add(rule);

    public List<string> GetErrors()
    {
        return _errors;
    }
}