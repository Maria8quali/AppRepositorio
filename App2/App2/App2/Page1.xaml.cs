using Acr.UserDialogs;
using App2.DAL;
using App2.Modelo;
using Newtonsoft.Json;
//using ServiceReference8quali;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage,INotifyPropertyChanged
	{
        byte[] bytesFoto;
        private OcorenciaDAL ocorrenciaDal = new OcorenciaDAL();
        private OcorrenciaAnexoDAL ocorrenciaAnexo = new OcorrenciaAnexoDAL();
        private DTODisposicaoInicialDAL dTODisposicaoInicialDAL = new DTODisposicaoInicialDAL();

        private UsuarioDAL usuarioDAL = new UsuarioDAL();
        private Ocorrencias Ocorrencias = new Ocorrencias();

        private Services.UserKey userKey = new Services.UserKey();
        private Services.NomeUsuarioLogado username = new Services.NomeUsuarioLogado();
        bool camposValidados = false;


        public Page1 ()
		{
			InitializeComponent ();
            
        }

        private async void Botao_Anexo(object sender, EventArgs e)
        {
            botao_anexo.IsEnabled = false;
         
            await Navigation.PushAsync(new PaginaDeCamera());
            botao_anexo.IsEnabled = true;


        }


        private async void Button_Clicked_Back(object sebder,EventArgs ev)
        {
            botao_back.IsEnabled = false;


            await Navigation.PopAsync();

        }

        private bool ValidaCampo()
        {
            if (descricaoOcorrencia.Text == null && ShowName.IsChecked==false && Anonimous.IsChecked==false)
            {
                descricaoOcorrencia.BackgroundColor = Color.LightPink;
                ShowName.BackgroundColor = Color.LightPink;
                Anonimous.BackgroundColor = Color.LightPink;
                return false;

            }
            else if (descricaoOcorrencia.Text == null && !ShowName.IsChecked && !Anonimous.IsChecked)
            {
                descricaoOcorrencia.BackgroundColor = Color.LightPink;
                ShowName.BackgroundColor = Color.LightPink;
                Anonimous.BackgroundColor = Color.LightPink;
                return false;
            }
            else if (descricaoOcorrencia.Text == null)
            {
                descricaoOcorrencia.BackgroundColor = Color.LightPink;
                return false;

            }

            else if (!ShowName.IsChecked && !Anonimous.IsChecked)
            {
                dispoInicial.BackgroundColor = Color.LightPink;
                ShowName.BackgroundColor = Color.LightPink;
                Anonimous.BackgroundColor = Color.LightPink;
                return false;
            }
         
            else if (!ShowName.IsChecked && !Anonimous.IsChecked)
            {
                ShowName.BackgroundColor = Color.LightPink;
                Anonimous.BackgroundColor = Color.LightPink;
                return false;
            }

            return true;


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
        private async void SalvaItems()
        {
            var Id = 0;
            if (ocorrenciaDal.GetAll().Count() == 0)
            {
                Id = ocorrenciaDal.GetAll().Count() + 1;
            }
            else
            {
                Id = (int)(ocorrenciaDal.GetAll().Max(w => w.OcorrenciaId) + 1);

            }



            var validou = ValidaCampo();
            camposValidados = validou;
            var listaAnexos = ocorrenciaAnexo.GetAll().Where(w => w.OcorrenciaId == Id).ToList();
            foreach(var item in listaAnexos)
            {
                if (item.FilePath != null)
                {
                    var bytes = GetPhoto(item.FilePath);

                    item.Anexo = bytes;
                }
            }

            var listaDispo = dTODisposicaoInicialDAL.GetAll().Where(w => w.OcorrenciaId == Id).ToList();


            DTODisposicaoInicial[] disposicoesInicial = new DTODisposicaoInicial[listaDispo.Count];

            for (int j = 0; j < listaDispo.Count; j++)
            {

                disposicoesInicial[j] = listaDispo[j];
            }

            OcorrenciaAnexo[] anexos = new OcorrenciaAnexo[listaAnexos.Count];

            for (int i = 0; i < listaAnexos.Count; i++)
            {
                anexos[i] = listaAnexos[i];

            }




            var usuarioIncluiu = usuarioDAL.GetAll().Where(w => w.usuarioLogado == true).First();
            var UserKey = userKey.chaveUsuario(usuarioIncluiu.email);
            var UserName = username.GetName(usuarioIncluiu.email);
        
            //valida campos
            if (validou)
            {



                if (ShowName.IsChecked)
                {
                    ocorrenciaDal.Add(new Ocorrencias()
                    {
                        Descricao = descricaoOcorrencia.Text.Trim(),
                        NomeUsuario = await UserName,
                        mostrarNome = true,
                        Anexos = anexos,
                        DisposicaoInicials = disposicoesInicial,
                        Chave = await UserKey,
                        sincronizar = true,
                        UserLogin = true,
                        NaoMostrarNome = false,
                        Codigo = DateTime.Now + "/ " + DateTime.Now.Month,
                    });

                    if (dispoInicial.Text != null)
                    {
                        dTODisposicaoInicialDAL.Add(new DTODisposicaoInicial
                        {
                            Descricao = dispoInicial.Text.Trim(),
                            LoginDoResponsavel = usuarioIncluiu.email,
                            OcorrenciaId = Convert.ToInt32(Id)

                        });
                    }
                    else
                    {
                        dTODisposicaoInicialDAL.Add(new DTODisposicaoInicial
                        {
                            Descricao = "",
                            LoginDoResponsavel = usuarioIncluiu.email,
                            OcorrenciaId = Convert.ToInt32(Id)

                        });

                    }
                    

                }

                else if (Anonimous.IsChecked)
                {
                    ocorrenciaDal.Add(new Ocorrencias()
                    {
                        Descricao = descricaoOcorrencia.Text.Trim(),
                        NomeUsuario = "Anônimo",
                        mostrarNome = false,
                        sincronizar = true,
                        DisposicaoInicials = disposicoesInicial,
                        UserLogin = true,
                        Anexos = anexos,
                        Chave = await UserKey,
                        NaoMostrarNome = true,
                        Codigo = DateTime.Now + "/ " + DateTime.Now.Month,


                    });
                    if (dispoInicial.Text != null)
                    {

                        dTODisposicaoInicialDAL.Add(new DTODisposicaoInicial
                        {
                            Descricao = dispoInicial.Text.Trim(),
                            LoginDoResponsavel = usuarioIncluiu.email,
                            OcorrenciaId = Convert.ToInt32(Id)


                        });
                    }
                    else
                    {
                        dTODisposicaoInicialDAL.Add(new DTODisposicaoInicial
                        {
                            Descricao = "",
                            LoginDoResponsavel = usuarioIncluiu.email,
                            OcorrenciaId = Convert.ToInt32(Id)


                        });

                    }
                    

                }

                //salva e volta para a pagina de listas
            }
            else
            {
                botao_save.IsEnabled = true;

            }

        }
      


        private async void Button_Clicked_Save(object sender, EventArgs e)
        {
            //desabilita botao
           
            botao_save.IsEnabled = false;
            using (UserDialogs.Instance.Loading(("Salvando...")))
            {
                await Task.Run(() => {
                    SalvaItems();

                });
            }
            if (camposValidados)
            {
                await Navigation.PopAsync();

            }
            else
            {

                await this.DisplayAlert("Erro", "Preencha os campos faltantes", "Ok");

            }



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