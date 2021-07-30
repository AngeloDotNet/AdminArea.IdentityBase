using Microsoft.EntityFrameworkCore;

namespace AdminArea_IdentityBase.Models.Services.Infrastructure
{
    public partial class AdminAreaDbContext : DbContext
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