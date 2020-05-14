using CMS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace HBS.LocalizedValidationAttributes.Kentico.MVC
{
    /// <summary>
    /// Localized version of the RegularExpressionAttribute, Error Message can contain resolve {$ localizedstring.key $}'s, and the resolved string can contain a {0} for the Property Name and {1} for the pattern
    /// </summary>
    public class LocalizedRegularExpressionAttribute : RegularExpressionAttribute, IClientValidatable
    {
        public LocalizedRegularExpressionAttribute(string pattern) : base(pattern)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            // This may error out if message contains localization strings, but we need to call it to run the SetupRegex() private method
            try
            {
                return base.FormatErrorMessage(name);
            }
            catch (Exception) { }
            ILocalizationService _LocalizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            return String.Format(CultureInfo.CurrentCulture, _LocalizationService.LocalizeString(ErrorMessageString, CultureInfo.CurrentCulture.Name), name, this.Pattern);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRegexRule(FormatErrorMessage(metadata.GetDisplayName()), Pattern);
        }
    }
}