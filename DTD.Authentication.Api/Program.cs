using DTD.Authentication.Api.Service;
using DTD.Core.Context;
using DTD.Service;
using Scalar.AspNetCore; 

using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using DTD.Core.Clients;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
 
builder.Services.AddControllers();
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<IUserService, UserService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOpenTelemetry()
.WithTracing(tracerProviderBuilder =>
{
    tracerProviderBuilder
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("DTD.Authentication.Api"))
        .AddSource("AuthenticationTracing")
        .AddAspNetCoreInstrumentation(options =>
        {
            options.Filter = httpContext =>
            {
                var path = httpContext.Request.Path.Value?.ToLowerInvariant().TrimEnd('/');
                if (string.IsNullOrWhiteSpace(path))
                {
                    return true;
                }
                // Trace edilmesini istemedigin endpointler:
                return !(path.StartsWith("/openapi") ||
                         path.StartsWith("/docs") || 
                         path == "/" ||
                         path == "/favicon.ico");
            };
        })
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

builder.Services.AddHttpClient<InvoiceClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:6062");
});
builder.Services.AddHttpClient<ShopClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:6064"); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs", options =>
    {
        options.WithTitle("DTD.Authentication.Api Documents").WithTheme(ScalarTheme.DeepSpace);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
