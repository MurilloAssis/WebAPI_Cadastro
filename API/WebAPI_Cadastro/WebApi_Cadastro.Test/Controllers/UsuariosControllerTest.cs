﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebAPI_Cadastro.Controllers;
using WebAPI_Cadastro.ViewModels;
using Xunit;

namespace WebApi_Cadastro.Test.Controllers
{
    public class UsuariosControllerTest
    {

        [Fact]
        public void Deve_Retornar_Lista_Usuarios()
        {
            var fakelogger = new Mock<ILogger<UsuariosController>>();

            var controller = new UsuariosController(fakelogger.Object);

            var result = controller.GetUsuarios();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Deve_Cadastrar_Usuario()
        {
            UsuariosViewModel novoUsuario = new UsuariosViewModel();
            novoUsuario.FirstName = "Teste";
            novoUsuario.SurName = "Teste";
            novoUsuario.Age = 10;

            var fakelogger = new Mock<ILogger<UsuariosController>>();
            var controller = new UsuariosController(fakelogger.Object);
            var resultado = controller.PostUsuarios(novoUsuario);

            ObjectResult actionResult = resultado as ObjectResult;

            Assert.NotNull(resultado);
            Assert.Equal(201, actionResult.StatusCode);
        }

        [Fact]
        public void Deve_Alterar_Usuario()
        {
            UsuariosViewModel alteraUsuario = new UsuariosViewModel();
            alteraUsuario.FirstName = "Teste";
            alteraUsuario.SurName = "Teste";
            alteraUsuario.Age = 10;

            var fakelogger = new Mock<ILogger<UsuariosController>>();
            var controller = new UsuariosController(fakelogger.Object);
            var resultado = controller.UpdateUsuarios(alteraUsuario, 1);

            ObjectResult actionResult = resultado as ObjectResult;

            Assert.NotNull(resultado);
            Assert.Equal(200, actionResult.StatusCode);
        }
        [Fact]
        public void Deve_Excluir_Usuario()
        {
            var fakelogger = new Mock<ILogger<UsuariosController>>();
            var controller = new UsuariosController(fakelogger.Object);

            var resultado = controller.DeletarUsuarios(1);

            ObjectResult actionResult = resultado as ObjectResult;

            Assert.NotNull(resultado);
            Assert.Equal(204, actionResult.StatusCode);
        }
    }
}
