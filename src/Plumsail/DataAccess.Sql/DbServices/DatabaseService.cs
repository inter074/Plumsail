using System.Threading.Tasks;
using DataAccess.Sql.Repositories;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Sql.DbServices
{
    public class DatabaseService : IDatabaseService
    {
        protected readonly DatabaseContext Context;

        public DatabaseService(DatabaseContext context)
        {
            Context = context;
        }

        public FormRepository Forms => new FormRepository(Context);

        public IRepository<FormElement> FormElements { get; }

        public void Commit() => Context.SaveChanges();

        public async Task CommitAsync() => await Context.SaveChangesAsync();
    }
}