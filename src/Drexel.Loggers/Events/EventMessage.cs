using System;
using System.Collections.Generic;
using System.Globalization;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a localizable log event message.
    /// </summary>
    public sealed class EventMessage : IEquatable<EventMessage>
    {
        private readonly CultureInfo preferredCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventMessage"/> class.
        /// </summary>
        /// <param name="invariant">
        /// The event message associated with the <see cref="CultureInfo.InvariantCulture"/> culture.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="invariant"/> is <see langword="null"/>.
        /// </exception>
        public EventMessage(string invariant)
        {
            if (invariant is null)
            {
                throw new ArgumentNullException(nameof(invariant));
            }

            this.preferredCulture = CultureInfo.InvariantCulture;
            this.Localizations = ReadOnlyDictionary.Create(
                new Dictionary<CultureInfo, string>(1)
                {
                    [CultureInfo.InvariantCulture] = invariant,
                });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventMessage"/> class.
        /// </summary>
        /// <param name="message">
        /// The event message associated with the culture specified by <paramref name="culture"/>.
        /// </param>
        /// <param name="culture">
        /// The culture of the specified <paramref name="message"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="message"/> or <paramref name="culture"/> is <see langword="null"/>.
        /// </exception>
        public EventMessage(string message, CultureInfo culture)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.preferredCulture = culture ?? throw new ArgumentNullException(nameof(culture));
            this.Localizations = ReadOnlyDictionary.Create(
                new Dictionary<CultureInfo, string>(1)
                {
                    [culture] = message,
                });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventMessage"/> class.
        /// </summary>
        /// <param name="localizations">
        /// The localized representations of this message.
        /// </param>
        /// <param name="preferredCulture">
        /// The culture to use when a message is requested but the request does not include a<see cref="CultureInfo"/>.
        /// If <paramref name="preferredCulture"/> is <see langword="null"/>,
        /// <see cref="CultureInfo.InvariantCulture"/> will be used.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="localizations"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// <paramref name="localizations"/> should include a localized messge for the the culture specified by
        /// <param name="preferredCulture"/> (or, if <paramref name="preferredCulture"/> is <see langword="null"/>,
        /// <see cref="CultureInfo.InvariantCulture"/>) to avoid runtime errors when comparing
        /// <see cref="EventMessage"/> instances.
        /// </remarks>
        public EventMessage(
            IReadOnlyDictionary<CultureInfo, string> localizations,
            CultureInfo? preferredCulture = null)
        {
            this.preferredCulture = preferredCulture ?? CultureInfo.InvariantCulture;
            this.Localizations = ReadOnlyDictionary.Create(
                localizations ?? throw new ArgumentNullException(nameof(localizations)));
        }

        /// <summary>
        /// Gets the localized messages of this <see cref="EventMessage"/> instance.
        /// </summary>
        public IReadOnlyDictionary<CultureInfo, string> Localizations { get; }

        /// <summary>
        /// Returns a value indicating whether <paramref name="left"/> and <paramref name="right"/> are both
        /// <see langword="null"/>, or have the message in their preferred cultures.
        /// </summary>
        /// <param name="left">
        /// The left <see cref="EventMessage"/>.
        /// </param>
        /// <param name="right">
        /// The right <see cref="EventMessage"/>.
        /// /param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are both
        /// <see langword="null"/>, or have the same preferred message in their preferred cultures; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <paramref name="left"/> and <paramref name="right"/> are non-<see langword="null"/>, but
        /// either <param name="left"/> or <paramref name="right"/> does not have a message in their preferred culture.
        /// </exception>
        public static bool operator ==(EventMessage? left, EventMessage? right)
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
        /// Returns a value indicating whether only one of <paramref name="left"/> and <paramref name="right"/> is
        /// <see langword="null"/>, or <paramref name="left"/> and <paramref name="right"/> do not have the same
        /// message in their preferred cultures.
        /// </summary>
        /// <param name="left">
        /// The left <see cref="EventMessage"/>.
        /// </param>
        /// <param name="right">
        /// The right <see cref="EventMessage"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if only one of <paramref name="left"/> and <paramref name="right"/> is
        /// <see langword="null"/>, or <paramref name="left"/> and <paramref name="right"/> have the same message in
        /// their preferred cultures; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <paramref name="left"/> and <paramref name="right"/> are non-<see langword="null"/>, but
        /// either <param name="left"/> or <paramref name="right"/> does not have a message in their preferred culture.
        /// </exception>
        public static bool operator !=(EventMessage? left, EventMessage? right)
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

        /// <summary>
        /// Returns a value indicating whether <paramref name="other"/> is non-<see langword="null"/> and has the same
        /// message as this instance in their preferred cultures.
        /// </summary>
        /// <param name="other">
        /// The <see cref="EventMessage"/> to compare this instance with.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="other"/> is non-<see langword="null"/> and has the same message
        /// as this instance in their preferred cultures; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <paramref name="other"/> is non-<see langword="null"/> and either this instance or
        /// <paramref name="other"/> does not have a message in its preferred culture.
        /// </exception>
        public bool Equals(EventMessage? other)
        {
            if (other is null)
            {
                return false;
            }
            else
            {
                return this.ToString() == other.ToString();
            }
        }

        /// <summary>
        /// Returns a value indicating whether <paramref name="other"/> is non-<see langword="null"/> and has the same
        /// message as this instance in the culture specified by <paramref name="culture"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="EventMessage"/> to compare this instance with.
        /// </param>
        /// <param name="culture">
        /// When non-<see langword="null"/>, the culture to use when performing the comparison. If a
        /// <see cref="EventMessage"/> instance does not have a message in the specified culture, the message in the
        /// preferred culture will be used. When falling back to the preferred culture, If a <see cref="EventMessage"/>
        /// instance does not have a message in the preferred culture, a <see cref="InvalidOperationException"/> will
        /// be thrown. When <see langword="null"/>, the preferred culture will be used.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="other"/> is non-<see langword="null"/> and has the same message
        /// as this instance in the culture specified by <paramref name="culture"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when falling back to the preferred culture if the <see cref="EventMessage"/> instance for which the
        /// fallback occurred does not have a message in the preferred culture.
        /// </exception>
        public bool Equals(EventMessage? other, CultureInfo? culture)
        {
            if (other is null)
            {
                return false;
            }
            else
            {
                return this.ToString(culture) == other.ToString(culture);
            }
        }

        /// <summary>
        /// Returns a value indicating whether <paramref name="obj"/> is a reference to a non-<see langword="null"/>
        /// <see cref="EventMessage"/> and, if so, whether this instance and <paramref name="obj"/> have the same
        /// message in the their preferred cultures.
        /// </summary>
        /// <param name="obj">
        /// The <see langword="object"/> to compare this instance with.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="obj"/> is a reference to a non-<see langword="null"/>
        /// <see cref="EventMessage"/> instance with the same message as this instance in their preferred cultures.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when falling back to the preferred culture if the <see cref="EventMessage"/> instance for which the
        /// fallback occurred does not have a message in the preferred culture.
        /// </exception>
        public override bool Equals(object? obj)
        {
            if (obj is EventMessage other)
            {
                return this.Equals(other);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the hash code for this instance. The hash code of this <see cref="EventMessage"/> is the hash code
        /// of the message in the preferred culture.
        /// </summary>
        /// <returns>
        /// The hash code for this instance.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when this <see cref="EventMessage"/> instance does not have a message in the preferred culture.
        /// </exception>
        public override int GetHashCode() => this.ToString().GetHashCode();

        /// <summary>
        /// Returns the message of this <see cref="EventMessage"/> instance in the preferred culture.
        /// </summary>
        /// <returns>
        /// The message of this <see cref="EventMessage"/> instance in the preferred culture.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when this <see cref="EventMessage"/> instance does not have a message in the preferred culture.
        /// </exception>
        public override string ToString() => this.ToString(this.preferredCulture);

        /// <summary>
        /// Returns the message of this <see cref="EventMessage"/> instance in the culture specified by
        /// <paramref name="culture"/>.
        /// </summary>
        /// <param name="culture">
        /// When non-<see langword="null"/>, the culture of the message to return. If this <see cref="EventMessage"/>
        /// instance does not have a message in the specified culture, the message in the preferred culture will be
        /// used. When falling back to the preferred culture, if this <see cref="EventMessage"/> instance does not have
        /// a message in the preferred culture, a <see cref="InvalidOperationException"/> will be thrown. When
        /// <see langword="null"/>, the preferred culture will be used.
        /// </param>
        /// <returns>
        /// The message of this <see cref="EventMessage"/> instance in the specified culture; or, if
        /// <paramref name="culture"/> is <see langword="null"/>, the message of this <see cref="EventMessage"/>
        /// instance in the preferred culture.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when falling back to the preferred culture, but this instance does not include a message in the
        /// preferred culture.
        /// </exception>
        public string ToString(CultureInfo? culture)
        {
            string message;
            if (culture is null)
            {
                if (this.Localizations.TryGetValue(this.preferredCulture, out message))
                {
                    return message;
                }
            }
            else if (this.Localizations.TryGetValue(culture, out message))
            {
                return message;
            }
            else if (this.Localizations.TryGetValue(this.preferredCulture, out message))
            {
                return message;
            }

            throw new InvalidOperationException("No message in the preferred culture available.");
        }
    }
}
