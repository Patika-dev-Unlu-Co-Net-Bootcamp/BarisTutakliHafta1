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

            return Ok(InMemoryDal.MemoryDal.ProductList);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Product result = InMemoryDal.MemoryDal.ProductList.SingleOrDefault(p => p.Id == id);
            if (result != null)
            {
                return Ok(result);
            }
            return NoContent();// return 204 
        }
    

        [HttpPost()]
        public IActionResult Create([FromBody]Product product)
        {
            bool check = true;
            InMemoryDal.MemoryDal.ProductList.ForEach(p =>
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
            try
            {
                InMemoryDal.MemoryDal.ProductList.Add(product);
            }
            catch (Exception)
            {

                return StatusCode(500);//500
            }
          
            return Created("Index",new { time = DateTime.Now });//201
            
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] Product product)
        {
            bool check = true;
            InMemoryDal.MemoryDal.ProductList.ForEach(p =>
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

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] Product product)
        {
            bool check = true;
            InMemoryDal.MemoryDal.ProductList.ForEach(p =>
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
                return NotFound();// 404
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool check = false;
            for (int i = 0; i < InMemoryDal.MemoryDal.ProductList.Count; i++)
            {
                if (InMemoryDal.MemoryDal.ProductList[i].Id == id)
                {
                    check = true;
                    InMemoryDal.MemoryDal.ProductList.Remove(InMemoryDal.MemoryDal.ProductList[i]);
                }
            }
          
            if (!check)
            {
                return NotFound();
            }
            return Ok();//200

        }
        

     
    }
}
