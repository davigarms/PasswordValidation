namespace PasswordValidation.Tests;

public class RuleValidatorTests
{
    [Test]
    public void RuleValidator_add_a_new_rule()
    {
        var ruleValidator = new RuleValidator();
        ruleValidator.Add(new LengthRule(8));
        Assert.That(ruleValidator.RuleCount, Is.EqualTo(1));
    }

    [TestCaseSource(nameof(RuleValidatorWithLengthRuleCases))]
    public void RuleValidator_with_LengthRule_returns_expected(int minLenght, string password, List<string> expectedErrorList)
    {
        var ruleValidator = new RuleValidatorBuilder()
            .WithLenghtRule(minLenght)
            .Build();
        ruleValidator.Validate(password);
        Assert.That(ruleValidator.GetErrors(), Is.EquivalentTo(expectedErrorList));
    }

    [TestCaseSource(nameof(RuleValidatorWithLengthRuleAndUpperCaseRuleCases))]
    public void RuleValidator_with_LengthRule_and_UpperCaseRule_returns_expected(int minLenght, string password, List<string> expectedErrorList)
    {
        var ruleValidator = new RuleValidatorBuilder()
            .WithLenghtRule(minLenght)
            .WithUpperCaseRule()
            .Build();
        ruleValidator.Validate(password);
        Assert.That(ruleValidator.GetErrors(), Is.EquivalentTo(expectedErrorList));
    }


    [Test]
    public void Validation_factory_returns_the_correct_type()
    {
        Assert.That(new ValidationOneFactory().Create(), Is.InstanceOf<Validation>());
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