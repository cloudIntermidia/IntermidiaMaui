using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;
using SQLite;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class FuncaoGenerica
    {

        private Boolean Usuario_Tem_Regra_Cliente(TipoRegra tipoRegra)
        {
            Boolean retorno = false;

            switch (tipoRegra)
            {
                case TipoRegra.Cliente:
                    if (Session_Generica.Instance.LIST_REGRA_CLIENTE != null)
                        retorno = true;
                    break;
                case TipoRegra.Marca:
                    if (Session_Generica.Instance.LIST_REGRA_MARCA != null)
                        retorno = true;
                    break;
                case TipoRegra.Produto:
                    if (Session_Generica.Instance.LIST_REGRA_PRODUTO != null)
                        retorno = true;
                    break;
            }

            return retorno;
        }

        public enum TipoRegra
        {
            Cliente,
            Marca,
            Produto
        }

        public enum TipoAcesso
        {
            //Todos,
            Permissao,
            Restricao
        }

        public ArrayList Retorna_Permissao(TipoRegra tipoRegra, TipoAcesso tipoAcesso)
        {
            ArrayList retorno = new ArrayList();

            if (Usuario_Tem_Regra_Cliente(tipoRegra))
            {
                String TpRegra = String.Empty;

                switch (tipoAcesso)
                {
                    case TipoAcesso.Permissao:
                        TpRegra = "P";
                        break;
                    case TipoAcesso.Restricao:
                        TpRegra = "R";
                        break;

                }

                switch (tipoRegra)
                {
                    case TipoRegra.Cliente:
                        foreach (TBT_REGRA_USUARIO_CLIENTE oED in Session_Generica.Instance.LIST_REGRA_CLIENTE)
                        {
                            if (oED.TpRegra.Equals(TpRegra) && oED.CodRepresentante.Equals(Session_Generica.Instance.LIST_REGRA_CLIENTE[0].CodRepresentante))
                            {
                                if (!retorno.Contains(oED.CodCliente))
                                    retorno.Add(oED.CodCliente);
                            }
                        }
                        break;
                    case TipoRegra.Marca:
                        foreach (TBT_REGRA_USUARIO_MARCA oED in Session_Generica.Instance.LIST_REGRA_MARCA)
                        {
                            if (oED.TpRegra.Equals(TpRegra) && oED.CodRepresentante.Equals(Session_Generica.Instance.LIST_REGRA_MARCA[0].CodRepresentante))

                            {
                                if (!retorno.Contains(oED.CodMarca))
                                    retorno.Add(oED.CodMarca);
                            }
                        }
                        break;
                    case TipoRegra.Produto:
                        foreach (TBT_REGRA_USUARIO_PRODUTO oED in Session_Generica.Instance.LIST_REGRA_PRODUTO)
                        {
                            if (oED.TpRegra.Equals(TpRegra) && oED.CodRepresentante.Equals(Session_Generica.Instance.LIST_REGRA_PRODUTO[0].CodRepresentante))

                            {
                                if (!retorno.Contains(oED.CodProduto))
                                    retorno.Add(oED.CodProduto);
                            }
                        }
                        break;

                }

            }

            return retorno;
        }

        public String Retorna_Where_Permissao(TipoRegra tipoRegra, String nomeCampo)
        {
            StringBuilder retorno = new StringBuilder();

            if (Usuario_Tem_Regra_Cliente(tipoRegra))
            {
                #region Permissao

                String whereTemp_Permissao = String.Empty;
                ArrayList listaPermissao = Retorna_Permissao(tipoRegra, TipoAcesso.Permissao);
                if (listaPermissao != null && listaPermissao.Count > 0)
                {
                    foreach (String valor in listaPermissao)
                    {
                        whereTemp_Permissao = String.Format("{0}'{1}',", whereTemp_Permissao, valor);
                    }
                }

                #endregion Permissao

                #region Restricao

                String whereTemp_Restricao = String.Empty;
                ArrayList listaRestricao = Retorna_Permissao(tipoRegra, TipoAcesso.Restricao);
                if (listaRestricao != null && listaRestricao.Count > 0)
                {
                    foreach (String valor in listaRestricao)
                    {
                        whereTemp_Restricao = String.Format("{0}'{1}',", whereTemp_Restricao, valor);
                    }
                }

                #endregion Restricao

                if (!String.IsNullOrEmpty(whereTemp_Permissao) || !String.IsNullOrEmpty(whereTemp_Restricao))
                {
                    retorno.Append(" AND ( ");

                    if (tipoRegra == TipoRegra.Cliente)
                        retorno.Append(" ( ");
                }

                if (!String.IsNullOrEmpty(whereTemp_Permissao))
                {
                    retorno.Append(nomeCampo);
                    retorno.Append(String.Format(" IN ({0})", whereTemp_Permissao.Substring(0, whereTemp_Permissao.Length - 1)));
                }

                if (!String.IsNullOrEmpty(whereTemp_Restricao))
                {
                    if (!String.IsNullOrEmpty(whereTemp_Permissao))
                        retorno.Append(" AND ");

                    retorno.Append(nomeCampo);
                    retorno.Append(String.Format(" NOT IN ({0})", whereTemp_Restricao.Substring(0, whereTemp_Restricao.Length - 1)));
                }

                if (!String.IsNullOrEmpty(whereTemp_Permissao) || !String.IsNullOrEmpty(whereTemp_Restricao))
                {
                    if (tipoRegra == TipoRegra.Cliente)
                        retorno.Append(" OR c.CodSituacaoCliente = 50 ) ");

                    retorno.Append(" ) ");
                }

            }

            return retorno.ToString();
        }

        //public String Retorna_Where_Permissao_Like(TipoRegra tipoRegra, String nomeCampo)
        //{
        //    StringBuilder retorno = new StringBuilder();

        //    if (Usuario_Tem_Regra_Cliente(tipoRegra))
        //    {
        //        #region Permissao

        //        String whereTemp_Permissao = String.Empty;
        //        ArrayList listaPermissao = Retorna_Permissao(tipoRegra, TipoAcesso.Permissao);
        //        if (listaPermissao != null && listaPermissao.Count > 0)
        //        {
        //            foreach (String valor in listaPermissao)
        //            {
        //                whereTemp_Permissao = String.Format("{0}'{1}',", whereTemp_Permissao, valor);
        //            }
        //        }

        //        #endregion Permissao

        //        #region Restricao

        //        String whereTemp_Restricao = String.Empty;
        //        ArrayList listaRestricao = Retorna_Permissao(tipoRegra, TipoAcesso.Restricao);
        //        if (listaRestricao != null && listaRestricao.Count > 0)
        //        {
        //            foreach (String valor in listaRestricao)
        //            {
        //                whereTemp_Restricao = String.Format("{0}'{1}',", whereTemp_Restricao, valor);
        //            }
        //        }

        //        #endregion Restricao

        //        if (!String.IsNullOrEmpty(whereTemp_Permissao) || !String.IsNullOrEmpty(whereTemp_Restricao))
        //        {
        //            retorno.Append(" AND ( ");

        //            if (tipoRegra == TipoRegra.Cliente)
        //                retorno.Append(" ( ");
        //        }

        //        if (!String.IsNullOrEmpty(whereTemp_Permissao))
        //        {
        //            retorno.Append(nomeCampo);
        //            retorno.Append(String.Format(" IN ({0})", whereTemp_Permissao.Substring(0, whereTemp_Permissao.Length - 1)));
        //        }

        //        if (!String.IsNullOrEmpty(whereTemp_Restricao))
        //        {
        //            if (!String.IsNullOrEmpty(whereTemp_Permissao))
        //                retorno.Append(" AND ");

        //            retorno.Append(nomeCampo);
        //            retorno.Append(String.Format(" NOT IN ({0})", whereTemp_Restricao.Substring(0, whereTemp_Restricao.Length - 1)));
        //        }

        //        if (!String.IsNullOrEmpty(whereTemp_Permissao) || !String.IsNullOrEmpty(whereTemp_Restricao))
        //        {
        //            if (tipoRegra == TipoRegra.Cliente)
        //                retorno.Append(" OR c.CodSituacaoCliente = 50 ) ");

        //            retorno.Append(" ) ");
        //        }

        //    }

        //    return retorno.ToString();
        //}


        //private String Retorna_Desconto_Tratado(String codCliente)
        //{
        //    StringBuilder retorno = new StringBuilder();
        //    retorno.Append("SELECT ");
        //    retorno.Append("    CASE ");
        //    retorno.Append("        WHEN TABD.DESCONTO_TEMP > 0 AND TABD.DESCONTO_TEMP < 100 ");
        //    retorno.Append("        THEN 100 - TABD.DESCONTO_TEMP ");
        //    retorno.Append("        ELSE TABD.DESCONTO_TEMP ");
        //    retorno.Append("    END AS DESCONTO_FINAL ");
        //    retorno.Append("FROM ( ");
        //    retorno.Append("    SELECT DISTINCT CAST(COALESCE(REPLACE(REPLACE(CTPD.DESCONTO, '-', ''), '+', '1'), '0') AS INT) AS DESCONTO_TEMP ");
        //    retorno.Append("    FROM TBT_CLIENTE_TABELA_PRECO CTP ");
        //    retorno.Append("    INNER JOIN TBT_CLIENTE_TABELA_PRECO_DESCONTO CTPD ON CTP.CodTabelaPreco = CTPD.CodTabelaPreco ");
        //    retorno.Append(String.Format("WHERE CTP.CodCliente = '{0}' AND CTP.CodEmpresa = P.CodEmpresa ", codCliente));
        //    retorno.Append(") TABD ");
        //    return retorno.ToString();
        //}

        public enum TipoDesconto
        {
            Desconto,
            Acrescimo
        }

        private String Retorna_Desconto_Tratado(TipoDesconto tipo, String codCliente)
        {
            String whereDesconto = String.Empty;
            if (tipo == TipoDesconto.Desconto)
            {
                whereDesconto = " AND (CTPD.Tipo IS NULL OR CTPD.Tipo = 'D')";
            }
            else
            {
                whereDesconto = " AND CTPD.Tipo = 'A'";
            }

            StringBuilder retorno = new StringBuilder();
            retorno.Append("SELECT ");

            if (tipo == TipoDesconto.Desconto)
            {
                retorno.Append("    CASE ");
                retorno.Append("        WHEN TABD.DESCONTO_TEMP > 0 AND TABD.DESCONTO_TEMP < 100 ");
                retorno.Append("        THEN 100 - TABD.DESCONTO_TEMP ");
                retorno.Append("        ELSE TABD.DESCONTO_TEMP ");
                retorno.Append("    END AS DESCONTO_FINAL ");
            }
            else
            {
                retorno.Append("    TABD.DESCONTO_TEMP AS DESCONTO_FINAL ");
            }

            retorno.Append("FROM ( ");
            retorno.Append("    SELECT DISTINCT CAST(COALESCE(REPLACE(REPLACE(CTPD.DESCONTO, '-', ''), '+', '1'), '0') AS INT) AS DESCONTO_TEMP ");
            retorno.Append("    FROM TBT_CLIENTE_TABELA_PRECO CTP ");
            retorno.Append("    INNER JOIN TBT_CLIENTE_TABELA_PRECO_DESCONTO CTPD ON CTP.CodTabelaPreco = CTPD.CodTabelaPreco ");
            retorno.Append(String.Format("WHERE CTP.CodCliente = '{0}' AND CTP.CodEmpresa = P.CodEmpresa ", codCliente));
            retorno.Append(whereDesconto);
            retorno.Append(") TABD ");
            return retorno.ToString();
        }



        public String Retorna_CampoValor_ClienteEmpresa(TipoDesconto tipo, String codCliente)
        {
            StringBuilder retorno = new StringBuilder();

            //codCliente = "34645";

            if (!String.IsNullOrEmpty(codCliente))
            {
                String nomeCampo = String.Empty;
                if (tipo == TipoDesconto.Desconto)
                {
                    nomeCampo = "PrecoVenda_ClienteEmpresa";
                }
                else
                {
                    nomeCampo = "PrecoVenda_ClienteEmpresa_Acrescimo";
                }

                retorno.Append(", (ROUND(ROUND(ITP.Preco, 2) / 100 * ");
                retorno.Append("(");
                //                retorno.Append(Retorna_Desconto_Tratado(codCliente));
                retorno.Append(Retorna_Desconto_Tratado(tipo, codCliente));
                retorno.Append(" ) ");
                retorno.Append(", 2) ");
                retorno.Append(String.Format(") AS {0} ", nomeCampo));
            }

            return retorno.ToString();
        }

        public String Retorna_CampoValor_ProdutoKit(String codCliente)
        {
            StringBuilder retorno = new StringBuilder();

            if (!String.IsNullOrEmpty(codCliente))
            {
                retorno.Append(", (SELECT(CASE ");
                retorno.Append("           WHEN KIT3.ValorTotal_Tabela IS NOT NULL AND KIT3.ValorTotal_Tabela > 0 ");
                retorno.Append("               THEN KIT3.ValorTotal_Tabela ");
                retorno.Append("           WHEN KIT3.ValorTotal_Tabela_Acrescimo IS NOT NULL AND KIT3.ValorTotal_Tabela_Acrescimo > 0 ");
                retorno.Append("               THEN KIT3.ValorTotal + KIT3.ValorTotal_Tabela_Acrescimo ");
                retorno.Append("               ELSE KIT3.ValorTotal ");
                retorno.Append("           END) AS ValorKit ");
                retorno.Append(" FROM ( ");

                retorno.Append("    SELECT KIT2.ValorTotal, ");
                retorno.Append("        (ROUND(ROUND(KIT2.ValorTotal, 2) / 100 * ");
                retorno.Append("            ( ");
                retorno.Append(Retorna_Desconto_Tratado(TipoDesconto.Desconto, codCliente));
                retorno.Append("            ), 2) ");
                retorno.Append("        ) AS ValorTotal_Tabela ");

                retorno.Append("        ,(ROUND(ROUND(KIT2.ValorTotal, 2) / 100 * ");
                retorno.Append("            ( ");
                retorno.Append(Retorna_Desconto_Tratado(TipoDesconto.Acrescimo, codCliente));
                retorno.Append("            ), 2) ");
                retorno.Append("        ) AS ValorTotal_Tabela_Acrescimo ");

                retorno.Append("        FROM (SELECT SUM(KIT1.ValorTotal) AS ValorTotal ");
                retorno.Append("    FROM (SELECT DISTINCT KIT.CodProduto, KIT.Quantidade, ITP.Preco As PrecoVenda, (ITP.Preco * KIT.Quantidade) AS ValorTotal ");
                retorno.Append("    FROM TBT_PRODUTO_KIT KIT");
                retorno.Append("    INNER JOIN TBT_PRODUTO PROD ON KIT.CodProduto = PROD.CodProduto ");
                retorno.Append("    LEFT JOIN TBT_ITEM_TABELA_PRECO ITP ON KIT.CodProduto = ITP.CodProduto ");
                retorno.Append("    WHERE KIT.CodProdutoKit = P.CodProduto ");
                retorno.Append("    ) KIT1) KIT2) KIT3) AS PrecoVenda_Kit");

            }
            else
            {
                retorno.Append(", (SELECT SUM(KIT1.ValorTotal) AS ValorTotal ");
                //                retorno.Append("    FROM (SELECT KIT.CodProduto, KIT.Quantidade, ITP.Preco As PrecoVenda, (ITP.Preco * KIT.Quantidade) AS ValorTotal ");
                retorno.Append("    FROM (SELECT DISTINCT KIT.CodProduto, KIT.Quantidade, ITP.Preco As PrecoVenda, (ITP.Preco * KIT.Quantidade) AS ValorTotal ");
                retorno.Append("    FROM TBT_PRODUTO_KIT KIT");
                retorno.Append("    INNER JOIN TBT_PRODUTO PROD ON KIT.CodProduto = PROD.CodProduto ");
                retorno.Append("    LEFT JOIN TBT_ITEM_TABELA_PRECO ITP ON KIT.CodProduto = ITP.CodProduto ");
                retorno.Append("    WHERE KIT.CodProdutoKit = P.CodProduto ");
                retorno.Append("        AND CodTabelaPreco = (SELECT COALESCE(MIN(CodTabelaPreco),0) FROM TBT_ITEM_TABELA_PRECO ITP2 WHERE ITP2.CodProduto = KIT.CodProduto) ");
                retorno.Append("    ) KIT1) AS PrecoVenda_Kit");
            }

            retorno.Append(@", (SELECT SUM(KIT1.ValorTotal) AS ValorTotal FROM (
		                        SELECT DISTINCT (ITP.Preco) AS ValorTotal     
		                        FROM TBT_PRODUTO_KIT KIT    
		                        INNER JOIN TBT_PRODUTO PROD ON KIT.CodProduto = PROD.CodProduto     
		                        LEFT JOIN TBT_ITEM_TABELA_PRECO ITP ON KIT.CodProduto = ITP.CodProduto     
		                        WHERE KIT.CodProdutoKit = P.CodProduto         
			                        AND CodTabelaPreco = (SELECT COALESCE(MIN(CodTabelaPreco),0) FROM TBT_ITEM_TABELA_PRECO ITP2 WHERE ITP2.CodProduto = KIT.CodProduto)
		                        ) KIT1
	                        ) AS PrecoVenda_Kit_Diferente");

            return retorno.ToString();
        }


        public String Retorna_Where_Permissao_CondicaoPagamento(String numCliente, String nomeCampo)
        {
            StringBuilder retorno = new StringBuilder();

            if (!String.IsNullOrEmpty(numCliente) && !String.IsNullOrEmpty(nomeCampo))
            {
                retorno.Append(String.Format(" AND ( {0} IN (SELECT DISTINCT CodMarca FROM TBT_MARCA_CLIENTE WHERE CodPessoaCLiente = '{1}') ) ", nomeCampo, numCliente));
            }

            return retorno.ToString();
        }

        //public String Retorna_Select_Produto_KIT(String codCarrinho, String codProduto)
        public String Retorna_Select_Produto_KIT(String codCliente, String codProduto)
        {
            String CampoValor_ProdutoKit = Retorna_CampoValor_ClienteEmpresa(TipoDesconto.Desconto, codCliente);
            String CampoValor_ProdutoKit_Acrescimo = Retorna_CampoValor_ClienteEmpresa(TipoDesconto.Acrescimo, codCliente);

            StringBuilder sbSelect = new StringBuilder();
            //            sbSelect.Append("SELECT PK.CodProduto, PK.Quantidade, ITP.Preco ");
            sbSelect.Append("SELECT DISTINCT PK.CodProduto, PK.Quantidade, ITP.Preco ");

            //if (consideraDescricaoProduto)
            //{
            //    sbSelect.Append(", () AS Descricao ");
            //}

            //            sbSelect.Append(CampoValor_ProdutoKit);

            if (!String.IsNullOrEmpty(codCliente))
            {
                sbSelect.Append(CampoValor_ProdutoKit);
                sbSelect.Append(CampoValor_ProdutoKit_Acrescimo);
            }

            sbSelect.Append(" FROM TBT_PRODUTO_KIT PK ");
            sbSelect.Append(" INNER JOIN TBT_PRODUTO P ON PK.CodProduto = P.CodProduto ");
            sbSelect.Append("INNER JOIN TBT_ITEM_TABELA_PRECO ITP ON PK.CodProduto = ITP.CodProduto ");
            sbSelect.Append($"WHERE PK.CodProdutoKit = '{codProduto}' ");

            return sbSelect.ToString();
        }

        public ItemCommandResult RetornaProdutoKit_Tratado(ItemCommandResult item, TBT_ITEM_CARRINHO_KIT oED_Kit, Boolean consideraCalculoQuantidade)
        {
            ItemCommandResult oED_Item = new ItemCommandResult();
            oED_Item.Descricao = oED_Kit.CodProduto;
            oED_Item.QtdTotal = oED_Kit.Quantidade;
            oED_Item.PrecoCusto = Convert.ToDecimal(oED_Kit.Preco);
            oED_Item.ProdutoComposicaoKIT = true;

            oED_Item.Imagem = item.Imagem;
            oED_Item.PercDesc = item.PercDesc;

            Decimal ValorCalculo = Convert.ToDecimal(oED_Kit.Preco);
            if (oED_Kit.PrecoVenda_ClienteEmpresa != null && oED_Kit.PrecoVenda_ClienteEmpresa > 0)
            {
                ValorCalculo = Convert.ToDecimal(oED_Kit.PrecoVenda_ClienteEmpresa);
                oED_Item.PrecoCusto = ValorCalculo;
            }

            if (oED_Item.PercDesc > 0)
            {
                double valor = Convert.ToDouble(ValorCalculo);
                double percentual = Convert.ToDouble(oED_Item.PercDesc) / 100.0;
                double valor_final = valor - (percentual * valor);
                ValorCalculo = Convert.ToDecimal(valor_final);
            }

            oED_Item.PrecoCustoComDesconto = ValorCalculo;
            oED_Item.ValorTotal = (ValorCalculo * oED_Item.QtdTotal) * item.QtdTotal;

            if (consideraCalculoQuantidade)
            {
                if (item.QtdTotal > 0)
                {
                    oED_Item.QtdTotal = oED_Item.QtdTotal * item.QtdTotal;
                }
            }

            return oED_Item;
        }

        public String Retorna_Where_DataDisponibilidade(List<NivelAtributoResult> Lista)
        {
            StringBuilder retorno = new StringBuilder();

            //(strftime('%m', IPE.DataDisponibilidade) = '04') AND (strftime('%Y', IPE.DataDisponibilidade) = '2022')

            if (Lista != null && Lista.Count > 0)
            {
                retorno.Append(" AND ( ");

                Int16 numContador = 0;

                foreach (NivelAtributoResult oED in Lista)
                {
                    numContador++;

                    String[] arrayPeriodo = oED.Codigo.Split('/');
                    retorno.Append(String.Format("(strftime('%m', IPE.DataDisponibilidade) = '{0}') AND (strftime('%Y', IPE.DataDisponibilidade) = '{1}')",
                                   arrayPeriodo[0], arrayPeriodo[1]));

                    if (numContador < Lista.Count)
                        retorno.Append(" OR ");
                }

                retorno.Append(" ) ");
            }

            return retorno.ToString();
        }

    }

    public class FuncaoGenerica_Conexao
    {
        public SQLiteAsyncConnection oSQLiteAsyncConnection = null;

        public async Task<List<TBT_ITEM_CARRINHO_KIT>> RetornaListaProdutoKit(String codCarrinho, String codProduto)
        {
            FuncaoGenerica oFuncaoGenerica = new FuncaoGenerica();
            String retorno = oFuncaoGenerica.Retorna_Select_Produto_KIT(codCarrinho, codProduto);
            oFuncaoGenerica = null;

            return await oSQLiteAsyncConnection.QueryAsync<TBT_ITEM_CARRINHO_KIT>(retorno);
        }

        public async Task<ObservableCollection<ItemCommandResult>> RetornaListaProduto_Tratado_ProdutoKIT(ObservableCollection<ItemCommandResult> listaItem,
                                                                                                          Boolean ConsideraProdutoKit,
                                                                                                          String CodPessoaCliente)
        {
            ObservableCollection<ItemCommandResult> retorno = listaItem;

            ObservableCollection<ItemCommandResult> listaItemTemp = new ObservableCollection<ItemCommandResult>();

            foreach (var item in retorno)
            {
                #region Produtos Composição KIT

                listaItemTemp.Add(item);

                if (ConsideraProdutoKit)
                {
                    item.ListaProdutoKIT = await RetornaListaProdutoKit(CodPessoaCliente, item.CodProduto);

                    if (item.ListaProdutoKIT != null && item.ListaProdutoKIT.Count > 0)
                    {
                        foreach (TBT_ITEM_CARRINHO_KIT oED_Kit in item.ListaProdutoKIT)
                        {
                            Intermidia.Intermidia.Infra.Domain.Repositories.FuncaoGenerica oFuncaoGenerica = new Intermidia.Intermidia.Infra.Domain.Repositories.FuncaoGenerica();
                            ItemCommandResult oED_Item = oFuncaoGenerica.RetornaProdutoKit_Tratado(item, oED_Kit, true);
                            oFuncaoGenerica = null;

                            listaItemTemp.Add(oED_Item);
                        }
                    }
                }

                #endregion Produtos Composição KIT
            }

            if (ConsideraProdutoKit)
            {
                retorno = listaItemTemp;
            }

            return retorno;
        }

    }
}
