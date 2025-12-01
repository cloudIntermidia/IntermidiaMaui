using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public interface ICondicaoPagamentoRepository
    {
        Task<TabelaPrecoResult> BuscarCondicaoPagamento(BuscarCondicaoPagamentoCommand command);
        Task<List<GenericComboResult>> BuscarCondicoesParaFechamento(BuscarCondicaoPagamentoCommand command);
        Task<List<GenericComboResult>> BuscarCondicoesParaFechamento(BuscarCondicaoPagamentoCommand command, string codCarrinho);
        Task<List<GenericComboResult>> BuscarCondicaoParaFechamentoPadrao();
        Task<List<GenericComboResult>> BuscarPrazosMedio(BuscarCondicaoPagamentoCommand command);
        Task<GenericComboResult> BuscarCondicoesParaCliente(BuscarCondicaoPagamentoCommand command);
        Task<GenericComboResult> BuscarCondicaoPadrao(BuscarCondicaoPagamentoCommand command);
    }
}
