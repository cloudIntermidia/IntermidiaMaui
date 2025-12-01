using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class BuscarGestaoClienteCommand
    {
        public string CodMarca { get; set; }
        public string CodPessoa { get; set; }
        public string CodTipoPessoa { get; set; }

        public string CodPessoaVendedor { get; set; }
        public string UfSelecionado { get; set; }
        public string ClienteSelecionado { get; set; }
        public string VendedorSelecionado { get; set; }
        public string GrupoSelecionado { get; set; }
        public string CidadeSelecionada { get; set; }

        public string FMDataEmissao { get; set; }

        public DateTime Data1a90Inicial { get; set; }
        public DateTime Data1a90Final { get; set; }
        public DateTime Data91a120Inicial { get; set; }
        public DateTime Data91a120Final { get; set; }
        public DateTime DataAcima121 { get; set; }
        public DateTime FMAtualInicial { get; set; }
        public DateTime FMAtualFinal { get; set; }
        public DateTime FMAnteriorInicial { get; set; }
        public DateTime FMAnteriorFinal { get; set; }

        public string TipoData { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }

        public decimal IndBloqueado { get; set; }
        public decimal IndPedidosAberto { get; set; }
        public decimal IndFaturadoAte90 { get; set; }
        public decimal IndFaturadoAte120 { get; set; }
        public decimal IndFaturadoAcima121 { get; set; }


        public BuscarGestaoClienteCommand(UsuarioCommandResult usuario)
        {
            //No metodo ClienteRepository > BuscarGestaoClientes esta sendo sebrescrito essas datas, por conta da funcionalidade do Filtro Data.
            CodMarca = usuario.CodMarca;
            CodPessoa = usuario.CodPessoa;
            CodTipoPessoa = usuario.CodTipoPessoa;

            FMAtualInicial = new DateTime(DateTime.Now.Year, 1, 1);
            FMAtualFinal = new DateTime(DateTime.Now.Year, 12, 31);

            FMAnteriorInicial = new DateTime(DateTime.Now.Year - 1, 1, 1);
            FMAnteriorFinal = new DateTime(DateTime.Now.Year - 1, 12, 31);

            Data1a90Final = DateTime.Now.Date;
            Data1a90Inicial = DateTime.Now.AddDays(-90).Date;
            Data91a120Inicial = DateTime.Now.AddDays(-120).Date;
            Data91a120Final = DateTime.Now.AddDays(-91).Date;
            DataAcima121 = DateTime.Now.AddDays(-121).Date;


        }
    }



}
