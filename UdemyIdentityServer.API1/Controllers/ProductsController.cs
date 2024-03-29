﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UdemyIdentityServer.API1.Models;

namespace UdemyIdentityServer.API1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [Authorize(Policy = "ReadProduct")]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var productList = new List<Product>()
            {
                new Product(){Id=1, Name="Kalem",Price=100,Stock=500},
                new Product(){Id=2, Name="Silgi",Price=50,Stock=250},
                new Product(){Id=3, Name="Defter",Price=75,Stock=700},
                new Product(){Id=4, Name="Kitap",Price=1000,Stock=100},
                new Product(){Id=5, Name="Bant",Price=10,Stock=1000},
            };
            return Ok(productList);
        }

        [Authorize(Policy ="UpdateOrCreate")]
        public IActionResult UpdateProduct(int id)
        {
            return Ok($"id'si {id} olan ürün güncellenmiştir.");
        }

        [Authorize(Policy = "UpdateOrCreate")]
        public IActionResult CreateProduct(Product product)
        {
            product.Id = 6;
            return Ok(product);
        }
    }

}
