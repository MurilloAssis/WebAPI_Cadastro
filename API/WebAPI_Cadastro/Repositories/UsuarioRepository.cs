using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WebAPI_Cadastro.Contexts;
using WebAPI_Cadastro.Interfaces;
using WebAPI_Cadastro.Models;
using System.Linq;
using WebAPI_Cadastro.ViewModels;

namespace WebAPI_Cadastro.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        IntelitraderContext ctx = new IntelitraderContext();
       

        public void DeletarUsuarios(string id)
        {
            try
            {
                Usuario usuarioBuscado = ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
                ctx.Usuarios.Remove(usuarioBuscado);
                ctx.SaveChangesAsync();                         
            }
            catch (Exception)
            {

                throw;      
            }
        }

        public Usuario UpdateUsuarios(UsuariosViewModel usuarioAtualizado, string id)
        {
            Usuario usuarioBuscado = ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
            try
            {
                usuarioBuscado.FirstName = usuarioAtualizado.FirstName;
                usuarioBuscado.SurName = usuarioAtualizado.SurName;
                usuarioBuscado.Age = usuarioAtualizado.Age;
                ctx.Usuarios.Update(usuarioBuscado);
                ctx.SaveChanges();

                return usuarioBuscado;
            }
            catch (Exception)
            {
                return null;    
                throw;
            };
        }

        public Usuario PostUsuarios(UsuariosViewModel novoUsuario)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.IdUsuario = Guid.NewGuid().ToString();
                usuario.FirstName = novoUsuario.FirstName;
                usuario.SurName = novoUsuario.SurName;
                usuario.CreationDate = DateTime.Now;
                usuario.Age = novoUsuario.Age;
                ctx.Usuarios.Add(usuario);
                ctx.SaveChanges();

                return usuario;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
           
        }

        public List<Usuario> GetUsuarios()
        {
            try
            {
                List<Usuario> usuarios = ctx.Usuarios.ToList();
                return usuarios;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
