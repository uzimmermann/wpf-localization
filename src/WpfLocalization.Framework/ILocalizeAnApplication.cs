using System;
using System.Globalization;

namespace WpfLocalization.Framework
{
    /// <summary>
    /// Provides the features required to localize an application.
    /// </summary>
    public interface ILocalizeAnApplication
    {
        /// <summary>
        /// Raised when <see cref="CurrentCulture"/> has a new value.
        /// </summary>
        event EventHandler<CultureChangedEventArgs> CultureChanged;

        /// <summary>
        /// Gets or sets the current culture.
        /// </summary>
        CultureInfo CurrentCulture { get; set; }

        /// <summary>
        /// Gets the localized texts.
        /// </summary>
        IHandleLocalizedTexts LocalizedTexts { get; }

        /// <summary>
        /// Gets the count of registered objects supporting localization.
        /// </summary>
        int CountOfRegisteredLocalizationSupporters { get; }

        /// <summary>
        /// Registers the given object for localization.
        /// </summary>
        /// <param name="localizationSupporter">The object which to register for localization.</param>
        void RegisterForLocalization(ISupportLocalization localizationSupporter);

        /// <summary>
        /// Deregisters the given object from localization.
        /// </summary>
        /// <param name="localizationSupporter">The object which to deregister from localization.</param>
        void DeregisterFromLocalization(ISupportLocalization localizationSupporter);
    }
}