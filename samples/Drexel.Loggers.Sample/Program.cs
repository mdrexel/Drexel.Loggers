using Drexel.Loggers.Events;
using Drexel.Loggers.Results;

namespace Drexel.Loggers.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double GetNumber(int index)
            {
                double input;
                do
                {
                    Console.Write($"Enter integer {index}: ");
                }
                while (!double.TryParse(Console.ReadLine(), out input));

                return input;
            }

            double input1 = GetNumber(1);
            double input2 = GetNumber(2);

            IFuncResult<int> sum = CalculateSum(input1, input2);

            Console.WriteLine();
            if (sum.Success)
            {
                Console.WriteLine($"Successfully calculated the sum of your inputs: {sum.Value}");
            }
            else
            {
                Console.WriteLine("Failed to calculate the sum of your inputs.");
            }

            void PrintEvent(ILogEvent logEvent, bool isError)
            {
                if (isError)
                {
                    Console.Error.WriteLine(logEvent.Info.Message);
                }
                else
                {
                    Console.Out.WriteLine(logEvent.Info.Message);
                }
            }

            Console.WriteLine();
            if (sum.AllEvents.Count > 0)
            {
                Console.WriteLine("The following events occurred:");
                foreach (IResultEvent resultEvent in sum.AllEvents)
                {
                    resultEvent.Operation(
                        x => PrintEvent(x.Event, isError: true),
                        x => PrintEvent(x.Event, isError: false));
                }
            }
            else
            {
                Console.WriteLine("No events occurred.");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            _ = Console.ReadKey(true);
        }

        private static IFuncResult<int> CalculateSum(double left, double right)
        {
            FuncResult<int> result = new FuncResult<int>();

            IFuncResult<double> Validate(double value, string parameterName)
            {
                FuncResult<double> innerResult = new FuncResult<double>();

                if (value > int.MaxValue || value < int.MinValue)
                {
                    return innerResult.AddError(
                        TemplateProvider.Singleton.InputOutOfRange.Create(
                            parameters: new EventParameters(
                                EventParameter.Create(ParameterNames.Parameter, parameterName),
                                EventParameter.Create(ParameterNames.Value, value))));
                }

                if (Math.Sign(value) == -1)
                {
                    innerResult.AddInformational(
                        TemplateProvider.Singleton.InputNegative.Create(
                            parameters: new EventParameters(
                                EventParameter.Create(ParameterNames.Parameter, parameterName),
                                EventParameter.Create(ParameterNames.Value, value))));
                }

                if (Math.Truncate(value) != value)
                {
                    innerResult.AddInformational(
                        TemplateProvider.Singleton.InputNonInteger.Create(
                            parameters: new EventParameters(
                                EventParameter.Create(ParameterNames.Parameter, parameterName),
                                EventParameter.Create(ParameterNames.Value, value))));
                }

                innerResult.SetValue(value);
                return innerResult;
            }

            if (!result
                .AddResult(Validate(left, nameof(left)))
                .AddResult(Validate(right, nameof(right))))
            {
                return result;
            }

            double sum = left + right;
            try
            {
                checked
                {
                    result.SetValue((int)sum);
                }
            }
            catch (OverflowException e)
            {
                result.AddError(
                    TemplateProvider.Singleton.OutputOutOfRange.Create(
                        e,
                        parameters: new EventParameters(
                            EventParameter.Create(ParameterNames.Value, sum))));
            }

            return result;
        }
    }

    public static class ParameterNames
    {
        public const string Parameter = "Parameter";
        public const string Value = "Value";
    }

    public interface ITemplateProvider<out TTemplate, out TEvent>
        where TTemplate : ILogEventTemplate<TEvent>
        where TEvent : ILogEvent
    {
        TTemplate InputOutOfRange { get; }

        TTemplate InputNegative { get; }

        TTemplate InputNonInteger { get; }

        TTemplate OutputOutOfRange { get; }
    }

    public sealed class TemplateProvider : ITemplateProvider<LogEventTemplate, ILogEvent>
    {
        private static readonly EventCodeGroup Group = new EventCodeGroup(1, "Example Events");

        private TemplateProvider()
        {
        }

        public static TemplateProvider Singleton { get; } = new TemplateProvider();

        public LogEventTemplate InputOutOfRange { get; } =
            new LogEventTemplate(
                new EventCode(Group, 0, nameof(InputOutOfRange)),
                EventSeverity.Error,
                "The input was out-of-range.",
                "I have arbitrarily decided that inputs must be within the range of `int32`.",
                new EventSuggestions(
                    "Enter a smaller value.",
                    "Modify the source code to remove the arbitrary check.",
                    "Re-enter the out-of-range value for fun."));

        public LogEventTemplate InputNegative { get; } =
            new LogEventTemplate(
                new EventCode(Group, 1, nameof(InputNegative)),
                EventSeverity.Warning,
                "The input was negative");

        public LogEventTemplate InputNonInteger { get; } =
            new LogEventTemplate(
                new EventCode(Group, 2, nameof(InputNonInteger)),
                EventSeverity.Information,
                "The input was a non-integer value.");

        public LogEventTemplate OutputOutOfRange { get; } =
            new LogEventTemplate(
                new EventCode(Group, 3, nameof(OutputOutOfRange)),
                EventSeverity.Error,
                "The output was out-of-range.");
    }
}