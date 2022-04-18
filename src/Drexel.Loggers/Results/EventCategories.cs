using System;

namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents categories of event types.
    /// </summary>
    [Flags]
    public enum EventCategories
    {
        /// <summary>
        /// Represents the category of all event types.
        /// </summary>
        All = Errors | Informationals,

        /// <summary>
        /// Represents the category of error events.
        /// </summary>
        Errors = 0b01,

        /// <summary>
        /// Represents the category of informational events.
        /// </summary>
        Informationals = 0b10,

        /// <summary>
        /// Represents the category of no event types.
        /// </summary>
        None = 0b00,
    }
}
