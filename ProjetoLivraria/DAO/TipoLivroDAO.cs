using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using ProjetoLivraria.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace ProjetoLivraria.DAO
{
    public class TipoLivroDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;
        internal BindingList<TipoLivro> BuscaCategorias(decimal? adcIdTipoLivro = null)
        {
            var ioListTipoLivro = new BindingList<TipoLivro>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (adcIdTipoLivro != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", adcIdTipoLivro));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO", ioConexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            ioListTipoLivro.Add(new TipoLivro(loReader.GetDecimal(0), loReader.GetString(1)));
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return ioListTipoLivro;
        }
    }
}