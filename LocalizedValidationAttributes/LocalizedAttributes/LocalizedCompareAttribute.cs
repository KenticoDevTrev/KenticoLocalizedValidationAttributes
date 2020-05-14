using CMS.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace HBS.LocalizedValidationAttributes.Kentico.MVC
{
    /// <summary>
    /// Localized version of the CompareAttribute, Error Message can contain resolve {$ localizedstring.key $}'s, and the resolved string can contain a {0} for the Property Name and {1} for the other Property Name
    /// </summary>
    public class LocalizedCompareAttribute : CompareAttribute, IClientValidatable
    {
        public LocalizedCompareAttribute(string otherProperty) : base(otherProperty)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            ILocalizationService _LocalizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            return String.Format(CultureInfo.CurrentCulture, _LocalizationService.LocalizeString(ErrorMessageString, CultureInfo.CurrentCulture.Name), name, OtherPropertyDisplayName ?? OtherProperty);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string name = "";
            if (metadata.ContainerType != null && this.OtherPropertyDisplayName == null)
            {
                CompareAttribute displayName = this;
                ModelMetadataProvider current = ModelMetadataProviders.Current;
                name = current.GetMetadataForProperty(() => metadata.Model, metadata.ContainerType, this.OtherProperty).GetDisplayName();
            }
            ILocalizationService _LocalizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            yield return new ModelClientValidationEqualToRule(String.Format(CultureInfo.CurrentCulture, _LocalizationService.LocalizeString(ErrorMessageString, CultureInfo.CurrentCulture.Name), name, OtherPropertyDisplayName ?? OtherProperty), FormatPropertyForClientValidation(this.OtherProperty));
        }
        public static string FormatPropertyForClientValidation(string property)
        {
            if (property == null)
            {
                throw new ArgumentException("Value cannot be null or empty.", "property");
            }
            return string.Concat("*.", property);
        }
    }
}