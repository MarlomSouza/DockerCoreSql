using docker_app_compose.Dominio;
using Microsoft.EntityFrameworkCore;

namespace docker_app_compose.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}