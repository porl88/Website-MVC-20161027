namespace MVC.Services.Message
{
    using Core.Configuration;

    public class MessageRequest
    {
        public MessageRequest()
        {
            this.FromAddress = EmailConfig.DefaultEmail;
        }

        public string SenderFirstName { get; set; }

        public string SenderLastName { get; set; }

        public string SenderFullName
        {
            get
            {
                return string.Format("{0} {1}", this.SenderFirstName, this.SenderLastName);
            }
        }

        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
