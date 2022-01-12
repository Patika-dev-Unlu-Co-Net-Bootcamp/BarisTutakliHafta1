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
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(InMemoryDal.MemoryDal.CategoryList);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            Category result = InMemoryDal.MemoryDal.CategoryList.SingleOrDefault(p => p.Id == id);
            if (result != null)
            {
                return Ok(result);
            }
            return NoContent();// return 204 
        }


        [HttpPost()]
        public IActionResult Create([FromBody] Category category)
        {
            bool check = true;
            InMemoryDal.MemoryDal.CategoryList.ForEach(p =>
            {
                if (p.Id == category.Id)
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
                InMemoryDal.MemoryDal.CategoryList.Add(category);
            }
            catch (Exception)
            {

                return StatusCode(500);//500
            }

            return Created("Index", new { time = DateTime.Now });//201

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            bool check = true;
            InMemoryDal.MemoryDal.CategoryList.ForEach(p =>
            {
                if (p.Id == category.Id)
                {

                    p.CategoryName = category.CategoryName;
                    p.Description = category.Description;
                 
                }
            });
            if (!check)
            {
                return NotFound();
            }
            return Ok();

        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] Category category)
        {
            bool check = true;
            InMemoryDal.MemoryDal.CategoryList.ForEach(p =>
            {
                if (p.Id == category.Id)
                {
                    p.CategoryName = category.CategoryName;
                    
                    p.Description = category.Description;
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
            for (int i = 0; i < InMemoryDal.MemoryDal.CategoryList.Count; i++)
            {
                if (InMemoryDal.MemoryDal.CategoryList[i].Id == id)
                {
                    check = true;
                    InMemoryDal.MemoryDal.CategoryList.Remove(InMemoryDal.MemoryDal.CategoryList[i]);
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

