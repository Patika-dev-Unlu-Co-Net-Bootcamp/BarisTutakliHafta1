using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            
            return Ok(InMemoryDal.MemoryDal.productList);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product result = InMemoryDal.MemoryDal.productList.SingleOrDefault(p => p.Id == id);
            if (result !=null)
            {
                return Ok(result);
            }
            return NoContent();// return 204 
        }
        [HttpPost]
        public IActionResult Create([FromBody]Product product)
        {
            bool check = true;
            InMemoryDal.MemoryDal.productList.ForEach(p =>
            {
                if (p.Id == product.Id)
                {
                     check = false;
                }
            });
            if (!check)
            {
                return BadRequest();
            }
            return Created("",new { time = DateTime.Now });
            
        }
        [HttpPut]
        public IActionResult Update([FromBody] Product product)
        {
            bool check = true;
            InMemoryDal.MemoryDal.productList.ForEach(p =>
            {
                if (p.Id == product.Id)
                {
                    p.ProductName = product.ProductName;
                    p.CategoryId = product.CategoryId;
                    p.PublishingDate = product.PublishingDate;
                }
            });
            if (!check)
            {
                return NotFound();
            }
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool check = true;
            InMemoryDal.MemoryDal.productList.ForEach(p =>
            {
                if (p.Id == id)
                {
                    InMemoryDal.MemoryDal.productList.Remove(p);
                }
            });
            if (!check)
            {
                return NotFound();
            }
            return Ok();

        }
    }
}
