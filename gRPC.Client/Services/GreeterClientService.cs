using Grpc.Net.Client;

namespace gRPC.Client.Services;

public class GreeterClientService(GrpcChannel channel)
{
    private readonly Greeter.GreeterClient _client = new(channel);

    public async Task SayHello(HelloRequest request)
    {
        var greeterResponse = await _client.SayHelloAsync(request);

        Console.WriteLine("GreeterService response from Server: {0}{1}{2}", ConsoleColorHelper.GREEN, greeterResponse.Message, ConsoleColorHelper.NORMAL);
        Console.ReadKey();
    }
}