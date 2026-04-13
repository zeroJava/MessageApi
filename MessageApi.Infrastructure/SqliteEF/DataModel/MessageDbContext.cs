using MessageApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace MessageApi.Infrastructure.Sqlite;

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

      modelBuilder.Entity<Message>(entity =>
      {
         entity.ToTable("Message");

         entity.HasIndex(e => e.Id, "IX_Message_ID").IsUnique();

         entity.Property(e => e.Id).HasColumnName("ID");
         entity.Property(e => e.MessageCreated).HasColumnName("MESSAGECREATED");
         entity.Property(e => e.MessageText).HasColumnName("MESSAGETEXT");
         entity.Property(e => e.SenderEmailAddress).HasColumnName("SENDEREMAILADDRESS");
         entity.Property(e => e.SenderId).HasColumnName("SENDERID");

         entity.HasOne(d => d.User).WithMany(p => p.Messages).HasForeignKey(d => d.SenderId);
      });

      modelBuilder.Entity<MessageDispatch>(entity =>
      {
         entity.ToTable("MessageDispatch");

         entity.HasIndex(e => e.Id, "IX_MessageDispatch_ID").IsUnique();

         entity.Property(e => e.Id).HasColumnName("ID");
         entity.Property(e => e.EmailAddress).HasColumnName("EMAILADDRESS");
         entity.Property(e => e.MessageId).HasColumnName("MESSAGEID");
         entity.Property(e => e.MessageReceived).HasColumnName("MESSAGERECEIVED");
         entity.Property(e => e.MessageReceivedTime).HasColumnName("MESSAGERECEIVEDTIME");

         entity.HasOne(d => d.Message).WithMany(p => p.MessageDispatches).HasForeignKey(d => d.MessageId);
      });

      modelBuilder.Entity<User>(entity =>
      {
         entity.ToTable("User");

         entity.HasIndex(e => e.EmailAddress, "IX_User_EMAILADDRESS").IsUnique();

         entity.HasIndex(e => e.Id, "IX_User_ID").IsUnique();

         entity.HasIndex(e => e.UserName, "IX_User_USERNAME").IsUnique();

         entity.Property(e => e.Id).HasColumnName("ID");
         entity.Property(e => e.DOB).HasColumnName("DOB");
         entity.Property(e => e.EmailAddress).HasColumnName("EMAILADDRESS");
         entity.Property(e => e.FirstName).HasColumnName("FIRSTNAME");
         entity.Property(e => e.Gender).HasColumnName("GENDER");
         entity.Property(e => e.Password).HasColumnName("PASSWORD");
         entity.Property(e => e.Surname).HasColumnName("SURNAME");
         entity.Property(e => e.UserName).HasColumnName("USERNAME");
      });
   }

   public MessageDbContext()
   {
      //Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
      //string path = Environment.GetFolderPath(folder);
      string path = @"C:\Users\zero_\Documents\Software Development\C# Applications\Webtech-Apps\Messaging\Resource\Database";
      dbpath = Path.Join(path, "database.db");
   }
}