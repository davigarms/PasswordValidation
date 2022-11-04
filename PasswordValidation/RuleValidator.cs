namespace PasswordValidation;

public class RuleValidator
{
    private readonly List<IRule> _rules;

    public RuleValidator()
    {
        _rules = new List<IRule>();
    }

    public bool Validate(string password) => _rules.All(rule => rule.Validate(password));

    public int RuleCount => _rules.Count;

    public void Add(IRule rule) => _rules.Add(rule);
}