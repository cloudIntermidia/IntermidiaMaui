using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;
using AutenticarUsuarioCommand = Intermidia.Intermidia.Infra.Domain.Commands.Inputs.AutenticarUsuarioCommand;

namespace Intermidia.Intermidia.Infra.Domain.Repositories.Interface
{
    public interface INivelRepository : IDisposable
    {
        Task<List<NivelResult>> GetConfiguracaoNivel();
        Task<List<NivelResult>> GetConfiguracaoNivelSortido();
        Task<List<NivelAtributoResult>> GetNivel(BuscarNivelCommand command, List<NivelAtributoResult> filtros);
        Task<List<NivelResult>> GetNiveisQuebra();
        Task<List<NivelProdutoResult>> GetNiveisProduto(string codProduto);
        Task<List<NivelProdutoResult>> GetNiveisDeQuebraProduto(string codProduto);
        Task<List<NivelProdutoResult>> GetNiveisProduto(string codProduto, string codNiveis);
        Task<List<NivelDescontoCommandResult>> GetDescontos(BuscarNivelDescontoCommand command);
        Task<List<NivelAtributoResult>> GetNiveisSegmentacao();
        Task<List<NivelAtributoResult>> GetNiveisSegmentacaoDoCliente(string codPessoaCliente);
        Task<List<NivelResult>> GetConfiguracaoNivelGeracaoCatalogo();
        Task<string> GetCodNivelDesconto(string titulo);
    }
}
