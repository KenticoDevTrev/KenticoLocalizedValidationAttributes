using CMS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace HBS.LocalizedValidationAttributes.Kentico.MVC
{
    /// <summary>
    /// Localized version of the RangeAttribute, Error Message can contain resolve {$ localizedstring.key $}'s, and the resolved string can contain a {0} for the Property Name, {1} for the min length and {2} for the max length
    /// </summary>
    public class LocalizedRangeAttribute : RangeAttribute, IClientValidatable
    {
        public LocalizedRangeAttribute(int minimum, int maximum) : base(minimum, maximum)
        {
        }

        public LocalizedRangeAttribute(Type type, string minimum, string maximum) : base(type, minimum, maximum)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            // This may error out if message contains localization strings, but we need to call it to run the SetupConversion private method
            try {
                return base.FormatErrorMessage(name);
            }
            catch (Exception) { }
            ILocalizationService _LocalizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            return String.Format(CultureInfo.CurrentCulture, _LocalizationService.LocalizeString(ErrorMessageString, CultureInfo.CurrentCulture.Name), name, this.Minimum, this.Maximum);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRangeRule(FormatErrorMessage(metadata.GetDisplayName()), Minimum, Maximum);
        }
    }
}