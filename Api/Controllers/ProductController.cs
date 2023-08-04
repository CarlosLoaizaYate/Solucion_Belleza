using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        protected readonly IProductService _productService;

        public ProductController(IProductService service)
        {
            _productService = service;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_productService.Get());
        }

        [HttpGet("{id}")]
        public ActionResult GetId(Guid id)
        {
            return Ok(_productService.GetId(id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            _productService.Save(product);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Product product, Guid id)
        {
            _productService.Update(product, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _productService.Delete(id);
            return Ok();
        }
    }
}
