using System;

namespace WpfLocalization.Framework
{
    /// <summary>
    /// Represents the key of an key-value-pair item of a text resource file. This class is an immutable class.
    /// </summary>
    public sealed class TextResourceFileItemKey
    {
        /// <summary>
        /// Backing field for property <see cref="Key"/>. This field is read-only.
        /// </summary>
        private readonly string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextResourceFileItemKey"/> class with the given key.
        /// </summary>
        /// <param name="key">The key of an key-value-pair item of a text resource file.</param>
        /// <exception cref="ArgumentException">When <paramref name="key"/> is null or empty or consist only of 
        /// white-space characters.</exception>
        private TextResourceFileItemKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(ErrorMessages.ArgumentCannotBeNullOrWhiteSpace, "key");
            }

            _key = key;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key
        {
            get { return _key; }
        }

        /// <summary>
        /// Converts the current instance of the <see cref="TextResourceFileItemKey"/> implicit to an instance of
        /// <see cref="string"/>.
        /// </summary>
        /// <param name="value">The instance of <see cref="TextResourceFileItemKey"/> which should be converted.</param>
        /// <returns>The value of property <see cref="Key"/>.</returns>
        public static implicit operator string(TextResourceFileItemKey value)
        {
            return value == null ? null : value.Key;
        }

        /// <summary>
        /// Creates an instance of <see cref="TextResourceFileItemKey"/> which has been initialized with the given key
        /// and which can be used as the value of a member of the <see cref="TextResourceFileItemKeys.Application"/>
        /// class.
        /// </summary>
        /// <param name="key">The key to initialize the instance of <see cref="TextResourceFileItemKey"/> with.</param>
        /// <returns>An instance of <see cref="TextResourceFileItemKey"/> which has been initialized with the given key
        /// and which can be used as the value of a member of the <see cref="TextResourceFileItemKeys.Application"/>
        /// class.</returns>
        public static TextResourceFileItemKey CreateForClassApplication(string key)
        {
            return new TextResourceFileItemKey(GetFullKey("Application", key));
        }

        /// <summary>
        /// Creates an instance of <see cref="TextResourceFileItemKey"/> which has been initialized with the given key
        /// and which can be used as the value of a member of the <see cref="TextResourceFileItemKeys.Shortcuts"/>
        /// class.
        /// </summary>
        /// <param name="key">The key to initialize the instance of <see cref="TextResourceFileItemKey"/> with.</param>
        /// <returns>An instance of <see cref="TextResourceFileItemKey"/> which has been initialized with the given key
        /// and which can be used as the value of a member of the <see cref="TextResourceFileItemKeys.Shortcuts"/>
        /// class.</returns>
        public static TextResourceFileItemKey CreateForClassShortcuts(string key)
        {
            return new TextResourceFileItemKey(GetFullKey("Shortcuts", key));
        }

        /// <summary>
        /// Creates an instance of <see cref="TextResourceFileItemKey"/> which has been initialized with the given key
        /// and which can be used as the value of a member of the <see cref="TextResourceFileItemKeys.OftenUsedWords"/>
        /// class.
        /// </summary>
        /// <param name="key">The key to initialize the instance of <see cref="TextResourceFileItemKey"/> with.</param>
        /// <returns>An instance of <see cref="TextResourceFileItemKey"/> which has been initialized with the given key
        /// and which can be used as the value of a member of the <see cref="TextResourceFileItemKeys.OftenUsedWords"/>
        /// class.</returns>
        public static TextResourceFileItemKey CreateForClassOftenUsedWords(string key)
        {
            return new TextResourceFileItemKey(GetFullKey("OftenUsedWords", key));
        }

        /// <summary>
        /// Creates an instance of <see cref="TextResourceFileItemKey"/> which has been initialized with the given key
        /// and which can be used as the value of a member of the <see cref="TextResourceFileItemKeys.OftenUsePhrases"/>
        /// class.
        /// </summary>
        /// <param name="key">The key to initialize the instance of <see cref="TextResourceFileItemKey"/> with.</param>
        /// <returns>An instance of <see cref="TextResourceFileItemKey"/> which has been initialized with the given key
        /// and which can be used as the value of a member of the <see cref="TextResourceFileItemKeys.OftenUsePhrases"/>
        /// class.</returns>
        public static TextResourceFileItemKey CreateForClassOftenUsePhrases(string key)
        {
            return new TextResourceFileItemKey(GetFullKey("OftenUsePhrases", key));
        }

        /// <summary>
        /// Returns the full key consisting of the given class name and the given key separated by an underscore e. g.
        /// "SomeClass" (class name) and "SomeKey" => "SomeClass_SomeKey".
        /// </summary>
        /// <param name="className">The name of the class containing the given key.</param>
        /// <param name="key">The key.</param>
        /// <returns>A string containing the full key consisting of the class name and the given key separated by an
        /// underscore.</returns>
        private static string GetFullKey(string className, string key)
        {
            return string.Concat(className, "_", key);
        }
    }
}
