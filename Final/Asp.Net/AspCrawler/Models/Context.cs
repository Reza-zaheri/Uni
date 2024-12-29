using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspCrawler.Models.Dto;
using AspCrawler.Models.Dto.Roles;

namespace AspCrawler.Models
{
    public class Context:IdentityDbContext<User, Role, string>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Enroll> Enrolls { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne<Category>()
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(p => p.CatId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
            modelBuilder.Entity<Enroll>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(p => p.IdP)
                .IsRequired(false);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.ProviderKey, p.LoginProvider });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(p => new { p.UserId, p.LoginProvider });
            modelBuilder.Entity<User>().Ignore(p => p.PhoneNumber);
            modelBuilder.Entity<User>().Ignore(p => p.PhoneNumberConfirmed);
            modelBuilder.Entity<User>().Ignore(p => p.TwoFactorEnabled);
            modelBuilder.Entity<User>().Ignore(p => p.NormalizedEmail);
            modelBuilder.Entity<Product>().Property(u =>u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Enroll>().Property(u => u.Id).ValueGeneratedOnAdd();
        }
        public DbSet<UserEditDto> UserEditDto { get; set; } = default!;
        public DbSet<AspCrawler.Models.Dto.UserListDto> UserListDto { get; set; } = default!;
        public DbSet<AspCrawler.Models.Dto.Roles.RoleListDto> RoleListDto { get; set; } = default!;
    }
}
