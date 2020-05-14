using CMS.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace HBS.LocalizedValidationAttributes.Kentico.MVC
{
    /// <summary>
    /// Localized version of the ValidationAttribute, for use on custom Validation Attributes you create. Error Message can contain resolve {$ localizedstring.key $}'s, and the resolved string can contain a {0} for the Property Name.
    /// </summary>
    public class LocalizedValidationAttribute : ValidationAttribute
    {

        public override string FormatErrorMessage(string name)
        {
            ILocalizationService _LocalizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            return String.Format(CultureInfo.CurrentCulture, _LocalizationService.LocalizeString(ErrorMessageString, CultureInfo.CurrentCulture.Name), name);
        }

        /// <summary>
        /// Localizes the given expression given the current Culture's Name
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public string LocalizeString(string Expression)
        {
            ILocalizationService _LocalizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            return _LocalizationService.LocalizeString(Expression, CultureInfo.CurrentCulture.Name);
        }
    }
}