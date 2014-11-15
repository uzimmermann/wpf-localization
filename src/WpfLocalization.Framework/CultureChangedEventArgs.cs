using System;
using System.Globalization;

namespace WpfLocalization.Framework
{
    /// <summary>
    /// Provides data for the <see cref="ILocalizableApplication.CultureChangedEvent"/>. This class cannot be
    /// inherited.
    /// </summary>
    public sealed class CultureChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Saves the new culture. This field is readonly.
        /// </summary>
        private readonly CultureInfo _newCultureInfo;

        /// <summary>
        /// Gets the new culture.
        /// </summary>
        public CultureInfo NewCultureInfo
        {
            get { return _newCultureInfo; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CultureChangedEventArgs"/> class with the given instance of
        /// <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="newCultureInfo">An instance of <see cref="CultureInfo"/> representing the new culture.</param>
        public CultureChangedEventArgs(CultureInfo newCultureInfo)
        {
            _newCultureInfo = newCultureInfo;
        }

        /// <summary>
        /// Returns a string which represents the current instance of the <see cref="CultureChangedEventArgs"/> class.
        /// </summary>
        /// <returns>
        /// A string which represents the current instance of the <see cref="CultureChangedEventArgs"/> class.
        /// </returns>
        public override string ToString()
        {
            return string.Format("NewCultureInfo: {0}", NewCultureInfo);
        }
    }
}