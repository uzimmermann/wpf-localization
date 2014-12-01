using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;

namespace WpfLocalization.Framework
{
    /// <summary>
    /// Provides a base implementation of <see cref="IHandleLocalizedTexts"/>.
    /// </summary>
    public abstract class LocalizedTexts : IHandleLocalizedTexts
    {
        /// <summary>
        /// Saves property names of UI elements supporting data binding. They will be used as part of a text resource 
        /// name. This field is read-only.
        /// </summary>
        private static readonly string[] NamesOfPropertiesSupportingDataBinding =
        {
            "BusyContent", 
            "Caption", 
            "Content", 
            "Header", 
            "Text", 
            "ToolTip"
        };

        /// <summary>
        /// Backing field for <see cref="NameOfAssemblyContainingLocalizedTextResources"/>. This field is read-only.
        /// </summary>
        private readonly string _nameOfAssemblyContainingLocalizedTextResources;

        /// <summary>
        /// Backing field for <see cref="NamespaceOfTextResourceFiles"/>. This field is read-only.
        /// </summary>
        private readonly string _namespaceOfTextResourceFiles;

        /// <summary>
        /// Backing field for <see cref="NameOfTextResourceFileForDefaultCulture"/>. This field is read-only.
        /// </summary>
        private readonly string _nameOfTextResourceFileForDefaultCulture;

        /// <summary>
        /// Backing field for <see cref="ResourceManager"/>. This field is read-only.
        /// </summary>
        private readonly ResourceManager _resourceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedTexts"/> class with the given assembly name,
        /// the given root namespace of the text resource files and the given name of the file containing the texts for
        /// the default culture.
        /// </summary>
        /// <param name="nameOfAssemblyContainingLocalizedTextResources">The fully qualified name of the assembly
        /// containing the text resource files.
        /// </param>
        /// <param name="namespaceOfTextResourceFiles">The namespace containing the text resource files e. g. 
        /// "SomeCompany.SomeProject.Properties".</param>
        /// <param name="nameOfTextResourceFileForDefaultCulture">The name of the text resource file (without extension)
        /// containing the text resources for the default culture e. g. "StringsTable".
        /// </param>
        /// <exception cref="ArgumentException">When <paramref name="nameOfAssemblyContainingLocalizedTextResources"/>
        /// is null or empty or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentException">When <paramref name="namespaceOfTextResourceFiles"/>
        /// is null or empty or consists only of white-space characters.</exception>
        /// <exception cref="ArgumentException">When <paramref name="nameOfTextResourceFileForDefaultCulture"/>
        /// is null or empty or consists only of white-space characters.</exception>
        protected LocalizedTexts(
            string nameOfAssemblyContainingLocalizedTextResources, 
            string namespaceOfTextResourceFiles,
            string nameOfTextResourceFileForDefaultCulture)
        {
            if (string.IsNullOrWhiteSpace(nameOfAssemblyContainingLocalizedTextResources))
            {
                throw new ArgumentException(
                    ErrorMessages.ArgumentCannotBeNullOrWhiteSpace,
                    "nameOfAssemblyContainingLocalizedTextResources");
            }

            if (string.IsNullOrWhiteSpace(namespaceOfTextResourceFiles))
            {
                throw new ArgumentException(
                    ErrorMessages.ArgumentCannotBeNullOrWhiteSpace,
                    "namespaceOfTextResourceFiles");
            }

            if (string.IsNullOrWhiteSpace(nameOfTextResourceFileForDefaultCulture))
            {
                throw new ArgumentException(
                    ErrorMessages.ArgumentCannotBeNullOrWhiteSpace,
                    "nameOfTextResourceFileForDefaultCulture");
            }

            _nameOfAssemblyContainingLocalizedTextResources = nameOfAssemblyContainingLocalizedTextResources;
            _namespaceOfTextResourceFiles = namespaceOfTextResourceFiles;
            _nameOfTextResourceFileForDefaultCulture = nameOfTextResourceFileForDefaultCulture;

            _resourceManager = new ResourceManager(
                NamespaceOfTextResourceFiles + "." + NameOfTextResourceFileForDefaultCulture, 
                Assembly.Load(NameOfAssemblyContainingLocalizedTextResources));
        }

        /// <summary>
        /// Gets the fully qualified name of the assembly containing the text resource files.
        /// </summary>
        public string NameOfAssemblyContainingLocalizedTextResources
        {
            get { return _nameOfAssemblyContainingLocalizedTextResources; }
        }

        /// <summary>
        /// Gets the name of the text resource file (without extension) containing the texts for the default culture.
        /// </summary>
        public string NameOfTextResourceFileForDefaultCulture
        {
            get { return _nameOfTextResourceFileForDefaultCulture; }
        }

