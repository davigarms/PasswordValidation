namespace PasswordValidation.Tests;

[TestFixture]
public class ValidationFactoryTests
{
    [Test]
    public void Create_returns_the_correct_validation_type()
    {
        Assert.That(new ValidationOneFactory().Create(), Is.InstanceOf<Validation>());
    }
}