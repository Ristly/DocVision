using DocVisionMessageApi.DbContexts;

namespace DocVisionMessageApi.Services;

public class MessageService : IMessageService
{
    private readonly ApplicationDbContext _context;

    public MessageService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> RegisterMessage(Message message)
    {
        try
        {
            if(!DateChecker(message.SendDate))
                return false;

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
#if DEBUG
            Console.WriteLine(ex.ToString());
#endif
            return false;
        }
    }

    public bool DateChecker(DateTime date)
        => date.Date < DateTime.UtcNow.Date ? false : true; 
}
