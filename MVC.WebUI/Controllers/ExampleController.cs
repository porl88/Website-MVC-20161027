namespace MVC.WebUI.Controllers
{
	using System.Collections.Generic;
	using System.IO;
	using System.Web;
	using System.Web.Mvc;

	public class ExampleController : Controller
	{
		public ViewResult Index()
		{
			return this.View();
		}

		[ActionName("file-upload")]
		public ViewResult FileUpload()
		{
			return this.View();
		}

		[HttpPost]
		[ActionName("file-upload")]
		public ViewResult FileUpload(HttpPostedFileBase file)
		{
			if (file != null && file.ContentLength > 0)
			{
				ViewBag.File = file;

				var fileName = Path.GetFileName(file.FileName);
				var path = Path.Combine(Server.MapPath("~/_temp"), fileName);
				//file.SaveAs(path);
			}

			return this.View();
		}

		[ActionName("file-upload-multiple")]
		public ViewResult FileUploadMultiple()
		{
			return this.View();
		}

		[HttpPost]
		[ActionName("file-upload-multiple")]
		public ViewResult FileUploadMultiple(IEnumerable<HttpPostedFileBase> files)
		{
			ViewBag.Files = files;

			foreach (var file in files)
			{
				if (file.ContentLength > 0)
				{
					var fileName = Path.GetFileName(file.FileName);
					var path = Path.Combine(Server.MapPath("~/_temp"), fileName);
					//file.SaveAs(path);
				}
			}

			return this.View();
		}

		public ViewResult FormData()
        {
			return this.View();
        }
    }
}