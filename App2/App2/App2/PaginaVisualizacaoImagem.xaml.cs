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
    public partial class PaginaVisualizacaoImagem : ContentPage
    {

        private OcorrenciaAnexoDAL ocorrencia = new OcorrenciaAnexoDAL();
        public PaginaVisualizacaoImagem(long OcorrenciaId)
        {
            InitializeComponent();

            var teste = ocorrencia.GetAll().Where(w=>w.OcorrenciaId==OcorrenciaId);
            listAnexo.ItemsSource = teste;
            //anexo.Source = teste.Anexo.ToString();

        }
    }
}