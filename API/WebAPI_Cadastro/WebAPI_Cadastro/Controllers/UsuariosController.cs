using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI_Cadastro.Contexts;
using WebAPI_Cadastro.Models;
using WebAPI_Cadastro.ViewModels;

namespace WebAPI_Cadastro.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UsuariosController : Controller
    {
        IntelitraderContext ctx = new IntelitraderContext();
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(ILogger<UsuariosController> logger)
        {
            _logger = logger;  
        }
        
        [HttpGet]
        [Route("ListarUsuarios")]
        public IActionResult GetUsuarios()
        {
            try
            {
                List<Usuario> usuarios = ctx.Usuarios.ToList();
                _logger.LogInformation("Usuarios listados" + usuarios);

                return Ok(usuarios);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Usuarios não encontrados");
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("CadastrarUsuarios")]
        public IActionResult PostUsuarios(UsuariosViewModel novoUsuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuario = new Usuario();
                    usuario.FirstName = novoUsuario.FirstName;
                    usuario.SurName = novoUsuario.SurName;
                    usuario.CreationDate = DateTime.Now;
                    usuario.Age = novoUsuario.Age;
                    ctx.Usuarios.Add(usuario);
                    ctx.SaveChanges();
                    _logger.LogInformation("Usuário cadastrado" + usuario);


                    return StatusCode(201, usuario);
                }
                else
                {
                    _logger.LogWarning("Dados inválidos: " + novoUsuario);
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao cadastrar um usuário");
                return StatusCode(500);
            }
        }

        [HttpPut("AtualizarUsuarios/{id}")]
        public IActionResult UpdateUsuarios(UsuariosViewModel usuarioAtualizado, int id)
        {
            Usuario usuarioBuscado = ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

            try
            {
                if (ModelState.IsValid)
                {
                    usuarioBuscado.FirstName = usuarioAtualizado.FirstName;
                    usuarioBuscado.SurName = usuarioAtualizado.SurName;
                    usuarioBuscado.Age = usuarioAtualizado.Age;
                    ctx.Usuarios.Update(usuarioBuscado);
                    ctx.SaveChanges();
                    _logger.LogInformation("Usuario Atualizado" + usuarioBuscado);

                    return Ok(usuarioBuscado);
                }
                else
                {
                    _logger.LogWarning("Dados inválidos: " + usuarioAtualizado);
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Ocorreu um erro ao cadastrar um usuário");
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("DeletarUsuarios/{id:int}")]
        public IActionResult DeletarUsuarios(int id)
        {
            Usuario usuarioBuscado = ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
            try
            {
                ctx.Usuarios.Remove(usuarioBuscado);
                ctx.SaveChangesAsync();
                _logger.LogInformation("Usuário deletado! " + usuarioBuscado);

                return StatusCode(204);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Usuário não encontrado");
                return StatusCode(400, ex);
            }
        }
    }
}
