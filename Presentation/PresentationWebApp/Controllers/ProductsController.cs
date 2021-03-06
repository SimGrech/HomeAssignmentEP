﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using X.PagedList;

namespace PresentationWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private readonly ILogger<ProductsController> _logger;
        private IHostingEnvironment _env;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService, IHostingEnvironment env, ILogger<ProductsController> logger) {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _env = env;
            _logger = logger;
        }


        /* Old method for index used, changed because of pagination
        public IActionResult Index() {
            var list = _productsService.GetProducts();
            
            //Same method from create used
            //Fetch a list of categories
            var listOfCategories = _categoriesService.GetCategories();

            //We pass the categories to the page
            ViewBag.Categories = listOfCategories;

            

            return View(list);
        }
        */

        //Video example used for pagination https://www.youtube.com/watch?v=vnxN_zBisIo&ab_channel=ErcanEker
        public IActionResult Index(int? page) {

            var pageNumber = page ?? 1; //If page parameter is null, set to 1
            
            
            int pageSize = 10;
            //Paginated list
            var list = _productsService.GetProducts().ToPagedList(pageNumber, pageSize);


            //Same method from create used
            //Fetch a list of categories
            var listOfCategories = _categoriesService.GetCategories();

            //We pass the categories to the page
            ViewBag.Categories = listOfCategories;

            return View(list);
        }

        [HttpPost]
        public IActionResult CategorySearch(int category) //Using a Form, and the select list must have name attribute = category
        {

            //Create a method to filter the list using the category
            //var list = _productsService.GetProducts(category).ToList();
            //Changing ToList to ToPagedList to suit the new index method, 10 is being defined in a hard coded manner but it could be done by setting a global variable
            var list = _productsService.GetProducts(category).ToPagedList(1, 10);



            //Same method from create used
            //Fetch a list of categories
            var listOfCategories = _categoriesService.GetCategories();

            //We pass the categories to the page
            ViewBag.Categories = listOfCategories;


            return View("Index", list);
            
        }

        [HttpPost]
        public IActionResult Search(string keyword) { //Using a form, and the select list must have name attribute = category
            //var list = _productsService.GetProducts(keyword).ToList();
            //Changing ToList to ToPagedList to suit the new index method, 10 is being defined in a hard coded manner but it could be done by setting a global variable
            var list = _productsService.GetProducts(keyword).ToPagedList(1, 10);


            //Same method from create used
            //Fetch a list of categories
            var listOfCategories = _categoriesService.GetCategories();

            //We pass the categories to the page
            ViewBag.Categories = listOfCategories;

            return View("Index", list);
        }

        public IActionResult Details(Guid id) {
            var p = _productsService.GetProduct(id);

            return View(p);
        }

        public IActionResult Hide(Guid id) {
            _productsService.DisableProduct(id);

            return RedirectToAction("Index");
        }

        //The engine will load a page with empty fields
        [HttpGet]
        [Authorize (Roles = "Admin")] //The create method is going to be accessed only by authenticated users
        public IActionResult Create() {
            //Feth a list of categories
            var listOfCategories = _categoriesService.GetCategories();

            //We pass the categories to the page
            ViewBag.Categories = listOfCategories;

            return View();
        }

        
        //Here details input by the user will be received
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductViewModel data, IFormFile f) {
            try
            {

                if (f != null)
                {
                    if (f.Length > 0)
                    {
                        //F:\School\Level 6 Year 2\Semester 1\Enterprise Programming\First Example\SWD62BEP\Presentation\PresentationWebApp\wwwroot
                        string newFileName = Guid.NewGuid() + System.IO.Path.GetExtension(f.FileName);
                        string newFileNameWithAbsolutePath = _env.WebRootPath + @"\images\" + newFileName;

                        using (var stream = System.IO.File.Create(newFileNameWithAbsolutePath))
                        {
                            f.CopyTo(stream);
                        }
                        data.ImageUrl = @"\images\" + newFileName;

                        _productsService.AddProduct(data);
                        TempData["feedback"] = "Product was added Successfully";
                    }
                }
                else {
                    TempData["warning"] = "Include an image";
                }
            }
            catch (Exception e) {
                //Log error
                _logger.LogError(e.Message);
                TempData["warning"] = "Product was not added";
                return RedirectToAction("Error", "Home");

            }

            //We resend the list of categories since the page will reload
            var listOfCategories = _categoriesService.GetCategories();

            //We pass the categories to the page
            ViewBag.Categories = listOfCategories;

            return View(data);
        }

        [Authorize(Roles = "Admin")]    
        public IActionResult Delete(Guid id) {
            try
            {
                _productsService.DeleteProduct(id);
                TempData["feedback"] = "Product was deleted";
            }
            catch (Exception ex) {
                //Log your error
                TempData["warning"] = "Product was not deleted"; 
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");

        }


    }
}
