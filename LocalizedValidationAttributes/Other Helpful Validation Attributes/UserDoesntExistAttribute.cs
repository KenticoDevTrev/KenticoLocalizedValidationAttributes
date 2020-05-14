using System.ComponentModel.DataAnnotations;

namespace Generic.Attributes
{
    public class UserDoesntExistAttribute : LocalizedValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return !new UserExistsAttribute().IsValid(value);
        }
    }
}