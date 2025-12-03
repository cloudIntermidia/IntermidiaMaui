namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class CriarPessoaCommand
    {
        public CriarPessoaCommand(string codPessoa, string nome, string codTipoPessoa, string codCondicaoPagamento, string email,
                                  string codPessoaAFV, string codPessoaERP,decimal percentualDesconto, decimal indAtivo,
                                  decimal codUsuario, string codInstalacao)
        {
            CodPessoa = codPessoa;
            Nome = nome;
            CodTipoPessoa = codTipoPessoa;
            CodCondicaoPagamento = codCondicaoPagamento;
            Email = email;
            CodPessoaAFV = codPessoaAFV;
            CodPessoaERP = codPessoaERP;
            PercentualDesconto = percentualDesconto;
            IndAtivo = indAtivo;
            CodUsuario = codUsuario;
            CodInstalacao = codInstalacao;
        }

        public decimal CodUsuario { get; private set; }
        public string CodInstalacao { get; private set; }
        public string CodPessoa { get; set; }
        public string Nome { get; private set; }
        public string CodTipoPessoa { get; private set; }
        public string CodCondicaoPagamento { get; private set; }
        public string Email { get; private set; }
        public string CodPessoaAFV { get; private set; }
        public string CodPessoaERP { get; private set; }
        public decimal PercentualDesconto { get; private set; }
        public decimal IndAtivo { get; private set; }
    }
}
