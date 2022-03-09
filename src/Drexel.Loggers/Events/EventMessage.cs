using System;
using System.Collections.Generic;
using System.Globalization;

// TODO: There's no way for a caller to handle the case where the message doesn't have a localized string in the
// specified culture? If they supply a message in the invariant culture, we'll silently fall back to it, giving them no
// chance to handle it themselves? (Ex. they want to emit their own default message, or they want to omit serializing
// the message, etc.)
// TODO: Add unit tests
// TODO: Check for typos before copying for reasons/suggestions
throw new NotImplementedException();

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a localizable log event message.
    /// </summary>
    /// <remarks>
    /// When comparing <see cref="EventMessage"/> instances, the invariant culture fallback message is used when
    /// culture information is not available. All localizable messages should have an invariant culture fallback
    /// message to avoid unexpected exceptions at runtime.
    /// </remarks>
    public sealed class EventMessage : IEquatable<EventMessage>
    {
        private static readonly IReadOnlyDictionary<CultureInfo, string> EmptyLocalizations =
            new Dictionary<CultureInfo, string>();

        private readonly string? invariant;
        private readonly IReadOnlyDictionary<CultureInfo, string> localizations;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventMessage"/> class.
        /// </summary>
        /// <param name="invariant">
        /// The invariant culture fallback message.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="invariant"/> is <see langword="null"/>.
        /// </exception>
        public EventMessage(string invariant)
        {
            this.invariant = invariant ?? throw new ArgumentNullException(nameof(invariant));
            this.localizations = EmptyLocalizations;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventMessage"/> class.
        /// </summary>
        /// <param name="localizations">
        /// The localized representations of this message.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="localizations"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// <paramref name="localizations"/> should include a localized messge for the
        /// <see cref="CultureInfo.InvariantCulture"/> to avoid runtime errors when comparing
        /// <see cref="EventMessage"/> instances.
        /// </remarks>
        public EventMessage(IReadOnlyDictionary<CultureInfo, string> localizations)
        {
            this.invariant = null;
            this.localizations = localizations ?? throw new ArgumentNullException(nameof(localizations));
        }

        /// <summary>
        /// Returns a value indicating whether <paramref name="left"/> and <paramref name="right"/> are both
        /// <see langword="null"/>, or have equal messages in the invariant culture.
        /// </summary>
        /// <param name="left">
        /// The left <see cref="EventMessage"/>.
        /// </param>
        /// <param name="right">
        /// The right <see cref="EventMessage"/>.
        /// /param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are both
        /// <see langword="null"/>, or have the same message in the invariant culture; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <paramref name="left"/> and <paramref name="right"/> are non-<see langword="null"/>, but
        /// <param name="left"/> or <paramref name="right"/> does not have a message in the invariant culture.
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
        /// <see langword="null"/>, or <paramref name="left"/> and <paramref name="right"/> have different messages in
        /// the invariant culture.
        /// </summary>
        /// <param name="left">
        /// The left <see cref="EventMessage"/>.
        /// </param>
        /// <param name="right">
        /// The right <see cref="EventMessage"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if only one of <paramref name="left"/> and <paramref name="right"/> is
        /// <see langword="null"/>, or <paramref name="left"/> and <paramref name="right"/> have different messages in
        /// the invariant culture; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <paramref name="left"/> and <paramref name="right"/> are non-<see langword="null"/>, but
        /// <paramref name="left"/> or <paramref name="right"/> does not have a message in the invariant culture.
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
        /// message as this instance in the invariant culture.
        /// </summary>
        /// <param name="other">
        /// The <see cref="EventMessage"/> to compare this instance with.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="other"/> is non-<see langword="null"/> and has the same message
        /// as this instance in the invariant culture; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when this instance does not have a message in the invariant culture, or <paramref name="other"/> is
        /// non-<see langword="null"/> and does not have a message in the invariant culture.
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
        /// message as this instance in the culture specified by <paramref name="cultureInfo"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="EventMessage"/> to compare this instance with.
        /// </param>
        /// <param name="cultureInfo">
        /// When non-<see langword="null"/>, the culture to use when performing the comparison. If a
        /// <see cref="EventMessage"/> instance does not have a message in the specified culture, the message in the
        /// invariant culture will be used. When falling back to the invariant culture, If a <see cref="EventMessage"/>
        /// instance does not have a message in the invariant culture, a <see cref="InvalidOperationException"/> will
        /// be thrown. When <see langword="null"/>, the invariant culture will be used.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="other"/> is non-<see langword="null"/> and has the same message
        /// as this instance in the culture specified by <paramref name="cultureInfo"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when falling back to the invariant message if the <see cref="EventMessage"/> instance for which the
        /// fallback occurred does not have a message in the invariant culture.
        /// </exception>
        public bool Equals(EventMessage? other, CultureInfo? cultureInfo)
        {
            if (other is null)
            {
                return false;
            }
            else
            {
                return this.ToString(cultureInfo) == other.ToString(cultureInfo);
            }
        }

        /// <summary>
        /// Returns a value indicating whether <paramref name="obj"/> is a reference to a non-<see langword="null"/>
        /// <see cref="EventMessage"/> and, if so, whether this instance and <paramref name="obj"/> have the same
        /// message in the invariant culture.
        /// </summary>
        /// <param name="obj">
        /// The <see langword="object"/> to compare this instance with.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="obj"/> is a reference to a non-<see langword="null"/>
        /// <see cref="EventMessage"/> instance with the same message as this instance in the invariant culture.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when falling back to the invariant message if the <see cref="EventMessage"/> instance for which the
        /// fallback occurred does not have a message in the invariant culture.
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
        /// of the message in the invariant culture.
        /// </summary>
        /// <returns>
        /// The hash code for this instance.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when this <see cref="EventMessage"/> instance does not have a message in the invariant culture.
        /// </exception>
        public override int GetHashCode() => this.ToString().GetHashCode();

        /// <summary>
        /// Returns the message of this <see cref="EventMessage"/> instance in the invariant culture.
        /// </summary>
        /// <returns>
        /// The message of this <see cref="EventMessage"/> instance in the invariant culture.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when this <see cref="EventMessage"/> instance does not have a message in the invariant culture.
        /// </exception>
        public override string ToString() => this.ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// Returns the message of this <see cref="EventMessage"/> instance in the culture specified by
        /// <paramref name="culture"/>.
        /// </summary>
        /// <param name="culture">
        /// When non-<see langword="null"/>, the culture of the message to return. If this <see cref="EventMessage"/>
        /// instance does not have a message in the specified culture, the message in the invariant culture will be
        /// used. When falling back to the invariant culture, If a <see cref="EventMessage"/> instance does not have a
        /// message in the invariant culture, a <see cref="InvalidOperationException"/> will be thrown. When
        /// <see langword="null"/>, the invariant culture will be used.
        /// </param>
        /// <returns>
        /// The message of this <see cref="EventMessage"/> instance in the invariant culture.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when returning the message of this <see cref="EventMessage"/> instance in the invariant culture if
        /// this <see cref="EventMessage"/> instance does not have a message in the invariant culture.
        /// </exception>
        public string ToString(CultureInfo? culture)
        {
            if (this.localizations.TryGetValue(culture ?? CultureInfo.InvariantCulture, out string message))
            {
                return message;
            }
            else
            {
                return this.invariant
                    ?? throw new InvalidOperationException("No invariant culture message available.");
            }
        }
    }
}
