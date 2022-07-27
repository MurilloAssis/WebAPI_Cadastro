using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Intelitrader.Models;
using Intelitrader.ViewModels;
using Newtonsoft.Json;

namespace Intelitrader.Services
{
    public class ApiCadastro 
    {
        private HttpClient _httpClient = new HttpClient();
        private string _url = "http://192.168.0.30:5000/v1/";

        
        public async Task AddItemAsync(User user)
        {
            try
            {
                var usuario = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_url + "CadastrarUsuarios", usuario);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao adicionar usuário");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw ex;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_url}DeletarUsuarios/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao excluir usuário");

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine( ex.Message);
                throw ex;              
            }
        }

        public List<User> Listar()
        {
            string urlGet = _url + "ListarUsuarios";
            try
            {
                var response = _httpClient.GetStringAsync(urlGet).Result;
                Debug.WriteLine(response);
                var usuarios = JsonConvert.DeserializeObject<List<User>>(response); 
                Debug.WriteLine(usuarios);
                return usuarios;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw ex;
            }
        }

        public async Task UpdateItemAsync(User user)
        {
            try
            {
                UserViewModel userModel = new UserViewModel();
                userModel.Firstname = user.Firstname;
                userModel.Surname = user.Surname;
                userModel.Age = user.Age;
                userModel.CreationDate = user.creationDate;

                var usuario = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(_url + "AtualizarUsuarios/" + user.IdUsuario, usuario);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao atualizar usuário");

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;               
            }
        }
    }
}
