using System.Threading.Tasks;
using Drexel.Loggers.Events;

namespace Drexel.Loggers
{
    /// <summary>
    /// Represents a logger.
    /// </summary>
    /// <typeparam name="T">
    /// The type of event accepted by the logger.
    /// </typeparam>
    public interface ILogEventLogger<in T> where T : ILogEvent
    {
        /// <summary>
        /// Logs the specified event.
        /// </summary>
        /// <param name="logEvent">
        /// The event to log.
        /// </param>
        void LogEvent(T logEvent);

        /// <summary>
        /// Asynchronously logs the specified event.
        /// </summary>
        /// <param name="logEvent">
        /// The event to log.
        /// </param>
        /// <returns>
        /// A <see cref="ValueTask"/> representing the log operation.
        /// </returns>
        ValueTask LogEventAsync(T logEvent);
    }
}
