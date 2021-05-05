using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService1.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService1.Services
{
    public class OrderProcessorService : ProcessOrder.ProcessOrderBase
    {
        private readonly ILogger<OrderProcessorService> _logger;

        public OrderProcessorService(ILogger<OrderProcessorService> logger)
        {
            _logger = logger;
        }

        public override async Task<OrderResponse> Process(OrderRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"got order for {request.Id}");

            foreach (var item in request.Items)
            {
                await Task.Delay(1000);
                _logger.LogInformation($"processed {item}");
            }

            return new OrderResponse
            {
                Id = request.Id,
                PickupTime = Timestamp.FromDateTime(DateTime.Now.AddHours(1).ToUniversalTime()),
            };
        }
    }
}
