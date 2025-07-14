using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.validators
{
    public class LoginUtilisateurUniqueAttribute: ValidationAttribute
    {
        #nullable enable
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var usernameProperty = validationContext.ObjectType.GetProperty("Login");
        var usernameValue = usernameProperty.GetValue(validationContext.ObjectInstance);
                
        UserService? _context = validationContext.GetService(typeof(UserService)) as UserService;
        var entity = _context?.Users().FirstOrDefault(x => x.Email == value.ToString()
            && x.UserName != usernameValue);
            
        if (entity != null)
        {
            return new ValidationResult(GetErrorMessage(value.ToString()));
        }

        return ValidationResult.Success;
    }
    #nullable disable
    }
}