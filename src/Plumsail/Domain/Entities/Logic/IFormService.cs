using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Filters;
using Domain.RequestModels;
using Domain.ViewModels;

namespace Domain.Entities.Logic
{
    public interface IFormService
    {
        /// <summary>
        /// Get by Id ignore isDeleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FormViewModel> GetById(Guid id);
        /// <summary>
        /// Get form list by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<FormViewModel>> GetFormsByFilterAsync(FormFilter filter);

        /// <summary>
        /// Add new form
        /// </summary>
        /// <param name="model"></param>
        Task<FormViewModel> CreateFormAsync(FormRequestModel model);

        /// <summary>
        /// Update form
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<FormViewModel> UpdateFormAsync(Guid id, FormRequestModel model);

        /// <summary>
        /// Set isDeleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteFormAsync(Guid id);

        /// <summary>
        /// Mark all forms as isDeleted
        /// </summary>
        /// <returns></returns>
        Task DeleteAllFormsAsync();
    }
}