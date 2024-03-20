using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudyApp.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Courses = new HashSet<Course>();
        }

        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; }
    }
}
