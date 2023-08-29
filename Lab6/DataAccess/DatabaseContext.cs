using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<MessageSource> MessageSources { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<MessageResponse> MessagesResponse { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(builder =>
        {
            builder.HasOne(x => x.Employee)
                .WithOne(x => x.Account)
                .HasForeignKey<Employee>(b => b.AccountId);
        });

        modelBuilder.Entity<Employee>(builder =>
        {
            builder.HasOne(x => x.Account)
                .WithOne(x => x.Employee)
                .IsRequired(false);
        });

        modelBuilder.Entity<Employee>(builder =>
        {
            builder.HasDiscriminator<string>("Discriminator")
                .HasValue<Employee>(nameof(Employee))
                .HasValue<Manager>(nameof(Manager));
        });

        modelBuilder.Entity<Manager>(builder =>
        {
            builder.HasMany(x => x.Subordinates);
        });

        modelBuilder.Entity<MessageSource>(builder =>
            builder.HasDiscriminator<string>("Discriminator")
                .HasValue<PhoneMessageSource>(nameof(PhoneMessageSource))
                .HasValue<EmailMessageSource>(nameof(EmailMessageSource)));

        modelBuilder.Entity<Message>(builder =>
        {
            builder.HasOne(x => x.MessageSource);

            builder.HasDiscriminator<string>("Discriminator")
                .HasValue<EmailMessage>(nameof(EmailMessage))
                .HasValue<PhoneMessage>(nameof(PhoneMessage));
        });
        modelBuilder.Entity<Report>(builder =>
        {
            builder.HasOne(x => x.Manager);
        });
    }
}