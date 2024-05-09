using Microsoft.EntityFrameworkCore;
using UsersManagement.DAL.Entities;

namespace UsersManagement.DAL;

public class DataContext : DbContext
{
    public DataContext()
    {
    }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connection = "Host=localhost;Port=3306;Database=Subscriptions;User=user;Password=password;Pooling=True;Convert zero datetime=True;Charset=utf8";
        optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Subscription> Subscriptions { get; set; }
}