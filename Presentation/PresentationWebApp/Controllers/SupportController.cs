using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PresentationWebApp.Controllers
{
    public class SupportController : Controller
    {
        [HttpGet]
        public IActionResult Contact() //This will be used to load the page
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string email, string query) //This will be used to handle the form submission
        {
            //Inform the responsible staff about the query

            if (string.IsNullOrEmpty(query))
            {
                //Check if is empty
                ViewData["warning"] = "Please type something";
            }
            else {
                //Inform the user that the query was received
                ViewData["feedback"] = "Thank you for getting in touch with us. We will answer back asap";

            }

            return View();
        }
    }
}
