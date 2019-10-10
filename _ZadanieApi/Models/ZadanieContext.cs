using Microsoft.EntityFrameworkCore;

namespace ZadanieApi.Models {
    public class ZadanieContext : DbContext {
        public ZadanieContext (DbContextOptions<ZadanieContext> options) : base (options) { }
        public DbSet<Zadanie> Zadania { get; set; }
    }
}