using DTD.Core.Context;
using DTD.Invoice.Api.Service;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOpenTelemetry()
.WithTracing(tracerProviderBuilder =>
{
    tracerProviderBuilder
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("DTD.Invoice.Api"))
    .AddSource("InvoiceTracing")
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .AddSqlClientInstrumentation(options =>
    {
        options.SetDbStatementForText = true;
        options.RecordException = true;
    })
    .AddZipkinExporter(options =>
    {
        options.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs", options => 
    {
        options.WithTitle("DTD.Invoice.Api Documents").WithTheme(ScalarTheme.DeepSpace);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
