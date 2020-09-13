using System;
using System.Collections.Generic;

namespace Domain.RequestModels
{
    public class FormRequestModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// Form controls
        /// </summary>
        public IEnumerable<FormElementRequestModel> Elements { get; set; }
    }
}