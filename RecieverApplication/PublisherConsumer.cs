using Common;
using MassTransit;

namespace RecieverApplication
{
    public class PublisherConsumer : IConsumer<Person>
    {
        public async Task Consume(ConsumeContext<Person> context)
        {
            var info = context.Message;
        }
    }
}
