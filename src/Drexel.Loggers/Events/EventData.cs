namespace Drexel.Loggers.Events
{
    /// <summary>
    /// A simple implementation of <see cref="IEventData"/>.
    /// </summary>
    public class EventData : IEventData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventData"/> class.
        /// </summary>
        /// <param name="reason">
        /// The reason associated with the event occurrence, if one exists; otherwise, <see langword="null"/>.
        /// </param>
        /// <param name="suggestions">
        /// The suggestions associated with the event occurrence, if any exist; otherwise, <see langword="null"/>.
        /// </param>
        /// <param name="parameters">
        /// The parameters associated with the event occurrence, if any exist; otherwise <see langword="null"/>.
        /// </param>
        /// <param name="innerEvents">
        /// The inner events associated with the event occurrence, if any exist; otherwise, <see langword="null"/>.
        /// </param>
        public EventData(
            EventReason? reason = null,
            EventSuggestions? suggestions = null,
            EventParameters? parameters = null,
            EventInnerEvents? innerEvents = null)
        {
            this.Reason = reason;
            this.Suggestions = suggestions;
            this.EventParameters = parameters;
            this.InnerEvents = innerEvents;
        }

        /// <inheritdoc/>
        public EventReason? Reason { get; }

        /// <inheritdoc/>
        public EventSuggestions? Suggestions { get; }

        /// <inheritdoc/>
        public EventParameters? EventParameters { get; }

        /// <inheritdoc/>
        public EventInnerEvents? InnerEvents { get; }
    }

    /// <summary>
    /// A simple implementation of <see cref="IEventData{T}"/>.
    /// </summary>
    /// <typeparam name="TUserDefined">
    /// The user-defined type containing additional data associated with an event occurrence.
    /// </typeparam>
    public sealed class EventData<TUserDefined> : EventData, IEventData<TUserDefined>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventData{TUserDefined}"/> class.
        /// </summary>
        /// <param name="userDefined">
        /// The user-defined data associated with the event occurrence.
        /// </param>
        /// <param name="reason">
        /// The reason associated with the event occurrence, if one exists; otherwise, <see langword="null"/>.
        /// </param>
        /// <param name="suggestions">
        /// The suggestions associated with the event occurrence, if any exist; otherwise, <see langword="null"/>.
        /// </param>
        /// <param name="parameters">
        /// The parameters associated with the event occurrence, if any exist; otherwise <see langword="null"/>.
        /// </param>
        /// <param name="innerEvents">
        /// The inner events associated with the event occurrence, if any exist; otherwise, <see langword="null"/>.
        /// </param>
        public EventData(
            TUserDefined userDefined,
            EventReason? reason = null,
            EventSuggestions? suggestions = null,
            EventParameters? parameters = null,
            EventInnerEvents? innerEvents = null)
            : base(reason, suggestions, parameters, innerEvents)
        {
            this.UserDefined = userDefined;
        }

        /// <inheritdoc/>
        public TUserDefined UserDefined { get; }
    }
}
