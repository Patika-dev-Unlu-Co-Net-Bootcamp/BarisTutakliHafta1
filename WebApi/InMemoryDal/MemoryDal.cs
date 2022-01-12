using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.InMemoryDal
{
    public class MemoryDal
    {
        public static List<Product> ProductList = new List<Product> {
                new Product { Id=1, CategoryId=2, ProductName="Watch", PublishingDate=DateTime.Now },
                new Product { Id=2, CategoryId=3, ProductName="Phone", PublishingDate=DateTime.Now },
                new Product { Id=3, CategoryId=4, ProductName="Dumbell", PublishingDate=DateTime.Now }
            };
        public static List<Category> CategoryList = new List<Category> {
                new Category { Id = 1, CategoryName = "Mobil", Description = "" },
                 new Category { Id = 2, CategoryName = "Sport", Description = "" },
                new Category { Id = 3, CategoryName = "Home", Description = "" }
            };

        public static List<ProductDetail> ProductDetailList = new List<ProductDetail> {
                
            };
    }
}
               