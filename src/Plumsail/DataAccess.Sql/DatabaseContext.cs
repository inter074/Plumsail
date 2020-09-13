using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace DataAccess.Sql
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Form> Forms { get; set; }
        public DbSet<FormElement> FormElements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            {
                var eb = modelBuilder.Entity<FormElement>();
                eb.HasKey(m => m.Id);
                eb.Property(_ => _.Id);
                eb.Property(_ => _.State).HasColumnType("jsonb");
            }

            {
                var eb = modelBuilder.Entity<Form>();
                eb.HasKey(m => m.Id);
                eb.Property(_ => _.Id);
                eb.Property(_ => _.Title);
                eb.Property(_ => _.IsDeleted);
                eb.HasMany(_ => _.Elements).WithOne();
            }

            //.IgnoreQueryFilters() for get isDeleted
            modelBuilder.Entity<Form>().HasQueryFilter(o => !o.IsDeleted);
            base.OnModelCreating(modelBuilder);
        }

    }
}