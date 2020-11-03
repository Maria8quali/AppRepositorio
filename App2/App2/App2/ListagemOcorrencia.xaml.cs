
using Acr.UserDialogs;
using Android.App;
using App2.DAL;
using App2.Modelo;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SQLite.SQLite3;
using Color = Xamarin.Forms.Color;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListagemOcorrencia : ContentPage
    {
        private OcorenciaDAL ocorenciaDAL = new OcorenciaDAL();
        public OcorrenciaAnexoDAL ocorrenciaAnexoDAL = new OcorrenciaAnexoDAL();
        private IList<Ocorrencias> ListaOcorrencia = new List<Ocorrencias>();
        private DTODisposicaoInicialDAL disposicaoInicial = new DTODisposicaoInicialDAL();
        private UsuarioDAL usuarioDAL = new UsuarioDAL();
        private Services.Servico service = new Services.Servico();


        public ListagemOcorrencia()
        {

            InitializeComponent();
            OnAppearing();
            

        }

        protected override void OnAppearing()
       {
            //pegar somente ocorrencias do usuario logado

            listOcorrencias.ItemsSource = ocorenciaDAL.GetAll();
        }


        private async void BtnFloating_OnClicked(object sender, EventArgs ev)
        {
            floatingButton.IsEnabled = false;
               await Navigation.PushAsync(new Page1());
            floatingButton.IsEnabled = true;
        }





        private void ListOcorrencias_Scrolled(object sender, ScrolledEventArgs e)
        {
            Debug.WriteLine("ScrollX: " + e.ScrollX);
            Debug.WriteLine("ScrollY: " + e.ScrollY);
        }


        private void SearchConteudo_Focused(object sender, FocusEventArgs e)
        {
            SearchConteudo.BackgroundColor = Color.FromHex("#a4eaff");

        }
      
   


        private void SearchConteudo_Unfocused(object sender, FocusEventArgs e)
        {
         
            SearchConteudo.BackgroundColor = Color.White;

      
        }


        private void SwipeItem_Delete(object sender, EventArgs e)
        {
          
            var item = (SwipeItem)sender;
            var param = (Ocorrencias)item.CommandParameter;
         
           ocorenciaDAL.DeleteById(param.OcorrenciaId);
            
            var IdAnexo = ocorrenciaAnexoDAL.GetAll().Where(w => w.OcorrenciaId == param.OcorrenciaId).First();
            ocorrenciaAnexoDAL.DeleteById(IdAnexo.Id);
           
            ListaOcorrencia = ocorenciaDAL.GetAll().ToList();

            
            ListaOcorrencia.Remove(param);
            listOcorrencias.ItemsSource = ListaOcorrencia;




        }
        //aqui vou ter os dados inseridos por tal pessoa
        private async void SwipeItem_View(object sender, EventArgs e)
        {
            var item = (SwipeItem)sender;

            item.IsEnabled = false;
            var param = (Ocorrencias)item.CommandParameter;
         

            await Navigation.PushAsync(new PaginaEditarOcorrencia(param.OcorrenciaId,param.sincronizar));
            item.IsEnabled = true;
        }

        public static byte[] GetPhoto(string filePath)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        }

        //Post 8quali
        private async void SwipeItem_Sincronizar(object sender,EventArgs e)
        {


            var item = (SwipeItem)sender;

            item.IsEnabled = false;
            item.BackgroundColor = Color.FromHex("597FAC");

            item.IsVisible = false;
            using (UserDialogs.Instance.Loading(("Sincronizando dados...")))
            {
                await Task.Run(() => {
                    try
                    {

                        var param = (Ocorrencias)item.CommandParameter;
                        param.sincronizar = false;
                        ocorenciaDAL.Update(param);

                        var usuarioIncluiu = usuarioDAL.GetAll().First();
                        var listaAnexos = ocorrenciaAnexoDAL.GetAll().Where(w => w.OcorrenciaId == param.OcorrenciaId).ToList();
                        foreach (var item2 in listaAnexos)
                        {
                            var bytes = GetPhoto(item2.FilePath);

                            item2.Anexo = bytes;
                        }
                        var listaDispo = disposicaoInicial.GetAll().Where(w => w.OcorrenciaId == param.OcorrenciaId).ToList();

                        OcorrenciaAnexo[] anexos = new OcorrenciaAnexo[listaAnexos.Count];
                        DTODisposicaoInicial[] disposicoesInicial = new DTODisposicaoInicial[listaDispo.Count];

                        for (int j = 0; j < listaDispo.Count; j++)
                        {
                            disposicoesInicial[j] = listaDispo[j];
                        }

                        for (int i = 0; i < listaAnexos.Count; i++)
                        {
                            anexos[i] = listaAnexos[i];

                        }

                        Ocorrencias ocorrencias = new Ocorrencias();

                        ocorrencias.Codigo = param.Codigo;
                        ocorrencias.Chave = param.Chave;
                        ocorrencias.Descricao = param.Descricao;

                        ocorrencias.UsuarioIncluiuLogin = usuarioIncluiu.email.ToString();
                        ocorrencias.DataInclusao = DateTime.Now;
                        ocorrencias.Anexos = anexos;

                        ocorrencias.DisposicaoInicials = disposicoesInicial;




                        string output = JsonConvert.SerializeObject(ocorrencias);
                         service.SendAsync(ocorrencias);




                    }
                    catch (Exception ex)
                    {

                    }

                });
            }
            OnAppearing();


        }

        private void SearchConteudo_TextChanged(object sender, TextChangedEventArgs e)
        {
     
            listOcorrencias.BeginRefresh();

            var ocorrencias = ocorenciaDAL.GetAll();
            ObservableCollection<Ocorrencias> ocorrencia = new ObservableCollection<Ocorrencias>();
            if (SearchConteudo.Text.Trim() != "")
            {



                foreach (var item in ocorrencias)
                {
                    //vai pesquisar tanto pelo nome do usuario quanto pela  descrição
                    if (SearchConteudo.Text.Trim() == item.Descricao)
                    {
                        ocorrencia = new ObservableCollection<Ocorrencias>(ocorenciaDAL.GetAll().Where(w => w.Descricao.ToLower() == item.Descricao.ToLower()).ToList());


                        break;
                    }
                    else if (SearchConteudo.Text.Trim() == item.NomeUsuario)
                    {
                        ocorrencia = new ObservableCollection<Ocorrencias>(ocorenciaDAL.GetAll().Where(w => w.NomeUsuario.ToLower() == item.NomeUsuario.ToLower()).ToList());

                        break;

                    }
                }
                listOcorrencias.ItemsSource = ocorrencia.ToList();
                listOcorrencias.EndRefresh();


            }
            else
            {
                listOcorrencias.ItemsSource = ocorenciaDAL.GetAll();
                listOcorrencias.EndRefresh();

            }



        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            Debug.WriteLine("ScrollX: " + e.ScrollX);
            Debug.WriteLine("ScrollY: " + e.ScrollY);

        }

     
        private void naoSincronizado_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (naoSincronizado.IsChecked)
            {
                listOcorrencias.ItemsSource = ocorenciaDAL.GetAll().Where(w => w.sincronizar == true).ToList();
            }
            else
            {
                OnAppearing();
            }

        }

        private void sincronizado_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sincronizado.IsChecked)
            {
                listOcorrencias.ItemsSource = ocorenciaDAL.GetAll().Where(w => w.sincronizar == false).ToList();
            }
            else
            {
                OnAppearing();
            }

        }
    }
}