using Azure.Core;
using Azure.Identity;
using Serilog;
using Serilog.Sinks.ApplicationInsights.TelemetryConverters;
using ServiceBus.Core;
using WarshipEnrichment;
using WarshipImport;
using WarshipImport.Interfaces;
using Warships.Databases;
using Warships.Interfaces;
using WarshipSearchAPI.Interfaces;
using WarshipSearchAPI.Middleware;
using WarshipService.Processors;

Log.Logger = new LoggerConfiguration()
		 .WriteTo.Console()
		 .CreateBootstrapLogger();
Log.Information("Application Started");



var builder = WebApplication.CreateBuilder(args);
Log.Information("Builder created");


TokenCredential keyvaultCredential = new DefaultAzureCredential();
if (builder.Environment.IsDevelopment())
{
	keyvaultCredential = new ClientSecretCredential(builder.Configuration["tenantID"], builder.Configuration["clientID"], builder.Configuration["clientSecret"]);
}

var url = $"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/";
builder.Configuration.AddAzureKeyVault(new Uri(url), keyvaultCredential);



var appInsightsConnection = builder.Configuration["AppInsights"];
if (string.IsNullOrEmpty(appInsightsConnection))
	Log.Error("Application Insights Connection is NULL");

Log.Logger = new LoggerConfiguration()
	.Enrich.FromLogContext()
	.Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
	.WriteTo.ApplicationInsights(appInsightsConnection, new TraceTelemetryConverter())
	.WriteTo.Console()
	.CreateLogger();

builder.Logging.AddSerilog(Log.Logger);
Log.Information("Application started & Logger attached");

var connectionString = builder.Configuration["DBConnection"];
if (string.IsNullOrEmpty(connectionString))
	Log.Error($"Database connection string is NULL");




// Add services to the container.
builder.Services.AddSingleton<INationalityDB, NationalityDB>();
builder.Services.AddSingleton<IWarshipClassificationDB, WarshipClassificationDB>();
builder.Services.AddSingleton<IQueryRangeProcessor, QueryRangeProcessor>();
builder.Services.AddScoped<IWarshipDatabase, WarshipDatabase>();

builder.Services.AddSingleton<IMessageProcessor, AddOrUpdateShipProcessor>();
builder.Services.AddSingleton<IHostedService, AddWarshipConsumerHost>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseCors(options =>
	options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var conflictProcessor = app.Services.GetService<IHostedService>();

var tokenSource = new CancellationTokenSource();
var t = conflictProcessor!.StartAsync(tokenSource.Token);

app.Run();

tokenSource.Cancel();
await t;