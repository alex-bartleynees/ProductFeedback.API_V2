using System.Diagnostics.Metrics;


namespace MinimalApi.Diagnostics
{
    public static class ApplicationsDiagnostics
    {
        private const string ServiceName = "Suggestions.API";
        public static readonly Meter Meter = new(ServiceName);

        public static Counter<long> SuggestionsCreatedCounter = Meter.CreateCounter<long>("suggestions.created");
    }
}