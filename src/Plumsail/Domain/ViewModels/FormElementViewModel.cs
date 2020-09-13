using System;
using Newtonsoft.Json.Linq;

namespace Domain.ViewModels
{
    public class FormElementViewModel
    {
        public FormElementViewModel(){}

        public FormElementViewModel(Guid id, string type, int orderIndex, JToken state)
        {
            Id = id;
            Type = type;
            OrderIndex = orderIndex;
            State = state;
        }
        public Guid Id { get; set; }
        public string Type { get; set; }
        public int OrderIndex { get; set; }
        public JToken State { get; set; }
    }
}