using CMS.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace HBS.LocalizedValidationAttributes.Kentico.MVC
{
    /// <summary>
    /// Localized version of the CreditCardAttribute, Error Message can contain resolve {$ localizedstring.key $}'s, and the resolved string can contain a {0} for the Property Name
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class LocalizedCreditCardAttribute : DataTypeAttribute
    {
        public LocalizedCreditCardAttribute()
            : base(DataType.CreditCard)
        {

        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string ccValue = value as string;
            if (ccValue == null)
            {
                return false;
            }
            ccValue = ccValue.Replace("-", "");
            ccValue = ccValue.Replace(" ", "");

            int checksum = 0;
            bool evenDigit = false;

            // http://www.beachnet.com/~hstiles/cardtype.html
            foreach (char digit in ccValue.Reverse())
            {
                if (digit < '0' || digit > '9')
                {
                    return false;
                }

                int digitValue = (digit - '0') * (evenDigit ? 2 : 1);
                evenDigit = !evenDigit;

                while (digitValue > 0)
                {
                    checksum += digitValue % 10;
                    digitValue /= 10;
                }
            }

            return (checksum % 10) == 0;
        }

        public override string FormatErrorMessage(string name)
        {
            ILocalizationService _LocalizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            return String.Format(CultureInfo.CurrentCulture, _LocalizationService.LocalizeString(ErrorMessageString, CultureInfo.CurrentCulture.Name), name);
        }
    }
}