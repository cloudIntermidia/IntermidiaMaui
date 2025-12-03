using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Repositories.Interface
{
    public interface IClienteRepository
    {
        Task<ClienteCommandResult> BuscarCliente(BuscarClienteCommand command);
        Task<ClienteCommandResult> BuscarClienteIntegrado(string cnpj);

        Task<List<ClienteCommandResult>> BuscarClientes(BuscarClienteCommand command);

        Task<ClienteCommandResult> BuscarClientePorCode(BuscarClienteCommand command);

        Task<List<ClienteCommandResult>> BuscarTodosClientes(BuscarClienteCommand command);
        Task<EnderecoCommandResult> BuscarEnderecoPrincipal(string codPessoaCliente);
        Task<EnderecoCommandResult> BuscarEnderecoCobranca(string codPessoaCliente);
        Task<List<GestaoClienteCommandResult>> BuscarGestaoClientes(BuscarGestaoClienteCommand command);
        Task<List<GestaoClienteCommandResult>> BuscarGestaoClientesAll(BuscarGestaoClienteCommand command);

        Task<List<GenericComboResult>> BuscarClientes(UsuarioCommandResult command);
        Task<List<GenericComboResult>> BuscarVendedores(BuscarGestaoClienteCommand command);
        Task<List<GenericComboResult>> BuscarAllVendedores(BuscarGestaoClienteCommand command);
        Task<List<GenericComboResult>> BuscarClientes(BuscarGestaoClienteCommand command);
        Task<List<GenericComboResult>> BuscarTipoFeedback(BuscarTipoFeedback command);
        Task<List<GenericComboResult>> BuscarTipoFeedback();
        Task<string> BuscarRepresentantePorCliente(ClienteCommandResult command);
        Task<List<GenericComboResult>> BuscarUFs(BuscarGestaoClienteCommand command);
        Task<List<GenericComboResult>> BuscarGrupoDeClientes(BuscarGestaoClienteCommand command);

        Task<List<GenericComboResult>> BuscarUFCadastro();
        Task<List<GenericComboResult>> BuscarMunicipios(string uf);

        Task<List<GenericComboResult>> BuscarSegmentacaoCadastro();

        Task<bool> CriarFeedbackCliente(CriarFeedbackClienteCommand cliente);
        Task<bool> CnpjJaExiste(string cnpj);
        Task<bool> CriarCliente(CriarClienteCommand cliente);
        Task<bool> AtualizarCliente(CriarClienteCommand command, IParametroSincronizacaoRepository parametroSincronizacaoRepository);

        Task<bool> AtualizarClienteIntegrado(string codPessoaCliente, string codPessoaErp);
        Task<bool> AtualizarEnviarERP(string codPessoaCliente, string codPessoaErp);
        Task<bool> AtualizarClienteNoCarrinho(string codCarrinho, string codPessoaErp);
        Task<bool> RelacionarClienteComRepresentante(RelacionarClienteCommand command);

        Task<bool> AdicionarEndereco(EnderecoCommandResult endereco);
        Task<WcfClienteModelInput> BuscarClienteParaTransmissao(string codPessoaCliente);

        Task<bool> VerificaClienteFeira(BuscarClienteCommand command);

        Task<decimal> BuscarMinimoFretePorCliente(string codPessoaCliente);
        Task<string> BuscarTransportadoraPorCliente(string codPessoaCliente);
        Task<decimal> BuscarMinimoRegiao(string codPessoaCliente);

        //Task<List<ED_CollectionView_Generico>> ListarTabelaPreco_Disponivel();
        //Task<List<ED_CollectionView_Generico>> ListarEmppesa_Disponivel();
        Task<List<ED_CollectionView_Generico>> ListarEmpresa_TabelaPreco_Disponivel();
        Task<bool> AtualizarClienteTabelaPreco(string codPessoaCliente, string codPessoaErp);

    }

}
