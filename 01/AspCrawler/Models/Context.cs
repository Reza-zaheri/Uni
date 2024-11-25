using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspCrawler.Models.Dto;

namespace AspCrawler.Models
{
    public class Context:IdentityDbContext<User, Role, string>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Fluctuation> Fluctuations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne<Category>()
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(p => p.CatId)
                .IsRequired(false);
            modelBuilder.Entity<Media>()
                .HasOne<Product>()
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(p => p.Id_P)
                .IsRequired(false);
            modelBuilder.Entity<Fluctuation>()
                .HasOne<Product>()
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(p => p.IdP)
                .IsRequired(false);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.ProviderKey, p.LoginProvider });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(p => new { p.UserId, p.LoginProvider });
            modelBuilder.Entity<User>().Ignore(p => p.PhoneNumber);
            modelBuilder.Entity<User>().Ignore(p => p.PhoneNumberConfirmed);
            modelBuilder.Entity<User>().Ignore(p => p.TwoFactorEnabled);
            modelBuilder.Entity<User>().Ignore(p => p.NormalizedEmail);
        }
        public DbSet<UserEditDto> UserEditDto { get; set; } = default!;
        public DbSet<AspCrawler.Models.Dto.UserListDto> UserListDto { get; set; } = default!;
    }
}
