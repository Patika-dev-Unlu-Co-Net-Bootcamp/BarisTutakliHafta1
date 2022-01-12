using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.InMemoryDal
{
    public class MemoryDal
    {
        public static List<Product> productList = new List<Product> {
                new Product { Id=1, CategoryId=2, ProductName="Watch", PublishingDate=DateTime.Now },
                new Product { Id=2, CategoryId=3, ProductName="Phone", PublishingDate=DateTime.Now },
                new Product { Id=3, CategoryId=4, ProductName="Tablet", PublishingDate=DateTime.Now }
            };
    }
}
