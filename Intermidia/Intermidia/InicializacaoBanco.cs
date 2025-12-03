using Intermidia.Intermidia.Infra;
using Intermidia.Intermidia.Infra.DataContext;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;
using Intermidia.Intermidia.Infra.Domain.Messages;
using SQLite;
using System.Diagnostics;

namespace Intermidia.Intermidia
{
    public class InicializacaoBanco : IInicializacaoBanco
    {
        private readonly SQLiteAsyncConnection _connection;
        public InicializacaoBanco(ISqliteConnection context)
        {
            _connection = context.DbConnectionAsync();
        }
        /// <summary>
        /// Script de inicialização do banco de dados
        /// </summary>
        public async Task<bool> BancoJaExiste()
        {
            int count = await _connection.ExecuteScalarAsync<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'TBT_USUARIO';");
            if (count == 0)
            {
                return false;
            }
            return count > 0;

        }

        /// <summary>
        /// Script de inicialização do banco de dados
        /// Cria as tabelas iniciais do projeto e seus indices.
        /// </summary>
        public async virtual Task Init()
        {
            int rowError = 0;
            string sqlError = string.Empty;
            try
            {
                int count = await _connection.ExecuteScalarAsync<int>("SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'TBT_USUARIO';");
                if (count == 0)
                {
                    // Cria as tabelas que estão no arquivo INIT.
                    await CriarTabelas();

                    var script = ManagerQuery.MakeSql("INDEX", "DDL", null);
                    var statements = script.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var statement in statements)
                    {
                        rowError++;
                        sqlError = statement;
                        await _connection.ExecuteAsync(statement);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no script de inicialização do banco.\nLinha: {rowError} \nScript: {sqlError} \nMessage: {ex.Message}");
            }
        }

        /// <summary>
        /// Script de inserção de dados iniciais da aplicação.
        /// Ex: versão, urls
        /// </summary>
        /// <returns></returns>
        public async virtual Task DataInit()
        {

        }

        /// <summary>
        /// Criar tabelas iniciais do sistema
        /// </summary>
        /// <returns></returns>
        public virtual async Task CriarTabelas()
        {
            int rowError = 0;
            string sqlError = string.Empty;
            try
            {
                var script = ManagerQuery.MakeSql("INIT", "DDL", null);
                var statements = script.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var statement in statements)
                {
                    rowError++;
                    sqlError = statement;
                    await _connection.ExecuteAsync(statement);
                }

                script = ManagerQuery.MakeSql("INIT", "DDL", null);
                statements = script.Replace("TBT_", "SYNC_").Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var statement in statements)
                {
                    rowError++;
                    sqlError = statement;
                    await _connection.ExecuteAsync(statement);
                }


                bool tabelaExiste;

                tabelaExiste = await TabelaExiste("TBT_SINCRONIZACAO_USUARIO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("TBT_SINCRONIZACAO_USUARIO");


                tabelaExiste = await TabelaExiste("TBT_LISTA_GENERICA");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_LISTA_GENERICA");

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no script de criação das tabelas.\nLinha: {rowError} \nScript: {sqlError} \nMessage: {ex.Message}");
            }
        }

        /// <summary>
        /// Scripts de atualizações do banco de dados
        /// </summary>
        public async virtual Task Atualizacoes()
        {
        }

