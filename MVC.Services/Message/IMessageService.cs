namespace MVC.Services.Message
{
    public interface IMessageService
    {
        void SendMessage(MessageRequest message);
    }
}
