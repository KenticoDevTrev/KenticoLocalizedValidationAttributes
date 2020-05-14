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

# Contributions, but fixes and License
Feel free to Fork and submit pull requests to contribute.

You can submit bugs through the issue list and i will get to them as soon as i can, unless you want to fix it yourself and submit a pull request!

Check the License.txt for License information

# Compatability
Can be used on any Kentico 12 SP site (hotfix 29 or above).
