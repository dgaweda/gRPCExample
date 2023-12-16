using Grpc.Net.Client;
using gRPCServer;

namespace gRPC.Client.Services;

public class HelloWorld2ClientService(GrpcChannel channel)
{
    private readonly HelloWorld2Service.HelloWorld2ServiceClient _client = new(channel);

    public async Task GetHelloWorld2Message(HelloWorld2Request request)
    {
        var response = await _client.GetHelloWorld2MessageAsync(request);
        Console.WriteLine("HelloWorld2Service response from Server: {0}{1}{2}", ConsoleColorHelper.GREEN, response.Response, ConsoleColorHelper.NORMAL);
        Console.ReadKey();
    }
}