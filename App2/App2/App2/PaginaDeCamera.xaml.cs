using Acr.UserDialogs;
using App2.DAL;
using App2.Modelo;
using PCLStorage;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaDeCamera : ContentPage
    {

        private OcorenciaDAL ocorrenciaDal = new OcorenciaDAL();
        private Ocorrencias ocorrencias = new Ocorrencias();
        private OcorrenciaAnexoDAL ocorrenciaAnexoDal = new OcorrenciaAnexoDAL();
        private List<OcorrenciaAnexo> ListaAnexos = new List<OcorrenciaAnexo>();
        private string extension = "";
        private string FileName = "";


        private string caminhoArquivo;        private int ListaAnexosId = 0;        private byte[] bytesFoto;
        public PaginaDeCamera()
        {
            InitializeComponent();
           

            InserirFotoNova();
            RegistraClickBotaoCamera(idocorrencia.Text.Trim());
            RegistraClickBotaoAlbum();
            listAnexo.ItemsSource = ocorrenciaAnexoDal.GetAll();

           // OnAppearing();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                botaoSave.IsEnabled = true;

                listAnexo.ItemsSource = ListaAnexos;


            }
            catch (Exception ex)
            {
                this.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }
        private void BtnGravarClick(object sender,EventArgs args)
        {
            botaoSave.IsEnabled = false;

            string testando = Convert.ToBase64String(bytesFoto);
            if (ListaAnexos.Count > 0)
            {
                ListaAnexosId = ListaAnexos.Count + 1;
            }
            else
            {
                ListaAnexosId = ListaAnexosId + 1;
            }


            ListaAnexos.Add(new OcorrenciaAnexo
            {
              //  Anexo = bytesFoto,
                Id=ListaAnexosId,   
                FilePath=caminhoArquivo,
                ImageBytes = Convert.ToBase64String(bytesFoto),
                OcorrenciaId = Convert.ToInt32(idocorrencia.Text),
                ExtencaoAnexo = extension,
                NomeAnexo=FileName,

                dataFoto = DateTime.Now,
           



            }) ;
             listAnexo.ItemsSource = ListaAnexos.ToList();
            botaoSave.IsEnabled = true;

        }
 

        private void InserirFotoNova()
        {
            var existeOcorrencia = ocorrenciaDal.GetAll().Count();
            long novoId = 0;
            if (existeOcorrencia>0) {
                 novoId = ocorrenciaDal.GetAll().Max(w => w.OcorrenciaId) + 1;

            }
            else
            {
                novoId = 1;

            }

            idocorrencia.Text = novoId.ToString().Trim();
            fotoOcorrencia.Source = null;
        }

       private void RegistraClickBotaoCamera(string idparafoto)
        {
       

            BtnCamera.Clicked += async (sender, args) =>
            {
                botaoSave.IsEnabled = false;
                botaoBack.IsEnabled = false;

                await CrossMedia.Current.Initialize();
                if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
                {
                   await  DisplayAlert("Não existe camêra", "A camera não esta disponível", "Ok");
                }

                var file = await CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = FileSystem.Current.LocalStorage.Name,
                    Name = "foto_" + idparafoto + ".jpg",
                    CompressionQuality=40,
                    SaveToAlbum = true
                });                          if (file == null)                    return;
              


                    var stream = file.GetStream();

                    var memoryStream = new MemoryStream();
                    string filename = "foto_" + idparafoto + ".jpg";

                    stream.CopyTo(memoryStream);
                    caminhoArquivo = file.Path;

                    extension = Path.GetExtension(filename);
                    FileName = "foto_" + idparafoto + ".jpg";



                    fotoOcorrencia.Source = ImageSource.FromStream(() =>
                    {


                        var s = file.GetStream();
                        file.Dispose();


                        return s;

                    });





                    bytesFoto = memoryStream.ToArray();

                botaoSave.IsEnabled = true;
                botaoBack.IsEnabled = true;

            };

           
        }

        private void RegistraClickBotaoAlbum()
        {
            BtnAlbum.Clicked += async (sender, args) =>
            {
                
               
                    await CrossMedia.Current.Initialize();
                    //	Verificação	de	disponibilização	do	álbum
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await this.DisplayAlert("Álbum	não	suportado", "Não	existe	permissão para    acessar o   álbum   de  fotos", "OK");
                        return;
                    }
                    var file = await CrossMedia.Current.PickPhotoAsync();

                    if (file == null)
                        return;


                    var stream = file.GetStream();

                    var memoryStream = new MemoryStream();
                    stream.CopyTo(memoryStream);

                    //	Recupera	o	arquivo	com	base	no	caminho	da	foto	selecionada
                    var getArquivoPCL = FileSystem.Current.GetFileFromPathAsync(file.Path);

                    var rootFolder = FileSystem.Current.LocalStorage;
                    /*	Caso	a	pasta	Fotos	não	exista,	ela	é	criada	para	
                                armazenamento	da	foto	selecionada	*/
                    var folderFoto = await rootFolder.CreateFolderAsync("Fotos", CreationCollisionOption.OpenIfExists);
                    //	Cria	o	arquivo	referente	à	foto	selecionada
                    var setArquivoPCL = folderFoto.CreateFileAsync(getArquivoPCL.Result.Name, CreationCollisionOption.ReplaceExisting);
                    //	Guarda	o	caminho	do	arquivo	para	ser	utilizado	na	gravção do item criado

                    caminhoArquivo = getArquivoPCL.Result.Path;
                    //	Recupera	o	arquivo	selecionado	e	o	atribui	ao	controle	no formulário
                    var fileName = setArquivoPCL.Result.Name;

                    extension = Path.GetExtension(fileName);
                    FileName = setArquivoPCL.Result.Name;


                    fotoOcorrencia.Source = ImageSource.FromStream(() =>
                    {
                        var s = file.GetStream();
                        file.Dispose();
                        return s;
                    });
                    bytesFoto = memoryStream.ToArray();


                
            };
            botaoBack.IsEnabled = true;
            botaoSave.IsEnabled = true;
        }

        private async void Button_Clicked_Save(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading(("Salvando...")))
            {
                await Task.Run(() =>
                {
                    foreach (var listas in ListaAnexos)
                    {
                        ocorrenciaAnexoDal.Add(new OcorrenciaAnexo
                        {
                            Anexo = listas.Anexo,
                            dataFoto = listas.dataFoto,
                            ExtencaoAnexo = listas.ExtencaoAnexo,
                            FilePath = listas.FilePath,
                            ImageBytes = listas.ImageBytes,
                            NomeAnexo = listas.NomeAnexo,
                            OcorrenciaId = listas.OcorrenciaId
                        });
                    }
                  });
                }
            botaoBack.IsEnabled = false;
            var item = (Button)sender;
            await Navigation.PopAsync();
        }

        private async void ListAnexo_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (ListView)sender;
            var param = (OcorrenciaAnexo)item.SelectedItem;
            //aqui posso visualizar a image maior
            await Navigation.PushAsync(new PaginaVisualizacaoImagem(param.Id));



        }
        private void AtualizaDados()
        {
            listAnexo.ItemsSource = ListaAnexos.ToList();
        }


        private void SwipeItem_Delete(object sender, EventArgs e)
        {
            var item = (SwipeItem)sender;
            var param = (OcorrenciaAnexo)item.CommandParameter;
            foreach(var item1 in ListaAnexos)
            {
                if (item1.Id == param.Id)
                {
                    ListaAnexos.Remove(item1);
                    break;
                }
            }
            AtualizaDados();

        }
    }
}