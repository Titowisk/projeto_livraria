using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class LivroAutorDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;
        public int InsereLivroAutor (LivroAutor aoNovoLivroAutor)
        {
            if (aoNovoLivroAutor == null) throw new NullReferenceException();

            int liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // pra fazer isso?
                    // ioQuery = new SqlCommand($"INSERT INTO AUT_AUTORES({atributosAutores["id"]}, AUT_NM_NOME, AUT_NM_SOBRENOME, AUT_DS_EMAIL) VALUES(@idAutor, @nomeAutor, @sobrenomeAutor, @emailAutor)", ioConexao); 
                    ioQuery = new SqlCommand(
                        @"INSERT INTO LIA_LIVRO_AUTOR(LIA_ID_AUTOR, LIA_ID_LIVRO, LIA_PC_ROYALTY) 
                        VALUES(@idAutor, @idLivro, @pcRoyalty)",
                        ioConexao);

                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoLivroAutor.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivroAutor.lia_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@pcRoyalty", aoNovoLivroAutor.lia_pc_royalty));

                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw new Exception("Erro ao cadastrar nova relação de livro com autor.");
                }
            }

            return liQtdRegistrosInseridos;
        }
    }
}