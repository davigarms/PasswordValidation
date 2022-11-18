namespace PasswordValidation;

public interface IValidationFactory
{
    public IValidation Create();
}

public class ValidationOneFactory : IValidationFactory
{
    public IValidation Create() => new Validation(new RuleValidatorBuilder()
        .WithLenghtRule(8)
        .WithUpperCaseRule()
        .WithLowerCaseRule()
        .WithIsNumberRule()
        .WithIncludesCharacterRule('_')
        .Build());
}

public class ValidationTwoFactory : IValidationFactory
{
    public IValidation Create() => new Validation(new RuleValidatorBuilder()
        .WithLenghtRule(6)
        .WithUpperCaseRule()
        .WithLowerCaseRule()
        .WithIsNumberRule()
        .Build());
}

public class ValidationThreeFactory : IValidationFactory
{
    public IValidation Create() => new Validation(new RuleValidatorBuilder()
        .WithLenghtRule(16)
        .WithUpperCaseRule()
        .WithLowerCaseRule()
        .WithIncludesCharacterRule('_')
        .Build());
}