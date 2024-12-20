using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

// Create the ASP.NET Core web server
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Log for debugging
Console.WriteLine("Starting server...");

// Map the endpoint directly for SOAP POST
app.MapPost("/soap", async (HttpContext context) =>
{
    try
    {
        Console.WriteLine("POST /soap endpoint hit");

        // Set the response content type to text/xml for SOAP
        context.Response.ContentType = "text/xml";

        // Send a response payload as a valid SOAP response
        await context.Response.WriteAsync(@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <Response>SOAP response successful!</Response>
  </soap:Body>
</soap:Envelope>");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        context.Response.StatusCode = 500;
    }
});

// Ensure routing is configured properly
app.UseRouting();
app.Map("/", async context =>
{
    context.Response.ContentType = "text/plain";
    await context.Response.WriteAsync("Root endpoint");
});

// Run server
Console.WriteLine("Running server on http://localhost:5000...");
await app.StartAsync();
Console.WriteLine("SOAP-like server operational.");
Console.WriteLine("Press Enter to stop the server.");

Console.ReadLine();
await app.StopAsync();
































