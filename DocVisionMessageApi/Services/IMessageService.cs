namespace DocVisionMessageApi.Services
{
    public interface IMessageService
    {
        public Task<bool> RegisterMessage(Message message);
    }
}