using System;
using System.Linq.Expressions;

namespace WpfLocalization.Framework
{
    /// <summary>
    /// Provides the features required to handle localized texts.
    /// </summary>
    public interface IHandleLocalizedTexts
    {
        /// <summary>Returns the localized value of the text resource having the given name.</summary>
        /// <param name="resourceName">The name of the text resource for which to retrieve the localized value.</param>
        /// <returns>A string containing the localized value of the text resource having the given name.</returns>
        string GetLocalizedStringValue(string resourceName);

        /// <summary>
        /// Returns the localized value of the property having the given name which must be a member of the given view
        /// model class. 
        /// </summary>
        /// <param name="viewModelClassName">
        /// The name of the view model class which contains the given property.
        /// </param>
        /// <param name="propertyName">The name of the property for which to retrieve the localized value.</param>
        /// <returns>A string containing the localized value of the given property.</returns>
        string GetLocalizedStringPropertyValue(string viewModelClassName, string propertyName);

        /// <summary>
        /// Returns the localized value of the property represented by the given expression tree. 
        /// </summary>
        /// <typeparam name="TClass">The type of the view model class containg the property for which to retrieve the
        /// localized value.</typeparam>
        /// <typeparam name="TProperty">The type of the property for which to retrieve the localized value.</typeparam>
        /// <returns>A string containing the localized value of the property.</returns>
        string GetLocalizedStringPropertyValue<TClass, TProperty>(Expression<Func<TProperty>> propertyExpression) 
            where TClass : class;
    }
}