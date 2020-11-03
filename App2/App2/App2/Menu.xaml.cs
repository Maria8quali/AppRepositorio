using App2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        private UsuarioDAL usuarioDAL = new UsuarioDAL();
        private OcorenciaDAL ocorenciaDAL = new OcorenciaDAL();

        public Menu()
        {
            InitializeComponent();
         
        }



        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            logout.IsEnabled = false;
            //procuro usuariologado==true
            var usuarioLogado = usuarioDAL.GetAll().Where(w=>w.usuarioLogado==true).First();

            usuarioLogado.usuarioLogado = false;
            usuarioDAL.Update(usuarioLogado);
            await Navigation.PopToRootAsync();
            await Navigation.PushAsync(new MainPage());
            logout.IsEnabled = true;


        }
    }
}