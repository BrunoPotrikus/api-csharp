using Microsoft.EntityFrameworkCore;
using MinhaApi.Models;

namespace MinhaApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<TodoModel> TodoModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("DataSource=app.db;Cache=Shared");
        }
    }
}
