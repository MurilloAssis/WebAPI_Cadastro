using System.Collections.Generic;
using WebAPI_Cadastro.Models;
using WebAPI_Cadastro.ViewModels;

namespace WebAPI_Cadastro.Interfaces
{
    public interface IUsuarioRepository
    {
        void DeletarUsuarios(int id);
        Usuario UpdateUsuarios(UsuariosViewModel usuarioAtualizado, int id);
        Usuario PostUsuarios(UsuariosViewModel novoUsuario);
        List<Usuario> GetUsuarios();
    }
}
