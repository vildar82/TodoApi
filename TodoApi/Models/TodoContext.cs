using System;
using Microsoft.EntityFrameworkCore;
using TodoEntities;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public void Seed()
        {
            for (var i = 1; i <= 3; i++)
            {
                TodoItems.Add(new TodoItem {Name = "Item " + i, Created = DateTime.Now});
            }
            SaveChanges();
        }
    }
}
