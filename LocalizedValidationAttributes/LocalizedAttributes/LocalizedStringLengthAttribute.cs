using CMS.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace HBS.LocalizedValidationAttributes.Kentico.MVC
{
    /// <summary>
    /// Localized version of the StringLengthAttribute, Error Message can contain resolve {$ localizedstring.key $}'s, and the resolved string can contain a {0} for the Property Name, {1} for the maximum length, {2} for the minimum length
    /// </summary>
    public class LocalizedStringLengthAttribute : StringLengthAttribute
    {
        public LocalizedStringLengthAttribute(int maximumLength) : base(maximumLength)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            try
            {
                base.FormatErrorMessage(name);
            } catch(InvalidOperationException ioe)
            {
                // Rethrow this invalid operation exception
                throw ioe;
            } catch(Exception)
            {

            }
            ILocalizationService _LocalizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            return String.Format(CultureInfo.CurrentCulture, _LocalizationService.LocalizeString(ErrorMessageString, CultureInfo.CurrentCulture.Name), name, this.MaximumLength, this.MinimumLength);
        }
    }
}