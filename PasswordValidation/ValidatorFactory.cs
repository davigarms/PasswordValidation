namespace PasswordValidation;

public abstract class ValidatorFactory
{
    protected abstract IValidationRule FactoryMethod();

    public bool ValidatePassword(string password)
    {
        var validationRule = FactoryMethod();
        return validationRule.IsValid(password);
    }
}

public class PasswordValidatorOne : ValidatorFactory
{
    protected override IValidationRule FactoryMethod() => new ValidationRuleOne();
}

public class PasswordValidatorTwo : ValidatorFactory
{
    protected override IValidationRule FactoryMethod() => new ValidationRuleTwo();
}

public class PasswordValidatorThree : ValidatorFactory
{
    protected override IValidationRule FactoryMethod() => new ValidationRuleThree();
}