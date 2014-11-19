using System;
using System.Linq.Expressions;
using System.Reflection;

namespace WpfLocalization.Framework
{
    /// <summary>
    /// Helper class for returning the name of a property given by an expression tree.
    /// </summary>
    internal static class PropertySupport
    {
        /// <summary>
        /// Returns the name of the given property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">
        /// An expression tree representing the property to retrieve the name for.
        /// </param>
        /// <returns>A string containing the name of the given property.</returns>
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
        public static string ExtractPropertyName<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");
            }

            var member = body.Member as PropertyInfo;

            if (member == null)
            {
                throw new ArgumentException(
                    "The member access expression does not access a property.", 
                    "propertyExpression");
            }

            if (member.GetGetMethod(true).IsStatic)
            {
                throw new ArgumentException("The referenced property is a static property.", "propertyExpression");
            }

            return body.Member.Name;
        }
    }
}
