using System;
using Lawyers.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lawyers.WebApp.Controllers
{
    public class LawyersController:Controller
    {
        private readonly ILawyersPageFactory _lawyersPageFactory;

        public LawyersController(ILawyersPageFactory lawyersPageFactory)
        {
            _lawyersPageFactory = lawyersPageFactory;
        }

        public IActionResult Index(string param)
        {
            int page = 1;
            if (Request.Query.ContainsKey("page"))
                int.TryParse(Request.Query["page"], out page);
            var data = _lawyersPageFactory.HandlePage(param, page);
            return View(data.ViewName,data.Model);
        }

      
    }

   

     
}