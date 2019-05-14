using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoCategorias : System.Web.UI.Page
    {
        TipoLivroDAO ioTipoLivroDAO = new TipoLivroDAO();

        public BindingList<TipoLivro> ListaCategorias
        {
            get
            {
                if ((BindingList<TipoLivro>)ViewState["ViewStateListaCategorias"] == null)
                    CarregaDados();
                return (BindingList<TipoLivro>)ViewState["ViewStateListaCategorias"];
            }
            set
            {
                ViewState["ViewStateListaCategorias"] = value;
            }
        }

        private void CarregaDados()
        {
            ListaCategorias = ioTipoLivroDAO.BuscaCategorias();
            gvGerenciamentoCategorias.DataSource = ListaCategorias;
            gvGerenciamentoCategorias.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregaDados();
        }

        protected void btnNovoTipoLivro_Click(object sender, EventArgs e)
        {
            try
            {
                // pegar os valores
                decimal ldcIdAutor = this.ListaCategorias.OrderByDescending(c => c.til_id_tipo_livro).First().til_id_tipo_livro + 1;

                string lsNomeCategoria = this.tbxCadastroCategoriaTipoLivro.Text;

                // criar a Categoria
                TipoLivro loTipoLivro = new TipoLivro(ldcIdAutor, lsNomeCategoria);
                this.ioTipoLivroDAO.InsereTipoLivro(loTipoLivro);
                this.CarregaDados();
                // sucesso
                HttpContext.Current.Response.Write("<script>alert('Categoria cadastrada com sucesso!');</script>");
            }
            catch (Exception ex)
            {
                // erro
                HttpContext.Current.Response.Write("<script>console.log('Erro no cadastro da Categoria!');</script>");
                HttpContext.Current.Response.Write($"<script>console.log({ex.StackTrace});</script>");
                HttpContext.Current.Response.Write($"<script>console.log({ex.Message});</script>");
            }

            this.tbxCadastroCategoriaTipoLivro.Text = string.Empty;
            
        }

        protected void gvGerenciamentoCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGerenciamentoCategorias.EditIndex = -1;
            CarregaDados();
        }

        protected void gvGerenciamentoCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGerenciamentoCategorias.EditIndex = e.NewEditIndex;
            CarregaDados();
        }

        protected void gvGerenciamentoCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var currentRow = gvGerenciamentoCategorias.Rows[e.RowIndex];
            decimal ldcIdTipoLivro = Convert.ToDecimal((currentRow.FindControl("lblEditIdTipoLivro") as Label).Text);

            string lsNomeTipoLivro = (currentRow.FindControl("tbxEditNomeTipoLivro") as TextBox).Text;

            if (String.IsNullOrWhiteSpace(lsNomeTipoLivro))
                HttpContext.Current.Response.Write("<script>alert('Digite o nome da categoria.')</script>");
            else
            {
                try
                {
                    TipoLivro loTipoLivro = new TipoLivro(ldcIdTipoLivro, lsNomeTipoLivro);
                    ioTipoLivroDAO.AtualizaTipoLivro(loTipoLivro);
                    this.gvGerenciamentoCategorias.EditIndex = -1;
                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Categoria atualizada com sucesso!');</script>");

                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização da categoria.');</script>");
                    HttpContext.Current.Response.Write($"<script>console.log({ex.Message});</script>");
                    HttpContext.Current.Response.Write($"<script>console.log({ex.StackTrace});</script>");
                }
            }
        }

        protected void gvGerenciamentoCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvGerenciamentoCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}