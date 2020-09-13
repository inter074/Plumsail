using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataAccess.Sql.DbServices;
using Domain.Entities;
using Domain.Entities.Logic;
using Domain.Filters;
using Domain.RequestModels;
using Domain.ViewModels;
using Infrastructure.ErrorResult;
using Infrastructure.Extensions;
using Newtonsoft.Json.Linq;

namespace Logic.Services
{
    public class FormService : IFormService
    {
        private readonly IDatabaseService _dbService;
        public FormService(IDatabaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IEnumerable<FormViewModel>> GetFormsByFilterAsync(FormFilter filter)
        {
            return await _dbService.Forms.GetFormByFilter(filter);
        }

        public async Task<FormViewModel> GetById(Guid id)
        {
            return ToModals(new List<Form>(){ await _dbService.Forms.GetFormById(id) }).FirstOrDefault();
        }

        public async Task<FormViewModel> CreateFormAsync(FormRequestModel model)
        {
            if (model == null)
                throw new ErrorModel("Incorrect form").Throw(HttpStatusCode.BadRequest);
            var form = new Form()
            {
                Title = model.Title,
                Elements = model.Elements?.Where(x => x.State != null)
                    .Select(e => new FormElement() {Type = e.Type, OrderIndex = e.OrderIndex, State = e.State.ToString()}).ToArray()
            };
            _dbService.Forms.AddOne(form);
            await _dbService.CommitAsync();
            return ToModals(new List<Form>() {form}).FirstOrDefault();
        }

        public async Task<FormViewModel> UpdateFormAsync(Guid id, FormRequestModel model)
        {
            var form = await _dbService.Forms.GetFormById(id);
            if (form == null)
                throw new ErrorModel("Form not found").Throw(HttpStatusCode.NotFound);
            form.Elements ??= new List<FormElement>();
            foreach (var elementToRemove in form.Elements.Where(x => !model.Elements?.Select(e => e.Id).Contains(x.Id) ?? true).ToArray())
            {
                form.Elements.Remove(elementToRemove);
            }
            _dbService.Forms.UpdateOne(form);
            var hasChange = false;
            void UpdateOrAddElements(FormElementRequestModel model)
            {
                if (model.Id != default && form.Elements.Any(x => x.Id == model.Id))
                {
                    var element = form.Elements.FirstOrDefault(x => x.Id == model.Id);
                    if (element != null)
                    {
                        element.Type = model.Type;
                        element.State = model.State.ToString();
                        element.OrderIndex = model.OrderIndex;
                        hasChange = true;
                    }
                }

                else
                {
                    form.Elements.Add(new FormElement() { OrderIndex = model.OrderIndex, Type = model.Type, State = model.State.ToString() });
                    hasChange = true;
                }
            }

            foreach (var elementModel in model.Elements)
            {
                UpdateOrAddElements(elementModel);
            }
            hasChange = hasChange || form.Title != model.Title;
            form.Title = model.Title;
            if (hasChange)
            {
                _dbService.Forms.UpdateOne(form);
                await _dbService.CommitAsync();
            }

            return ToModals(new List<Form>() { form }).FirstOrDefault();
        }

        public async Task DeleteFormAsync(Guid id)
        {
            var form = _dbService.Forms.FindOne(x => x.Id == id);
            if (form == null)
                throw new ErrorModel("Form not found").Throw(HttpStatusCode.NotFound);

            form.IsDeleted = true;
            _dbService.Forms.UpdateOne(form);
            await _dbService.CommitAsync();
        }

        public async Task DeleteAllFormsAsync()
        {
            await _dbService.Forms.DeleteAll();
        }

        private IEnumerable<FormViewModel> ToModals(List<Form> forms) =>
            forms.Select(form => 
                new FormViewModel(form.Id, form.Title, 
                    form.Elements.Select(e => new FormElementViewModel(e.Id, e.Type, e.OrderIndex, JToken.Parse(e.State))).OrderBy(x => x.OrderIndex)
                    ))
                .ToArray();
    }
}