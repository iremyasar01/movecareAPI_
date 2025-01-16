using Microsoft.EntityFrameworkCore;
using movecareAPI.Models;

namespace movecareAPI.Data;

public class MoveDbContext : DbContext
{
    public MoveDbContext(DbContextOptions<MoveDbContext> options) : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=MoveCare;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsersModels>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.userId);

            entity.Property(e => e.userId)
                .HasColumnName("UserId")
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

            entity.Property(e => e.email)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.username)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.name)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.surname)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.phoneNumber)
                .IsRequired();

            entity.Property(e => e.password)
                .IsRequired();

            entity.Property(e => e.passwordAgain)
                .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<UsersModels> Users { get; set; }
}