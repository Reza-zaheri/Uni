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
        public DbSet<pr_digi> pr_digis { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
