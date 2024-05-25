using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Projeto_Web_Lh_Pets_versão_1
{
    class Banco
    {
        private List<Clientes> lista = new List<Clientes>();

        public List<Clientes> GetLista()
        {
            return lista;
        }

        public Banco()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    UserID = "sa",
                    Password = "1313",
                    DataSource = "SARAIVA\\SQLEXPRESS",
                    InitialCatalog = "vendas",
                    IntegratedSecurity = false
                };

                using (SqlConnection conexao = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "SELECT * FROM tblclientes";
                    using (SqlCommand comando = new SqlCommand(sql, conexao))
                    {
                        conexao.Open();
                        using (SqlDataReader tabela = comando.ExecuteReader())
                        {
                            while (tabela.Read())
                            {
                                lista.Add(new Clientes()
                                {
                                    cpf_cnpj = tabela["cpf_cnpj"]?.ToString() ?? string.Empty,
                                    nome = tabela["nome"]?.ToString() ?? string.Empty,
                                    endereco = tabela["endereco"]?.ToString() ?? string.Empty,
                                    rg_ie = tabela["rg_ie"]?.ToString() ?? string.Empty,
                                    tipo = tabela["tipo"]?.ToString() ?? string.Empty,
                                    valor = tabela["valor"] != DBNull.Value ? (float)Convert.ToDecimal(tabela["valor"]) : 0,
                                    valor_imposto = tabela["valor_imposto"] != DBNull.Value ? (float)Convert.ToDecimal(tabela["valor_imposto"]) : 0,
                                    total = tabela["total"] != DBNull.Value ? (float)Convert.ToDecimal(tabela["total"]) : 0
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public String GetListaString()
        {
            string enviar = "<!DOCTYPE html>\n<html>\n<head>\n<meta charset='utf-8' />\n" +
                          "<title>Cadastro de Clientes</title>\n</head>\n<body>";
            enviar += "<b>   CPF / CNPJ    -      Nome    -    Endereço    -   RG / IE   -   Tipo  -   Valor   - Valor Imposto -   Total  </b>";

            int i = 0;
            string corfundo = "", cortexto = "";

            foreach (Clientes cli in GetLista())
            {
                if (i % 2 == 0)
                {
                    corfundo = "#6f47ff";
                    cortexto = "white";
                }
                else
                {
                    corfundo = "#ffffff";
                    cortexto = "#6f47ff";
                }
                i++;

                enviar += $"\n<br><div style='background-color:{corfundo};color:{cortexto};'>" +
                          cli.cpf_cnpj + " - " +
                          cli.nome + " - " + cli.endereco + " - " + cli.rg_ie + " - " +
                          cli.tipo + " - " + cli.valor.ToString("C") + " - " +
                          cli.valor_imposto.ToString("C") + " - " + cli.total.ToString("C") + "<br>" +
                          "</div>";
            }
            return enviar;
        }

        public void imprimirListaConsole()
        {
            Console.WriteLine("   CPF / CNPJ   " + " - " + "    Nome   " +
                " - " + "   Endereço   " + " - " + "  RG / IE  " + " - " +
                "  Tipo " + " - " + "  Valor  " + " - " + "Valor Imposto" +
                " - " + "  Total  ");

            foreach (Clientes cli in GetLista())
            {
                Console.WriteLine(cli.cpf_cnpj + " - " +
                cli.nome + " - " + cli.endereco + " - " + cli.rg_ie + " - " +
                cli.tipo + " - " + cli.valor.ToString("C") + " - " +
                cli.valor_imposto.ToString("C") + " - " + cli.total.ToString("C"));
            }
        }
    }
}
