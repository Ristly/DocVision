using Microsoft.EntityFrameworkCore;

namespace DocVisionMessageApi.DbContexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; }
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

}

