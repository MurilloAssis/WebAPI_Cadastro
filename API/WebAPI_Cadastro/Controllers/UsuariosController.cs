using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI_Cadastro.Contexts;
using WebAPI_Cadastro.Interfaces;
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
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(ILogger<UsuariosController> logger, IUsuarioRepository repo)
        {
            _logger = logger;  
            _usuarioRepository = repo;
        }
        
        [HttpGet]
        [Route("ListarUsuarios")]
        public IActionResult GetUsuarios()
        {
            try
            {
                List<Usuario> usuarios = _usuarioRepository.GetUsuarios();

                if (usuarios.Count == 0)
                {
                    _logger.LogInformation("Não há usuários cadastrados no sistema");
                    return NoContent(); 
                }
                _logger.LogInformation(usuarios.Count + " Usuarios listados");

                return Ok(usuarios);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " Usuarios não encontrados");
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
                    if (novoUsuario.FirstName != null && novoUsuario.SurName != null && novoUsuario.Age > 0)
                    {

                        Usuario usuario = _usuarioRepository.PostUsuarios(novoUsuario);
                        _logger.LogInformation("Usuário cadastrado" + usuario);


                        return StatusCode(201, usuario);
                    }
                    return BadRequest();

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
        public IActionResult UpdateUsuarios(UsuariosViewModel usuarioAtualizado, string id)
        {
             try
            {
                if (usuarioAtualizado.FirstName != null && usuarioAtualizado.SurName != null && usuarioAtualizado.Age > 0)
                {
                    
                    Usuario usuarioBuscado = _usuarioRepository.UpdateUsuarios(usuarioAtualizado, id);
                    _logger.LogInformation("Usuario Atualizado" + usuarioBuscado);

                    return Ok(usuarioBuscado);
                    
                }
                else
                {
                    _logger.LogWarning("Dados inválidos: " + usuarioAtualizado);
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Ocorreu um erro ao cadastrar um usuário");
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("DeletarUsuarios/{id}")]
        public IActionResult DeletarUsuarios(string id)
        {         
            if (id != null)
            {
                try
                {
                    _usuarioRepository.DeletarUsuarios(id);
                    _logger.LogInformation($"Usuário id:{id} deletado!");

                    return NoContent();
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex, "Usuário não encontrado");
                    return StatusCode(400, ex);
                }
            }
            return BadRequest();
        }
    }
}
