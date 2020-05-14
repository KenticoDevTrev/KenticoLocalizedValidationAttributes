using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Generic.Attributes;
using HBS.LocalizedValidationAttributes.Kentico.MVC;

namespace Generic.Models
{
    public class ValidationTestModel
    {
        
        public string Compare1 { get; set; }
        [LocalizedCompare("Compare1", ErrorMessage = "{$ validationtest.compare $}")]
        public string Compare2 { get; set; }
        [LocalizedCreditCard(ErrorMessage = "{$ validationtest.creditcard $}")]
        public string CreditCard { get; set; }  
        [LocalizedCustomValidation(typeof(ValidationTestModel), "CustomCheck")]
        public string CustomValidator { get; set; }
        [LocalizedEmailAddress(ErrorMessage = "{$ validationtest.emailaddress $}")]
        public string EmailAddress { get; set; }
        [LocalizedFileExtensions(ErrorMessage = "{$ validationtest.fileextension $}", Extensions =".pdf")]
        public string FileExtension { get; set; }
        [LocalizedMaxLength(5, ErrorMessage = "{$ validationtest.maxlength $}")]
        public string MaxLength { get; set; }
        [LocalizedMinLength(5, ErrorMessage = "{$ validationtest.minlength $}")]
        public string MinLength { get; set; }
        [LocalizedPhone(ErrorMessage = "{$ validationtest.phone $}")]
        public string Phone { get; set; }
        [LocalizedRange(5, 10, ErrorMessage = "{$ validationtest.range $}")]
        public int Range { get; set; }
        [LocalizedRegularExpression("^[0-9]*$", ErrorMessage = "{$ validationtest.regularexpression $}")]
        public string RegularExpression { get; set; }
        [LocalizedRequired(ErrorMessage = "{$ validationtest.required $}")]
        public string Required { get; set; }
        [LocalizedStringLength(10, MinimumLength = 5, ErrorMessage = "{$ validationtest.stringlength $}")]
        public string StringLength { get; set; }
        [LocalizedPasswordPolicy(ErrorMessage = "{$ validationtest.passwordpolicy $}")]
        public string Password { get; set; }

        public static ValidationResult CustomCheck(string modelContext, ValidationContext validationContext)
        {
            return new ValidationResult("{$ validationtest.customvalidator $}", new List<string> { "CustomValidator" });
        }
    }
}