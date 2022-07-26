using Microsoft.EntityFrameworkCore;

namespace CompassTodo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite("DataSource=app.db;Cache=Shared");
        
    }
}
