namespace Intermidia.Intermidia.Infra.Domain.Entities
{
    public class TBT_REGRA_USUARIO_CLIENTE : Entity
    {
        public string TpRegra { get; set; }
        public string CodCliente { get; set; }
        public string CodRepresentante { get; set; }
        public Int32 ID { get; set; }
        public DateTime CtrlDataOperacao { get; set; }
    }

    public class TBT_REGRA_USUARIO_MARCA : Entity
    {
        public string TpRegra { get; set; }
        public string CodMarca { get; set; }
        public string CodRepresentante { get; set; }
        public Int32 ID { get; set; }
        public DateTime CtrlDataOperacao { get; set; }
    }

    //public class TBT_NIVEL_ATRIBUTO : Entity
    //{
    //    public string CodNivel { get; set; }
    //    public string CodAtributo { get; set; }
    //    public string Descricao { get; set; }
    //    public Int32 ID { get; set; }
    //    public DateTime CtrlDataOperacao { get; set; }
    //}

    public class TBT_REGRA_USUARIO_PRODUTO : Entity
    {
        public string TpRegra { get; set; }
        public string CodProduto { get; set; }
        public string CodRepresentante { get; set; }
        public Int32 ID { get; set; }
        public DateTime CtrlDataOperacao { get; set; }
    }
}
