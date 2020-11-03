using App2.DAL;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App2
{
    public partial class App : Application
    {
        public UsuarioDAL usuarioDAL = new UsuarioDAL();
        public App()
        {

           
            var existeUsuarioLogado = usuarioDAL.GetAll().Any(w=>w.usuarioLogado==true);


            InitializeComponent();
            // se nao existir usuario logado ele abre a tela de login caso contrario ele abre a tela com a listagem de ocorrencias
            if (existeUsuarioLogado==false)
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new ListaOcorrencias());

            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
