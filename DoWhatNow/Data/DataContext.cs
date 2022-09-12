using DoWhatNow.Models;
using Microsoft.EntityFrameworkCore;

namespace DoWhatNow.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        public DbSet<ToDoList> ToDoLists { get; set; }
    }
}
