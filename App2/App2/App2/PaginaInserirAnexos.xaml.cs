using Acr.UserDialogs;
using App2.DAL;
using App2.Modelo;
using PCLStorage;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaInserirAnexos : ContentPage
    {
        private OcorrenciaAnexoDAL ocorrenciaAnexoDal = new OcorrenciaAnexoDAL();
        private OcorenciaDAL ocorrenciaDAL = new OcorenciaDAL();
        private string caminhoArquivo;
        private string extension = "";
        private string FileName = "";
        byte[] bytesFoto;
        private long ocorrenciaId;
        public PaginaInserirAnexos(long ocorrenciaId)
        {
            InitializeComponent();
            RegistraClickBotaoCamera(idocorrencia.Text.Trim());
            RegistraClickBotaoAlbum();
            this.ocorrenciaId = ocorrenciaId;
        }



        private void RegistraClickBotaoCamera(string idparafoto)
        {
           
            BtnCamera.Clicked += async (sender, args) =>
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Não existe camêra", "A camera não esta disponível", "Ok");
                }
                /*	Método	que	habilita	a	câmera,	informando	a	pasta	onde	a foto	deverá
            ser	armazenada,	o	nome	a	ser	dado	ao	arquivo	e	se	é	ou	
        não	para	
                    armazenar	a	foto	também	no	álbum	*/
                var file = await CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = FileSystem.Current.LocalStorage.Name,
                    Name = "foto_" + idparafoto + ".jpg",
                    SaveToAlbum = true
                });
               // Id = Convert.ToInt32(idocorrencia.Text);                if (file == null)                    return;                var stream = file.GetStream();                var memoryStream = new MemoryStream();                stream.CopyTo(memoryStream);
                caminhoArquivo = file.Path;
                string filename = "foto_" + idparafoto + ".jpg";

                extension = Path.GetExtension(filename);



                fotoOcorrencia.Source = ImageSource.FromStream(() =>
                {


                    var s = file.GetStream();
                    file.Dispose();


                    return s;

                });




                bytesFoto = memoryStream.ToArray();

              //  ListaAnexo.Add(bytesFoto);





            };
        }
     

        private async void Button_Clicked_Back(object sender, EventArgs e)
        {
           
            await Navigation.PopAsync();
        }
        private void BtnGravarClick(object sender, EventArgs args)
        {
            save_button.IsEnabled = false;

           ocorrenciaAnexoDal.Add(new OcorrenciaAnexo
            {
                Anexo = bytesFoto,
                OcorrenciaId =Convert.ToInt32(ocorrenciaId),
                dataFoto = DateTime.Now,
                FilePath=caminhoArquivo,
               NomeAnexo=FileName,
               ExtencaoAnexo=extension,
               ImageBytes= Convert.ToBase64String(bytesFoto)




           });

         

            Navigation.PopAsync();

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
                UserDialogs.Instance.ShowLoading("Loading image..");


                var stream = file.GetStream();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                //	Recupera	o	arquivo	com	base	no	caminho	da	foto	selecionada
                var getArquivoPCL = FileSystem.Current.GetFileFromPathAsync(file.Path);
                //	Recupera	o	caminho	raiz	da	aplicação	no	dispositivo
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


                fotoOcorrencia.Source = ImageSource.FromStream(() =>
                {
                    var s = file.GetStream();
                    file.Dispose();
                    return s;
                });
                bytesFoto = memoryStream.ToArray();

                UserDialogs.Instance.HideLoading();


            };
        }

        private void FloatingButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}