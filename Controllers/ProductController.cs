using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Text.RegularExpressions;

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
               throw new Exception(ex.Message);
           }
           return product;
       }

        [HttpPost]
        public async Task Create([FromBody] ProductInfo item)
        {
            try
            {
                if(IsAlphaName(item) && IsNumericRate(item))
                {
                    _context.ProductInfo.Add(item);
                }        
                await _context.SaveChangesAsync();
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }    
        }

        [HttpPut("{id}")]
        public async Task Update(long id, [FromBody] ProductInfo item)
        {
            try
            {
                var result = _context.ProductInfo.FirstOrDefault(t => t.ID == id);    
                
                
                if(IsAlphaName(item) && IsNumericRate(item))
                {
                    result.Name = item.Name;
                    result.Rate = item.Rate;
                    result.Description = item.Description; 
                    _context.ProductInfo.Update(result);
                }    
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }    
        }


        [HttpDelete("{id}")]
        public async Task Delete(long id)
        { 
            try
            {
                var result = _context.ProductInfo.FirstOrDefault(t => t.ID == id);
                _context.ProductInfo.Remove(result);
                await _context.SaveChangesAsync();
            }catch(Exception ex){
                throw new Exception(ex.Message);
            }    
        }

        bool IsAlphaName(ProductInfo item)
        {
            string pattern = "^[A-Za-z ]+$";
            try
            {
                Regex regex = new Regex(pattern);
                if(regex.IsMatch(item.Name) == false)
                    return false;
                else
                    return true;
            }catch(Exception ex){
                throw new Exception(ex.Message);
            }            
        }

        bool IsNumericRate(ProductInfo item)
        {
            string pattern = "^[0-9 ]+$";
            string str = item.Rate.ToString();
            try
            {
                Regex regex = new Regex(pattern);
                if(regex.IsMatch(str) == false)
                    return false;
                else
                    return true;
            }catch(Exception ex){
                throw new Exception(ex.Message);
            }        
        }
    }
}