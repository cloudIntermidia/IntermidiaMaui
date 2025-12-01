using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using System;

namespace Intermidia.Intermidia.Infra.Domain.Entities
{
    public class TBT_GRADE_ITEM_CARRINHO : Entity
    {
        public TBT_GRADE_ITEM_CARRINHO()
        {

        }

        public TBT_GRADE_ITEM_CARRINHO(string codCarrinho,
                                       decimal codItemCarrinho,
                                       decimal codGradeItemCarrinho,
                                       DerivacaoGradeResult grade)
        {
            CodCarrinho = codCarrinho;
            CodItemCarrinho = (int)codItemCarrinho;
            CodGradeItemCarrinho = (int)codGradeItemCarrinho;
            CodDerivacao = grade.CodDerivacao;
            Qtd = (int)grade.Qtd;
            QtdNaGrade = (int)grade.QtdNaGrade;
            //CodProdutoSku = grade.CodProdutoSku;
            CtrlDataOperacao = DateTime.Now;
        }
        public string CodCarrinho { get; set; }
        //public string CodProdutoSku { get; set; }
        public int CodItemCarrinho { get; set; }
        public int CodGradeItemCarrinho { get; set; }
        public string CodDerivacao { get; set; }
        public int Qtd { get; set; }
        public int QtdNaGrade { get; set; }
        public int? CodInstalacao { get; set; }
        public Nullable<System.DateTime> CtrlDataOperacao { get; set; }
        public decimal ID { get; set; }
    }
}
