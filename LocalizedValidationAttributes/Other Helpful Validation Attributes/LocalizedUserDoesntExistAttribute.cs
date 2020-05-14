using HBS.LocalizedValidationAttributes.Kentico.MVC;

namespace Generic.Attributes
{
    public class LocalizedUserDoesntExistAttribute : LocalizedValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return !new LocalizedUserExistsAttribute().IsValid(value);
        }
    }
}