namespace PasswordValidation;

public class PasswordValidatorTests
{
    [TestCase("Passwordisgreaterthan8characters_")]
    [TestCase("Passw0rdContainsAUpperCaseCharacter_")]
    [TestCase("PASSW0RDCONTAINSALOWERCASECHARACTEr_")]
    [TestCase("Passw0rdContainsANumber_")]
    [TestCase("Passw0rdContainsAnUnderscore_")]
    public void ValidatePasswordOne_returns_true_when_password_is_valid(string password)
    {
        var validationRule = new ValidationRuleOne();
        var validator = new PasswordValidator(validationRule);
        var isValid = validator.ValidatePassword(password);
        Assert.That(isValid, Is.True);
    }

    [TestCase("Passwordisgreaterthan6charactes")]
    [TestCase("Passw0rdContainsAUpperCaseCharacter")]
    [TestCase("PASSW0RDCONTAINSALOWERCASECHARACTEr")]
    [TestCase("Passw0rdContainsANumber")]
    public void ValidatePasswordTwo_returns_true_when_password_is_valid(string password)
    {
        var validationRule = new ValidationRuleTwo();
        var validator = new PasswordValidator(validationRule);
        var isValid = validator.ValidatePassword(password);
        Assert.That(isValid, Is.True);
    }

    [TestCase("Passwordisgreaterthan16charactes_")]
    [TestCase("PasswordContainsAUpperCaseCharacter_")]
    [TestCase("PASSWORDCONTAINSALOWERCASECHARACTEr_")]
    [TestCase("PasswordContainsAnUnderscore_")]
    public void ValidatePasswordThree_returns_true_when_password_is_valid(string password)
    {
        var validationRule = new ValidationRuleThree();
        var validator = new PasswordValidator(validationRule);
        var isValid = validator.ValidatePassword(password);
        Assert.That(isValid, Is.True);
    }
}

