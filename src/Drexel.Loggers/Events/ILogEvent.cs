using System;
using System.Collections.Generic;

namespace Drexel.Loggers.Events
{
    public interface ILogEvent
    {
        EventInfo? Info { get; }

        ExceptionInfo? Exception { get; }

        IReadOnlyList<ILogEvent> InnerEvents { get; }
    }
}
