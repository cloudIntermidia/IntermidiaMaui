using System;

namespace Intermidia.Intermidia.Infra.Domain.Entities
{
    public class TBT_ENDERECO_CLIENTE : Entity
    {
        public string CodPessoaCliente { get; set; }
        public int SeqEndereco { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Telefone { get; set; }
        public string Fax { get; set; }
        public string CEP { get; set; }
        //public int IndComercial { get; set; }
        //public int IndResidencial { get; set; }
        public int IndCobranca { get; set; }
        //public int IndEntrega { get; set; }
        //public int IndContato { get; set; }
        public int IndPrincipal { get; set; }
        public decimal ID { get; set; }
        public Nullable<System.DateTime> CtrlDataOperacao { get; set; }
        //public string Latitude { get; set; }
        //public string Longitude { get; set; }
        public String CodMunicipio { get; set; }
    }

}
