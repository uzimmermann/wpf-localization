using System;

namespace WpfLocalization.Framework
{
    /// <summary>
    /// Provides the features required to support localization.
    /// </summary>
    public interface ISupportLocalization
    {
        /// <summary>
        /// Gets the identifier of this instance of an implementation of <see cref="ISupportLocalization"/>.      
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Does the localization of this instance of an implementation of <see cref="ISupportLocalization"/>.
        /// </summary>
        void Localize();

        /// <summary>
        /// Gets or sets a value indicating whether the localization of this instance is suspended.
        /// </summary>
        bool LocalizationIsSuspended { get; set; }

        /// <summary>
        /// Suspends localization of this instance of an implementation of <see cref="ISupportLocalization"/>.
        /// </summary>
        void SuspendLocalization();

        /// <summary>
        /// Resumes localization of this instance of an implementation of <see cref="ISupportLocalization"/>.
        /// </summary>
        void ResumeLocalization();
    }
}
