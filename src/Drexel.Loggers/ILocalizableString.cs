using System;
using System.Collections.Generic;
using System.Globalization;

namespace Drexel.Loggers
{
    /// <summary>
    /// Represents a localizable string. A localizable string is a mapping between cultures and strings, where all
    /// strings have the same semantic meaning.
    /// </summary>
    /// <typeparam name="TSelf">
    /// A type that implements this interface using the same type parameters as the implementor of this interface.
    /// </typeparam>
    /// <typeparam name="TDerived">
    /// A type that derives from <typeparamref name="TSelf"/>.
    /// </typeparam>
    public interface ILocalizableString<TSelf, TDerived> : IEquatable<TDerived?>, IComparable<TDerived?>
        where TSelf : ILocalizableString<TSelf, TDerived>
        where TDerived : class, TSelf
    {
        /// <summary>
        /// Gets the preferred culture of this instance.
        /// </summary>
        /// <remarks>
        /// The preferred culture is the culture that will be used when an operation performed on this instance
        /// requires culture information, but the operation does not specify (or is unable to specify) a culture.
        /// </remarks>
        CultureInfo PreferredCulture { get; }

        /// <summary>
        /// Gets the localizations of this instance.
        /// </summary>
        /// <value>
        /// A dictionary of localizations. A localization is guaranteed to exist for for this instance's
        /// <see cref="PreferredCulture"/>.
        /// </value>
        IReadOnlyDictionary<CultureInfo, string> Localizations { get; }

        /// <summary>
        /// Compares this instance with the instance specified by <paramref name="other"/> and returns an integer that
        /// indicates whether this instance precedes, follows, or occurs in the same position in the sort order as the
        /// other instance.
        /// </summary>
        /// <param name="other">
        /// The instance to compare this instance to.
        /// </param>
        /// <param name="culture">
        /// The culture to perform the comparison under. If <see langword="null"/>, the instances will be compared
        /// using their preferred cultures. If an instance does not have a localization in the specified culture, the
        /// comparison will use the localization in that instance's preferred culture.
        /// </param>
        /// <param name="options">
        /// An optional value indicating the options that should be used by the comparison.
        /// </param>
        /// <returns>
        /// <list type="bullet">
        /// <item>A negative value, when this instance precedes the specified instance.</item>
        /// <item>Zero, when this instance occurs in the same position as the specified instance.</item>
        /// <item>A positive value, when this instance follows the specified instance, or <paramref name="other"/> is
        /// <see langword="null"/>.</item>
        /// </list>
        /// </returns>
        int CompareTo(TDerived? other, CultureInfo? culture, CompareOptions options = CompareOptions.None);

        /// <summary>
        /// Returns a value indicating whether the instance specified by <paramref name="obj"/> is equal to this
        /// instance in the culture specified by <paramref name="culture"/>.
        /// </summary>
        /// <param name="obj">
        /// The instance to compare this instance to.
        /// </param>
        /// <param name="culture">
        /// The culture to perform the comparison under. If <see langword="null"/>, the instances will be compared
        /// using their preferred cultures. If an instance does not have a localization in the specified culture, the
        /// comparison will use the localization in that instance's preferred culture.
        /// </param>
        /// <param name="options">
        /// An optional value indicating the options that should be used by the comparison.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if this instance is equal to the object specified by <paramref name="obj"/> in the
        /// culture specified by <paramref name="culture"/> according to the specified options; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        bool Equals(TDerived? other, CultureInfo? culture, CompareOptions options = CompareOptions.None);

        /// <summary>
        /// Returns a value indicating whether the object specified by <paramref name="obj"/> is equal to this instance
        /// in the culture specified by <paramref name="culture"/>.
        /// </summary>
        /// <param name="obj">
        /// The object to compare this instance to.
        /// </param>
        /// <param name="culture">
        /// The culture to perform the comparison under. If <see langword="null"/>, the instances will be compared
        /// using their preferred cultures. If an instance does not have a localization in the specified culture, the
        /// comparison will use the localization in that instance's preferred culture.
        /// </param>
        /// <param name="options">
        /// An optional value indicating the options that should be used by the comparison.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if this instance is equal to the object specified by <paramref name="obj"/> in the
        /// culture specified by <paramref name="culture"/> according to the specified options; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        bool Equals(object? obj, CultureInfo? culture, CompareOptions options = CompareOptions.None);

        /// <summary>
        /// Returns the localization of this instance in the culture specified by <paramref name="culture"/>, or the
        /// localization in the preferred culture if <paramref name="culture"/> is <see langword="null"/> or this
        /// instance does not have a localization defined for that culture.
        /// </summary>
        /// <param name="culture">
        /// When non-<see langword="null"/>, the culture of the localization to return. If this instance does not have
        /// a localization in the specified culture, the localization in the preferred culture will be used. When
        /// <see langword="null"/>, the preferred culture will be used.
        /// </param>
        /// <returns>
        /// If <paramref name="culture"/> is non-<see langword="null"/> and this instance has a localization in the
        /// specified culture, the localization of this instance in the specified culture. If
        /// <paramref name="culture"/> is non-<see langword="null"/> and this instance does not have a localization in
        /// the specified culture, the localization of this instance in the preferred culture. If
        /// <paramref name="culture"/> is <see langword="null"/>, the localization of this instance in the preferred
        /// culture.
        /// </returns>
        string ToString(CultureInfo? culture);
    }
}
