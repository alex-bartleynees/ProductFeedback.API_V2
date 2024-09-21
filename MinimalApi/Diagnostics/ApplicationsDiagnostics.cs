using System.Diagnostics;
using System.Diagnostics.Metrics;


namespace MinimalApi.Diagnostics
{
    public static class ApplicationsDiagnostics
    {
        private const string ServiceName = "Suggestions.API";
        public static readonly Meter Meter = new(ServiceName);

        public const string ActivitySourceName = ServiceName;
        public static readonly ActivitySource ActivitySource = new(ActivitySourceName);

        public static Counter<long> SuggestionsCreatedCounter = Meter.CreateCounter<long>("suggestions.created");
    }
}