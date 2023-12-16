using Grpc.Core;

namespace gRPCServer.Services;

public class HelloWorld2Service : gRPCServer.HelloWorld2Service.HelloWorld2ServiceBase
{
    public override Task<HelloWorld2Response> GetHelloWorld2Message(HelloWorld2Request request, ServerCallContext context)
    {
        return Task.FromResult(new HelloWorld2Response()
        {
            Response = request.Message + " - HelloWorld2 from Server"
        });
    }
}