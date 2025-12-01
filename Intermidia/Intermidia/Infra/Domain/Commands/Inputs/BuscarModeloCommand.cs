using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class BuscarModeloCommand
    {
        /// <summary>
        /// Variavel que indica se deve trazer somente items do atendimento atual
        /// </summary>
        public decimal ItensEmAtendimento { get; set; }
        /// <summary>
        /// Variável que indica se traz somente o que tem Quantidade disponível em estoque
        /// </summary>
        public string ValidaEstoque { get; set; }

        public string CodUsuario { get; set; }
        public string CodAtendimento { get; set; }
        public string CodMarca { get; set; }
        public string FiltroPesquisa { get; set; }
        public string CodTabelaPreco { get; set; }
        public string CodPrazoMedio { get; set; }
        public string WhereNiveis { get; set; }
        public string WhereSegmentacao { get; set; }
        public List<NivelAtributoResult> FiltrosSelecionados { get; set; }
        public List<NivelAtributoResult> FiltrosSelecionados_DataDisponibilidade { get; set; }

        public decimal IndiceCoeficiente { get; set; }
        public string CodProdutoIn { get; set; }

        public string CodPessoaCliente { get; set; }
        public string CorTraduzida { get; set; }

        public string CodCondicaoPagamento { get; set; }
        public string CodModelo { get; set; }
        public string CodProduto { get; set; }

        public bool FiltrarProdutosExcluidos { get; set; }
        public decimal IndPrivateLabel { get; set; }
        public bool Promocao { get; set; }
        public string IndPromocional { get; set; }

        public bool ProntaEntrega { get; set; }
        public string IndProntaEntrega { get; set; }
        public string WherePermissao { get; set; }
        public string WherePermissaoProduto { get; set; }
        public string Ativo { get; set; }
        public string CampoValor_ClienteEmpresa { get; set; }
        public string CampoValor_ProdutoKit { get; set; }
        public string WhereDataDisponibilidade { get; set; }
        public string CampoValor_ClienteEmpresa_Acrescimo { get; set; }

    }


}
