namespace MVC.Services.Message
{
    using System.Net.Mail;
    
    public class EmailService : IMessageService
	{
		public void SendMessage(MessageRequest message)
		{
			//var msg = new MailMessage();
			//msg.To.Add(message.ToAddress);
			//msg.From = new MailAddress(message.FromAddress, message.SenderFullName);
			//msg.Subject = message.Subject;
			//msg.Body = message.Message;

			//// automatically detect if message is in HTML
			//msg.IsBodyHtml = HtmlHelper.IsHtml(message.Message);

			//var smtp = new SmtpClient();
			//smtp.Send(msg);
		}
	}
}
