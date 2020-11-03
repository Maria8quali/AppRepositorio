using App2.DAL;
using App2.Modelo;
using System;
using System.Linq;
using System.Net.Http;

using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace App2
{
    public partial class MainPage : ContentPage
    {
         

        private UsuarioDAL usuarioDAL = new UsuarioDAL();
       
        public MainPage()
        {
            
        InitializeComponent();

        }

        public async void MetodoAssincrono()
        {
            await Navigation.PushModalAsync(new ListaOcorrencias());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            logar.IsEnabled = false;
            if (email.Text.Trim()==null)
            {
                await this.DisplayAlert("Erro", "Insira seu email", "Ok");
            }
            else if (senha.Text.Trim() == null)
            {
                await this.DisplayAlert("Erro", "Insira sua senha", "Ok");
            }


            var validaLogin = await ValidarLogin(email.Text.Trim(),senha.Text.Trim());
            var existeUsuario = usuarioDAL.GetAll().Any(w=>w.email==email.Text);
             if (validaLogin)
             {

                //verifico se o usuario ja existe no banco,se ja existe so faço o uptade de que ele esta logado

                if (!existeUsuario)
                {
                    usuarioDAL.Add(new Usuario
                    {
                        email = email.Text.Trim(),
                        usuarioLogado = true

                    });
                }
                else
                {
                    var usuario = usuarioDAL.GetAll().Where(w => w.email == email.Text).First();
                    usuario.usuarioLogado = true;
                    usuarioDAL.Update(usuario);
                }
                 await Navigation.PushAsync(new ListaOcorrencias());
            }
             else 
             {

                await this.DisplayAlert("Erro", "Usuário ou senha inválidos!", "Ok");
             }

            logar.IsEnabled = true;
        }

        public async Task<Boolean> ValidarLogin(string user,string password)
        {

            String url = "https://novo.8quali.com.br/api/login?login="+user+"&senha="+password;

            UriBuilder builder = new UriBuilder();
            builder.Path =url;
            try
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(url);

                //verifica se a solicitação foi bem sucedida
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    return Convert.ToBoolean(content);

                }

            }
            catch(Exception e)
            {
                await this.DisplayAlert("Erro",Convert.ToString(e), "Ok");

            }
            return false;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var url = "http://materiais.8quali.com.br/index.php/solicite-uma-demonstracao-site/";

            await Browser.OpenAsync(new Uri(url), BrowserLaunchMode.SystemPreferred);

        }
    }
}
