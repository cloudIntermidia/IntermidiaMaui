namespace Intermidia.Intermidia.Infra.Domain.Entities
{
    public class TBT_CLIENTE : Entity
    {
        public string CodPessoaCliente { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string CodGrupoCliente { get; set; }
        public string CodSituacaoCliente { get; set; }
        public string CodCategoriaCliente { get; set; }
        public string Email { get; set; }
        public string EmailNFE { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Contato { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public Nullable<System.DateTime> DataCadastro { get; set; }
        public Nullable<System.DateTime> DataAlteracao { get; set; }
        public decimal ID { get; set; }
        public Nullable<System.DateTime> CtrlDataOperacao { get; set; }
        public string UltimoPedido { get; set; }
        public string CodRedeCliente { get; set; }
        public string Perfil { get; set; }
        public string Limite { get; set; }

        public DateTime? Datafundacao { get; set; }
        public string Emailcobranca { get; set; }
        public string EmailComercial { get; set; }
        public string RamoAtividade { get; set; }
        public string Suframa { get; set; }
        public string ClasificacaoComercial { get; set; }
        public string Parecer { get; set; }
        public string Grupo { get; set; }
        public string Prazo { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }

        public DateTime? ValidaSuframa { get; set; }
        public string ContatoFinanceiro { get; set; }
        public string ContatoComercial { get; set; }
        public string ReferenciaComercial { get; set; }
        public string ReferenciaBancaria { get; set; }
        public string CodMatriz { get; set; }
        public string Observacao { get; set; }

        public string NomeContatoFinanceiro { get; set; }
        public string SobrenomeContatoFinanceiro { get; set; }
        public string TelefoneFinanceiro { get; set; }
        public string CelularFinanceiro { get; set; }
        public string EmailFinanceiro { get; set; }
        public string NomeSocio { get; set; }
        public string SobrenomeSocio { get; set; }
        public string CelularSocio { get; set; }
        public string EmailSocio { get; set; }
        public string NomeComprador { get; set; }
        public string SobrenomeComprador { get; set; }
        public string CelularComprador { get; set; }
        public string EmailComprador { get; set; }
        public DateTime? DataAtualizacaoFinanceiro { get; set; }
    }
}
