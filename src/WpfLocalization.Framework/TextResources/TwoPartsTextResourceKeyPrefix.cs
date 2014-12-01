namespace WpfLocalization.Framework.TextResources
{
    /// <summary>
    /// Defines constants representing prefixes of the 'key'-parts of key-value-pairs contained in text resource
    /// files consisting of two parts separated by an underscore where the first part is a category and the second part
    /// an identifier like 'SomeCategory_SomeIdentifier'.
    /// </summary>
    public enum TwoPartsTextResourceKeyPrefix
    {
        /// <summary>
        /// The prefix used for keys of items with values representing messages used by the application.
        /// </summary>
        ApplicationMessages,

        /// <summary>
        /// The prefix used for keys of items with values representing often used words.
        /// </summary>
        OftenUsedWords,

        /// <summary>
        /// The prefix used for keys of items with values representing often used phrases.
        /// </summary>
        OftenUsedPhrases,

        /// <summary>
        /// The prefix used for keys of items with values representing shortcuts for application commands.
        /// </summary>
        Shortcuts
    }
}
