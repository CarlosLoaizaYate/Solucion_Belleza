using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        protected readonly IOrderService _orderService;

        public OrderController(IOrderService service)
        {
            _orderService = service;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_orderService.Get());
        }

        [HttpGet("{id}")]
        public ActionResult GetId(Guid id)
        {
            return Ok(_orderService.GetId(id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] Order order)
        {
            _orderService.Save(order);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Order order, Guid id)
        {
            _orderService.Update(order, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _orderService.Delete(id);
            return Ok();
        }
    }
}
