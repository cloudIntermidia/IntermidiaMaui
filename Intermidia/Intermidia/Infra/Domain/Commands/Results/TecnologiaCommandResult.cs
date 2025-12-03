

namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class TecnologiaCommandResult : BaseViewModel
    {
        private string _imagem;
        private string _imagemAberta;
        private string _codTecnologia;
        private string _descricao;
        private string _fileName;

        public string Imagem 
        {             
            get { return _imagem; }
            set
            {
                SetProperty(ref _imagem, value);
            } 
        }

        private object _imagemStream;


        public object  ImagemStream
        {
            get { return _imagemStream; }
            set
            {
                SetProperty(ref _imagemStream, value);
            }
        }
        public string ImagemAberta
        {
            get { return _imagemAberta; }
            set
            {
                SetProperty(ref _imagemAberta, value);
            }
        }

        public string CodTecnologia
        {
            get { return _codTecnologia; }
            set
            {
                SetProperty(ref _codTecnologia, value);
            }
        }

        public string Descricao
        {
            get { return _descricao; }
            set
            {
                SetProperty(ref _descricao, value);
            }
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                SetProperty(ref _fileName, value);
            }
        }

    }
}
