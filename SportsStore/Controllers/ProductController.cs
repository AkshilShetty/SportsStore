﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository repository;

        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        // GET: /<controller>/
        public ViewResult List(string category, int productPage = 1)
        {
            return View(new ProductListViewModel
            {
                Products = repository.Products
                            .Where(p=>p.Category == category || category == null )
                            .OrderBy(p => p.ProductID)
                            .Skip((productPage - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? 
                                            repository.Products.Count() :
                                            repository.Products.Where(p => p.Category == category).Count()
                },
                CurrentCattegory = category
            });
        }
    }
}
