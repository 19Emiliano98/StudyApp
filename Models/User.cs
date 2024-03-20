using System;
using System.Collections.Generic;

namespace StudyApp.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Pass { get; set; }
        public string? Rol { get; set; }
    }
}
