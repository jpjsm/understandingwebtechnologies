using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OdataWebApplication.Models
{
    public class ProductsContext: DbContext
    {
        public ProductsContext()
        {
        }

        public DbSet<Product> Products { get; set; } = new DbSet<Product>()
        {
            new Product()
            {
                ID = 1,
                Name = "Bread",
                Value = 3.75m
            }
            ,new Product()
            {
                ID = 2,
                Name = "Milk",
                Value = 1.59m
            }
            ,new Product()
            {
                ID = 3,
                Name = "Sliced Bread Loaf",
                Value = 2.09m
            }
        };
    }
}