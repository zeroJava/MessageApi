using MessageApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace MessageApi.Infastructure.Sqlite;

internal class MessageDbContext : DbContext
{
   readonly string dbpath = string.Empty;

   public DbSet<User> Users { get; set; }
   public DbSet<Message> Messages { get; set; }
   public DbSet<MessageDispatch> MessageDispatches { get; set; }

   protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={dbpath}");

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<User>().ToTable("User");
      modelBuilder.Entity<Message>().ToTable("Message");
      modelBuilder.Entity<MessageDispatch>().ToTable("MessageDispatch");
   }

   public MessageDbContext()
   {
      //Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
      //string path = Environment.GetFolderPath(folder);
      string path = @"C:\Users\zero_\Documents\Software Development\C# Applications\Webtech-Apps\Messaging\Resource\Database";
      dbpath = Path.Join(path, "database.db");
   }
}