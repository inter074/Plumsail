using System;
using Newtonsoft.Json.Linq;

namespace Domain.RequestModels
{
    public class FormElementRequestModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public int OrderIndex { get; set; }
        public JToken State { get; set; }
    }
}