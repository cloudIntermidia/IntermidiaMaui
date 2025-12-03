namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class BuscarClienteCommand
    {
        public string FiltroPesquisa { get; set; }
        public string CodMarca { get; set; }
        public string CodPessoaVendedor { get; set; }
        public string CodPessoaCliente { get; set; }
        public string CodTipoPessoa { get; set; }
        public decimal ValorMinimoFrete { get; set; }

        public BuscarClienteCommand(string codPessoaVendedor, string codMarca, string codPessoaCliente, string codtipoPessoa, string filtroPesquisa = null)
        {
            FiltroPesquisa = filtroPesquisa;
            CodMarca = codMarca;
            CodPessoaVendedor = codPessoaVendedor;
            CodPessoaCliente = codPessoaCliente;
            CodTipoPessoa = codtipoPessoa;
        }

        public string WherePermissao { get; set; }
        public string WhereFiltroLike { get; set; }

    }


}
