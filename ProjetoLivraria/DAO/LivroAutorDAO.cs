﻿using ProjetoLivraria.Models;
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

        public int AtualizaLivroAutor(LivroAutor aoLivroAutor)
        {
            int liQtdeRegistrosAtualizados = 0;
            if (aoLivroAutor == null) throw new NullReferenceException();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    // como o autor é trocado a partir do livro
                    // então o id do livro serve para definir qual livro terá sua relação
                    // com autor trocada
                    ioQuery = new SqlCommand(
                        @"UPDATE LIA_LIVRO_AUTOR
                            SET LIA_ID_AUTOR = @idAutor, LIA_PC_ROYALTY = @royaltyLivro
                            WHERE LIA_ID_LIVRO = @idLivro AND LIA_ID_AUTOR = @idAutor", // atualiza somente aquela relação
                        ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoLivroAutor.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivroAutor.lia_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@royaltyLivro", aoLivroAutor.lia_pc_royalty));
                    liQtdeRegistrosAtualizados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na remoção dos relacionamentos do livro.');</script>");

                }
            }
            return liQtdeRegistrosAtualizados;
        }

        public int DeletaLivroAutor(Livros aoLivro)
        {
            // Apaga todos os relacionamentos de autor-livro que envolvam o livro escolhido
            int liQtdeRegistrosDeletados = 0;
            if (aoLivro == null) throw new NullReferenceException();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    
                    ioQuery = new SqlCommand(
                        @"DELETE FROM LIA_LIVRO_AUTOR
                            WHERE LIA_ID_LIVRO = @idLivro",
                        ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivro.liv_id_livro));
                    liQtdeRegistrosDeletados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na remoção dos relacionamentos do livro.');</script>");
                    
                }
            }

            return liQtdeRegistrosDeletados;
        }
    }
}