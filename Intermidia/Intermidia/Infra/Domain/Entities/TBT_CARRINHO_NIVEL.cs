using System;

namespace Intermidia.Intermidia.Infra.Domain.Entities
{
    public class TBT_CARRINHO_NIVEL : Entity
    {
        public TBT_CARRINHO_NIVEL()
        {

        }

        public TBT_CARRINHO_NIVEL(string codCarrinho, string codNivel, string codAtributo)
        {
            CodCarrinho = codCarrinho;
            CodNivel = codNivel;
            CodAtributo = codAtributo;
        }

        public string CodCarrinho { get; set; }
        public string CodNivel { get; set; }
        public string CodAtributo { get; set; }
    }
}
