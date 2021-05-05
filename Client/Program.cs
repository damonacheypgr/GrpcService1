using Grpc.Core;
using Grpc.Net.Client;
using GrpcService1;
using GrpcService1.Protos;
using System;

namespace Client
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Doing the gRPC thing!");
            Console.WriteLine("Hit enter when the server is running");
            Console.ReadLine();

            Console.Write("What is your name? ");
            var name = Console.ReadLine();

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var greeterClient = new Greeter.GreeterClient(channel);

            var requestMessage = new HelloRequest() { Name = name };

            var response = await greeterClient.SayHelloAsync(requestMessage);

            Console.WriteLine($"The server said {response.Message}");

            Console.WriteLine("processing order now...");

            var orderProcessorClient = new ProcessOrder.ProcessOrderClient(channel);

            var orderRequest = new OrderRequest
            {
                Id = "12",
            };

            orderRequest.Items.AddRange(new[] { "1", "2", "3", "4", "5" });

            var orderResponse = await orderProcessorClient.ProcessAsync(orderRequest);

            Console.WriteLine($"done {orderResponse.PickupTime.ToDateTime():d}");

            var turnByTurnClient = new TurnByTurn.TurnByTurnClient(channel);

            var streamingResponse = turnByTurnClient.StartGuaidance(new GuidanceRequest { Address = "5112" });

            await foreach( var step in streamingResponse.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"Turn {step.Direction} onto {step.Road}");
            }
        }
    }
}
