using AdminArea_IdentityBase.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminArea_IdentityBase.Models.Services.Infrastructure
{
    public partial class AdminAreaDbContext : IdentityDbContext<ApplicationUser>
    {
        public AdminAreaDbContext(DbContextOptions<AdminAreaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}