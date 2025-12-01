using System;

namespace Intermidia.Intermidia.Infra.Domain.Entities
{
    public class TBT_MARCA_CLIENTE : Entity
    {
        public string CodMarca { get; set; }
        public string CodPessoaCliente { get; set; }
        public string CodPessoaRepresentante { get; set; }
        public string CodPessoaVendedor { get; set; }
        public string CodPessoaVendedorSeg { get; set; }
        public string CodTabelaPreco { get; set; }
        public string CodCondicaoPagamento { get; set; }
        public string CodFormaPagamento { get; set; }
        public string CodListaPreco { get; set; }
        public string CodTransportadora { get; set; }
        public string CodRedespacho { get; set; }
        public int IndBloqueioPedido { get; set; }
        public string TnsPedido { get; set; }
        public string AcePar { get; set; }
        public Nullable<int> Custom1 { get; set; }
        public Nullable<int> Custom2 { get; set; }
        public Nullable<int> Custom3 { get; set; }
        public decimal ID { get; set; }
        public Nullable<System.DateTime> CtrlDataOperacao { get; set; }

    }

}
