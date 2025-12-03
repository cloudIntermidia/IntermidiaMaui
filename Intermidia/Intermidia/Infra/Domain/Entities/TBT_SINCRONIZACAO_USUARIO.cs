namespace Intermidia.Intermidia.Infra.Domain.Entities
{
    public class TBT_SINCRONIZACAO_USUARIO : Entity
    {
        public string CodUsuario { get; set; }
        public DateTime DataUltimaSincronizacao { get; set; }
    }
}
