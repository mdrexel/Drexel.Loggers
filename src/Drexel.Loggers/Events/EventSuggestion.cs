using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a localizable log event suggestion.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class EventSuggestion : LocalizableString<EventSuggestion>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventSuggestion"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventSuggestion(string invariant)
            : base(invariant)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSuggestion"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventSuggestion(string localization, CultureInfo culture)
            : base(localization, culture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSuggestion"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventSuggestion(IReadOnlyDictionary<CultureInfo, string> localizations, CultureInfo preferredCulture)
            : base(localizations, preferredCulture)
        {
        }
    }
}
