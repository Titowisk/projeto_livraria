using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class LivrosDAO
    {
        private string _id = "LIV_ID_LIVRO"; // decimal
        private string _id_tipo_livro = "LIV_ID_TIPO_LIVRO"; // decimal
        private string _id_editor = "LIV_ID_EDITOR"; // decimal
        private string _titulo = "LIV_NM_TITULO"; // string
        private string _preco = "LIV_VL_PRECO"; // decimal
        private string _royalty = "LIV_PC_ROYALTY"; // decimal
        private string _resumo = "LIV_DS_RESUMO"; // string
        private string _edicao = "LIV_NU_EDICAO"; // int
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        // select
        public BindingList<Livros> BuscaLivros (decimal ? adcIdLivro = null)
        {
            var ioListLivros = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if ( adcIdLivro != null)
                    {
                        ioQuery = new SqlCommand($"SELECT * FROM LIV_LIVROS WHERE {_id} = @idLivros", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idLivros", adcIdLivro));

                    }
                    else
                    {
                        ioQuery = new SqlCommand($"SELECT * FROM LIV_LIVROS", ioConexao);
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            ioListLivros.Add(new Livros(
                                loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2),
                                loReader.GetString(3), loReader.GetDecimal(4), loReader.GetDecimal(5),
                                loReader.GetString(6), loReader.GetInt32(7)
                                ));
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return ioListLivros;
            }
            
        }

        public BindingList<Livros> FindLivrosByAutor(Autores ioAutor)
        {
            var ioListLivros = new BindingList<Livros>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                ioConexao.Open();
                try
                {
                    if (ioAutor == null)
                    {
                        throw new ArgumentException("Autor para busca não foi informado");
                    }
                    else
                    {
                        ioQuery = new SqlCommand(
                            @"SELECT * FROM LIV_LIVROS
                                INNER JOIN LIA_LIVRO_AUTOR ON LIV_ID_LIVRO = LIA_ID_LIVRO
                                INNER JOIN AUT_AUTORES ON AUT_ID_AUTOR = LIA_ID_AUTOR
                                WHERE AUT_ID_AUTOR = @idAutor", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idAutor", ioAutor.aut_id_autor));
                    }
                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            ioListLivros.Add(new Livros(
                                loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2),
                                loReader.GetString(3), loReader.GetDecimal(4), loReader.GetDecimal(5),
                                loReader.GetString(6), loReader.GetInt32(7)
                                ));
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return ioListLivros;

        }

        // insert
        public int InsereLivro (Livros aoNovoLivro)
        {
            var liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (aoNovoLivro == null) throw new NullReferenceException();
                    ioQuery = new SqlCommand($@"INSERT INTO LIV_LIVROS ({_id}, {_id_tipo_livro}, {_id_editor}, {_titulo},
                                            {_preco}, {_royalty}, {_resumo}, {_edicao})
                                            VALUES (@idLivro, @idTipoLivro, @idEditor, @tituloLivro, @precoLivro, @royaltyLivro, @resumoLivro, @edicaoLivro)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoLivro.liv_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoLivro.liv_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@tituloLivro", aoNovoLivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@precoLivro", aoNovoLivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royaltyLivro", aoNovoLivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumoLivro", aoNovoLivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@edicaoLivro", aoNovoLivro.liv_nu_edicao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();


                }
                catch (Exception)
                {

                    throw new Exception("Não foi possível enserir livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }

        // update
        public int AtualizaLivro(Livros aoLivro)
        {
            var liQtdRegistrosAtualizados = 0;
            if (aoLivro == null) throw new NullReferenceException();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    
                    ioQuery = new SqlCommand($@"UPDATE LIV_LIVROS 
                                            SET @idTipoLivro = {_id_tipo_livro}, @idEditor = {_id_editor}, @tituloLivro = {_titulo}, 
                                            @precoLivro = {_preco}, @royaltyLivro = {_royalty}, @resumoLivro = {_resumo}, 
                                            @edicaoLivro = {_edicao}
                                            WHERE @idLivro = {_id}");
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoLivro.liv_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoLivro.liv_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@tituloLivro", aoLivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@precoLivro", aoLivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royaltyLivro", aoLivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumoLivro", aoLivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@edicaoLivro", aoLivro.liv_nu_edicao));
                    liQtdRegistrosAtualizados = ioQuery.ExecuteNonQuery();


                }
                catch (Exception)
                {

                    throw new Exception("Não foi possível inserir livro.");
                }
            }
            return liQtdRegistrosAtualizados;
        }
        // delete
        public int DeletaLivro(Livros aoLivro)
        {
            var liQtdRegistrosDeletados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStrings"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand($@"DELETE FROM LIV_LIVROS WHERE {_id} = @idLivro");
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivro.liv_id_livro));
                    liQtdRegistrosDeletados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw new Exception("Não foi possível deletar livro");
                }
            }
            return liQtdRegistrosDeletados;
        }
    }
}