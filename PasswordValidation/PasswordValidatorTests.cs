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
        var validator = new PasswordValidator(new ValidationRuleOneFactory());
        var isValid =  validator.IsValid(password);
        Assert.That(isValid, Is.True);
    }

    [TestCase("Passwordisgreaterthan6charactes")]
    [TestCase("Passw0rdContainsAUpperCaseCharacter")]
    [TestCase("PASSW0RDCONTAINSALOWERCASECHARACTEr")]
    [TestCase("Passw0rdContainsANumber")]
    public void ValidatePassword_rule_two_returns_true_when_password_is_valid(string password)
    {
        var validator = new PasswordValidator(new ValidationRuleTwoFactory());
        var isValid =  validator.IsValid(password);
        Assert.That(isValid, Is.True);
    }

    [TestCase("Passwordisgreaterthan16charactes_")]
    [TestCase("PasswordContainsAUpperCaseCharacter_")]
    [TestCase("PASSWORDCONTAINSALOWERCASECHARACTEr_")]
    [TestCase("PasswordContainsAnUnderscore_")]
    public void ValidatePassword_rule_three_returns_true_when_password_is_valid(string password)
    {
        var validator = new PasswordValidator(new ValidationRuleThreeFactory());
        var isValid =  validator.IsValid(password);
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
}