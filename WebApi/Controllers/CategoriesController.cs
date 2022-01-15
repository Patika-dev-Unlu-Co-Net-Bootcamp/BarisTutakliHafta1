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

        /// <summary>
        ///  Get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(InMemoryDal.MemoryDal.CategoryList);
        }
        /// <summary>
        /// Get a specific category by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var result = CheckByIdIfItemExist(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return NoContent();// return 204 
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost()]
        public IActionResult Create([FromBody] Category category)
        {
            var result = CheckByIdIfItemExist(category.Id);
            if (result is not null)
            {
                return BadRequest();
            }

            try
            {
                InMemoryDal.MemoryDal.CategoryList.Add(category);
            }
            catch (Exception)
            {

                return StatusCode(500); //500
            }
            return Created("Index", new { message = "Category added.", time = DateTime.Now });//201


        }
        /// <summary>
        ///  Update a category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category category)
        {
            var result = CheckByIdIfItemExist(category.Id);
            if (CheckByIdIfItemExist(category.Id) is null)
            {
                return NotFound();
            }

            try
            {
                
                result.CategoryName = category.CategoryName;
                result.Description = category.Description;
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

            return Ok();
           

        }

        /// <summary>
        /// Update the description of a category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] Category category)
        {
            var result = CheckByIdIfItemExist(category.Id);
            if (CheckByIdIfItemExist(category.Id) is null)
            {
                return NotFound();
            }

            try
            {

                
                result.Description = category.Description;
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = CheckByIdIfItemExist(id);

            if (result is null)
            {
                return BadRequest();
            }
            try
            {
                InMemoryDal.MemoryDal.CategoryList.Remove(result);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }



            return Ok();//200

        }
     
        private Category CheckByIdIfItemExist(int id)
        {

            var temp = InMemoryDal.MemoryDal.CategoryList.SingleOrDefault(p => p.Id == id);
            if (temp is not null)
            {
                return temp;
            }
            return null;

        }

    }
}

