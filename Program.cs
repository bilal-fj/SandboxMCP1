// Add References
using ModelContextProtocol;
using ModelContextProtocol.Server;
using McpServer.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add MCP server services with HTTP transport and Sandbox tool
builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<SandboxTool>()
;

// Add CORS for HTTP transport support in browsers
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable CORS
app.UseCors();

// Map MCP endpoints at root
app.MapMcp();

// Add a simple home page
app.MapGet("/status", () => "MCP Server on Azure App Service - Ready for use with HTTP transport");

app.Run();
