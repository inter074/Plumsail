using System.Collections;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Form : Entity
    {
        public string Title { get; set; }
        /// <summary>
        /// Form controls
        /// </summary>
        public ICollection<FormElement> Elements { get; set; }
        public bool IsDeleted { get; set; }
    }
}