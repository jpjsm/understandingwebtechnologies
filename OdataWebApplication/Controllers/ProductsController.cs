using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OdataWebApplication.Models;
using System.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNet.OData.Query;
using System.Web.Http;

namespace OdataWebApplication.Controllers
{
    public class ProductsController : ODataController
    {
        private IQueryable<Product> products = new List<Product>()
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
        }.AsQueryable();

        [EnableQuery]
        public IQueryable<Product> Get()
        {
            return products;
        }

        [EnableQuery]
        public SingleResult<Product> Get([FromODataUri] int key)
        {
            IQueryable<Product> result = products.Where(p => p.ID == key);
            return SingleResult.Create(result);
        }
    }
}