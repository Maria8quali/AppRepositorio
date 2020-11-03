using App2.DAL;
using App2.Modelo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaOcorrencias : MasterDetailPage
    {


        public ListaOcorrencias()
        {
            InitializeComponent();
            this.Master = new Menu();
            this.Detail = new NavigationPage(new ListagemOcorrencia());

        }

      
    }
}