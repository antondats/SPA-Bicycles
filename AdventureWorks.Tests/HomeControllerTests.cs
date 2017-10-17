using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

using AdventureWorks.Abstract;
using AdventureWorks.Controllers;
using AdventureWorks.Models;

namespace AdventureWorks.Tests
{
    public class HomeControllerTests
    {
        private Mock<IAWRepository> repoMock = new Mock<IAWRepository>();
        private Mock<IDateTime> clockMock = new Mock<IDateTime>();
        private Mock<IMemoryCache> cacheMock = new Mock<IMemoryCache>();
        private IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

        [Fact]
        public void GetTopFiveBikesReturnsCorrectValue()
        {
            //Arrange
            repoMock.Setup(repo => repo.GetTopPopularProducts()).Returns(GetTestTopPopular());
            var controller = new HomeController(repoMock.Object, cacheMock.Object, clockMock.Object);
            var expected = controller.Json(GetTestTopPopular());

            //Action
            var result = controller.GetTopFive();

            //Assert
            var jsonResult = Assert.IsType<System.Threading.Tasks.Task<JsonResult>>(result);
            Assert.Equal(expected.Value.ToString(), result.Result.Value.ToString());
        }

        [Fact]
        public void GetProductDetailWhenIdIsNull()
        {
            //Arrange
            var controller = new HomeController(repoMock.Object, cacheMock.Object, clockMock.Object);
            var expected = controller.Json(false);

            //Action
            var result = controller.ViewProductDetail(null);

            //Assert
            Assert.Equal(expected.Value, result.Result.Value);
        }

        [Fact]
        public void GetProductDetailIfProductNotExist()
        {
            //Arange
            int productDetail = 1;
            repoMock.Setup(r => r.GetProductDetail(productDetail)).Returns(null as ModelForProductDetail);
            var controller = new HomeController(repoMock.Object, cacheMock.Object, clockMock.Object);
            var expected = controller.Json(false);

            //Action
            var result = controller.ViewProductDetail(productDetail);

            //Assert
            Assert.Equal(expected.Value, result.Result.Value);
        }

        [Fact]
        public void GetProductDetailReturnsProduct()
        {
            //Arange
            int productDetail = 1;
            repoMock.Setup(r => r.GetProductDetail(productDetail)).Returns(GetTestProductDetail());
            var controller = new HomeController(repoMock.Object, cacheMock.Object, clockMock.Object);
            var expected = controller.Json(GetTestProductDetail());

            //Action
            var result = controller.ViewProductDetail(productDetail);

            //Assert
            Assert.Equal(expected.Value.ToString(), result.Result.Value.ToString());
        }

        [Fact]
        public void GetProductsFromSearch()
        {
            //Arrange
            string search = "string";
            List<ModelForProductsList> products = new List<ModelForProductsList>();
            repoMock.Setup(repo => repo.SearchProduct(search)).Returns(GetTestTopPopular());
            clockMock.Setup(m => m.AddMinutes(3)).Returns(System.DateTime.Now.AddMinutes(3));
            clockMock.Setup(m => m.FromMinutes(3)).Returns(System.TimeSpan.FromMinutes(3));

            var controller = new HomeController(repoMock.Object, memoryCache, clockMock.Object);
            var expected = controller.Json(GetTestTopPopular());

            //Action
            var result = controller.SearchProduct(search);

            //Assert
            var jsonResult = Assert.IsType<System.Threading.Tasks.Task<JsonResult>>(result);
            Assert.Equal(expected.Value.ToString(), result.Result.Value.ToString());
        }

