using Common;
using MassTransit;

namespace RecieverApplication
{

    public class SenderConsumer : IConsumer<Product>
    {
        public async Task Consume(ConsumeContext<Product> context)
        {
            var product = context.Message;
        }
    }
}