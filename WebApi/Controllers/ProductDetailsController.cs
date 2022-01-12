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
    public class ProductDetailsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(InMemoryDal.MemoryDal.ProductDetailList);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            ProductDetail result = InMemoryDal.MemoryDal.ProductDetailList.SingleOrDefault(p => p.Id == id);
            if (result != null)
            {
                return Ok(result);
            }
            return NoContent();// return 204 
        }


        [HttpPost()]
        public IActionResult Create([FromBody] ProductDetail productDetail)
        {
            bool check = true;
            InMemoryDal.MemoryDal.ProductDetailList.ForEach(p =>
            {
                if (p.Id == productDetail.Id)
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
                InMemoryDal.MemoryDal.ProductDetailList.Add(productDetail);
            }
            catch (Exception)
            {

                return StatusCode(500);//500
            }

            return Created("Index", new { time = DateTime.Now });//201

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductDetail productDetail)
        {
            bool check = true;
            InMemoryDal.MemoryDal.ProductDetailList.ForEach(p =>
            {
                if (p.Id == productDetail.Id)
                {
                    p.UnitInStock = productDetail.UnitInStock;
                    p.UnitOnOrder = productDetail.UnitOnOrder;
                    p.UnitPrice = productDetail.UnitPrice;
                    p.Description = productDetail.Description;
                    p.QuantityPerUnit = productDetail.QuantityPerUnit;
                }
            });
            if (!check)
            {
                return NotFound();
            }
            return Ok();

        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] ProductDetail productDetail)
        {
            bool check = true;
            InMemoryDal.MemoryDal.ProductDetailList.ForEach(p =>
            {
                if (p.Id == productDetail.Id)
                {
                    p.Description = productDetail.Description;
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
            for (int i = 0; i < InMemoryDal.MemoryDal.ProductDetailList.Count; i++)
            {
                if (InMemoryDal.MemoryDal.ProductDetailList[i].Id == id)
                {
                    check = true;
                    InMemoryDal.MemoryDal.ProductDetailList.Remove(InMemoryDal.MemoryDal.ProductDetailList[i]);
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
