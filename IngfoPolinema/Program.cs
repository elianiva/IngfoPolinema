using IngfoPolinema;
using IngfoPolinema.Hubs;
using IngfoPolinema.Infrastructure;
using IngfoPolinema.Infrastructure.DataSource;
using IngfoPolinema.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.Configure<DataSourceOptions>(builder.Configuration.GetSection("DataSourceOptions"));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient();
builder.Services.AddVisitorService();
builder.Services.AddPingRepository();
builder.Services.AddPingService();
builder.Services.AddPingEventStore();
builder.Services.AddPingEventHubConnection();
builder.Services.AddBackgroundServices();

builder.Services.AddResponseCompression(options =>
{
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseResponseCompression();
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<PingEventHub>("/PingEventHub");
app.MapFallbackToPage("/_Host");

app.Run();
