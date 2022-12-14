namespace PasswordValidation.Tests;

public class PasswordValidatorTests
{
    [TestCaseSource(nameof(IsValidReturnsExpectedCases))]
    public void  IsValid_returns_expected_result(List<string> expectedErrorList, bool expectedResult)
    {
        var validatorFactory = A.Fake<IValidationFactory>();
        var ruleValidator = A.Fake<IValidation>();
        A.CallTo(() => validatorFactory.Create()).Returns(ruleValidator);
        A.CallTo(() => ruleValidator.GetErrors()).Returns(expectedErrorList);

        var validator = new PasswordValidator(validatorFactory);
        Assert.That(validator.IsValid("any-password"), Is.EqualTo(expectedResult));
    }
    
    [TestCase("Passwordisgreaterthan8characters_")]
    [TestCase("Passw0rdContainsAUpperCaseCharacter_")]
    [TestCase("PASSW0RDCONTAINSALOWERCASECHARACTEr_")]
    [TestCase("Passw0rdContainsANumber_")]
    [TestCase("Passw0rdContainsAnUnderscore_")]
    public void IsValid_returns_true_when_password_is_valid_on_validation_one(string password)
    {
        var validator = new PasswordValidator(new ValidationOneFactory());
        var isValid = validator.IsValid(password);
        Assert.That(isValid, Is.True);
    }

    [TestCase("Passwordisgreaterthan6charactes")]
    [TestCase("Passw0rdContainsAUpperCaseCharacter")]
    [TestCase("PASSW0RDCONTAINSALOWERCASECHARACTEr")]
    [TestCase("Passw0rdContainsANumber")]
    public void IsValid_returns_true_when_password_is_valid_on_validation_two(string password)
    {
        var validator = new PasswordValidator(new ValidationTwoFactory());
        var isValid = validator.IsValid(password);
        Assert.That(isValid, Is.True);
    }

    [TestCase("Passwordisgreaterthan16charactes_")]
    [TestCase("PasswordContainsAUpperCaseCharacter_")]
    [TestCase("PASSWORDCONTAINSALOWERCASECHARACTEr_")]
    [TestCase("PasswordContainsAnUnderscore_")]
    public void IsValid_returns_true_when_password_is_valid_on_validation_three(string password)
    {
        var validator = new PasswordValidator(new ValidationThreeFactory());
        var isValid = validator.IsValid(password);
        Assert.That(isValid, Is.True);
    }
    
    private static IEnumerable<object[]> IsValidReturnsExpectedCases()
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