using System.Threading.Tasks;
using DataAccess.Sql.Repositories;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace DataAccess.Sql.DbServices
{
    public interface IDatabaseService
    {
        FormRepository Forms { get; }
        IRepository<FormElement> FormElements { get; }
        void Commit();

        Task CommitAsync();

    }
}