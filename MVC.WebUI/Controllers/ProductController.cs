namespace MVC.WebUI.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;
	
	public class ProductController : Controller
    {
        // GET: /Product/
        public ActionResult Index()
        {
            return this.View();
        }
	}
}