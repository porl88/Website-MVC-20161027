namespace MVC.Core.Exceptions
{
    using System;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Net.Mail;
    using System.Reflection;
    using System.Text;
    using System.Web;
    using Configuration;

    public class EmailExceptionHandler : IExceptionHandler
    {
        private const int EmailLimit = 30;
        private static int counter;
        private static DateTime timeStamp;
        private Exception exception;

        public void HandleException(Exception ex)
        {
            if (timeStamp != DateTime.Today)
            {
                counter = 0;
                timeStamp = DateTime.Today;
            }

            counter++;

            if (counter <= EmailLimit)
            {
                try
                {
                    this.exception = ex;
                    this.SendEmail();
                }
                catch
                {
                }
            }
        }

        private void SendEmail()
        {
            var domain = AppDomain.CurrentDomain.BaseDirectory;
            var from = "errors@" + WebsiteConfig.WebsiteUrl;
            var to = EmailConfig.TechSupportEmail;
            var subject = string.Format("ERROR - {0}: {1}", WebsiteConfig.WebsiteUrl, this.exception.Message);
            var body = this.CreateEmailBody();
            var mailMessage = new MailMessage(from, to, subject, body);
            mailMessage.IsBodyHtml = true;
            var client = new SmtpClient();
            client.Send(mailMessage);
        }

        private string CreateEmailBody()
        {
            var assembly = Assembly.GetExecutingAssembly();
            // exception-email.html - 'Build Action' property needs to be set to 'Embedded Resource'
            using (var stream = assembly.GetManifestResourceStream("MVC.Core.Exceptions.exception-email.html"))
            {
                using (var reader = new StreamReader(stream))
                {
                    var body = reader.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        var context = HttpContext.Current;

                        if (context != null)
                        {
                            var request = context.Request;
                            body = body
                                .Replace("##URL##", request.Url.AbsoluteUri)
                                .Replace("##URLREFERRER##", request.UrlReferrer == null ? "None" : string.Format("<a href=\"{0}\">{0}</a>", request.UrlReferrer.AbsoluteUri))
                                .Replace("##USERAGENT##", request.UserAgent)
                                .Replace("##METHOD##", request.RequestType)
                                .Replace("##IPADDRESS##", request.UserHostAddress);

                            var user = context.User;
                            if (user != null)
                            {
                                body = body
                                .Replace("##USERNAME##", user.Identity.Name);
                            }
                        }

                        if (this.exception != null)
                        {
                            body = body
                                .Replace("##EXCEPTIONTYPE##", this.exception.GetType().ToString())
                                .Replace("##EXCEPTION##", this.exception.Message)
                                .Replace("##SOURCE##", this.exception.Source)
                                .Replace("##STACKTRACE##", this.exception.StackTrace)
                                .Replace("##INNEREXCEPTIONS##", this.CreateInnerExceptionHtml())
                                .Replace("##ERROR##", this.exception.ToString());
                        }

                        body = body
                            .Replace("##SERVERNAME##", Environment.MachineName);

                        return body;
                    }
                }
            }

            return string.Empty;
        }

        private string CreateInnerExceptionHtml()
        {
            var error = new StringBuilder();
            var exception = this.exception.InnerException;

            while (exception != null)
            {
                error.Append("<table>");
                error.AppendFormat("<tr><th>Message</th><td>{0}</td></tr>", exception.Message);
                error.AppendFormat("<tr><th>Stack Trace</th><td>{0}</td></tr>", exception.StackTrace);

                if (exception is DbEntityValidationException)
                {
                    error.Append("<tr><th>Entity Validation Exceptions</th><td>");

                    var entityException = (DbEntityValidationException)exception;
                    foreach (var eve in entityException.EntityValidationErrors)
                    {
                        error.AppendFormat("<p>Entity of type \"{0}\" in state \"{1}\" has the following validation errors:</p>", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        error.Append("<ul>");
                        foreach (var validationErrors in eve.ValidationErrors)
                        {
                            error.AppendFormat("<li>Property: \"{0}\", Error: \"{1}\"</li>", validationErrors.PropertyName, validationErrors.ErrorMessage).AppendLine();
                        }
                        error.Append("</ul>");
                    }

                    error.Append("</td></tr>");
                }

                error.Append("</table>");

                exception = exception.InnerException;
            }

            return error.ToString();
        }
    }
}
