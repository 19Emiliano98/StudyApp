using System;
using System.Collections.Generic;

namespace StudyApp.Models
{
    public partial class Course
    {
        public int IdCourse { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Duracion { get; set; }
        public int? IdCategoria { get; set; }
        public decimal? Precio { get; set; }

        public virtual Categoria? oCategoria { get; set; }
    }
}
