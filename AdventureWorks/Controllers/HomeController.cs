using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;

using AdventureWorks.Models;
using AdventureWorks.Abstract;

namespace AdventureWorks.Controllers
{
    public class HomeController : Controller
    {
        private IAWRepository repository;
        private IMemoryCache cache;
        private IDateTime clock;

        public HomeController(IAWRepository repo, IMemoryCache memoryCache, IDateTime machineClock)
        {
            repository = repo;
            cache = memoryCache;
            clock = machineClock;
        }

        [HttpGet("home/bikes")]
        [ResponseCache(Duration = 300)]
        public async Task<JsonResult> GetTopFive()
        {
            List<ModelForProductsList> model = new List<ModelForProductsList>();
            var topFiveBikes = await Task.Run(() => repository.GetTopPopularProducts().ToList());
            model = topFiveBikes.Select(s => new ModelForProductsList
            {
                Name = s.Name,
                ProductId = s.ProductId,
                Color = s.Color,
                ListPrice = s.ListPrice,
                ProductNumber = s.ProductNumber
            }).ToList();

            return Json(model);
        }

        [HttpPost("home/bikes")]
        public async Task<JsonResult> Create([FromBody] ModelForProductDetail bike)
        {
            if (!ModelState.IsValid)
                return Json(false);

            bool result = await Task.Run(() => repository.AddProduct(bike));

            return Json(result);
        }

        [HttpDelete("home/bikes/{id}")]
        public async Task<JsonResult> Delete(int? id)
        {
            if (!id.HasValue)
                return Json(false);

            bool result = await Task.Run(() => repository.DeleteProduct(id.Value));

            return Json(result);
        }

        [HttpGet("home/bikes/details/{id}")]
        public async Task<JsonResult> ViewProductDetail(int? id)
        {
            if (!id.HasValue)
                return Json(false);

            ModelForProductDetail product = await Task.Run(() => repository.GetProductDetail(id.Value));
            if (product == null)
                return Json(false);

            return Json(product);
        }

        [HttpGet("home/bikes/{detail}")]
        public async Task<JsonResult> SearchProduct(string detail)
        {
            List<ModelForProductsList> products = new List<ModelForProductsList>();
            if (!cache.TryGetValue<List<ModelForProductsList>>(detail, out products))
            {
                products = await Task.Run(() => repository.SearchProduct(detail).ToList());
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = clock.AddMinutes(3);
                options.SlidingExpiration = clock.FromMinutes(3);
                cache.Set<List<ModelForProductsList>>(detail, products, options);
            }

            return Json(products);
        }
    }
}
