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
    public partial class GerenciamentoEditores : System.Web.UI.Page
    {
        EditoresDAO ioEditoresDAO = new EditoresDAO();

        public BindingList<Editores> ListaEditores
        {
            get
            {
                if ((BindingList<Editores>)ViewState["ViewStateListaEditores"] == null) this.CarregaDados();

                return (BindingList<Editores>)ViewState["ViewStateListaEditores"];
            }

            set
            {
                ViewState["ViewStateListaEditores"] = value;
            }
        }

        protected void CarregaDados()
        {
            this.ListaEditores = this.ioEditoresDAO.BuscaEditores();
            this.gvGerenciamentoEditores.DataSource = this.ListaEditores.OrderBy(loEditor => loEditor.edi_nm_editor);
            this.gvGerenciamentoEditores.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaDados();
            }
        }

        protected void btnNovoEditor_Click(object sender, EventArgs e)
        {
            try
            {
                // pegar os valores de Editor
                // ultimo id criado + 1
                decimal ldcIdEditor = ListaEditores.OrderByDescending(editor => editor.edi_id_editor).FirstOrDefault().edi_id_editor + 1;
                // nome
                string lsNomeEditor = tbxCadastroNomeEditor.Text;
                // email
                string lsEmailEditor = tbxCadastroEmailEditor.Text;
                // url
                string lsUrlEditor = tbxCadastroUrlEditor.Text;

                // criar o Editor
                Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor, lsEmailEditor, lsUrlEditor);
                ioEditoresDAO.InsereEditor(loEditor);
                CarregaDados();
                // sucesso
                HttpContext.Current.Response.Write("<script>alert('Editor cadastrado com sucesso!');</script>");

            }
            catch (Exception ex)
            {

                // erro
                HttpContext.Current.Response.Write("<script>alert('Editor não pôde ser cadastrado!');</script>");
                HttpContext.Current.Response.Write($"<script>console.log({ex.StackTrace});</script>");
                HttpContext.Current.Response.Write($"<script>console.log({ex.Message});</script>");
            }

            // apaga os campos preenchidos
            tbxCadastroNomeEditor.Text = String.Empty;
            tbxCadastroEmailEditor.Text = String.Empty;
            tbxCadastroUrlEditor.Text = String.Empty;
        }

        protected void gvGerenciamentoEditores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // pega o index da linha que será editada
            gvGerenciamentoEditores.EditIndex = e.NewEditIndex;
            CarregaDados();
        }

        protected void gvGerenciamentoEditores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // reseta o index de linhas
            gvGerenciamentoEditores.EditIndex = -1;
            CarregaDados();
        }

        protected void gvGerenciamentoEditores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var currentRow = gvGerenciamentoEditores.Rows[e.RowIndex];
            // pegar os valores sob a mascaraca de edição
            // id
            decimal ldcIdEditor = Convert.ToDecimal((currentRow.FindControl("lblEditIdEditor") as Label).Text);
            // nome
            string lsNomeEditor = (currentRow.FindControl("tbxEditNomeEditor") as TextBox).Text;
            // email
            string lsEmailEditor = (currentRow.FindControl("tbxEditEmailEditor") as TextBox).Text;
            // url
            string lsUrlEditor = (currentRow.FindControl("tbxEditUrlEditor") as TextBox).Text;

            // verificar se são diferentes de espaço em branco
            if (String.IsNullOrWhiteSpace(lsNomeEditor))
            {
                HttpContext.Current.Response.Write("<script>alert('Preencha o campo de nome!');</script>");
            }
            else if (String.IsNullOrWhiteSpace(lsEmailEditor))
            {
                HttpContext.Current.Response.Write("<script>alert('Preencha o campo de email!');</script>");
            }
            else if (String.IsNullOrWhiteSpace(lsUrlEditor))
            {
                HttpContext.Current.Response.Write("<script>alert('Preencha o campo de url!');</script>");
            }
            else
            {
                try
                {
                    Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor, lsEmailEditor, lsUrlEditor);
                    // usar o DAO para atualizar o Editor
                    ioEditoresDAO.AtualizaEditor(loEditor);

                    // resetar o index
                    gvGerenciamentoEditores.EditIndex = -1;
                    // recarregar a lista de editores
                    CarregaDados();
                    // sucesso
                    HttpContext.Current.Response.Write("<script>alert('Editor atualizado com sucesso!');</script>");

                }
                catch (Exception ex)
                {

                    // caso erro
                    HttpContext.Current.Response.Write("<script>alert('Ocorreu um problema ao atualizar Editor.');</script>");
                    HttpContext.Current.Response.Write($"<script>console.log({ex.StackTrace});</script>");
                    HttpContext.Current.Response.Write($"<script>console.log({ex.Message});</script>");
                }
            }
        }

        protected void gvGerenciamentoEditores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                decimal ldcIdeditor = Convert.ToDecimal((gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("lblIdEditor") as Label).Text);
                // buscar o editor
                Editores loEditor = ioEditoresDAO.BuscaEditores(ldcIdeditor).FirstOrDefault();
                if (loEditor != null)
                {
                    //Crie LivrosDAO e o método FindLivrosByEditor() que deve receber um Editor como parâmetro e retornar
                    //uma lista de livros.
                    LivrosDAO loLivrosDAO = new LivrosDAO();

                    if (loLivrosDAO.FindLivrosByEditor(loEditor).Count > 0)
                    {
                        // se a busca retornar pelo menos 1 livro, não pode deletar o editor
                        HttpContext.Current.Response.Write("<script>alert('Não é possível remover o editor selecionado pois existem livros associados a ele.');</script>");
                    }
                    else
                    {
                        // deletar o editor
                        ioEditoresDAO.DeletaEditor(loEditor);
                        // sucesso
                        HttpContext.Current.Response.Write("<script>alert('Editor removido com sucesso!');</script>");
                        // reseta o index
                        gvGerenciamentoEditores.EditIndex = -1;
                        // recarrega os dados
                        CarregaDados();

                    }
                }
            }
            catch (Exception ex)
            {
                // erro
                HttpContext.Current.Response.Write("<script>alert('Editor removido com sucesso!');</script>");
                HttpContext.Current.Response.Write($"<script>console.log({ex.Message});</script>");
                HttpContext.Current.Response.Write($"<script>console.log({ex.StackTrace});</script>");
            }

        }

        protected void gvGerenciamentoEditores_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        
    }
}