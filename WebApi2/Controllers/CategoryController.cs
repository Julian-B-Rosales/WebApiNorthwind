using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi2.Models;

namespace WebApiNorthwind.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        
        private readonly NorthwindContext _context;
        public CategoryController(NorthwindContext context)
        {
            _context = context;
        }

        //GET /api/category
        [HttpGet]
        public IEnumerable<Category> get()
        {
            List<Category> categories = (from c in _context.Categories
                                        select c).ToList();

            return categories;
        }

        //GET /api/category/categoryId
        [HttpGet("{categoryId}")]
        public Category get(int categoryId)
        {
            Category category = (from c in _context.Categories
                                 where c.CategoryId == categoryId
                                 select c).SingleOrDefault();

            return category;
        }

    }
}
