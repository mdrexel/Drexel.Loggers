﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Drexel.Loggers
{
    /// <summary>
    /// Represents a localizable string.
    /// </summary>
    /// <typeparam name="TDerived">
    /// The derived type of this instance.
    /// </typeparam>
    [DebuggerDisplay("{ToString(),nq}")]
    public abstract class LocalizableString<TDerived> : ILocalizableString<LocalizableString<TDerived>, TDerived>
        where TDerived : LocalizableString<TDerived>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableString{TDerived}"/> class.
        /// </summary>
        /// <param name="invariant">
        /// The localization in the invariant culture.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="invariant"/> is <see langword="null"/>.
        /// </exception>
        private protected LocalizableString(string invariant)
        {
            if (invariant is null)
            {
                throw new ArgumentNullException(nameof(invariant));
            }

            this.PreferredCulture = CultureInfo.InvariantCulture;
            this.Localizations = ReadOnlyDictionary.Create(
                new Dictionary<CultureInfo, string>(1)
                {
                    [CultureInfo.InvariantCulture] = invariant,
                });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableString{TDerived}"/> class.
        /// </summary>
        /// <param name="localization">
        /// The localization in the culture specified by <paramref name="culture"/>.
        /// </param>
        /// <param name="culture">
        /// The culture of the localization.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="localization"/> or <paramref name="culture"/> is <see langword="null"/>.
        /// </exception>
        private protected LocalizableString(string localization, CultureInfo culture)
        {
            if (localization is null)
            {
                throw new ArgumentNullException(nameof(localization));
            }

            this.PreferredCulture = culture ?? throw new ArgumentNullException(nameof(culture));
            this.Localizations = ReadOnlyDictionary.Create(
                new Dictionary<CultureInfo, string>(1)
                {
                    [culture] = localization,
                });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableString{TDerived}"/> class.
        /// </summary>
        /// <param name="localizations">
        /// The localizations. The supplied dictionary must contain a localization for the culture specified by
        /// <paramref name="localizations"/>.
        /// </param>
        /// <param name="preferredCulture">
        /// The preferred culture.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="localizations"/> or <paramref name="preferredCulture"/> is
        /// <see langword="null"/>.
        /// </exception>
        private protected LocalizableString(
            IReadOnlyDictionary<CultureInfo, string> localizations,
            CultureInfo preferredCulture)
        {
            this.Localizations = ReadOnlyDictionary.Create(
                localizations ?? throw new ArgumentNullException(nameof(localizations)));
            this.PreferredCulture = preferredCulture ?? throw new ArgumentNullException(nameof(preferredCulture));
        }

        /// <inheritdoc/>
        public CultureInfo PreferredCulture { get; protected set; }

        /// <inheritdoc/>
        public IReadOnlyDictionary<CultureInfo, string> Localizations { get; protected set; }

        /// <summary>
        /// Returns a value indicating whether <paramref name="left"/> and <paramref name="right"/> are both
        /// <see langword="null"/>, or have the string in their preferred cultures.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are both
        /// <see langword="null"/>, or have the same string in their preferred cultures; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public static bool operator ==(LocalizableString<TDerived>? left, LocalizableString<TDerived>? right)
        {
            if (left is null)
            {
                return right is null;
            }
            else
            {
                return left.Equals(right);
            }
        }

        /// <summary>
        /// Returns a value indicating whether exactly one of <paramref name="left"/> and <paramref name="right"/> is
        /// <see langword="null"/>, or <paramref name="left"/> and <paramref name="right"/> do not have the same
        /// string in their preferred cultures.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if exactly one of <paramref name="left"/> and <paramref name="right"/> is
        /// <see langword="null"/>, or <paramref name="left"/> and <paramref name="right"/> have the same string in
        /// their preferred cultures; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(LocalizableString<TDerived>? left, LocalizableString<TDerived>? right)
        {
            if (left is null)
            {
                return !(right is null);
            }
            else
            {
                return !left.Equals(right);
            }
        }

        /// <inheritdoc/>
        public int CompareTo(TDerived? other, CultureInfo? culture, CompareOptions options = CompareOptions.None)
        {
            if (other is null)
            {
                return 1;
            }

            string left = this.ToString(culture);
            string right = other.ToString(culture);

            return (culture ?? this.PreferredCulture).CompareInfo.Compare(left, right, options);
        }

        /// <inheritdoc/>
        public int CompareTo(TDerived? other) => this.CompareTo(other, null);

        /// <inheritdoc/>
        public bool Equals(TDerived? other, CultureInfo? culture, CompareOptions options = CompareOptions.None)
        {
            if (other is null)
            {
                return false;
            }

            string left = this.ToString(culture);
            string right = other.ToString(culture);

            return (culture ?? this.PreferredCulture).CompareInfo.Compare(left, right, options) == 0;
        }

        /// <inheritdoc/>
        public bool Equals(TDerived? other) => this.Equals(other, null);

        /// <inheritdoc/>
        public bool Equals(object? obj, CultureInfo? culture, CompareOptions options = CompareOptions.None)
        {
            if (obj is TDerived other)
            {
                return this.Equals(other, culture, options);
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is TDerived other)
            {
                return this.Equals(other, null);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the hash code of this instance's preferred localization.
        /// </summary>
        /// <returns>
        /// The hash code of this instance's preferred localization.
        /// </returns>
        public override int GetHashCode() => this.ToString().GetHashCode();

        /// <inheritdoc/>
        public string ToString(CultureInfo? culture)
        {
            if (culture is null)
            {
                return this.Localizations[this.PreferredCulture];
            }
            else if (this.Localizations.TryGetValue(culture, out string value))
            {
                return value;
            }
            else
            {
                return this.Localizations[this.PreferredCulture];
            }
        }

        /// <summary>
        /// Returns the localization of this instance in this instance's preferred culture.
        /// </summary>
        /// <returns>
        /// The localization of this instance in this instance's preferred culture.
        /// </returns>
        public override string ToString() => this.ToString(null);
    }
}
