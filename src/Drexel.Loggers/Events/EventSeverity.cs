namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Indicates the severity of an event.
    /// </summary>
    public enum EventSeverity
    {
        /// <summary>
        /// Indicates the event represents a critical error.
        /// </summary>
        /// <remarks>
        /// A critical error typically indicates a loss of system functionality that persists across operations. For
        /// example, a dependent service being unavailable would be a critical event, because operations are expected
        /// to fail until the service is restored.
        /// </remarks>
        Critical,

        /// <summary>
        /// Indicates the event represents an error.
        /// </summary>
        /// <remarks>
        /// An error typically indicates a failed or prematurely aborted operation.
        /// </remarks>
        Error,

        /// <summary>
        /// Indicates the event represents a warning.
        /// </summary>
        /// <remarks>
        /// A warning typically indicates an operation was allowed to continue, but the result of the operation may
        /// deviate from what could be reasonably expected.
        /// </remarks>
        Warning,

        /// <summary>
        /// Indicates the event represents information.
        /// </summary>
        /// <remarks>
        /// An informational event is one which pertains to an operation, but which has no bearing on its success or
        /// failure, and which could be reasonably expected to occur.
        /// </remarks>
        Information,

        /// <summary>
        /// Indicates the event is intended for debug use.
        /// </summary>
        /// <remarks>
        /// This severity can be used for any purpose by a developer, and has no strict definition.
        /// </remarks>
        Debug,
    }
}
