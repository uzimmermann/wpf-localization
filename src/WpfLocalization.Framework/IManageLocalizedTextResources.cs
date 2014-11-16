using System;
using System.Linq.Expressions;

namespace WpfLocalization.Framework
{
    /// <summary>
    /// Provides the features required to manage localized text resources. 
    /// </summary>
    public interface IManageLocalizedTextResources
    {
        /// <summary>
        /// Returns the localized text resource having the given name.
        /// </summary>
        /// <param name="name">The name of the localized text resource to retrieve.</param>
        /// <returns>A string containing the localized text resource.</returns>
        string GetLocalizedTextResource(string name);

        /// <summary>
        /// Returns a string containing the localized value of the property with the given name which is a member of
        /// the view model class with the given name.
        /// </summary>
        /// <param name="viewModelClassName">
        /// The name of the view model class containing the property <paramref name="propertyName"/>.
        /// </param>
        /// <param name="propertyName">
        /// The name of a property of type <see cref="string"/> which is a member of the
        /// view model class <paramref name="viewModelClassName"/> and for which to retrieve the localized value.
        /// </param>
        /// <returns>A string containing the localized value of the given property.</returns>
        string GetLocalizedStringPropertyValue(string viewModelClassName, string propertyName);

        /// <summary>
        ///  Returns a string containing the localized value of the property represented by the given lambda expression.
        /// </summary>
        /// <typeparam name="TClass">The type of the view model class containing the property represented by the given
        /// lambda expression.</typeparam>
        /// <typeparam name="TProperty">The type of the property for which to retrieve the localized value.</typeparam>
        /// <param name="propertyExpression">A lambda expression representing the property for which to retrieve
        /// the localized value.</param>
        /// <returns>A string containing the localized value of the given property.</returns>
        string GetLocalizedStringPropertyValue<TClass, TProperty>(Expression<Func<TProperty>> propertyExpression) 
            where TClass : class;
    }
}
