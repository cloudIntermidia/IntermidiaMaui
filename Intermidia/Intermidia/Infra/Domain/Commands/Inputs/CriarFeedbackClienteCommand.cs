using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class CriarFeedbackClienteCommand : BaseViewModel
    {
        private string _codTipoFeedback;
        private string _codPessoaCliente;
        private int? _ID;
        private string _codPessoaRepresentante;
        private string _observacao;
        private DateTime _ctrlDataOperacao;

        public CriarFeedbackClienteCommand(string codTipoFeedBack, string codPessoaCliente, string codPessoaRepresentante, string observacao, DateTime ctrlDataOperacao)
        {
            CodTipoFeedback = codTipoFeedBack;
            CodPessoaCliente = codPessoaCliente;
            CodPessoaRepresentante = codPessoaRepresentante;
            Observacao = observacao;
            CtrlDataOperacao = ctrlDataOperacao;
        }


        public string CodTipoFeedback { get => _codTipoFeedback; set => SetProperty(ref _codTipoFeedback, value); }
        public string CodPessoaCliente { get => _codPessoaCliente; set => SetProperty(ref _codPessoaCliente, value); }
        public string CodPessoaRepresentante { get => _codPessoaRepresentante; set => SetProperty(ref _codPessoaRepresentante, value); }
        public string Observacao { get => _observacao; set => SetProperty(ref _observacao, value); }
        public DateTime CtrlDataOperacao { get => _ctrlDataOperacao; set => SetProperty(ref _ctrlDataOperacao, value); }
        public int? ID { get => _ID; set => SetProperty(ref _ID, value); }

    }


}
