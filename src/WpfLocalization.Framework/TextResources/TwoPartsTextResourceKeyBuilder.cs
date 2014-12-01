using System;
using System.Globalization;

namespace WpfLocalization.Framework.TextResources
{
    /// <summary>
    /// Helper class for creating keys consisting of two parts (prefix and identifier separated by an underscore) that
    /// are used in key-value-pairs of text resource files.
    /// </summary>
    public class TwoPartsTextResourceKeyBuilder
    {
        /// <summary>
        /// Saves the prefix of the key. This field is read-only.
        /// </summary>
        private readonly TwoPartsTextResourceKeyPrefix _prefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoPartsTextResourceKeyBuilder"/> with the given member of the
        /// <see cref="TwoPartsTextResourceKeyPrefix"/> enumeration.
        /// </summary>
        /// <param name="prefix">A member of the <see cref="TwoPartsTextResourceKeyPrefix"/> enumeration.</param>
        private TwoPartsTextResourceKeyBuilder(TwoPartsTextResourceKeyPrefix prefix)
        {
            _prefix = prefix;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        private string Identifier { get; set; }

        /// <summary>
        /// Returns a new instance of the <see cref="TwoPartsTextResourceKeyBuilder"/> class that was initialized with
        /// the given member of the <see cref="TwoPartsTextResourceKeyPrefix"/> enumeration.
        /// </summary>
        /// <param name="prefix">A member of the <see cref="TwoPartsTextResourceKeyPrefix"/> enumeration.</param>
        /// <returns></returns>
        public static TwoPartsTextResourceKeyBuilder WithNewForPrefix(TwoPartsTextResourceKeyPrefix prefix)
        {
            if (!Enum.IsDefined(typeof (TwoPartsTextResourceKeyPrefix), prefix))
            {
                throw new ArgumentOutOfRangeException("prefix");
            }
            
            return new TwoPartsTextResourceKeyBuilder(prefix);
        }

        /// <summary>
        /// Sets the identifier part.
        /// </summary>
        /// <param name="identifier">The part of the key representing the identifier.</param>
        /// <returns>
        /// A string containing the full key consisting of the prefix and the identifier separated by an underscore.
        /// </returns>
        public string WithIdentifier(string identifier)
        {
            Identifier = identifier;
            
            return ToString();
        }

        /// <summary>
        /// Returns a string that represents the current instance of the <see cref="TwoPartsTextResourceKeyBuilder"/>
        /// class.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return String.Format(
                CultureInfo.InvariantCulture, 
                "{0}_{1}", 
                _prefix, 
                String.IsNullOrWhiteSpace(Identifier) ? "[ not set ]" : Identifier);
        }
    }
}