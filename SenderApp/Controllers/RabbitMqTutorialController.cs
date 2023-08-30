using Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RecieverApplication;

namespace SenderApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RabbitMqTutorialController : ControllerBase
    {
        private IBus _bus;
        private IRequestClient<BalanceUpdate> _request;
        public RabbitMqTutorialController(IBus bus, IRequestClient<BalanceUpdate> requestClient)
        {

            _bus = bus;
            _request = requestClient;
        }

        // command sending section
        [HttpPost]
        public async Task<IActionResult> SendTutorial()
        {
            var product = new Product()
            {
                Name = "computer",
                Price = 500
            };
            var url = new Uri("rabbitmq://localhost/SendTutorial");
            var endpoint = await _bus.GetSendEndpoint(url);

            await endpoint.Send(product);

            return Ok("command sending tutorial");
        }

        [HttpPost]
        public async Task<IActionResult> PublishTutorial()
        {
            var person = new Person()
            {
                Name = "computer",
                Email = "ali@gmail.com"
            };

            await _bus.Publish(person);

            return Ok("publish tutorial");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBalance(BalanceUpdate model)
        {
            var response = await _request.GetResponse<BalanceUpdate>(new
            {
                TypeOfInstruction = model.typeOfInstruction,
                Amount = model.Amount
            });

            return Ok(response);
        }
    }
}