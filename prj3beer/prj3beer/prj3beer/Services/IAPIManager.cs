using prj3beer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace prj3beer.Services
{
    public interface IAPIManager
    {

        Task<List<Brand>> GetBrands();
    }
}
