using Intelitrader.Models;
using Intelitrader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intelitrader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        
        private ApiCadastro _service = new ApiCadastro();
        public MainPage()
        {
            InitializeComponent();
            listaUsuarios.ItemsSource = _service.Listar();
        }
        async void TelaCadastro (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cadastro());
        }

        void RefreshView_Refreshing(object sender, EventArgs e)
        {
            listaUsuarios.ItemsSource = _service.Listar();
            myRefreshView.IsRefreshing = false;
        }

        private void Excluir_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            User usuario = menuItem.CommandParameter as User;
            _ = _service.Delete(usuario.IdUsuario);
            Thread.Sleep(3000);
            RefreshView_Refreshing(sender, e);
        }

        private void Atualizar_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            User usuario = menuItem.CommandParameter as User;
            Console.Write(usuario.IdUsuario.ToString());
            Navigation.PushAsync(new InfoUsuario(usuario));
        }
    }
}