        /// <summary>
        /// Gets the namespace containing the text resource files.
        /// </summary>
        public string NamespaceOfTextResourceFiles
        {
            get { return _namespaceOfTextResourceFiles; }
        }

        /// <summary>
        /// Gets an instance of <see cref="ResourceManager"/> providing access to the localized texts.
        /// </summary>
        protected ResourceManager ResourceManager
        {
            get { return _resourceManager; }
        }

        /// <summary>Returns the localized value of the text resource having the given name.</summary>
        /// <param name="resourceName">The name of the text resource for which to retrieve the localized value.</param>
        /// <returns>A string containing the localized value of the text resource having the given name.</returns>
        /// <exception cref="ArgumentException">When <paramref name="resourceName"/> is null or empty or consists only
        /// of white-space characters.</exception>
        /// <exception cref="InvalidOperationException">
        /// When a text resource with the given name could not be found.
        /// </exception>
        public virtual string GetLocalizedStringValue(string resourceName)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException(
                    ErrorMessages.ArgumentCannotBeNullOrWhiteSpace,
                    "resourceName");
            }

            var localizedString = _resourceManager.GetString(resourceName);

            if (!string.IsNullOrWhiteSpace(localizedString))
            {
                return localizedString;
            }

            var message = string.Format(
                CultureInfo.InvariantCulture, 
                ErrorMessages.TextResourceCouldNotBeFoundFormatString, 
                resourceName);

            throw new InvalidOperationException(message);
        }

        /// <summary>
        /// Returns the localized value of the property represented by the given expression tree. 
        /// </summary>
        /// <typeparam name="TClass">The type of the view model class containg the property for which to retrieve the
        /// localized value.</typeparam>
        /// <typeparam name="TProperty">The type of the property for which to retrieve the localized value.</typeparam>
        /// <returns>A string containing the localized value of the property.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="propertyExpression"/> is null.</exception>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="propertyExpression"/> contains no member access expression.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="propertyExpression"/> accesses no property.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="propertyExpression"/> accesses a static property.
        /// </exception>
        public virtual string GetLocalizedStringPropertyValue<TClass, TProperty>(
            Expression<Func<TProperty>> propertyExpression)
            where TClass : class
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);

            return GetLocalizedStringPropertyValue(typeof(TClass).Name, propertyName);
        }

        /// <summary>
        /// Returns the localized value of the property having the given name which must be a member of the given view
        /// model class. 
        /// </summary>
        /// <param name="viewModelClassName">
        /// The name of the view model class which contains the given property.
        /// </param>
        /// <param name="propertyName">The name of the property for which to retrieve the localized value.</param>
        /// <returns>A string containing the localized value of the given property.</returns>
        /// <exception cref="ArgumentException">
        /// When <paramref name="viewModelClassName"/> is null or empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// When <paramref name="propertyName"/>is null or empty or consists only of white-space characters.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// When a text resource having a name consisting of the given view model class name and the given property name
        /// could not be found.</exception>
        public virtual string GetLocalizedStringPropertyValue(string viewModelClassName, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(viewModelClassName))
            {
                throw new ArgumentException(
                    ErrorMessages.ArgumentCannotBeNullOrWhiteSpace,
                    "viewModelClassName");
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException(
                    ErrorMessages.ArgumentCannotBeNullOrWhiteSpace,
                    "propertyName");
            }

            var firstPartOfTextResourceName = Regex.Match(viewModelClassName, @".*(?=ViewModel$)").Value;

            var patternForSecondAndThirdPartOfTextResourceName = string.Format(
                @"(?<secondPartOfTextResourceName>.*)(?<thirdPartOfTextResourceName>{0})", 
                string.Join("|", NamesOfPropertiesSupportingDataBinding));

            var secondAndThirdPartOfTextResourceNameMatch = Regex.Match(
                propertyName, 
                patternForSecondAndThirdPartOfTextResourceName, 
                RegexOptions.RightToLeft);

            if (!secondAndThirdPartOfTextResourceNameMatch.Success)
            {
                var message = String.Format(
                    CultureInfo.InvariantCulture,
                    ErrorMessages.TextResourceWithGivenArgumentsCouldNotBeFoundFormatString,
                    "viewModelClassName",
                    "propertyName");

                throw new InvalidOperationException(message);

            }

            var secondPartOfTextResourceName = secondAndThirdPartOfTextResourceNameMatch
                .Groups["secondPartOfTextResourceName"]
                .Value;
            
            var thirdPartOfTextResourceName = secondAndThirdPartOfTextResourceNameMatch
                .Groups["thirdPartOfTextResourceName"]
                .Value;

            var resourceName = String.Format(
                CultureInfo.InvariantCulture, 
                "{0}_{1}_{2}", 
                firstPartOfTextResourceName, 
                secondPartOfTextResourceName,
                thirdPartOfTextResourceName);

            return GetLocalizedStringValue(resourceName);
        }
    }
}