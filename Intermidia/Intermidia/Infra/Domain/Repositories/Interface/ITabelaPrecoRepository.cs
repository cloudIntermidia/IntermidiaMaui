using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;
using AutenticarUsuarioCommand = Intermidia.Intermidia.Infra.Domain.Commands.Inputs.AutenticarUsuarioCommand;

namespace Intermidia.Intermidia.Infra.Domain.Repositories.Interface
{
    public interface ITabelaPrecoRepository : IDisposable
    {
        Task<List<TabelaPrecoResult>> BuscarTabelasDePreco(BuscarTabelaPrecoCommand command);
        Task<TabelaPrecoResult> BuscarTabelaDePreco(BuscarTabelaPrecoCommand command);
        Task<TabelaPrecoResult> BuscarTabelaDePrecoPadrao(BuscarTabelaPrecoCommand command);
        Task<List<TabelaPrecoIndiceResult>> BuscarIndicesTabelaDePreco(BuscarTabelaPrecoIndiceCommand command);
        Task<List<TabelaPrecoResult>> BuscarTabelasPorCode(BuscarTabelaPrecoCommand command);
        Task<decimal> BuscarComissaoItem(BuscarItemTabelaPrecoCommand command);
    }
}
