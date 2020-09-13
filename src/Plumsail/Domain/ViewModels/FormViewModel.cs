using System;
using System.Collections.Generic;

namespace Domain.ViewModels
{
    public class FormViewModel
    {
        public FormViewModel(){}

        public FormViewModel(Guid id, string title, IEnumerable<FormElementViewModel> elements)
        {
            Id = id;
            Title = title;
            Elements = elements;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<FormElementViewModel> Elements { get; set; }
    }
}