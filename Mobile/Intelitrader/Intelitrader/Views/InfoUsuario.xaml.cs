using Intelitrader.Models;
using Intelitrader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intelitrader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoUsuario : ContentPage
    {
        private ApiCadastro _service = new ApiCadastro();
        private User user { get; set; }
        public InfoUsuario(User usuario)
        {
            InitializeComponent();
            BindingContext = usuario;
            user = usuario;
            Botao.Clicked += Botao_Clicked;
        }
        private void Botao_Clicked(object sender, EventArgs e)
        {
            Botao.IsEnabled = false;
            Resposta.Text = "Carregando";
            if (FirstName.Text != null & Age.Text != null)
            {
                User userAtualizado = new User();
                userAtualizado.IdUsuario = user.IdUsuario;
                userAtualizado.Firstname = FirstName.Text;
                userAtualizado.Surname = Surname.Text;
                userAtualizado.Age = Convert.ToInt32(Age.Text);

                _ = _service.UpdateItemAsync(userAtualizado);

                Resposta.Text = "Usuário Atualizado!";
                Thread.Sleep(5000);
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                Resposta.Text = "Firstname e Age são obrigatórios!";
            }
        }
    }
}