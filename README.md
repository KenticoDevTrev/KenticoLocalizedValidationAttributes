# Kentico Localized Validation Attributes
This set of Validation Attributes extend the default DataAnnotations Attributes, allowing you to have Error Messages that contain Kentico Localized String Macros (ex "{$ customvalidation.message $}")

There are also a handful of Validation Attributes that are not in the assembly due to dependencies on Interfaces that you may wish to implement yourslef, these are in the [Other Helpful Validation Attributes](https://github.com/KenticoDevTrev/KenticoLocalizedValidationAttributes/tree/master/LocalizedValidationAttributes/Other%20Helpful%20Validation%20Attributes)

# Installation
1. Install the `HBS.LocalizedValidationAttributes.Kentico.MVC` NuGet Package to your MVC Site

# Usage
Where you would put your normal ValidationAttribute, just add "Localized" before it.

Example:
`[Required(ErrorMessage = "I wish i was localized")]` =>` [LocalizedRequired(ErrorMessage = "{$ custom.requirederror $}"]`

You can also inherit `LocalizedValidationAttribute` instead of `ValidationAttribute` for your own custom validations. This will, by default, localize the error message, but you may need to still overwrite the FormatErrorMessage if you need to add additional properties to the formatted message.

[Example of an inherited ValidationAttribute can be found here](https://github.com/KenticoDevTrev/KenticoLocalizedValidationAttributes/blob/master/LocalizedValidationAttributes/Other%20Helpful%20Validation%20Attributes/LocalizedUserExistsAttribute.cs).

[Example of an inherited ValidationAttribute with an overwritten FormatErrorMessage can be found here](https://github.com/KenticoDevTrev/KenticoLocalizedValidationAttributes/blob/master/LocalizedValidationAttributes/Other%20Helpful%20Validation%20Attributes/LocalizedPasswordPolicyAttribute.cs).

Here's a sample test class
``` csharp
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

        public static ValidationResult CustomCheck(string modelContext, ValidationContext validationContext)
        {
            return new ValidationResult("{$ validationtest.customvalidator $}", new List<string> { "CustomValidator" });
        }
    }
```

# Contributions, but fixes and License
Feel free to Fork and submit pull requests to contribute.

You can submit bugs through the issue list and i will get to them as soon as i can, unless you want to fix it yourself and submit a pull request!

Check the License.txt for License information

# Compatability
Can be used on any Kentico 12 SP site (hotfix 29 or above).
