using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class WcfClienteModelInput
    {
        public TBT_PESSOA Pessoa { get; set; }
        public TBT_CLIENTE Cliente { get; set; }
        public List<TBT_MARCA_CLIENTE> MarcaCliente { get; set; }
        public List<TBT_ENDERECO_CLIENTE> Enderecos { get; set; }
        public List<TBT_CLIENTE_EMPRESA_TABELA_PRECO_ENVIAR_ERP> Lista_Empresa_TabelaPreco { get; set; }
        public String CodPessoaRepresentante { get; set; }
        public String FLG_AMBIENTE { get; set; }

        private object ObjetoTranmissao()
        {
            return new { Pessoa, Cliente, MarcaCliente, Enderecos };
        }

    }


}
