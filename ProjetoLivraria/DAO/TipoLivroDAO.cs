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

        internal int InsereTipoLivro(TipoLivro loTipoLivro)
        {
            var liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (loTipoLivro == null) throw new NullReferenceException();
                    ioQuery = new SqlCommand($@"INSERT INTO TIL_TIPO_LIVRO(TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO)
                                            VALUES (@idTipoLivro, @descricaoTipoLivro)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", loTipoLivro.til_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@descricaoTipoLivro", loTipoLivro.til_ds_descricao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return liQtdRegistrosInseridos;
        }

        internal int AtualizaTipoLivro(TipoLivro aoTipoLivro)
        {
            var liQtdRegistrosAtualizados = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (aoTipoLivro == null) throw new NullReferenceException();
                    ioQuery = new SqlCommand(
                        $@"UPDATE TIL_TIPO_LIVRO
                            SET TIL_DS_DESCRICAO = @descricaoTipoLivro
                            WHERE TIL_ID_TIPO_LIVRO = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoTipoLivro.til_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@descricaoTipoLivro", aoTipoLivro.til_ds_descricao));
                    liQtdRegistrosAtualizados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return liQtdRegistrosAtualizados;
        }
    }
}