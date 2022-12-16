namespace PasswordValidation.Tests;

public class RuleValidatorTests
{
    [Test]
    public void Add_a_new_rule()
    {
        var ruleValidator = new RuleValidator();
        ruleValidator.Add(new LengthRule(8));
        Assert.That(ruleValidator.RuleCount, Is.EqualTo(1));
    }

    [TestCaseSource(nameof(RuleValidatorWithLengthRuleCases))]
    public void Validate_returns_expected_error_when_LengthRule(int minLenght, string password, List<string> expectedErrorList)
    {
        var ruleValidator = new RuleValidatorBuilder()
            .WithLenghtRule(minLenght)
            .Build();
        ruleValidator.Validate(password);
        Assert.That(ruleValidator.GetErrors(), Is.EquivalentTo(expectedErrorList));
    }

    [TestCaseSource(nameof(RuleValidatorWithLengthRuleAndUpperCaseRuleCases))]
    public void Validate_returns_expected_when_LengthRule_and_UpperCaseRule(int minLenght, string password, List<string> expectedErrorList)
    {
        var ruleValidator = new RuleValidatorBuilder()
            .WithLenghtRule(minLenght)
            .WithUpperCaseRule()
            .Build();
        ruleValidator.Validate(password);
        Assert.That(ruleValidator.GetErrors(), Is.EquivalentTo(expectedErrorList));
    }

    private static IEnumerable<object[]> RuleValidatorWithLengthRuleCases()
    {
        yield return new object[]
        {
            8,
            "validpassword", 
            new List<string>()
        };
        yield return new object[]
        {
            16, 
            "invalidpassword", 
            new List<string>
            {
                "Length error"
            }
        };
    }

    private static IEnumerable<object[]> RuleValidatorWithLengthRuleAndUpperCaseRuleCases()
    {
        yield return new object[]
        {
            8,
            "Validpassword", 
            new List<string>()
        };
        yield return new object[]
        {
            16, 
            "invalidpassword", 
            new List<string>
            {
                "Length error",
                "Uppercase error"
            }
        };
        yield return new object[]
        {
            16, 
            "invalid", 
            new List<string>
            {
                "Length error",
                "Uppercase error"
            }
        };
    }
}