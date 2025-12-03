using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;
using AutenticarUsuarioCommand = Intermidia.Intermidia.Infra.Domain.Commands.Inputs.AutenticarUsuarioCommand;

namespace Intermidia.Intermidia.Infra.Domain.Repositories.Interface
{
    public interface IProdutoRepository
    {
        Task<decimal> GetPreco(BuscarItemTabelaPrecoCommand command);
        Task<string> GetCodTabelaPrecoMaisProxByProdutoAndPreco(BuscarItemTabelaPrecoCommand command);
        Task<List<ItemCommandResult>> BuscarProdutosDoModelo(ModeloCommandResult command, AtendimentoCommandResult atendimento, string estoque);
        Task<List<DerivacaoGradeResult>> BuscarGradesDoProduto(BuscarGradesProdutoCommand command);
        Task<List<DerivacaoGradeResult>> BuscarGradesDoProdutoProjecao(BuscarGradesProdutoCommand command);
        Task<List<DerivacaoGradeResult>> BuscarGradesDoProdutoPE(BuscarGradesProdutoCommand command);
        Task<List<DerivacaoGradeResult>> BuscarTamanhosPossiveisDoProduto(BuscarGradesProdutoCommand command);
        Task<List<DerivacaoGradeResult>> BuscarTamanhosPossiveisDoProdutoProjecao(BuscarGradesProdutoCommand command);
        Task<List<DerivacaoGradeResult>> BuscarTamanhosPossiveisDoProdutoPE(BuscarGradesProdutoCommand command);
        Task<List<DerivacaoGradeResult>> BuscarTamanhosPossiveisDoKit(BuscarGradesProdutoCommand command);
        Task<List<TecnologiaCommandResult>> BuscarTecnologiasDoProduto(BuscarTecnologiaCommand command);
        Task<List<MaterialCommandResult>> BuscarMateriaisDoProduto(BuscarMaterialCommand command);
        Task<TabelaPrecoIndiceResult> BuscarPrecoDaTabela(BuscarPrecoCommand command);
        Task<ItemCommandResult> BuscarQtdItemNoAtendimento(BuscarItemAtendimentoCommand command);
        Task<bool> ProdutoEstaNaSegmentacao(string codProduto, string codSegmentacao);
        Task<List<string>> BuscarInformacoesDoDetalhe(string codProduto);
        Task<string> BuscarDescontoMaximo(string codProduto);

        Task<List<EstoqueResult>> BuscarEstoques(BuscarEstoquesCommand command);
        Task<List<GenericComboResult>> BuscarEstoqueDisponiveisDoProduto(BuscarEstoquesCommand command);
        Task<List<EstoqueResult>> BuscarEstoquesGradeFechada(BuscarEstoquesCommand command);
        Task<List<EstoqueResult>> BuscarEstoqueKit(BuscarEstoquesCommand command);

        Task<string> GetString(string codProduto, string columnName);
        Task<string> GetCodBarraDerivacao(string codProduto, string codDerivacao);

        Task<List<GenericComboResult>> BuscarGradesDisponiveisDoProduto(BuscarGradesProdutoCommand command);
        Task<List<GenericComboResult>> BuscarCDProduto(BuscarCDProdutoCommand command);
        Task<List<string>> BuscarProdutosRelacionados(string codProduto);
        Task<List<TecnologiaCommandResult>> BuscarTecnologiasDoModelo(BuscarTecnologiaCommand command);

        Task<ItemCommandResult> BuscarProduto(string CodProduto);
    }

}
