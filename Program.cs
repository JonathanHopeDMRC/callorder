using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    // This works with 6.0.8, but not with 6.0.9.
    Console.WriteLine(Pinger.Ping());
});

builder.Services.AddControllers();
builder.Services.AddSingleton<IPinger, Pinger>();

var app = builder.Build();

Pinger = app.Services.GetRequiredService<IPinger>();

app.Run();

public interface IPinger
{
    string Ping();
}

public sealed class Pinger : IPinger
{
    public string Ping() => "Pong";
}

partial class Program
{
    public static IPinger Pinger { get; set; }
}