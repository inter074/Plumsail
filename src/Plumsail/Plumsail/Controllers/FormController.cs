using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Sql.DbServices;
using Domain.Entities;
using Domain.Entities.Logic;
using Domain.Filters;
using Domain.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Plumsail.Controllers
{
    [Route("form")]
    public class FormController : Controller
    {
        private IFormService _formService;
        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Create(Guid id) => Ok(await _formService.GetById(id));

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]FormRequestModel model) => Ok(await _formService.CreateFormAsync(model));

        [HttpGet("byFilter")]
        public async Task<IActionResult> GetByFilter([FromQuery]FormFilter filter) => Ok(await _formService.GetFormsByFilterAsync(filter));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FormRequestModel model) => Ok(await _formService.UpdateFormAsync(id, model));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _formService.DeleteFormAsync(id);
            return NoContent();
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteAll()
        {
            await _formService.DeleteAllFormsAsync();
            return NoContent();
        }
    }
}
