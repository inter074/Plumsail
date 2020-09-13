using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FormElement : Entity
    {
        public string Type { get; set; }
        /// <summary>
        /// Порядок сортировки элементов на форме
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Параметры элемнета в json формате
        /// </summary>
        public string State { get; set; }
    }
}
