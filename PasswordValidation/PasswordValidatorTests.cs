using System.Data;
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

    [TestCaseSource(nameof(ValidatorReturnsExpectedCases))]
    public void Validator_returns_expected_result(List<string> expectedErrorList, bool expectedResult)
    {
        var validatorFactory = A.Fake<IValidationFactory>();
        var ruleValidator = A.Fake<IValidation>();
        A.CallTo(() => ruleValidator.GetErrors()).Returns(expectedErrorList);
        A.CallTo(() => validatorFactory.Create()).Returns(ruleValidator);

        var validator = new PasswordValidator(validatorFactory);
        Assert.That(validator.IsValid("any-password"), Is.EqualTo(expectedResult));
    }

    [Test]
    public void Validator_factory_returns_the_correct_type()
    {
        Assert.IsInstanceOf<Validation>(new ValidationOneFactory().Create());
    }
    
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
    
    [TestCase(8, "validpassword", true)]
    [TestCase(16, "invalidpassword", false)]
    public void LenghtRule_returns_expected_value(int minLength, string password, bool expected)
    {
        var rule = new LengthRule(minLength);
        Assert.That(rule.IsValid(password), Is.EqualTo(expected));
    }

    [TestCase("Validpassword", true)]
    [TestCase("invalidpassword", false)]
    public void UpperCaseRule_returns_expected_value(string password, bool expected)
    {
        var rule = new UpperCaseRule();
        Assert.That(rule.IsValid(password), Is.EqualTo(expected));
    }
    
    [TestCase("VALIDPASSWORd", true)]
    [TestCase("INVALIDPASSOWORD", false)]
    public void LowerCaseRule_returns_expected_value(string password, bool expected)
    {
        var rule = new LowerCaseRule();
        Assert.That(rule.IsValid(password), Is.EqualTo(expected));
    }
    
    [TestCase("validpassword1", true)]
    [TestCase("invalidpassword", false)]
    public void IncludesNumberRule_returns_expected_value(string password, bool expected)
    {
        var rule = new IncludesNumberRule();
        Assert.That(rule.IsValid(password), Is.EqualTo(expected));
    }
    
    [TestCase("validpassword_", '_', true)]
    [TestCase("validpassword&", '&', true)]
    [TestCase("invalidpassword", '_', false)]
    [TestCase("invalidpassword", '&', false)]
    public void IncludesCharacterRule_returns_expected_value(string password, char character, bool expected)
    {
        var rule = new IncludesCharacterRule(character);
        Assert.That(rule.IsValid(password), Is.EqualTo(expected));
    }

    [Test]
    public void LengthRule_return_expected_error_message()
    {
        var rule = new LengthRule(TestContext.CurrentContext.Random.Next(Int32.MaxValue));
        Assert.That(rule.GetErrorMessage, Is.EqualTo("Length error"));
    }
    
    [Test]
    public void UpperCaseRule_return_expected_error_message()
    {
        var rule = new UpperCaseRule();
        Assert.That(rule.GetErrorMessage, Is.EqualTo("Uppercase error"));
    }
    
    [Test]
    public void LowerCaseRule_return_expected_error_message()
    {
        var rule = new LowerCaseRule();
        Assert.That(rule.GetErrorMessage, Is.EqualTo("Lowercase error"));
    }
    
    [Test]
    public void IncludesNumberRule_return_expected_error_message()
    {
        var rule = new IncludesNumberRule();
        Assert.That(rule.GetErrorMessage, Is.EqualTo("Number error"));
    }
    
    [Test]
    public void IncludesCharacterRule_return_expected_error_message()
    {
        var rule = new IncludesCharacterRule('_');
        Assert.That(rule.GetErrorMessage, Is.EqualTo("Character error"));
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

    private static IEnumerable<object[]> ValidatorReturnsExpectedCases()
    {
        yield return new object[]
        {
            new List<string>(),
            true
        };
        yield return new object[]
        {
            new List<string>
            {
                "Length error"
            },
            false
        };
    }
}