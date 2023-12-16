using Grpc.Core;
using Grpc.Net.Client;

namespace gRPC.Client.Services;

public class HelloWorldClientService(GrpcChannel channel)
{
    private readonly HelloWorld.HelloWorldClient _client = new(channel);

    public async Task GetHelloWorldUnary(HelloWorldRequest request)
    {
        var response = await _client.GetHelloWorldMessageAsync(request);
        Console.WriteLine("Unary - Server Response: {0}{1}{2}", ConsoleColorHelper.GREEN, response.Message, ConsoleColorHelper.NORMAL);
        Console.ReadKey();
    }

    public async Task GetHelloWorldStream(HelloWorldRequest request)
    {
        Console.WriteLine("Server streaming");
        using var stream = _client.GetHelloWorldStream(request);
        while (await stream.ResponseStream.MoveNext())
        {
            Console.WriteLine("Server Response: {0}{1}{2}", ConsoleColorHelper.GREEN, stream.ResponseStream.Current, ConsoleColorHelper.NORMAL);
        }
        Console.ReadKey();
    }

    public async Task SendHelloWorldStreamAndGetSingleResponse()
    {
        using var stream = _client.ReadHelloWorldStream();
        for (var i = 0; i < 3; i++)
        {
            await stream.RequestStream.WriteAsync(new HelloWorldRequest()
            {
                Message = "Send Hello World Stream And Get Single Response | " + i
            });
        }

        await stream.RequestStream.CompleteAsync();

        var response = await stream;
        Console.WriteLine("Client Streaming - Server Response: {0}{1}{2}", ConsoleColorHelper.GREEN, response, ConsoleColorHelper.NORMAL);
    }

    public async Task TestBiDirectionalHelloWorld()
    {
        Console.WriteLine("BiDirectional streaming");
        using var stream = _client.BiDirectionalHelloWorld();

        Console.WriteLine("Start background reading task");
        var readTask = Task.Run(async () =>
        {
            await foreach (var response in stream.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine("Response Stream - Server Response: {0}{1}{2}", ConsoleColorHelper.GREEN, response.Message, ConsoleColorHelper.NORMAL);
            }
        });

        Console.WriteLine("Start send messages - If you want to cancel sending messages type: Cancel");
        while (true)
        {
            var message = Console.ReadLine();
            if (message is "cancel" or "Cancel" or "CANCEL")
                break;

            await stream.RequestStream.WriteAsync(new HelloWorldRequest() { Message = message });
        }

        Console.WriteLine("Disconnected");
        await stream.RequestStream.CompleteAsync();
        await readTask;
    }
}