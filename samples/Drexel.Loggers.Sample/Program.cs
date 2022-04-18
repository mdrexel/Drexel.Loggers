using Drexel.Loggers.Events;
using Drexel.Loggers.Templates;

namespace Drexel.Loggers.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            Console.ReadLine();
        }


    }

    public interface ITemplateProvider<out TTemplate, out TEvent>
        where TTemplate : ILogEventTemplate<TEvent>
        where TEvent : ILogEvent
    {

    }

    public sealed class TemplateProvider : ITemplateProvider<LogEventTemplate, ILogEvent>
    {
        private static readonly EventCodeGroup Group = new EventCodeGroup(1, "Example Events");

        private TemplateProvider()
        {
        }

        public static TemplateProvider Singleton { get; } = new TemplateProvider();

        public LogEventTemplate InputNegative { get; } =
            new LogEventTemplate(
                new EventCode(Group, 0, nameof(InputNegative)),
                EventSeverity.Error,
                "The input was negative");
    }
}