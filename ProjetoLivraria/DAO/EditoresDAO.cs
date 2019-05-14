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
    public class EditoresDAO
    {
        private string _id = "EDI_ID_EDITOR";
        private string _nome = "EDI_NM_EDITOR";
        private string _email = "EDI_DS_EMAIL";
        private string _url = "EDI_DS_URL";
        SqlConnection ioConexao;
        SqlCommand ioQuery;

        public BindingList<Editores> BuscaEditores(decimal? adcIdEditor = null)
        {
            var ioListEditores = new BindingList<Editores>();
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (adcIdEditor != null)
                    {
                        ioQuery = new SqlCommand($"SELECT * FROM EDI_EDITORES WHERE {_id} = @idEditor", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idEditor", adcIdEditor));
                        //ioQuery.Parameters.AddWithValue("@idEditor", adcIdEditor); é a mesma coisa?
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM EDI_EDITORES", ioConexao);

                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            ioListEditores.Add(new Editores(loReader.GetDecimal(0), loReader.GetString(1), loReader.GetString(2), loReader.GetString(3)));
                        }
                    }
                }
                catch (Exception)
                {

                    throw new Exception("Não foi possível fazer a busca de editor(es)");
                }

            }

            return ioListEditores;
        }

        // insert
        public int InsereEditor (Editores aoNovoEditor)
        {
            var liQtdRegistrosInseridos = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (aoNovoEditor == null) throw new NullReferenceException();
                    ioQuery = new SqlCommand($@"INSERT INTO EDI_EDITORES({_id}, {_nome}, {_email}, {_url})
                                            VALUES (@idEditor, @nomeEditor, @emailEditor, @urlEditor)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoEditor.edi_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeEditor", aoNovoEditor.edi_nm_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@emailEditor", aoNovoEditor.edi_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@urlEditor", aoNovoEditor.edi_ds_url));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();

                }
                catch (Exception)
                {

                    throw new Exception("Não foi possível enserir editor.");
                }
            }
            return liQtdRegistrosInseridos;
        }
        // update
        public int AtualizaEditor(Editores aoEditor)
        {
            var liQtdRegistrosAtualizados = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (aoEditor == null) throw new NullReferenceException();
                    ioQuery = new SqlCommand(
                        $@"UPDATE EDI_EDITORES
                            SET ({_nome} = @nomeEditor, {_email} = @emailEditor, {_url} = @urlEditor)
                            WHERE {_id} = @idEditor");
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoEditor.edi_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeEditor", aoEditor.edi_nm_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@emailEditor", aoEditor.edi_ds_email));
                    ioQuery.Parameters.Add(new SqlParameter("@urlEditor", aoEditor.edi_ds_url));
                    liQtdRegistrosAtualizados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw new Exception("Não foi possível atualizar o editor.");
                }
            }
            return liQtdRegistrosAtualizados;
        }

        // delete
        public int DeletaEditor(Editores aoEditor)
        {
            var liQtdRegistrosDeletados = 0;

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (aoEditor == null) throw new NullReferenceException();
                    ioQuery = new SqlCommand(
                        $@"DELETE FROM EDI_EDITORES
                            WHERE {_id} = @idEditor");
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoEditor.edi_id_editor));
                    liQtdRegistrosDeletados = ioQuery.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw new Exception("Não foi possível deletar o editor.");
                }
            }
            return liQtdRegistrosDeletados;
        }
    }
}