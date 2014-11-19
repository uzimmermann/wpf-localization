using System;

namespace WpfLocalization.Framework
{
    /// <summary>
    /// Indicates that a property is localizable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class LocalizableAttribute : Attribute
    {
    }
}