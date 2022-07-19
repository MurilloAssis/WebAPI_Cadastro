using System.ComponentModel.DataAnnotations;

namespace WebAPI_Cadastro.ViewModels
{
    public class UsuariosViewModel
    {
        [Required(ErrorMessage = "Nome deve ser preenchido!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Sobrenome deve ser preenchido!")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Idade deve ser preenchida!")]
        public int Age { get; set; }
    }
}
