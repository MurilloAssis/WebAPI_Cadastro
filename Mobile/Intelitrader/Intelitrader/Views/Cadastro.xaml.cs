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
    public partial class Cadastro : ContentPage
    {
        private ApiCadastro _service = new ApiCadastro();
        public Cadastro()
        {
            InitializeComponent();
            Botao.Clicked += BotaoClicked;
        }

        private async void BotaoClicked(object sender, EventArgs e)
        {
            Botao.IsEnabled = false;
            Resposta.Text = "Carregando...";
            if (FirstName.Text != null & Age.Text != null)
            {
                User newUser = new User();
                newUser.Firstname = FirstName.Text;
                newUser.Surname = SurName.Text;
                newUser.Age = Convert.ToInt32(Age.Text);

                await _service.AddItemAsync(newUser);
                Resposta.Text = "Usuário cadastrado com sucesso. Redirecionando...";
                Thread.Sleep(5000);
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                Resposta.Text = "FirstName e Age são Campos obrigatórios";
            }
        }
    }
}