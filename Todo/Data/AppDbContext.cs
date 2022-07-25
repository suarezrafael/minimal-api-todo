using Microsoft.EntityFrameworkCore;

namespace Todos.Data { 

    public class AppDbContext : DbContext
    {
        public DbSet<Todos.Models.Todo> Todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}
