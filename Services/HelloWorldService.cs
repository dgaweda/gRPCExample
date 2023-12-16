using Grpc.Core;

namespace gRPCServer.Services;

public class HelloWorldService(ILogger<GreeterService> logger) : HelloWorld.HelloWorldBase
{
    public override Task<HelloWorldResponse> GetHelloWorldMessage(HelloWorldRequest request, ServerCallContext context)
    {
        
        logger.LogInformation("Hello World on Server incoming request {0}", request);
        return Task.FromResult(new HelloWorldResponse
        {
            Message = request.Message + " - Hello world from Server!",
        });
    }

    public override async Task GetHelloWorldStream(HelloWorldRequest request, IServerStreamWriter<HelloWorldResponse> responseStream, ServerCallContext context)
    {
        for(var i = 0; i < 3; i++)
        {
            await Task.Delay(1000);

            await responseStream.WriteAsync(new HelloWorldResponse()
            {
                Message = i + " | Client:" + request.Message + " | Server: HelloWorld From Server | " + DateTime.Now
            });
        }
    }

    public override async Task<HelloWorldResponse> ReadHelloWorldStream(IAsyncStreamReader<HelloWorldRequest> requestStream, ServerCallContext context)
    {
        var response = string.Empty;
        while (await requestStream.MoveNext())
        {
            response += string.Join(Environment.NewLine, requestStream.Current.Message + " | " + DateTime.Now);
        }

        return new HelloWorldResponse()
        {
            Message = response
        };
    }

    public override async Task BiDirectionalHelloWorld(IAsyncStreamReader<HelloWorldRequest> requestStream, IServerStreamWriter<HelloWorldResponse> responseStream,
        ServerCallContext context)
    {
        while (await requestStream.MoveNext())
        {
            await responseStream.WriteAsync(new HelloWorldResponse()
            {
                Message = requestStream.Current.Message + " | Append done by server"
            });
        }
    }
}