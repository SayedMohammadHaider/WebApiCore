using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationApiPractice.Model;

namespace WebApplicationApiPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly AppDbContext context;
        public ProductController(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Create New Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            try
            {
                context.Product.Add(product);
                context.SaveChanges();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();
                Product product = context.Product.Find(id);
                if (product != null)
                {
                    context.Product.Remove(product);
                    context.SaveChanges();
                    return Ok(product);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Get List of Products
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = context.Product.ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Get Product Details By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var product = context.Product.Find(id);
                if (product != null) return Ok(product);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }            
        }

        /// <summary>
        /// Update Product by Id
        /// </summary>
        /// <param name="productChanges"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(Product productChanges)
        {
            try
            {
                if(productChanges.Id<=0)
                    return BadRequest();
                var product = context.Product.Attach(productChanges);
                product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return Ok(productChanges);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}