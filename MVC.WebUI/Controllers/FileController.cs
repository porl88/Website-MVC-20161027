namespace MVC.WebUI.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;
	using System.Web.Hosting;
	using System.Web.Http;

	public class FileController : ApiController
	{
		// POST api/file
		// http://www.asp.net/web-api/overview/advanced/sending-html-form-data,-part-2
		// http://www.intstrings.com/ramivemula/articles/file-upload-using-multipartformdatastreamprovider-in-asp-net-webapi/
		// http://www.strathweb.com/2012/08/a-guide-to-asynchronous-file-uploads-in-asp-net-web-api-rtm/
		// http://benfoster.io/blog/web-api-multipart-file-upload-additional-form-data
		// http://jflood.net/2012/06/03/asp-net-webapi-upload-image-with-custom-mediatypeformatter/
		public async Task<HttpResponseMessage> PostFormData()
		{
			if (!Request.Content.IsMimeMultipartContent())
			{
				throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
			}

			string fileSaveLocation = HostingEnvironment.MapPath("~/_temp");
			var provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
			
			foreach (var item in provider.Contents)
			{
				provider.Contents.Remove(item);
			}
			
			try
			{
				await Request.Content.ReadAsMultipartAsync(provider);
	
				var files = new List<string>();

				foreach (MultipartFileData file in provider.FileData)
				{
					files.Add(Path.GetFileName(file.LocalFileName));
				}

				return Request.CreateResponse(HttpStatusCode.OK, files, "application/json");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
			}
		}

		public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
		{
			public CustomMultipartFormDataStreamProvider(string path)
				: base(path)
			{ }

			public override string GetLocalFileName(HttpContentHeaders headers)
			{
				var fileName = headers.ContentDisposition.FileName;
				var name = !string.IsNullOrWhiteSpace(fileName) ? fileName : "NoName";
				return name.Trim('"'); //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped
			}
		}

		//public HttpResponseMessage Post()
		//{
		//	var files = HttpContext.Current.Request.Files;
		//	if (files.Count > 0)
		//	{
		//		string[] permittedFiles = { "image/jpeg", "image/png", "image/gif" };
		//		for (var i = 0; i < files.Count; i++)
		//		{
		//			var file = files[i];
		//			if (permittedFiles.Contains(file.ContentType))
		//			{
		//				var filePath = HostingEnvironment.MapPath("~/_temp/" + file.FileName);
		//				file.SaveAs(filePath);
		//			}
		//		}

		//		return Request.CreateResponse(HttpStatusCode.Created);
		//	}
		//	else
		//	{
		//		return Request.CreateResponse(HttpStatusCode.BadRequest);
		//	}
		//}
		
		//public void Post([FromBody]IEnumerable<HttpPostedFileBase> files)
		//{
		//	foreach (var file in files)
		//	{
		//		if (file.ContentLength > 0)
		//		{
		//			var fileName = Path.GetFileName(file.FileName);
		//			var path = Path.Combine(HostingEnvironment.MapPath("~/_temp"), fileName);
		//			file.SaveAs(path);
		//		}
		//	}
		//}
	}
}
