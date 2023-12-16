namespace gRPC.Client;

public static class ConsoleColorHelper
{
    public static string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
    public static string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
}