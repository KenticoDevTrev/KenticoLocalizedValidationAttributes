using CMS.Core;
using System;
using System.Globalization;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace HBS.LocalizedValidationAttributes.Kentico.MVC
{
    /// <summary>
    /// Localized version of the CompareAttribute, Error Message can contain resolve {$ localizedstring.key $}'s, and the resolved string can contain a {0} for the Property Name and {1} for the other Property Name
    /// </summary>
    public class LocalizedCompareAttribute : CompareAttribute
    {
        public LocalizedCompareAttribute(string otherProperty) : base(otherProperty)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            ILocalizationService _LocalizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            return String.Format(CultureInfo.CurrentCulture, _LocalizationService.LocalizeString(ErrorMessageString, CultureInfo.CurrentCulture.Name), name, OtherPropertyDisplayName ?? OtherProperty);
        }
    }
}