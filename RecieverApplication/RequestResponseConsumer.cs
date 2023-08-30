using Common;
using MassTransit;

namespace RecieverApplication
{
     
    public class RequestResponseConsumer : IConsumer<BalanceUpdate>
    {
        public async Task Consume(ConsumeContext<BalanceUpdate> context)
        {   
            // Process the request and prepare the response
            var data = context.Message;
            data.Amount = 500;

            // Send the response back to the requester
            await context.RespondAsync<BalanceUpdate>(data);
        }
    }
}
