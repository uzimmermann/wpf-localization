using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace WpfLocalization.Framework
{
    /// <summary>
    /// Provides a base implementation of <see cref="ILocalizeAnApplication"/>.
    /// </summary>
    public abstract class LocalizableApplication : ILocalizeAnApplication
    {
        /// <summary>
        /// Backing field for the <see cref="LocalizedTexts"/> property. This field is read-only.
        /// </summary>
        private readonly IHandleLocalizedTexts _localizedTexts;

        /// <summary>
        /// Saves the objects that have been registered for localization. This field is read-only.
        /// </summary>
        private readonly IList<ISupportLocalization> _localizationSupporters = new List<ISupportLocalization>();

        /// <summary>
        /// Represents the method that will handle the <see cref="ILocalizeAnApplication.CultureChanged"/> event.
        /// </summary>
        private EventHandler<CultureChangedEventArgs> _cultureChangedEventHandler;

        /// <summary>
        /// Backing field for the <see cref="CurrentCulture"/> property.
        /// </summary>
        private CultureInfo _currentCulture;

        /// <summary>
        /// Backing field for the <see cref="DefaultCulture"/> property.
        /// </summary>
        private CultureInfo _defaultCulture;

        /// <summary>
        /// Raised when <see cref="CurrentCulture"/> has a new value.
        /// </summary>
        public event EventHandler<CultureChangedEventArgs> CultureChanged
        {
            add { _cultureChangedEventHandler += value; }
// ReSharper disable once DelegateSubtraction
            remove { _cultureChangedEventHandler -= value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableApplication"/> with the given localized texts.
        /// </summary>
        /// <param name="localizedTexts">The localized texts.</param>
        protected LocalizableApplication(IHandleLocalizedTexts localizedTexts)
        {
            _localizedTexts = localizedTexts;
        }

        /// <summary>
        /// Gets the default culture.
        /// </summary>
        public CultureInfo DefaultCulture
        {
            get { return _defaultCulture ?? (_defaultCulture = ProvideDefaultCulture()); }
        }

        /// <summary>
        /// Gets or sets the current culture.
        /// </summary>
        public CultureInfo CurrentCulture
        {
            get
            {
                return _currentCulture ?? (_currentCulture = DefaultCulture);
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (_currentCulture != null)
                {
                    var bothCulturesAreEqual = string.Equals(
                        _currentCulture.Name, 
                        value.Name, 
                        StringComparison.Ordinal);

                    if (bothCulturesAreEqual)
                    {
                        return;
                    }
                }

                _currentCulture = value;

                Thread.CurrentThread.CurrentCulture = CurrentCulture;
                Thread.CurrentThread.CurrentUICulture = CurrentCulture;

                OnCultureChanged();

                foreach (var localizationSupporter in _localizationSupporters.Where(x => !x.LocalizationIsSuspended))
                {
                    var currentLocalizationSupporter = localizationSupporter;

                    currentLocalizationSupporter.Localize();
                }
            }
        }

        /// <summary>
        /// Gets the localized texts.
        /// </summary>
        public IHandleLocalizedTexts LocalizedTexts { get { return _localizedTexts; } }

        /// <summary>
        /// Gets the count of registered objects supporting localization.
        /// </summary>
        public int CountOfRegisteredLocalizationSupporters
        {
            get { return _localizationSupporters.Count; }
        }

        /// <summary>
        /// Registers the given object for localization.
        /// </summary>
        /// <param name="localizationSupporter">The object which to register for localization.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="localizationSupporter"/> is null.</exception>
        public virtual void RegisterForLocalization(ISupportLocalization localizationSupporter)
        {
            if (localizationSupporter == null)
            {
                throw new ArgumentNullException("localizationSupporter");
            }

            var objectIsAlwaysRegistered = _localizationSupporters
                .FirstOrDefault(x => x.Id == localizationSupporter.Id) != null;

            if (objectIsAlwaysRegistered)
            {
                return;
            }

            _localizationSupporters.Add(localizationSupporter);
        }

        /// <summary>
        /// Deregisters the given object from localization.
        /// </summary>
        /// <param name="localizationSupporter">The object which to deregister from localization.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="localizationSupporter"/> is null.</exception>
        public virtual void DeregisterFromLocalization(ISupportLocalization localizationSupporter)
        {
            if (localizationSupporter == null)
            {
                throw new ArgumentNullException("localizationSupporter");
            }

            var itemToDeregister = _localizationSupporters.FirstOrDefault(x => x.Id == localizationSupporter.Id);

            if (itemToDeregister == null)
            {
                return;
            }

            _localizationSupporters.Remove(itemToDeregister);
        }

        /// <summary>
        /// Returns an instance of <see cref="CultureInfo"/> representing the default culture.
        /// </summary>
        protected virtual CultureInfo ProvideDefaultCulture()
        {
            return CultureInfo.CreateSpecificCulture("de-DE");
        }

        /// <summary>
        /// Raises the <see cref="CultureChanged"/> event.
        /// </summary>
        protected virtual void OnCultureChanged()
        {
            var handler = _cultureChangedEventHandler;

            if (handler == null)
            {
                return;
            }

            handler(this, new CultureChangedEventArgs(CurrentCulture));
        }
    }
}