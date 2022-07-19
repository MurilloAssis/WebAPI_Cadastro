using System.Collections.Generic;
using WebAPI_Cadastro.Models;
using WebAPI_Cadastro.ViewModels;

namespace WebAPI_Cadastro.Interfaces
{
    public interface IUsuarioRepository
    {
        void DeletarUsuarios(string id);
        Usuario UpdateUsuarios(UsuariosViewModel usuarioAtualizado, string id);
        Usuario PostUsuarios(UsuariosViewModel novoUsuario);
        List<Usuario> GetUsuarios();
    }
}
