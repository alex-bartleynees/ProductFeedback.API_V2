using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
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
                        .AddAttributes(
                        [
                            new KeyValuePair<string, object>("service.version", Assembly.GetExecutingAssembly().GetName().Version!.ToString())
                        ]);
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
                        .AddSource(ApplicationsDiagnostics.ActivitySourceName)
                       .AddHttpClientInstrumentation()
                       .AddOtlpExporter(options =>
                           {
                               options.Endpoint = new Uri(cs);
                               options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                           });
               })
               .WithMetrics(metrics =>
                    metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddProcessInstrumentation()
                    .AddMeter("Microsoft.AspNetCore.Hosting")
                    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                    .AddMeter("Microsoft.AspNetCore.Http.Connections")
                    .AddMeter("Microsoft.AspNetCore.Routing")
                    .AddMeter("Microsoft.AspNetCore.Diagnostics")
                    .AddMeter("Microsoft.AspNetCore.RateLimiting")
                    .AddMeter(ApplicationsDiagnostics.Meter.Name)
                    .AddOtlpExporter(options => 
                    {
                        options.Endpoint = new Uri(cs);
                        options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                    })
                )
               .WithLogging(logging =>
                    {
                        logging
                            .AddOtlpExporter(options => options.Endpoint = new Uri(cs));
                    },
                    options =>
                    {
                        options.IncludeFormattedMessage = true;
                    }
               );
        }
    }
}
