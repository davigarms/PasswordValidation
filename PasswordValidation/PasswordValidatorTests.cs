using NUnit.Framework;
using FakeItEasy;

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
        var validator = new PasswordValidator(new ValidationOneFactory());
        var isValid = validator.IsValid(password);
        Assert.That(isValid, Is.True);
    }

    [TestCase("Passwordisgreaterthan6charactes")]
    [TestCase("Passw0rdContainsAUpperCaseCharacter")]
    [TestCase("PASSW0RDCONTAINSALOWERCASECHARACTEr")]
    [TestCase("Passw0rdContainsANumber")]
    public void ValidatePassword_rule_two_returns_true_when_password_is_valid(string password)
    {
        var validator = new PasswordValidator(new ValidationTwoFactory());
        var isValid = validator.IsValid(password);
        Assert.That(isValid, Is.True);
    }

    [TestCase("Passwordisgreaterthan16charactes_")]
    [TestCase("PasswordContainsAUpperCaseCharacter_")]
    [TestCase("PASSWORDCONTAINSALOWERCASECHARACTEr_")]
    [TestCase("PasswordContainsAnUnderscore_")]
    public void ValidatePassword_rule_three_returns_true_when_password_is_valid(string password)
    {
        var validator = new PasswordValidator(new ValidationThreeFactory());
        var isValid = validator.IsValid(password);
        Assert.That(isValid, Is.True);
    }

    [Test]
    public void Validator_returns_expected_result([Values] bool expected)
    {
        var validatorFactory = A.Fake<IValidationFactory>();
        var ruleValidator = A.Fake<IValidation>();
        A.CallTo(() => ruleValidator.Validate(A<string>._)).Returns(expected);
        A.CallTo(() => validatorFactory.Create()).Returns(ruleValidator);

        var validator = new PasswordValidator(validatorFactory);
        Assert.That(validator.IsValid("any-password"), Is.EqualTo(expected));
    }

    [Test]
    public void Validator_factory_returns_the_correct_type()
    {
        Assert.IsInstanceOf<Validation>(new ValidationOneFactory().Create());
    }

    [TestCase(8, "validpassword", true)]
    [TestCase(16, "invalidpassword", false)]
    public void LenghtRule_returns_expected_value(int minLength, string password, bool expected)
    {
        var rule = new LenghtRule(minLength);
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
        ruleValidator.Add(new LenghtRule(8));
        Assert.That(ruleValidator.RuleCount, Is.EqualTo(1));
    }

    [TestCase(8, "validpassword", true)]
    [TestCase(16, "invalidpassword", false)]
    public void RuleValidator_with_LengthRule_return_expected(int minLenght, string password, bool expected)
    {
        var ruleValidator = new RuleValidatorBuilder()
            .WithLenghtRule(minLenght)
            .Build();
        Assert.That(ruleValidator.Validate(password), Is.EqualTo(expected));
    }

    [TestCase(8, "Validpassword", true)]
    [TestCase(16, "invalidpassword", false)]
    [TestCase(16, "invalid", false)]
    public void RuleValidator_with_LengthRule_and_UpperCaseRule_return_expected(int minLenght, string password, bool expected)
    {
        var ruleValidator = new RuleValidatorBuilder()
            .WithLenghtRule(minLenght)
            .WithUpperCaseRule()
            .Build();
        Assert.That(ruleValidator.Validate(password), Is.EqualTo(expected));
    }
}