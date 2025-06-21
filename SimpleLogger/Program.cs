
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ILogger, FileLoggerService>();
var app = builder.Build();

app.UseMiddleware<UseRequestLogging>();

app.MapGet("/", () => "hellooo");

app.Run();
