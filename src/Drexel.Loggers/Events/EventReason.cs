using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a localizable log event reason.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class EventReason : LocalizableString<EventReason>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventReason"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventReason(string invariant)
            : base(invariant)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventReason"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventReason(string localization, CultureInfo culture)
            : base(localization, culture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventReason"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventReason(IReadOnlyDictionary<CultureInfo, string> localizations, CultureInfo preferredCulture)
            : base(localizations, preferredCulture)
        {
        }

        [return: System.Diagnostics.CodeAnalysis.NotNullIfNotNull("value")]
        public static implicit operator EventReason?(string? value)
        {
            if (value is null)
            {
                return null;
            }
            else
            {
                return new EventReason(value);
            }
        }
    }
}
