using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public sealed class SandboxTool
{
    [McpServerTool, Description("Gets the amount of sand in a sand box")]
    public static string getSandAmount()
    {
        return "10kg";
    }
}