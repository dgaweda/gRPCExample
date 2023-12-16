using Grpc.Core;

namespace gRPCServer.Services
{
    public class GreeterService(ILogger<GreeterService> logger) : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            logger.LogInformation("Incoming Request: {0}", request);
            logger.LogInformation("Incoming Request data: {0}", request.Name);
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
