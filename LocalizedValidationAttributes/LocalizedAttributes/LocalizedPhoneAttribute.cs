using CMS.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace HBS.LocalizedValidationAttributes.Kentico.MVC
{
    /// <summary>
    /// Localized version of the PhoneAttribute, Error Message can contain resolve {$ localizedstring.key $}'s, and the resolved string can contain a {0} for the Property Name
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class LocalizedPhoneAttribute : DataTypeAttribute
    {
        // see unit tests for examples
        private static Regex _regex = CreateRegEx();
        private const string _additionalPhoneNumberCharacters = "-.()";

        public LocalizedPhoneAttribute()
            : base(DataType.PhoneNumber)
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string valueAsString = value as string;

            // Use RegEx implementation if it has been created, otherwise use a non RegEx version.
            if (_regex != null)
            {
                return valueAsString != null && _regex.Match(valueAsString).Length > 0;
            }
            else
            {
                if (valueAsString == null)
                {
                    return false;
                }

                valueAsString = valueAsString.Replace("+", string.Empty).TrimEnd();
                valueAsString = RemoveExtension(valueAsString);

                bool digitFound = false;
                foreach (char c in valueAsString)
                {
                    if (Char.IsDigit(c))
                    {
                        digitFound = true;
                        break;
                    }
                }

                if (!digitFound)
                {
                    return false;
                }

                foreach (char c in valueAsString)
                {
                    if (!(Char.IsDigit(c)
                        || Char.IsWhiteSpace(c)
                        || _additionalPhoneNumberCharacters.IndexOf(c) != -1))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private static Regex CreateRegEx()
        {
            // We only need to create the RegEx if this switch is enabled.
            /*if (AppSettings.DisableRegEx)
            {
                return null;
            }*/

            const string pattern = @"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$";
            const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

            // Set explicit regex match timeout, sufficient enough for phone parsing
            // Unless the global REGEX_DEFAULT_MATCH_TIMEOUT is already set
            TimeSpan matchTimeout = TimeSpan.FromSeconds(2);

            try
            {
                if (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") == null)
                {
                    return new Regex(pattern, options, matchTimeout);
                }
            }
            catch
            {
                // Fallback on error
            }

            // Legacy fallback (without explicit match timeout)
            return new Regex(pattern, options);
        }

        private static string RemoveExtension(string potentialPhoneNumber)
        {
            int lastIndexOfExtension = potentialPhoneNumber
                .LastIndexOf("ext.", StringComparison.InvariantCultureIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                string extension = potentialPhoneNumber.Substring(lastIndexOfExtension + 4);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            lastIndexOfExtension = potentialPhoneNumber
                .LastIndexOf("ext", StringComparison.InvariantCultureIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                string extension = potentialPhoneNumber.Substring(lastIndexOfExtension + 3);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }


            lastIndexOfExtension = potentialPhoneNumber
                .LastIndexOf("x", StringComparison.InvariantCultureIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                string extension = potentialPhoneNumber.Substring(lastIndexOfExtension + 1);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            return potentialPhoneNumber;
        }

        private static bool MatchesExtension(string potentialExtension)
        {
            potentialExtension = potentialExtension.TrimStart();
            if (potentialExtension.Length == 0)
            {
                return false;
            }

            foreach (char c in potentialExtension)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            string LocalErrorMessage = ErrorMessageString;
            if (string.IsNullOrWhiteSpace(LocalErrorMessage))
            {
                LocalErrorMessage = "The {0} field is not a valid phone number.";
            }
            ILocalizationService _LocalizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            return String.Format(CultureInfo.CurrentCulture, _LocalizationService.LocalizeString(LocalErrorMessage, CultureInfo.CurrentCulture.Name), name);
        }
    }
}