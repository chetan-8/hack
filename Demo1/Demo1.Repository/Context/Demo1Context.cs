using Demo1.Core.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace Demo1.Repository.Context
{
    public class Demo1Context : DbContext
    {
        public Demo1Context(DbContextOptions<Demo1Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        public DbSet<Customer> Customer { get; set; }
    }
}
