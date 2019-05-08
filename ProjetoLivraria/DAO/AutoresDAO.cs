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
    public class AutoresDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;
        // Posso fazer isso??
        //public static Dictionary<string, string> atributosAutores = new Dictionary<string, string>();

        //public AutoresDAO()
        //{
        //    atributosAutores.Add("id", "AUT_ID_AUTOR");
        //}

        public BindingList<Autores> BuscaAutores(decimal ? adcIdAuthor = null)
        {
            BindingList<Autores> ioListAutores = new BindingList<Autores>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (adcIdAuthor != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES WHERE AUT_ID_AUTOR = @idAutor", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idAutor", adcIdAuthor));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM AUT_AUTORES", ioConexao);
                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Autores loNovoAutor = new Autores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3));
                            ioListAutores.Add(loNovoAutor);
                        }
                        //loReader.Close(); é necessário?
                    }
                    

                }
                catch (Exception)
                {

                    throw new Exception("Erro ao buscar o(s) autor(es).");
                }
            }

            return ioListAutores;
        }

        public int InsereAutor(Autores aoNovoAutor)
        {
            if (aoNovoAutor == null)
                throw new NullReferenceException();

            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // pra fazer isso?
                    // ioQuery = new SqlCommand($"INSERT INTO AUT_AUTORES({atributosAutores["id"]}, AUT_NM_NOME, AUT_NM_SOBRENOME, AUT_DS_EMAIL) VALUES(@idAutor, @nomeAutor, @sobrenomeAutor, @emailAutor)", ioConexao); 
                    ioQuery = new SqlCommand(
                        @"INSERT INTO AUT_AUTORES(AUT_ID_AUTOR, AUT_NM_NOME, AUT_NM_SOBRENOME, AUT_DS_EMAIL) 
                        VALUES(@idAutor, @nomeAutor, @sobrenomeAutor, @emailAutor)",
                        ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoAutor.aut_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeAutor", aoNovoAutor.aut_nm_nome));
                    ioQuery.Parameters.Add(new SqlParameter("@sobrenomeAutor", aoNovoAutor.aut_nm_sobrenome));
                    ioQuery.Parameters.Add(new SqlParameter("@emailAutor", aoNovoAutor.aut_ds_email));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw new Exception("Erro ao cadastrar novo autor.");
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int AtualizaAutor(Autores aoAutor)
        {
            if (aoAutor == null) throw new NullReferenceException();
            int liQtdRegistrosAtualizados = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand(
                            @"UPDATE AUT_AUTORES
                            SET AUT_NM_NOME = @nomeAutor,
                                AUT_NM_SOBRENOME = @sobrenomeAutor,
                                AUT_DS_EMAIL = @emailAutor
                            WHERE AUT_ID_AUTOR = @idAutor",
                        ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoAutor.aut_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeAutor", aoAutor.aut_nm_nome));
                    ioQuery.Parameters.Add(new SqlParameter("@sobrenomeAutor", aoAutor.aut_nm_sobrenome));
                    ioQuery.Parameters.Add(new SqlParameter("@emailAutor", aoAutor.aut_ds_email));
                    liQtdRegistrosAtualizados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw new Exception("Erro ao atualizar o autor.");
                }
            }
            return liQtdRegistrosAtualizados;
        }

        public int DeletaAutor(Autores aoAutor)
        {
            if (aoAutor == null) throw new NullReferenceException();
            int liQtdRegistrosDeletados = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM AUT_AUTORES WHERE AUT_ID_AUTOR = @idAutor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoAutor.aut_id_autor));
                    liQtdRegistrosDeletados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return liQtdRegistrosDeletados;
        }
    }
}