        public async Task AtualizacoesCore()
        {
            try
            {
                bool tabelaExiste, campoExiste, parametroExiste;

                //////////////////////////////
                // CRIAÇÃO DE NOVAS TABELAS //
                //////////////////////////////
                ///


                ///SET URL DE AMBIENTE
                if (Utils.Settings.Ambiente == "T")
                {
                    await ExecutarAlteracoesCore("UPDATE_AFV_PARAMETRO_SINC_TESTE");
                }
                else
                {
                    await ExecutarAlteracoesCore("UPDATE_AFV_PARAMETRO_SINC_PROD");
                }


                tabelaExiste = await TabelaExiste("SYNC_SINCRONIZACAO_USUARIO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("TBT_SINCRONIZACAO_USUARIO");

                tabelaExiste = await TabelaExiste("SYNC_GERENTE_VENDEDOR");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_GERENTE_VENDEDOR");

                tabelaExiste = await TabelaExiste("TBT_KIT");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_KIT");

                tabelaExiste = await TabelaExiste("TBT_FRETE_REGIAO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_FRETE_REGIAO");

                tabelaExiste = await TabelaExiste("TBT_NIVEL_CLIENTE");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_NIVEL_CLIENTE");

                tabelaExiste = await TabelaExiste("TBT_CRITICA_CARRINHO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_CRITICA_CARRINHO");

                tabelaExiste = await TabelaExiste("TBT_PRAZO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_PRAZO");

                tabelaExiste = await TabelaExiste("TBT_PRODUTO_RELACIONADO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_PRODUTO_RELACIONADO");

                tabelaExiste = await TabelaExiste("TBT_TABELA_PRECO_COND_PGTO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_TABELA_PRECO_COND_PGTO");

                tabelaExiste = await TabelaExiste("TBT_TIPO_FEEDBACK");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_TIPO_FEEDBACK");

                tabelaExiste = await TabelaExiste("TBT_FEEDBACK_CLIENTE");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_FEEDBACK_CLIENTE");

                tabelaExiste = await TabelaExiste("TBT_IDIOMA");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_IDIOMA");


                tabelaExiste = await TabelaExiste("TBT_REPORT_GRUPO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_REPORT_GRUPO");

                tabelaExiste = await TabelaExiste("TBT_META");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_META");

                tabelaExiste = await TabelaExiste("TBT_PARAMETRO_MARCA");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_PARAMETRO_MARCA");

                /////////////////////////////
                // CRIAÇÃO DE NOVOS CAMPOS //
                /////////////////////////////

                campoExiste = await CampoExiste("IndTecnologia", "TBT_NIVEL");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTERACAO_TBT_NIVEL");

                //Campo IndBloqueado será para indicar qual o carrinho o representante está trabalhando no momento.
                campoExiste = await CampoExiste("IndBloqueado", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_CARRINHO_ADD_INDBLOQUEADO");

                campoExiste = await CampoExiste("CupomChave", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTERACAO_TBT_CARRINHO");

                campoExiste = await CampoExiste("CorPrimaria", "TBT_MARCA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_MARCA_ADD_CORES");

                campoExiste = await CampoExiste("TipoVenda", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_CLIENTE_ADD_TIPOVENDA");

                campoExiste = await CampoExiste("DefineDesconto", "TBT_NIVEL");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_NIVEL_ADD_DEFINEDESCONTO");

                campoExiste = await CampoExiste("DefinePreco", "TBT_NIVEL");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_NIVEL_ADD_DEFINEPRECO");

                campoExiste = await CampoEPK("CodPessoaCliente", "TBT_NIVEL_DESCONTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_NIVEL_DESCONTO");

                campoExiste = await CampoExiste("TipoVenda", "TBT_ATENDIMENTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ATENDIMENTO_ADD_TIPOVENDA");

                campoExiste = await CampoExiste("RecarregaCatalogo", "TBT_NIVEL");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_NIVEL_ADD_RECARREGACATALOGO");

                campoExiste = await CampoExiste("Html", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PRODUTO_ADD_HTML");

                campoExiste = await CampoExiste("IndKit", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PRODUTO_ADD_INDKIT");

                campoExiste = await CampoExiste("ConfiguracaoAtendimento", "TBT_ATENDIMENTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ATENDIMENTO_ADD_CONFIGURACAOATENDIMENTO");

                campoExiste = await CampoExiste("QtdMultipla", "TBT_GRADE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_GRADE_ADD_QTDMULTIPLA");

                campoExiste = await CampoExiste("QtdMultipla", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PRODUTO_ADD_QTDMULTIPLA");

                campoExiste = await CampoExiste("CodPedidoDestino", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_CARRINHO_ADD_CODPEDIDODESTINO");

                campoExiste = await CampoExiste("ID", "SYNC_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_SYNC_CARRINHO_ADD_ID");

                campoExiste = await CampoExiste("IndDetalheMaterial", "TBT_NIVEL");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_NIVEL_ADD_INDDETALHEMATERIAL");

                campoExiste = await CampoExiste("QtdMultiplaEncaixotamento", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PRODUTO_ADD_QTDMULTIPLAENCAIXOTAMENTO");

                campoExiste = await CampoExiste("Cor", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PRODUTO_ADD_COR");

                /*campoExiste = await CampoExiste("CodProdutoSku", "SYNC_GRADE_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoes("Add_Produto_Sku");*/

                campoExiste = await CampoExiste("CodKit", "TBT_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_CARRINHO_ADD_CODKIT");

                campoExiste = await CampoExiste("CodItemProntaEntrega", "TBT_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_CARRINHO_ADD_CODITEMPRONTAENTREGA");

                //Este script é uma correção na ordem das coluna, visto que a sincronização insere conforme a ordem de cada campo.
                campoExiste = await CampoExisteNaOrdemCorreta("QtdMultipla", "SYNC_GRADE", "6");
                if (!campoExiste)
                    await RecriarTabelaSync("TBT_GRADE");


                campoExiste = await CampoExiste("CodBarra", "TBT_ITEM_PRONTA_ENTREGA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("CREATE_CODBARRAS_IPE");

                campoExiste = await CampoExiste("CodBarra", "SYNC_ITEM_PRONTA_ENTREGA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("CREATE_CODBARRAS_SYNC_IPE");

                //Adicionado na tabela de marca a tabela padrão, que deve ser utilizada em caso de não ter atendimento aberto.
                campoExiste = await CampoExiste("CodTabelaPreco", "TBT_MARCA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_MARCA_ADD_TABELA_PRECO");


                campoExiste = await CampoExiste("CodTabelaPreco", "TBT_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_CARRINHO_ADD_CODTABELAPRECO");


                //#22789
                //Adicionado na tabela de marca o campo 'ConsultaSoComAtendimento', 
                //que deve ser utilizada para verificar se a marca precisa de atendimento aberto para consultar catalogo.
                //Isso é necessário para que não seja exibido o catalogo completo para clientes segmentados.
                campoExiste = await CampoExiste("ConsultaSoComAtendimento", "TBT_MARCA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_MARCA_ADD_CONSULTASOCOMATENDIMENTO");

                campoExiste = await CampoExiste("DataEstoque", "TBT_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_CARRINHO_ADD_DATAESTOQUE");


                campoExiste = await CampoExiste("CodProdutoRelacionado", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PRODUTO_ADD_CODPRODUTORELACIONADO");

                campoExiste = await CampoExiste("CodProdutoRelacionado", "SYNC_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_SYNC_PRODUTO_ADD_CODPRODUTORELACIONADO");

                campoExiste = await CampoExiste("Tipo", "TBT_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_CARRINHO_ADD_TIPO");

                campoExiste = await CampoExiste("QtdNaCaixa", "TBT_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_CARRINHO_ADD_QTDNACAIXA");

                campoExiste = await CampoExiste("QtdNaGrade", "TBT_GRADE_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_GRADE_ITEM_CARRINHO_ADD_QTDNAGRADE");

                campoExiste = await CampoExiste("QtdEmbalada", "TBT_GRADE_ITEM_PEDIDO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_GRADE_ITEM_PEDIDO_ADD_QTDSNAGRADE");

                // Este campo foi adicionado no item, porque em alguns casos o tipoGrade da tabela de grade é quebrado em mais tipos
                // Exemplo: TipoGrade = 'A', mas na inclusão este 'A' pode ser 'M' (Musical) ou 'S' (Solida)
                // E para edição do item esta informação é de extrema importancia.
                campoExiste = await CampoExiste("TipoGradeIncluida", "TBT_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_CARRINHO_ADD_TIPOGRADEINCLUIDA");

                //Esta tabela foi adicionada para salvar listas genericas para poder preencher os combos 
                tabelaExiste = await TabelaExiste("TBT_LISTA_GENERICA");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_LISTA_GENERICA");

                campoExiste = await CampoExiste("PercentualDescontoPermitido", "TBT_MARCA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_MARCA_ADD_PERCENTUALDESCONTOPERMITIDO");

                campoExiste = await CampoExiste("PercentualDescontoPermitido", "SYNC_MARCA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_MARCA_ADD_PERCENTUALDESCONTOPERMITIDO_SYNC");

                campoExiste = await CampoExiste("PercentualComissaoRep", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_CARRINHO_ADD_PERCCOMISSAOREP");

                campoExiste = await CampoExiste("PercentualComissaoRep2", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_CARRINHO_ADD_PERCCOMISSAOREP2");

                campoExiste = await CampoExiste("Observacoes1", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_CARRINHO_ADD_OBSERVACAO1");

                campoExiste = await CampoExiste("UsaDescontoItem", "TBT_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_CARRINHO_ADD_USADESCONTOITEM");

                campoExiste = await CampoExiste("FiltroCatalogoSortido", "TBT_NIVEL");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_NIVEL_ADD_FILTROCATALOGOSORTIDO");

                tabelaExiste = await TabelaExiste("TBT_ITEM_SORTIDO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_ITEM_SORTIDO");


                campoExiste = await CampoExiste("PercentualDesconto1", "TBT_ATENDIMENTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_ATENDIMENTO_ADD_DESCONTO");

                campoExiste = await CampoExiste("ValorMinimoFrete", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_CLIENTE_ADD_MINIMOFRETE");

                campoExiste = await CampoExiste("PercDescFrete", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_CLIENTE_CAMPOS_DESCONTO_FRETE");


                tabelaExiste = await TabelaExiste("TBT_FOTO_RELATORIO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_FOTO_RELATORIO");

                campoExiste = await CampoExiste("Imagem", "TBT_FOTO_RELATORIO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_FOTO_RELATORIO_ADD_IMAGEM");

                #region "Cores da marca"
                //Quebrei os campos de cores da tabela tbt_marca, porque por algum motivo não criou todos os campos.
                campoExiste = await CampoExiste("CorButtonNiveis", "TBT_MARCA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_MARCA_ADD_CORBUTTONNIVEIS");

                campoExiste = await CampoExiste("CorFontButtonNiveis", "TBT_MARCA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_MARCA_ADD_CORFONTBUTTONNIVEIS");

                campoExiste = await CampoExiste("CorPrimaria", "TBT_MARCA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_MARCA_ADD_CORPRIMARIA");

                campoExiste = await CampoExiste("CorDestaque", "TBT_MARCA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_MARCA_ADD_CORDESTAQUE");
                /// Fim script de cores da marca
                #endregion

                #region "Campos TBT_ITEM_PRONTA_ENTREGA"
                campoExiste = await CampoExiste("Cor", "TBT_ITEM_PRONTA_ENTREGA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_PRONTA_ENTREGA_ADD_COR");

                campoExiste = await CampoExiste("Site", "TBT_ITEM_PRONTA_ENTREGA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_PRONTA_ENTREGA_ADD_SITE");

                campoExiste = await CampoExiste("Configuracao", "TBT_ITEM_PRONTA_ENTREGA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_PRONTA_ENTREGA_ADD_CONFIGURACAO");

                campoExiste = await CampoExiste("Localizacao", "TBT_ITEM_PRONTA_ENTREGA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_PRONTA_ENTREGA_ADD_LOCALIZACAO");

                campoExiste = await CampoExiste("NumeroItem", "TBT_ITEM_PRONTA_ENTREGA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_PRONTA_ENTREGA_ADD_NUMEROITEM");
                #endregion

                campoExiste = await CampoExiste("IndGeraCatalogo", "TBT_NIVEL");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_NIVEL_ADD_INDGERACATALOGO");

                campoExiste = await CampoExiste("IndObrigatorioGeraCatalogo", "TBT_NIVEL");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_NIVEL_ADD_INDOBRIGATORIOGERACATALOGO");

                campoExiste = await CampoExiste("IndPermiteMultiploGeraCatalogo", "TBT_NIVEL");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_NIVEL_ADD_INDPERMITEMULTIPLOGERACATALOGO");

                campoExiste = await CampoExiste("OrdemGeraCatalogo", "TBT_NIVEL");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_NIVEL_ADD_ORDEMGERACATALOGO");

                //Esta tabela serve para excluir as referencias que serao geradas 
                //No catalogo, chamado #23495...
                tabelaExiste = await TabelaExiste("TBT_PRODUTO_CATALOGO_EXCLUSAO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_PRODUTO_CATALOGO_EXCLUSAO");

                campoExiste = await CampoExiste("ComposicaoCabedal", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PRODUTO_ADD_COMPOSICAOCABEDAL");

                campoExiste = await CampoExiste("ComposicaoSolado", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PRODUTO_ADD_COMPOSICAOSOLADO");

                campoExiste = await CampoExiste("DescricaoReduzida", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PRODUTO_ADD_DESCRICAOREDUZIDA");

                campoExiste = await CampoExiste("Tipo", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PRODUTO_ADD_TIPO");

                campoExiste = await CampoExiste("IndSeq", "TBT_REPORT_GRUPO_ITEM");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_REPORT_GRUPO_ITEM");

                campoExiste = await CampoExiste("Referencia", "TBT_REPORT_GRUPO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_REPORT_GRUPO_REFERENCIA");

                campoExiste = await CampoExiste("Status", "TBT_FOTO_RELATORIO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_FOTO_RELATORIO_STATUS");

                campoExiste = await CampoExiste("OrdGenero", "TBT_REPORT_GRUPO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_REPORT_GRUPO_ORDGENERO");

                campoExiste = await CampoExiste("TipoProduto", "TBT_REPORT_GRUPO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_REPORT_GRUPO _TIPOPRODUTO");

                campoExiste = await CampoExiste("IndPrivateLabel", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_INDPRIVATELABEL_CARRINHO");

                campoExiste = await CampoExiste("IndPrivateLabel", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_INDPRIVATELABEL_PRODUTO");

                campoExiste = await CampoExiste("IndPrivateLabel", "TBT_ATENDIMENTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_INDPRIVATELABEL_ATENDIMENTO");

                campoExiste = await CampoExiste("Comissao", "TBT_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_COMISSAO_ITEM_CARRINHO");

                campoExiste = await CampoExiste("IndTamanhoGrande", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PRODUTO_ADD_INDTAMANHOGRADE");

                campoExiste = await CampoExiste("CodigoSegmento", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_SEGMENTO_TBT_CLIENTE");


                tabelaExiste = await TabelaExiste("TBT_VALOR_MINIMO_REGIAO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_VALOR_MINIMO_REGIAO");


                campoExiste = await CampoExiste("QtdDevolvido", "TBT_GRADE_ITEM_PEDIDO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_GRADE_PEDIDO_QTDDEVOLVIDO");

                campoExiste = await CampoExiste("Fatorkit", "TBT_ITEM_PEDIDO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_PEDIDO_PEDIDO_MAE");

                tabelaExiste = await TabelaExiste("TBT_CLIENTE_MARCA");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TABLE_CLIENTE_MARCA");

                campoExiste = await CampoExiste("IndAlertaPisCofins", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_INDICATIVOS_CLIENTE");

                campoExiste = await CampoExiste("IndCredenciamento", "TBT_MARCA_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_IndCredenciamento_MARCA_CLIENTE");


                tabelaExiste = await TabelaExiste("TBT_REPRESENTANTE_MARCA");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TABLE_REPRESENTANTE_MARCA");

                tabelaExiste = await TabelaExiste("TBT_CANAL_CLIENTE_PRODUTO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_CANAL_CLIENTE_PRODUTO");

                campoExiste = await CampoExiste("CodCanal", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_CODCANAL_CLIENTE");

                campoExiste = await CampoExiste("CodCanal", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_CODCANAL_PRODUTO");

                campoExiste = await CampoExiste("ValidaExists", "TBT_NIVEL");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_VALIDA_EXISTS");

                campoExiste = await CampoExiste("CodSubMarca", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_CODSUBMARCA_PRODUTO");

                campoExiste = await CampoExiste("UsaMarcaExcecao", "TBT_PESSOA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_USAMARCAEXC_PESSOA");

                campoExiste = await CampoExiste("CodSubMarca", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_SUBMARCA_CARRINHO");

                campoExiste = await CampoExiste("PrecoCampanha", "TBT_ITEM_TABELA_PRECO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_PRECOCAMPANHA_ITP");

                campoExiste = await CampoExiste("IndPromocional", "TBT_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_INDPROMOCIONAL_ITEM_CAR");

                campoExiste = await CampoExiste("DescontoClasse", "TBT_MARCA_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_DESCONTO_CLIENTE");

                tabelaExiste = await TabelaExiste("TBT_DESCONTO_FAIXA");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_DESCONTO_FAIXA");

                tabelaExiste = await TabelaExiste("TBT_MARCA_CONDICAO_PAGAMENTO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_MARCA_COND_PGTO");

                campoExiste = await CampoExiste("QtdMinima", "TBT_MARCA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_QtdMinima_MARCA");

                tabelaExiste = await TabelaExiste("TBT_DEPOSITO_TIPO_PEDIDO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_DEPOSITO_TIPO_PEDIDO");

                campoExiste = await CampoExiste("IndPermiteReserva", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_CLIENTE_PERMITERESERVA");

                campoExiste = await CampoExiste("CodTipoPedido", "TBT_ATENDIMENTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_TIPOPEDIDO_ATENDIMENTO");

                tabelaExiste = await TabelaExiste("TBT_PRAZO_FAIXA");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_PRAZO_FAIXA");


                campoExiste = await CampoExiste("Tipo", "TBT_GERENTE_VENDEDOR");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_TIPO_GER_REP");

                campoExiste = await CampoExiste("EmailNFE", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_EMAILNFE_CARRINHO");


                campoExiste = await CampoExiste("Tipo", "TBT_PESSOA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_TIPO_PESSOA");

                campoExiste = await CampoExiste("EmailBloqueioFinanceiro", "TBT_MARCA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_EMAIL_FINANC_MARCA");

                tabelaExiste = await TabelaExiste("TBT_DEPOSITO_QUEBRA_CARRINHO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_DEPOSITO_QUEBRA_CARRINHO");

                campoExiste = await CampoExiste("InscricaoMunicipal", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("UPDATE_CAMPOS_CLIENTE");

                campoExiste = await CampoExiste("ContatoFinanceiro", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_CAMPOS_CLIENTE_CONTATO");

                campoExiste = await CampoExiste("NomeContatoFinanceiro", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_CAMPOS_CLIENTE_CONTATO_FINAN");

                campoExiste = await CampoExiste("Pendente", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_PENDENTE_CARRINHO");

                campoExiste = await CampoExiste("OrdemCompra", "TBT_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_ORDEM_COMPRA_ITEM_CARRINHO");

                campoExiste = await CampoExiste("IndPossuiRelacionamento", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_INDPOSSUIRELACIO_CLIENTE");

                campoExiste = await CampoExiste("CodGrupoCliente", "TBT_ATENDIMENTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_GRUPOCLIENTE_ATENDIMENTO");

                campoExiste = await CampoExiste("CodCarrinhosAgrupados", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_CARRINHOS_AGRUPADOS");

                tabelaExiste = await TabelaExiste("TBT_CLIENTE_CNPJ");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_CLIENTE_CNPJ");

                campoExiste = await CampoExiste("MaisFiltro", "TBT_NIVEL");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_MAIS_FILTRO");

                campoExiste = await CampoExiste("AprovaDescontoFaixa", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_CAMPOS_VALIDACAO_CARRINHO");

                campoExiste = await CampoExiste("TipoPessoaDesconto", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_FLAG_ALCADA_CARRINHO");

                campoExiste = await CampoExiste("NCM", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PRODUTO_ADD_NCM");

                campoExiste = await CampoExiste("ValorTotalOriginal", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_CARRINHO_ADD_VALORTOTALORIGINAL");

                campoExiste = await CampoExiste("PrazoMedioOriginal", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_CARRINHO_ADD_PRAZOMEDIOORIGINAL");

                tabelaExiste = await TabelaExiste("TBT_PRODUTO_FOTO_ID");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_PRODUTO_FOTO_ID");

                campoExiste = await CampoExiste("IndBestSeller", "TBT_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_INDICATIVOS_PRODUTOS");

                campoExiste = await CampoExiste("FoneBloqFinanceiro", "TBT_MARCA");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_FONE_FINANCEIRO");

                campoExiste = await CampoExiste("TipoDocumento", "TBT_TITULO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_TITULO_AND_TMP_TITULO");

                campoExiste = await CampoExiste("CodGrupoCliente", "SYNC_TITULO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_TITULO_CodGrupoCliente");

                campoExiste = await CampoExiste("UrlVideo", "SYNC_PRODUTO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_URLVideo_TBT_PRODUTO");

                campoExiste = await CampoExiste("NomeContatoFinanceiro", "TBT_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_FINC_CLIEN_CARRINHO");

                campoExiste = await CampoExiste("IndFeira", "TBT_USUARIO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_CAMPOS_USUARIO_FEIRA");

                campoExiste = await CampoExiste("NomeSocio", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_CLIENTE_ADD_NOVOSCAMPOS_CADASTRO");

                campoExiste = await CampoExiste("EnderecoAlteradoRF", "TBT_CLIENTE");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_CARRINHO_ADD_EnderecoAlteradoRF");

                tabelaExiste = await TabelaExiste("TBT_CLIENTE_ALCADA");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_CLIENTE_ALCADA");

                campoExiste = await CampoExiste("NroParcela", "TBT_TITULO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ADD_NRO_PARCELA_TITULO");

                tabelaExiste = await TabelaExiste("TBT_TIPO_SUPFRMPGTO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("TBT_TIPO_SUPFRMPGTO");

                tabelaExiste = await TabelaExiste("CREATE_VIEW_PROMOCIONAL");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_VIEW_PROMOCIONAL");

                tabelaExiste = await TabelaExiste("TBT_PROMOCAO");
                if (!tabelaExiste)
                    await ExecutarAlteracoesCore("CREATE_TBT_PROMOCAO");

                //campoExiste = await CampoExiste("PromocaoId", "TBT_CARRINHO");
                //if (!campoExiste)
                //    await ExecutarAlteracoesCore("ALTER_TBT_CARRINHO_ADD_PromocaoId");

                campoExiste = await CampoExiste("PromocaoId", "TBT_ITEM_CARRINHO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_ITEM_CARRINHO_ADD_PromocaoId");

                campoExiste = await CampoExiste("TextoTrade", "TBT_PROMOCAO");
                if (!campoExiste)
                    await ExecutarAlteracoesCore("ALTER_TBT_PROMOCAO_ADD_TextoTrade");

                await ExecutarAlteracoesCore("CREATE_INDEX");
                await ExecutarAlteracoesCore("ANALYZE");

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no script de atualizações do banco.\n\nMessage: {ex.Message}");
            }
        }


        /// <summary>
        /// Verifica no sqlite_master se um determinado campo existe em um tabela
        /// </summary>
        /// <returns><c>true</c>, if existe was campoed, <c>false</c> otherwise.</returns>
        /// <param name="campo">Campo.</param>
        /// <param name="tabela">Tabela.</param>
        public async Task<bool> CampoExiste(string campo, string tabela)
        {
            try
            {
                var camposItem = (await _connection.QueryAsync<SqliteTableInfoCommandResult>($"PRAGMA table_info({tabela});")).Select(x => x.name).ToList();
                return camposItem.Contains(campo);

            }
            catch (Exception e)
            {
                return true;
            }
        }

        public async Task<int> ExisteChaves(string tabela)
        {
            var camposItem = (await _connection.QueryAsync<SqliteTableInfoCommandResult>($"PRAGMA table_info({tabela});")).Select(x => x.pk).ToList();
            int existe = 0;
            foreach (int key in camposItem)
            {
                if (key != 0)
                {
                    existe += 1;
                }
            }
            return existe;
        }


        /// <summary>
        /// Verifica no sqlite_master se um determinado campo existe em um tabela
        /// </summary>
        /// <returns><c>true</c>, if existe was campo, <c>false</c> otherwise.</returns>
        /// <param name="campo">Campo.</param>
        /// <param name="tabela">Tabela.</param>
        /// <param name="columnId">columnId.</param>
        public async Task<bool> CampoExisteNaOrdemCorreta(string campo, string tabela, string columnId)
        {
            var camposItem = (await _connection.QueryAsync<SqliteTableInfoCommandResult>($"PRAGMA table_info({tabela});")).Where(x => x.name == campo && x.cid == columnId).FirstOrDefault();
            return camposItem != null;
        }

        /// <summary>
        /// Verifica se tem o parametro na tabela AFV_PARAMETRO_SINCRONIZACAO
        /// </summary>
        /// <param name="codParametro"></param>
        /// <returns></returns>
        public async Task<bool> ParametroSincrinizacaoExiste(string codParametro)
        {
            int count = await _connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM AFV_PARAMETRO_SINCRONIZACAO WHERE CodParametro = '{codParametro}';");
            if (count != 0)
                return true;

            return false;
        }

        /// <summary>
        /// Verifica no sqlite_master se uma determinada tabela existe.
        /// </summary>
        /// <returns><c>true</c>, if existe was tabelaed, <c>false</c> otherwise.</returns>
        /// <param name="tabela">Tabela.</param>
        public async Task<bool> TabelaExiste(string tabela)
        {
            int count = await _connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = '{tabela}';");
            if (count != 0)
                return true;

            return false;
        }

        public async Task<bool> ViewExiste(string tabela)
        {
            int count = await _connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM sqlite_master WHERE type = 'view' AND name = '{tabela}';");
            if (count != 0)
                return true;

            return false;
        }

        /// <summary>
        /// Executa o script passado por parametro.
        /// </summary>
        /// <param name="scriptName">Script name.</param>
        public async virtual Task ExecutarAlteracoes(string scriptName)
        {
            int rowError = 0;
            string sqlError = string.Empty;
            string script = string.Empty;
            string[] statements;

            try
            {

                script = ManagerQuery.MakeSql(scriptName, "ALTERACOES", null);
                statements = script.Trim().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var statement in statements)
                {
                    try
                    {
                        rowError++;
                        sqlError = statement;
                        await _connection.ExecuteAsync(statement);
                    }
                    catch (SQLiteException sqliteException)
                    {
                        if (sqliteException.Result == SQLite3.Result.Constraint)
                        {
                            Debug.WriteLine("SQLITE EXCEPTION EX: " + sqliteException.Message + " QUERY: " + sqlError);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro no script de inicialização do banco");
                Debug.WriteLine($"{rowError}");
                Debug.WriteLine($"{sqlError}");
                Debug.WriteLine($"{ex.Message}");
                throw ex;
            }

        }

        public async Task DeleteClientesIndPedidoBloqueado()
        {
            try
            {
                var indBloqueados = await _connection.QueryAsync<TBT_CLIENTE>("SELECT * FROM TBT_CLIENTE WHERE IndBloqueioPedido = 1");

                if (indBloqueados.Any())
                    await _connection.ExecuteAsync("DELETE FROM TBT_CLIENTE WHERE IndBloqueioPedido = 1");
            }
            catch (Exception ex)
            {
                //Debug.WriteLine("Erro no script de inicialização do banco: 'DeleteClientesIndPedidoBloqueado' TBT_CLIENTE");
                //Debug.WriteLine($"{ex.Message}");
                //throw ex;
            }
        }
        public async Task ExecutarAlteracoesCore(string scriptName)
        {
            int rowError = 0;
            string sqlError = string.Empty;
            string script = string.Empty;
            string[] statements;

            try
            {

                script = ManagerQuery.MakeSql(scriptName, "ALTERACOES", null);
                statements = script.Trim().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var statement in statements)
                {
                    try
                    {
                        rowError++;
                        sqlError = statement;
                        await _connection.ExecuteAsync(statement);
                    }
                    catch (SQLiteException sqliteException)
                    {
                        if (sqliteException.Result == SQLite3.Result.Constraint)
                        {
                            Debug.WriteLine("SQLITE EXCEPTION EX: " + sqliteException.Message + " QUERY: " + sqlError);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro no script de inicialização do banco");
                Debug.WriteLine($"{rowError}");
                Debug.WriteLine($"{sqlError}");
                Debug.WriteLine($"{ex.Message}");
                throw ex;
            }

        }


        public async Task RecriarTabelaSync(string nomeTabela)
        {
            try
            {
                string nomeTabelaSync = nomeTabela.Replace("TBT_", "SYNC_");
                string script = await BuscarScriptDaTabela(nomeTabela);
                script = script.Replace(nomeTabela, nomeTabelaSync);
                await DropTabela(nomeTabelaSync);
                await _connection.ExecuteAsync(script);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro no script de inicialização do banco: 'RecriarTabelaSync' {nomeTabela}");
                Debug.WriteLine($"{ex.Message}");
                throw ex;
            }

        }

        public async Task DropTabela(string nomeTabela)
        {
            try
            {
                await _connection.ExecuteAsync($"DROP TABLE IF EXISTS {nomeTabela}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro no script de inicialização do banco: 'RecriarTabelaSync' {nomeTabela}");
                Debug.WriteLine($"{ex.Message}");
                throw ex;
            }

        }

        public async Task LimparFotos()
        {
            await ExecutarAlteracoesCore("LIMPAR_FOTOS");
        }

        public async Task<string> BuscarScriptDaTabela(string nomeTabela)
        {
            string sql = $"SELECT sql FROM sqlite_master sm WHERE sm.tbl_name = '{nomeTabela}' AND type = 'table'";
            var script = await _connection.ExecuteScalarAsync<string>(sql);
            return script;
        }

        public async Task LimparDados()
        {
            string sql = "SELECT tbl_name as NomeTabela FROM sqlite_master sm WHERE sm.tbl_name LIKE 'TBT_%' AND sm.tbl_name NOT LIKE '%FOTO%' AND type = 'table'";
            var tabelas = await _connection.QueryAsync<Table>(sql);
            foreach (var tabela in tabelas)
            {
                string statement = $"DELETE FROM {tabela.NomeTabela}";
                await _connection.ExecuteAsync(statement);
            }
            await LimparSinc();
        }

        //método para zerar a data da AFV_PARAMETRO_SINCRONIZACAO ao limpar dados. Sem isso, ao limpar dados fica preso em loop na sincronizacao
        //porque tabelas genéricas não são baixadas novamente até serem alteradas
        public async Task LimparSinc()
        {
            string sql = "SELECT tbl_name as NomeTabela FROM sqlite_master sm WHERE sm.tbl_name = 'AFV_PARAMETRO_SINCRONIZACAO'";
            var tabelas = await _connection.QueryAsync<Table>(sql);
            foreach (var tabela in tabelas)
            {
                string statement = $"UPDATE {tabela.NomeTabela} SET Valor = '2000-01-01T00:00:00' WHERE CodParametro = 'DATAULTIMAATUALIZACAO'";
                await _connection.ExecuteAsync(statement);
            }
        }

        public async Task<bool> CampoEPK(string campo, string tabela)
        {
            var camposItem = (await _connection.QueryAsync<SqliteTableInfoCommandResult>($"PRAGMA table_info({tabela});")).ToList();
            return camposItem.Any(x => x.name == campo && x.pk > 0);
        }

        public void Dispose()
        {
        }
    }
}
