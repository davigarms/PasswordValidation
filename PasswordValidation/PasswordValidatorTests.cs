using System.Data;
using FakeItEasy;
using NUnit.Framework.Constraints;

namespace PasswordValidation;

public class PasswordValidatorTests
{
    [TestCase("Passwordisgreaterthan8characters_")]
    [TestCase("Passw0rdContainsAUpperCaseCharacter_")]
    [TestCase("PASSW0RDCONTAINSALOWERCASECHARACTEr_")]
    [TestCase("Passw0rdContainsANumber_")]
    [TestCase("Passw0rdContainsAnUnderscore_")]
    public void ValidatePassword_rule_one_returns_true_when_password_is_valid(string password)
    {
        var validator = new PasswordValidator(new ValidationRuleOneFactory());
        var isValid = validator.IsValid(password);
        Assert.That(isValid, Is.True);
    }

    [TestCase("Passwordisgreaterthan6charactes")]
    [TestCase("Passw0rdContainsAUpperCaseCharacter")]
    [TestCase("PASSW0RDCONTAINSALOWERCASECHARACTEr")]
    [TestCase("Passw0rdContainsANumber")]
    public void ValidatePassword_rule_two_returns_true_when_password_is_valid(string password)
    {
        var validator = new PasswordValidator(new ValidationRuleTwoFactory());
        var isValid = validator.IsValid(password);
        Assert.That(isValid, Is.True);
    }

    [TestCase("Passwordisgreaterthan16charactes_")]
    [TestCase("PasswordContainsAUpperCaseCharacter_")]
    [TestCase("PASSWORDCONTAINSALOWERCASECHARACTEr_")]
    [TestCase("PasswordContainsAnUnderscore_")]
    public void ValidatePassword_rule_three_returns_true_when_password_is_valid(string password)
    {
        var validator = new PasswordValidator(new ValidationRuleThreeFactory());
        var isValid = validator.IsValid(password);
        Assert.That(isValid, Is.True);
    }

    [Test]
    public void Validator_returns_expected_result([Values] bool expected)
    {
        var validatorFactory = A.Fake<IValidationFactory>();
        var validationRule = A.Fake<IValidationRule>();
        A.CallTo(() => validationRule.IsValid(A<string>._)).Returns(expected);
        A.CallTo(() => validatorFactory.Create()).Returns(validationRule);

        var validator = new PasswordValidator(validatorFactory);
        Assert.That(validator.IsValid("any-password"), Is.EqualTo(expected));
    }

    [Test]
    public void Validator_factory_returns_the_correct_type()
    {
        Assert.IsInstanceOf<ValidationRuleOne>(new ValidationRuleOneFactory().Create());
    }

    [TestCase(8, "validpassword", true)]
    [TestCase(16, "invalidpassword", false)]
    public void LenghtRule_returns_expected_value(int minLength, string password, bool expected)
    {
        var rule = new LenghtValidationRule(minLength);
        Assert.That(rule.Validate(password), Is.EqualTo(expected));
    }

    [TestCase("Validpassword", true)]
    [TestCase("invalidpassword", false)]
    public void UpperCaseRule_returns_expected_value(string password, bool expected)
    {
        var rule = new UpperCaseRule();
        Assert.That(rule.Validate(password), Is.EqualTo(expected));
    }
    
    [Test]
    public void RuleValidator_add_a_new_rule()
    {
        var ruleValidator = new RuleValidator();
        ruleValidator.Add(new LenghtValidationRule(8));
        Assert.That(ruleValidator.RuleCount, Is.EqualTo(1));
    }

    [TestCase(8, "validpassword", true)]
    [TestCase(16, "invalidpassword", false)]
    public void RuleValidator_with_LengthRule_return_expected(int minLenght, string password, bool expected)
    {
        var ruleValidator = new RuleValidator();
        ruleValidator.Add(new LenghtValidationRule(minLenght));
        Assert.That(ruleValidator.IsValid(password), Is.EqualTo(expected));
    }
}

public class RuleValidator
{
    private readonly List<IRule> _rules;

    public RuleValidator()
    {
        _rules = new List<IRule>();
    }

    public bool IsValid(string password) => _rules.All(rule => rule.Validate(password));

    public int RuleCount => _rules.Count;

    public void Add(IRule rule) => _rules.Add(rule);
}

public interface IRule
{
    public bool Validate(string password);
}

public class UpperCaseRule
{
    public bool Validate(string password) => password.Any(char.IsUpper);
}

public class LenghtValidationRule : IRule
{
    private readonly int _length;

    public LenghtValidationRule(int length)
    {
        _length = length;
    }

    public bool Validate(string password) => password.Length > _length;
}