using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


namespace MinimalApi.Diagnostics
{
    public static class OpenTelemetryConfiguration
    {
        public static void ConfigureOpenTelemetry(this WebApplicationBuilder builder)
        {
            var cs = builder.Configuration.GetConnectionString("OTLP_Endpoint") ?? throw new ArgumentNullException(nameof(builder), "No OTLP connection string provided");

            builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                {
                    resource
                        .AddService("ProductFeedback.API")
                        .AddAttributes(new[]
                        {
                            new KeyValuePair<string, object>("service.version", Assembly.GetExecutingAssembly().GetName().Version!.ToString())
                        });
                })
                .WithTracing(tracing =>
               {
                   tracing
                       .AddAspNetCoreInstrumentation()
                       .AddSqlClientInstrumentation(options =>
                       {
                           options.EnableConnectionLevelAttributes = true;
                           options.RecordException = true;
                           options.SetDbStatementForText = true;
                       })

                       .AddHttpClientInstrumentation()
                       .AddOtlpExporter(options => options.Endpoint = new Uri(cs));
               })
               .WithMetrics(metrics =>
                    metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddMeter("Microsoft.AspNetCore.Hosting")
                    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                    .AddMeter(ApplicationsDiagnostics.Meter.Name)
                    .AddOtlpExporter(options => options.Endpoint = new Uri(cs))
               );
        }
    }
}