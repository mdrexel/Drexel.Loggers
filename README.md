# Drexel.Loggers
C# library providing logging primitives.

## Example
Declare what events your application can produce:
```csharp
public class Templates
{
    private readonly EventCodeGroup eventCodes = new EventCodeGroup(1234567);

    public LogEventTemplate OverflowDetected { get; } =
        new LogEventTemplate(
            new EventCode(eventCodes, 0, nameof(OverflowDetected)),
            EventSeverity.Error,
            "An overflow was detected.");
```

Declare a method as returning a result:
```csharp
public IFuncResult<int> Add(Templates templates, int left, int right)
{
    FuncResult<int> result = new FuncResult<int>();
    try
    {
        result.SetValue(checked(left + right));
    }
    catch (OverflowException ex)
    {
        result.AddError(templates.OverflowDetected.Create());
    }

    return result;
}
```

Compose more complex results:
```csharp
public IFuncResult<IReadOnlyList<int>> Add(Templates templates, IEnumerable<(int Left, int Right)> pairs)
{
    List<int> sums = new List<int>();
    FuncResult<IReadOnlyList<int>> result = new FuncResult<IReadOnlyList<int>>();
    result.SetValue(sums);

    foreach ((int Left, int Right) pair in pairs)
    {
        if (result.AddResult(
            Add(templates, pair.Left, pair.Right),
            out int sum))
        {
            sums.Add(sum);
        }
    }

    return result;
}
```

## Purpose
A commonly desired application feature is for the application to log its activities to some persistent event store.
This can be as simple as a text file, or a more complex solution that addresses concerns like traceability. These logs
are used to trace the behavior of the application, most often when unexpected behavior occurs. These logging mechanisms
facilitate this by providing to application developers a mechanism for describing the state of the application.
However, the logging mechanism does not require the state described by the developer to match the actual state of the
application. Additionally, context around an operation is often only available in an unstructured form (or lost
entirely) based on what information a developer decided was appropriate to log, and when it was appropriate to log it.

`Drexel.Loggers` attempts to address these issues by making logging part of the
[control flow](https://en.wikipedia.org/wiki/Control_flow) of the application. By making logging part of the control
flow, it ensure developers do not lose context information, because any information they wish to log must roll up to
the root of the operation. Additionally, it enforces a structured method by which operations can report success or
failure, allowing easier composability of code.

Some notable downsides of `Drexel.Loggers` are:
* There are several existing logging solutions, like the built-in Microsoft
  [ILogger](https://docs.microsoft.com/en-us/dotnet/core/extensions/logging) or [Serilog](https://serilog.net/), that
  more readily address the actual "logging" - that is, writing things out to disk or an event sink.
* It introduces a new convention developers must understand before they can contribute to a code-base using it.
* Because using results forces operations to roll up before they can be logged, events are delayed until the operation
  completes - or, if an operation is interrupted (ex. by an unexpected exception), events could be lost.

## Theory of Operation
A C# application is essentially a series of method-calls. Ignoring the application entry point and other CLR hooks,
everything that happens over the lifetime of a C# application is just methods calling other methods. When using
`Drexel.Loggers`, methods that can produce events return an `IActionResult` or `IFuncResult<T>`. These interfaces
encapsulate:
* The success or failure state of the method call.
* The events that occurred during the course of the method call.
* The result of the method call, if one exists.

The events that occurred during a method call are exposed as `IResultEvent` instances, which decorate an `ILogEvent`
instance with a flag indicating whether the event is informational or control-flow related. If a method is
control-flow related (AKA an error), then it is the responsibility of the caller to act accordingly. In all cases, the
caller should roll up the events returned by the operation as appropriate. (In some cases, it may be desirable for a
caller to discard some or all events produced by the operation.)

## Sample
For a simple example application, see [`Program.cs`](/samples/Drexel.Loggers.Sample/Program.cs).

## Usage
* Add a reference to `Drexel.Loggers`
* Add some `LogEventTemplate`s somewhere - dependency inject them, use a singleton, whatever works
* Create some methods that return `IActionResult` or `IFuncResult<T>`, then
  * Instantiate an instance of `ActionResult` or `FuncResult<T>`
  * Use the `LogEventTemplate`s you declared earlier to add events to the result object
  * Set the value of the result object if appropriate; you can use `.SetValue(T)`
* Call your methods, using `.AddResult(IActionResult)` or `.AddResult(IFuncResult<T>, out T)` as desired

## Building from source
Prerequisites:
* [Visual Studio 2022 Community](https://visualstudio.microsoft.com/vs/community/)
  * The production code only requires .NET Standard 1.1 support.
  * The tests and sample require the .NET 6.0 Runtime.
  * To check test coverage, use [FineCodeCoverage Extension](https://github.com/FortuneN/FineCodeCoverage)