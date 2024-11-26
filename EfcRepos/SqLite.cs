using Microsoft.EntityFrameworkCore;
using ServerEntities;

namespace EfcRepos;

public class SqLite : DbContext {
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Channel> Channels => Set<Channel>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}