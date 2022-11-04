namespace PasswordValidation;

public interface IValidationFactory
{
    public IValidation Create();
}

public class ValidationOneFactory : IValidationFactory
{
    public IValidation Create() => new ValidationOne();
}

public class ValidationTwoFactory : IValidationFactory
{
    public IValidation Create() => new ValidationTwo();
}

public class ValidationThreeFactory : IValidationFactory
{
    public IValidation Create() => new ValidationThree();
}

public class ValidationFourFactory : IValidationFactory
{
    public IValidation Create() => new ValidationFour();
}