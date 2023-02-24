using Serilog;
using MyReminder.API;
using MyReminder.Infrastructure.Persistence;

AppDomain.CurrentDomain.UnhandledException += AppUncatchedExceptionHandler;

var environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(
        $"appsettings.json",
        optional: false,
        reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{environmentVariable ?? "Production"}.json",
        optional: true,
        reloadOnChange: true)
    .AddJsonFile(
        $"appsettings-serilog.json",
        optional: false,
        reloadOnChange: true)
    .AddJsonFile(
        $"appsettings-serilog.{environmentVariable ?? "Production"}.json",
        optional: true,
        reloadOnChange: true);

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

builder.Services
    .RegisterDependencyInjection(builder.Configuration);

var webApplication = builder.Build();

if (webApplication.Environment.IsDevelopment())
{
    webApplication.UseSwagger();
    webApplication.UseSwaggerUI();
}

webApplication
    .UseMyReminderContextMigrations()
    .UseCors()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(builder => builder.MapControllers());

try
{
    webApplication.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception.Message);
}
finally
{
    Log.CloseAndFlush();
}


/*
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
*/

static void AppUncatchedExceptionHandler(object sender, UnhandledExceptionEventArgs eventArgs)
{
    var exception = (Exception)eventArgs.ExceptionObject;
    Log.Fatal("Uncatched exception!. Message: {exceptionMessage}", exception.Message);
    Log.Fatal(
        "Runtime terminating: {systemTerminated}. Stack trace: {stackTrace}",
        eventArgs.IsTerminating,
        exception.StackTrace);
}
