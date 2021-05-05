using Grpc.Core;
using GrpcService1.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService1.Services
{
    public class TurnByTurnService : TurnByTurn.TurnByTurnBase
    {
        public override async Task StartGuaidance(GuidanceRequest request, IServerStreamWriter<Step> responseStream, ServerCallContext context)
        {
            var steps = new List<Step>
            {
                new Step { Direction = "Left", Road = "Prospect" },
                new Step { Direction = "Left", Road = "Main" },
                new Step { Direction = "Left", Road = "14th St." },
            };

            foreach (var step in steps)
            {
                await Task.Delay(new Random().Next(2000, 8000));
                await responseStream.WriteAsync(step);
            }
        }
    }
}
