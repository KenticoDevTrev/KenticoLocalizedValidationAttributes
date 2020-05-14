using CMS.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace HBS.LocalizedValidationAttributes.Kentico.MVC
{
    /// <summary>
    /// Localized version of the MinLengthAttribute, Error Message can contain resolve {$ localizedstring.key $}'s, and the resolved string can contain a {0} for the Property Name and {1} for the min length
    /// </summary>
    public class LocalizedMinLengthAttribute : MinLengthAttribute
    {
        public LocalizedMinLengthAttribute(int length) : base(length)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            ILocalizationService _LocalizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            return String.Format(CultureInfo.CurrentCulture, _LocalizationService.LocalizeString(ErrorMessageString, CultureInfo.CurrentCulture.Name), name, Length);
        }
    }
}