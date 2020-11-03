using App2.DAL;
using App2.Modelo;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace App2.ViewModel
{
    public class ListagemOcorrenciaViewModel:INotifyPropertyChanged
    {
        private OcorenciaDAL ocorrenciaDal = new OcorenciaDAL();
        private UsuarioDAL usuarioDAL = new UsuarioDAL();

        private Services.Servico service = new Services.Servico();


        public ListagemOcorrenciaViewModel()
        {
          


        }


    public string Descricao
        {
            get { return Descricao; }
            set { Descricao = value; }
        }

        public string NomeUsuario
        {
            get { return NomeUsuario; }
            set { NomeUsuario = value; }

        }

        public string DispoInicial
        {
            get { return DispoInicial; }
            set { DispoInicial = value; }

        }
        public bool IsBusy
        {
            get { return IsBusy; }
            set
            {
                IsBusy = value;
            }

            
        }

        public bool ShowName
        {
            get { return ShowName; }
            set
            {
                ShowName = value;
            }
        }

        public bool Anonimous
        {
            get { return Anonimous; }
            set
            {
                Anonimous = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    


    }
}
