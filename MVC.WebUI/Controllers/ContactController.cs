namespace MVC.WebUI.Controllers
{
	using System.Web.Mvc;
	using Core.Configuration;
	using MVC.Services.Message;
	using MVC.WebUI.Models.Shared;

	public class ContactController : Controller
    {
        private readonly IMessageService messageService;

        public ContactController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        // GET: /contact
        public ViewResult Index()
        {
			return this.View();
        }

        // GET: /contact/email
        [ChildActionOnly]
        public PartialViewResult Email()
        {
			return this.PartialView();
        }

        // POST: /contact/email
        [ChildActionOnly, HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult Email(EmailViewModel model)
        {
			if (ModelState.IsValid)
            {
				if (!Request.IsLocal)
				{
					var message = new MessageRequest
					{
						SenderFirstName = model.FirstName,
						SenderLastName = model.LastName,
						FromAddress = model.Email,
						Subject = model.Subject,
						Message = model.Message
					};

					this.messageService.SendMessage(message);

					// send copy to user
					message.ToAddress = model.Email;
					message.FromAddress = "donotreply@" + WebsiteConfig.WebsiteUrl;
					message.Subject = string.Format("Copy of message sent to {0}: {1}", WebsiteConfig.WebsiteUrl, message.Subject);
					this.messageService.SendMessage(message);
				}

				return this.PartialView("email-confirm");
            }

			return this.PartialView();
        }
    }
}