        [Fact]
        public void SetProductsListFromSearchInCacheMemory()
        {
            //Arrange
            string search = "search";
            List<ModelForProductsList> products = new List<ModelForProductsList>();
            repoMock.Setup(repo => repo.SearchProduct(search)).Returns(GetTestTopPopular());
            clockMock.Setup(m => m.AddMinutes(3)).Returns(System.DateTime.Now.AddMinutes(3));
            clockMock.Setup(m => m.FromMinutes(3)).Returns(System.TimeSpan.FromMinutes(3));

            var controller = new HomeController(repoMock.Object, memoryCache, clockMock.Object);

            //Action
            var result = controller.SearchProduct(search);
            Task.WaitAll(result);

            //Assert
            Assert.Equal(GetTestTopPopular().ToString(), memoryCache.Get(search).ToString());
        }

        [Fact]
        public void DeleteProductWhenIdIsNull()
        {
            //Arrange
            var controller = new HomeController(repoMock.Object, cacheMock.Object, clockMock.Object);
            var expected = controller.Json(false);

            //Action
            var result = controller.Delete(null);

            //Assert
            Assert.Equal(expected.Value, result.Result.Value);
        }

        [Fact]
        public void DeleteProductWhenExceptionWithdeleting()
        {
            //Arrange
            int id = 1;
            repoMock.Setup(r => r.DeleteProduct(id)).Returns(false);
            var controller = new HomeController(repoMock.Object, cacheMock.Object, clockMock.Object);
            var expected = controller.Json(false);

            //Action
            var result = controller.Delete(id);

            //Assert
            Assert.Equal(expected.Value, result.Result.Value);
            repoMock.Verify(r => r.DeleteProduct(id));
        }

        [Fact]
        public void DeleteProductWhenAllIsGood()
        {
            //Arrange
            int id = 1;
            repoMock.Setup(r => r.DeleteProduct(id)).Returns(true);
            var controller = new HomeController(repoMock.Object, cacheMock.Object, clockMock.Object);
            var expected = controller.Json(false);

            //Action
            var result = controller.Delete(id);

            //Assert
            Assert.NotEqual(expected.Value, result.Result.Value);
            repoMock.Verify(r => r.DeleteProduct(id));
        }

        [Fact]
        public void CreateNewProductWhenModelStateInvalid()
        {
            //Arrange
            var controller = new HomeController(repoMock.Object, cacheMock.Object, clockMock.Object);
            var product = GetTestProductDetail();
            var expected = controller.Json(false);
            controller.ModelState.AddModelError("Description", "Required");

            //Action
            var result = controller.Create(product);

            //Assert
            Assert.Equal(expected.Value, result.Result.Value);
        }

        [Fact]
        public void CreateNewProductWhenexceptionWithAddition()
        {
            //Arrange
            var product = GetTestProductDetail();
            repoMock.Setup(r => r.AddProduct(product)).Returns(false);
            var controller = new HomeController(repoMock.Object, cacheMock.Object, clockMock.Object);
            var expected = controller.Json(false);

            //Action
            var result = controller.Create(product);

            //Assert
            Assert.Equal(expected.Value, result.Result.Value);
            repoMock.Verify(r => r.AddProduct(product));
        }

        [Fact]
        public void CreateNewProductWhenAllIsGood()
        {
            //Arrange
            var product = GetTestProductDetail();
            repoMock.Setup(r => r.AddProduct(product)).Returns(true);
            var controller = new HomeController(repoMock.Object, cacheMock.Object, clockMock.Object);
            var expected = controller.Json(false);

            //Action
            var result = controller.Create(product);

            //Assert
            Assert.NotEqual(expected.Value, result.Result.Value);
            repoMock.Verify(r => r.AddProduct(product));
        }

        private ModelForProductDetail GetTestProductDetail()
        {
            return new ModelForProductDetail { Name = "Bike3", ProductId = 1, Color = "White", ListPrice = 123, ProductNumber = "number3", Description = "cool bike" };
        }

        private List<ModelForProductsList> GetTestTopPopular()
        {
            var bikes = new List<ModelForProductsList>
            {
                new ModelForProductsList {Name = "Bike1", ProductId = 1, Color = "White", ListPrice = 123},
                new ModelForProductsList {Name = "Bike2", ProductId = 2, Color = "White", ListPrice = 123}
            };

            return bikes;
        }


    }
}
