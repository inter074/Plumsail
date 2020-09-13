using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Filters;
using Domain.ViewModels;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace DataAccess.Sql.Repositories
{
    public class FormRepository : Repository<Form>
    {
        public FormRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Form> GetFormById(Guid formId)
        {
            return await Context.Forms.Where(x => x.Id == formId).Include(x => x.Elements).IgnoreQueryFilters()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FormViewModel>> GetFormByFilter(FormFilter filter)
        {
            filter ??= new FormFilter(){Take = 1};

            Expression<Func<Form, bool>> predicate = form => !form.IsDeleted;
            if (!string.IsNullOrEmpty(filter.Title))
                predicate = predicate.AndAlso(form => form.Title == filter.Title);

            var result = await Context.Forms
                .Include(x => x.Elements)
                .Where(predicate)
                .Skip(filter.Skip).Take(filter.Take < 0 ? Int32.MaxValue : filter.Take).ToArrayAsync();

            return result.Select(form => new FormViewModel(form.Id, form.Title,
                form.Elements.Select(e => new FormElementViewModel(e.Id, e.Type, e.OrderIndex, JToken.Parse(e.State))).OrderBy(x => x.OrderIndex)));
        }

        public async Task DeleteAll()
        {
             await Database.ExecuteSqlRawAsync("UPDATE \"Forms\" SET \"IsDeleted\" = true WHERE \"IsDeleted\" = false");
        }
    }
}