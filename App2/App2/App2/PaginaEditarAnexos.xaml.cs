using App2.DAL;
using App2.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaEditarAnexos : ContentPage, INotifyPropertyChanged
    {
        private OcorrenciaAnexoDAL ocorenciaDAL = new OcorrenciaAnexoDAL();
        private OcorenciaDAL ocorrencia = new OcorenciaDAL();
        public IList<OcorrenciaAnexo> listaAnexo = new ObservableCollection<OcorrenciaAnexo>();
        private long OcorrenciaId = 0;
        public PaginaEditarAnexos(long id)
        {
            
            InitializeComponent();
           listAnexo.ItemsSource = ocorenciaDAL.GetAll().Where(w => w.OcorrenciaId == id);
            var item = ocorrencia.GetAll().Where(w => w.OcorrenciaId == id).First();
            OcorrenciaId = id;

            if (item.sincronizar==false) {
                inserir.IsEnabled = false;

            }
        }

        private async void ListAnexo_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
      
            //aqui posso visualizar a image maior
            await Navigation.PushAsync(new PaginaVisualizacaoImagem(OcorrenciaId));



        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            listAnexo.ItemsSource = ocorenciaDAL.GetAll().Where(w => w.OcorrenciaId == OcorrenciaId);



        }


        private void AtualizaDados()
        {
           listAnexo.ItemsSource = ocorenciaDAL.GetAll().Where(w => w.OcorrenciaId == OcorrenciaId);
        }
    

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var item = (SwipeItem)sender;
            var param = (OcorrenciaAnexo)item.CommandParameter;
            ocorenciaDAL.DeleteById(param.Id);
            listaAnexo.Remove(param);

            AtualizaDados();

        }
    

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            inserir.IsEnabled = false;
            Navigation.PushAsync(new PaginaInserirAnexos(OcorrenciaId));
            inserir.IsEnabled = true;
        }
    }
}