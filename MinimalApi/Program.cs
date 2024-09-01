using MinimalApi.Diagnostics;
using MinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();
builder.ConfigureOpenTelemetry();

var app = builder.Build();

app.RegisterAppConfig();
app.Run();
