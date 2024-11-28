using Microsoft.EntityFrameworkCore;

namespace TestsManager.DataAccess.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> ops) : base(ops)
    {
        
    }
}
