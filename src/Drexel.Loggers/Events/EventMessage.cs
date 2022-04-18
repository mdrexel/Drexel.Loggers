using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a localizable log event message.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class EventMessage : LocalizableString<EventMessage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventMessage"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventMessage(string invariant)
            : base(invariant)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventMessage"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventMessage(string localization, CultureInfo culture)
            : base(localization, culture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventMessage"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventMessage(IReadOnlyDictionary<CultureInfo, string> localizations, CultureInfo preferredCulture)
            : base(localizations, preferredCulture)
        {
        }

        /// <inheritdoc/>
        [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull("value")]
        public static implicit operator EventMessage?(string? value)
        {
            if (value is null)
            {
                return null;
            }
            else
            {
                return new EventMessage(value);
            }
        }
    }
}
