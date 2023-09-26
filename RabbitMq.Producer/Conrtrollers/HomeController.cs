using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMq.Common.Services;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Producer.Conrtrollers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;

        public HomeController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage()
        {
            int n = 0;
            while (n < 100)
            {
                using var connection = _rabbitMqService.CreateChannel();
                using var model = connection.CreateModel();
                var body = Encoding.UTF8.GetBytes("Hi - " + n);
                model.BasicPublish("UserExchange",
                                     string.Empty,
                                     basicProperties: null,
                                     body: body);

                n++;
            }
              
           
            return Ok();
        }
    }
}
