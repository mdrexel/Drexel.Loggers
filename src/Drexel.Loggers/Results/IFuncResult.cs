using Drexel.Loggers.Events;

namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents the result of an operation that returns a value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of value returned by the operation.
    /// </typeparam>
    /// 
    public interface IFuncResult<out T> : IActionResult, IReadOnlyValueContainer<T>
    {
    }

    /// <summary>
    /// Represents the result of an operation that returns a value with strongly-typed events.
    /// </summary>
    /// <typeparam name="TEvent">
    /// The type of event returned by the operation.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// The type of value returned by the operation.
    /// </typeparam>
    public interface IFuncResult<out TEvent, out TValue> : IFuncResult<TValue>, ITryResult<TEvent>
        where TEvent : ILogEvent
    {
    }
}
