using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;
using System.Collections.ObjectModel;

namespace Intermidia.Intermidia.Infra.Domain.Repositories.Interface
{
    public interface ICarrinhoRepository
    {
        Task<CarrinhoCommandResult> FindCarrinho(BuscarCarrinhoCommand command);
        Task<List<CarrinhoCommandResult>> GetCarrinhos(BuscarCarrinhoCommand command);
        Task<CarrinhoCommandResult> Criar(TBT_CARRINHO carrinho, List<TBT_ITEM_CARRINHO> itens, List<TBT_GRADE_ITEM_CARRINHO> grades, List<TBT_CARRINHO_NIVEL> niveisCarrinho);
        Task<CarrinhoCommandResult> Atualizar(TBT_CARRINHO carrinho, List<TBT_ITEM_CARRINHO> itens, List<TBT_GRADE_ITEM_CARRINHO> grades);
        //        Task<WcfPedidoModelInput> BuscarCarrinhoParaTransmissao(string codCarrinho, string codSufarmaCliente = "");
        Task<WcfPedidoModelInput> BuscarCarrinhoParaTransmissao(string codCarrinho, string codSufarmaCliente = "", bool materialPDV = false);

        Task<CarrinhoCommandResult> BuscarPedidoCopia(string codPedido);
        Task<bool> AtualizarPedidoImplantado(string codPedido, string codSituacaoPedido, string codCarrinho, decimal valorFinalComImposto);
        Task AlterarItensCarrinho(string codCarrinho, string CodTabelaPreco, decimal precoDesejado, decimal PercDesc1, decimal PercDesc2, string codAtributo);
        Task<ObservableCollection<ItemCommandResult>> BuscarItensCarrinho(BuscarItensCarrinhoCommand command);
        Task<ItemCommandResult> BuscarItemCarrinho(BuscarItensCarrinhoCommand command);
        Task<List<DerivacaoGradeResult>> BuscarGradesDoItem(BuscarGradesItemCommand command);
        Task<bool> DesmembrarItens(string codCarrinhoDestino, List<TBT_ITEM_CARRINHO> itens);
        Task<bool> Copiar(CopiarCarrinhoCommand command);
        Task<bool> Cancelar(CancelarCarrinhoCommand command);
        Task<bool> SalvarFechamento(AtualizarCarrinhoFechamentoCommand command);
        Task<string> QuebrarPedido(QuebrarPedidoCommand command);
        Task<string> GerarNovoCodCarrinho(UsuarioCommandResult usuario);
        //        Task<List<string>> ValidacoesDoCarrinho(string codCarrinho);
        Task<List<string>> ValidacoesDoCarrinho(string codCarrinho, bool validaTipoFrete);
        Task AlterarClienteDoCarrinho(string codCarrinho, string codPessoaCliente);
        Task<List<ItemCommandResult>> GetListItensResumo(CarrinhoCommandResult carrinho, string codNivelAgrupamento);
        Task BloquearCarrinhos(List<CarrinhoCommandResult> carrinhos);
        Task<bool> AlterarSituacao(string codCarrinho, string codSituacaoPedido);
        Task<bool> AlterarAtendimento(string codCarrinho, string codAtendimento);
        Task<bool> AlterarPedidoDestino(string codPedidoDestino, string codPedidoOrigem);
        Task<List<CriticaCarrinhoCommandResult>> BuscarCriticas(string codCarrinho);
        Task<bool> AtualizarDataFaturamento(string codCarrinho);
        Task<ObservableCollection<DerivacaoGradeResult>> GetListItensGradeResumo(string codCarrinho);
        Task<bool> AtualizaQtdCarrinho(string codCarrinho);
        Task ExcluirItens(List<TBT_ITEM_CARRINHO> itens);
        Task ReordenarItens(string codCarrinho);
        Task CadastraCarrinhoHistorico(string codCarrinho);
    }

}
