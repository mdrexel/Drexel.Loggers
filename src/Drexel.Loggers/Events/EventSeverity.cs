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
        /// An error typically indicates a failed or prematurely aborted operation. The failure may impact future
        /// operations, but the failures occur in the application domain. (For example, attempting to create a resource
        /// could fail with an error, leaving behind a partially-created resource that blocks future operations.
        /// However, the system could still successfully process operations that do not interact with the
        /// partially-created resource.)
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
        /// Indicates the event represents verbose information.
        /// </summary>
        /// <remarks>
        /// A verbose event is similar to an <see cref="Information"/> event, but which is only expected to be
        /// presented when the user opts in to additional information.
        /// </remarks>
        Verbose,

        /// <summary>
        /// Indicates the event is intended for debug use.
        /// </summary>
        /// <remarks>
        /// This severity can be used for any purpose by a developer, and has no strict definition. It should only be
        /// used for events that do not impact the success or failure of an operation, and which a user should not be
        /// exposed to.
        /// </remarks>
        Debug,
    }
}
