using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI_Cadastro.Models
{
    public partial class Usuario
    {
        public string IdUsuario { get; set; }
        public string FirstName { get; set; }
        public string? SurName { get; set; }
        public int Age { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
