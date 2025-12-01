using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class BuscarCarrinhoCommand
    {
        public string CodCarrinho { get; set; }
        public string CodAtendimento { get; set; }
        public string CodSituacaoPedido { get; set; }
        public decimal CodUsuario { get; set; }
        public string CodPessoaCliente { get; set; }
        public string CodTipoPedido { get; set; }
        public string CodMarca { get; set; }
        public Boolean ConsideraProdutoKit { get; set; }

        public BuscarCarrinhoCommand()
        {
        }

        public BuscarCarrinhoCommand(AtendimentoCommandResult atendimento, UsuarioCommandResult usuario, string codSituacaoPedido, string codTipoPedido = null, string codCarrinho = null)
        {
            CodCarrinho = codCarrinho;
            CodAtendimento = atendimento?.CodAtendimento;
            CodSituacaoPedido = codSituacaoPedido;
            CodUsuario = usuario.CodUsuario;
            CodPessoaCliente = atendimento?.CodPessoaCliente;
            CodTipoPedido = codTipoPedido;
            CodMarca = usuario.CodMarca;
        }

        public BuscarCarrinhoCommand(string codCarrinho, string codAtendimento, string codSituacaoPedido, decimal codUsuario, string codPessoaCliente, string codTipoPedido, string codMarca)
        {
            CodCarrinho = codCarrinho;
            CodAtendimento = codAtendimento;
            CodSituacaoPedido = codSituacaoPedido;
            CodUsuario = codUsuario;
            CodPessoaCliente = codPessoaCliente;
            CodTipoPedido = codTipoPedido;
            CodMarca = codMarca;
        }

        public BuscarCarrinhoCommand(AtendimentoCommandResult atendimento, UsuarioCommandResult usuario, string codSituacaoPedido, Boolean produtoKit = false)
        {
            //            CodCarrinho = codCarrinho;
            CodAtendimento = atendimento?.CodAtendimento;
            CodSituacaoPedido = codSituacaoPedido;
            CodUsuario = usuario.CodUsuario;
            CodPessoaCliente = atendimento?.CodPessoaCliente;
            //            CodTipoPedido = codTipoPedido;
            CodMarca = usuario.CodMarca;
            ConsideraProdutoKit = produtoKit;
        }
    }


}
