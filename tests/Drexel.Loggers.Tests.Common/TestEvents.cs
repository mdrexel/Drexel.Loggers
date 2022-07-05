using Drexel.Loggers.Events;

namespace Drexel.Loggers.Tests.Common
{
    public static class TestEvents
    {
        static TestEvents()
        {
            TestEvents.Group = new EventCodeGroup(ushort.MaxValue, "Test Group");
            TestEvents.Template =
                new LogEventTemplate(
                    new EventCode(
                        TestEvents.Group,
                        ushort.MaxValue,
                        "Test Event"),
                    EventSeverity.Error,
                    "Test message"!,
                    "Test reason",
                    new EventSuggestions()
                    {
                        "Test suggestion 1"!,
                        "Test suggestion 2"!,
                        "Test suggestion 3"!,
                    });
        }

        public static EventCodeGroup Group { get; }

        public static LogEventTemplate Template { get; }
    }
}
