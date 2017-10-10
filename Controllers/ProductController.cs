using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly MyContext _context;

        public ProductController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<List<ProductInfo>> Get()
        {
           return  await _context.ProductInfo.ToListAsync();
        }

       [HttpGet("{id}")]
       public async Task<List<ProductInfo>> GetById(int id)
       {
            ProductInfo objectProductInfo = await _context.ProductInfo.FindAsync(id);
            List<ProductInfo> product = new List<ProductInfo>();
           try
           {
                product.Add(objectProductInfo);
           }catch(Exception ex){
               Console.WriteLine(ex.Message);
           }
           return product;
       }

        [HttpPost]
        public async Task Create([FromBody] ProductInfo item)
        {
            try
            {
                _context.ProductInfo.Add(item);
                await _context.SaveChangesAsync();
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }    
        }

        [HttpPut("{id}")]
        public async Task Update(long id, [FromBody] ProductInfo item)
        {
            var result = _context.ProductInfo.FirstOrDefault(t => t.ID == id);
            result.Name = item.Name;
            result.Description = item.Description;
            try
            {
                _context.ProductInfo.Update(result);
                await _context.SaveChangesAsync();
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }    
        }


        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            var result = _context.ProductInfo.FirstOrDefault(t => t.ID == id);
            try
            {
                _context.ProductInfo.Remove(result);
                await _context.SaveChangesAsync();
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }    
        }
    }
}