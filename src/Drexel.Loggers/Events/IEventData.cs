namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents the data associated with an occurrence of an event.
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// Gets the reason associated with the event occurrence, if one exists.
        /// </summary>
        /// <value>
        /// The reason associated with the event occurrence, or <see langword="null"/> if no reason exists.
        /// </value>
        EventReason? Reason { get; }

        /// <summary>
        /// Gets the suggestions associated with the event occurrence, if any exist.
        /// </summary>
        /// <value>
        /// The suggestions associated with the event occurrence, or <see langword="null"/> if no suggestions exist.
        /// </value>
        EventSuggestions? Suggestions { get; }

        /// <summary>
        /// Gets the event parameters associated with the event occurrence, if any exist.
        /// </summary>
        /// <value>
        /// The parameters associated with the event occurrence, or <see langword="null"/> if no parameters exist.
        /// </value>
        EventParameters? EventParameters { get; }

        /// <summary>
        /// Gets the inner events associated with the event occurrence, if any exist.
        /// </summary>
        /// <value>
        /// The inner evers associated with the event occurrence, or <see langword="null"/> if no inner events exist.
        /// </value>
        EventInnerEvents? InnerEvents { get; }
    }

    /// <summary>
    /// Represents the data associated with an occurrence of an event.
    /// </summary>
    /// <typeparam name="TUserDefined">
    /// The user-defined type containing additional data associated with an event occurrence.
    /// </typeparam>
    public interface IEventData<out TUserDefined> : IEventData
    {
        /// <summary>
        /// Gets the user-defined data associated with the event occurrence.
        /// </summary>
        TUserDefined UserDefined { get; }
    }
}
