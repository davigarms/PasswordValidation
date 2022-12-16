using NUnit.Framework;

namespace PasswordValidation;

public class RuleTests
{
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
}