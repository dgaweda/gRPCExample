using gRPC.Client;
using gRPC.Client.Services;
using Grpc.Net.Client;
using gRPCServer;

using var channel = GrpcChannel.ForAddress("https://localhost:7059");
var helloWorldClientService = new HelloWorldClientService(channel);
var helloWorld2ClientService = new HelloWorld2ClientService(channel);
var greetClientService = new GreeterClientService(channel);

Console.WriteLine("{0}Press any key to continue...", ConsoleColorHelper.NORMAL);
Console.ReadKey();

await helloWorld2ClientService.GetHelloWorld2Message(new HelloWorld2Request() { Message = "Hello World from Client!" });

await greetClientService.SayHello(new HelloRequest() { Name = "Darek" });

await helloWorldClientService.GetHelloWorldUnary(new HelloWorldRequest { Message = "Get Hello World single request single response" });
await helloWorldClientService.GetHelloWorldStream(new HelloWorldRequest { Message = "Get Hello World stream" });
await helloWorldClientService.SendHelloWorldStreamAndGetSingleResponse();
await helloWorldClientService.TestBiDirectionalHelloWorld();