namespace MVC.WebUI.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;
	
	public class ProductsController : Controller
    {
        // GET: /products
        public ViewResult Index()
        {
            return this.View();
        }
	}
}