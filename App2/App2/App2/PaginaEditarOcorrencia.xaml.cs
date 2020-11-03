using App2.DAL;
using App2.Modelo;
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
    public partial class PaginaEditarOcorrencia : ContentPage
    {
        private OcorenciaDAL ocorrenciaDAL = new OcorenciaDAL();
        private DTODisposicaoInicialDAL disposicaoInicial = new DTODisposicaoInicialDAL();
        private UsuarioDAL usuarioDAL = new UsuarioDAL();
        private OcorrenciaAnexoDAL ocorrenciaAnexoDAL = new OcorrenciaAnexoDAL();
        private Services.NomeUsuarioLogado username = new Services.NomeUsuarioLogado();
        private Services.UserKey userKey = new Services.UserKey();


        long Id = 0;
        byte[] bytesFoto;

        public PaginaEditarOcorrencia(long OcorrenciaId,bool sincronizar)
        {
            InitializeComponent();

            var ocorrencias = ocorrenciaDAL.GetItemById(OcorrenciaId);
            var disposicoes = disposicaoInicial.GetAll().Where(w => w.OcorrenciaId == OcorrenciaId).First();
           
            descricaoOcorrencia.Text = ocorrencias.Descricao;
            
         
          
                dispoInicial.Text = disposicoes.Descricao;
            
           
            ShowName.IsChecked = ocorrencias.mostrarNome;
            Anonimous.IsChecked = ocorrencias.NaoMostrarNome;
            // se ja foi sincronizado desabilito o botao de salvar, a condição é assim se sincronizar=true quer dizer que pode sincronizar se false não pode
            if (sincronizar==false)
            {
                salvar.IsVisible = false;
            }

            Id = OcorrenciaId;

        }

        private async void Botao_Anexo(object sender, EventArgs e)
        {
          

            await Navigation.PushAsync(new PaginaEditarAnexos(Id));
        }
        private async void Button_Clicked_Back(object sebder, EventArgs ev)
        {
            //volta para lista de ocorrencias
            await Navigation.PopAsync();
        }
        private async void Button_Clicked_Save(object sender, EventArgs e)
        {
            var listaAnexos = ocorrenciaAnexoDAL.GetAll().Where(w=>w.OcorrenciaId==Id).ToList();
            var disposicoes = disposicaoInicial.GetAll().Where(w => w.OcorrenciaId == Id).First();
            var ocorrencias = ocorrenciaDAL.GetAll().Where(w => w.OcorrenciaId == Id).First();


            var id = ocorrenciaId.Text;

            if (descricaoOcorrencia == null)
            {
                descricaoOcorrencia.BackgroundColor = Color.LightPink;
            }
            else if (dispoInicial == null)
            {
                dispoInicial.BackgroundColor = Color.LightPink;
            }


            if (ShowName.IsChecked)
            {
                ocorrencias.Descricao = descricaoOcorrencia.Text;
               

                ocorrenciaDAL.Update(ocorrencias) ;
                disposicoes.Descricao = dispoInicial.Text;
                disposicaoInicial.Update(disposicoes);

            }

            else if (Anonimous.IsChecked)
            {
                ocorrenciaDAL.Update(ocorrencias);
                disposicoes.Descricao = dispoInicial.Text;
                disposicaoInicial.Update(disposicoes);

            }
            else
            {
                await this.DisplayAlert("Erro", " Por favor,insira o emissor", "Ok");
                ShowName.Color = Color.LightPink;
                Anonimous.Color = Color.LightPink;
            }

            //salva e volta para a pagina de listas
            await Navigation.PopAsync();


        }
        private void DescricaoOcorrencia_Focused(object sender, FocusEventArgs e)
        {
            descricaoOcorrencia.BackgroundColor = Color.AliceBlue;
        }

        private void DispoInicial_Focused(object sender, FocusEventArgs e)
        {
            dispoInicial.BackgroundColor = Color.AliceBlue;
        }

        private void DescricaoOcorrencia_Unfocused(object sender, FocusEventArgs e)
        {
            descricaoOcorrencia.BackgroundColor = Color.White;
        }

        private void DispoInicial_Unfocused(object sender, FocusEventArgs e)
        {
            dispoInicial.BackgroundColor = Color.White;
        }
    }
}