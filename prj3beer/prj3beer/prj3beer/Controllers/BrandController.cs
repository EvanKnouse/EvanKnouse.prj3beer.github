using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prj3beer.Models;
using prj3beer.Utilities;

namespace prj3beer.Controllers
{
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly BeerContext _context;

        public BrandController(BeerContext context)
        {
            _context = context;
        }

        //Get: API Posts
        [HttpGet]
        public IEnumerable<Brand> GetPost()
        {
            var brands = _context.Brands.OrderBy(p => p.brandName.ToLower());
            return brands;
        }

    }
}
