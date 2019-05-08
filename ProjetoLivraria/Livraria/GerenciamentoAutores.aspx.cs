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
    public partial class GerenciamentoAutores : System.Web.UI.Page
    {
        AutoresDAO ioAutoresDAO = new AutoresDAO();

        public BindingList<Autores> ListaAutores
        {
            get
            {
                if ((BindingList<Autores>)ViewState["ViewStateListaAutores"] == null) this.CarregaDados();

                return (BindingList<Autores>)ViewState["ViewStateListaAutores"];
            }
            set
            {
                ViewState["ViewStateListaAutores"] = value;
            }
        }

        public Autores AutorSessao
        {
            get { return (Autores)Session["SessionAutorSelecionado"]; }
            set { Session["SessionAutorSelecionado"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CarregaDados();
            }
        }

        private void CarregaDados()
        {
            this.ListaAutores = this.ioAutoresDAO.BuscaAutores();
            this.gvGerenciamentoAutores.DataSource = this.ListaAutores.OrderBy(loAutor => loAutor.aut_nm_nome);
            this.gvGerenciamentoAutores.DataBind();

        }

        protected void BtnNovoAutor_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ldcIdAutor = this.ListaAutores.OrderByDescending(a => a.aut_id_autor).First().aut_id_autor + 1;

                string lsNomeAutor = this.tbxCadastroNomeAutor.Text;
                string lsSobrenomeAutor = this.tbxCadastroSobrenomeAutor.Text;
                string lsEmailAutor = this.tbxCadastroEmailAutor.Text;

                Autores loAutores = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
                this.ioAutoresDAO.InsereAutor(loAutores);
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Autor cadastrado com sucesso!');</script>");
            }
            catch (Exception ex)
            {

                HttpContext.Current.Response.Write("<script>console.log('Erro no cadastro do Autor!');</script>");
                HttpContext.Current.Response.Write($"<script>console.log({ex.StackTrace});</script>");
                HttpContext.Current.Response.Write($"<script>console.log({ex.Message});</script>");
            }

            this.tbxCadastroNomeAutor.Text = string.Empty;
            this.tbxCadastroSobrenomeAutor.Text = string.Empty;
            this.tbxCadastroEmailAutor.Text = string.Empty;
        }

        //protected void gvGerenciamentoAutores_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        protected void gvGerenciamentoAutores_RowEditing (object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoAutores.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }

        protected void gvGerenciamentoAutores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoAutores.EditIndex = -1;
            this.CarregaDados();
        }

        protected void gvGerenciamentoAutores_RowUpdating (object sender, GridViewUpdateEventArgs e)
        {
            string IdAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("lblEditIdAutor") as Label).Text; 
            decimal ldcIdAutor = Convert.ToDecimal(IdAutor);

            string lsNomeAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditNomeAutor") as TextBox).Text;
            string lsSobrenomeAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditSobrenomeAutor") as TextBox).Text;
            string lsEmailAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditEmailAutor") as TextBox).Text;

            if (String.IsNullOrWhiteSpace(lsNomeAutor))
                HttpContext.Current.Response.Write("<script>alert('Digite o nome do Autor.')</script>");
            else if (String.IsNullOrWhiteSpace(lsSobrenomeAutor))
                HttpContext.Current.Response.Write("<script>alert('Digite o sobrenome do Autor.')</script>");
            else if (String.IsNullOrWhiteSpace(lsEmailAutor))
                HttpContext.Current.Response.Write("<script>alert('Digite o email do Autor.')</script>");
            else
            {
                try
                {
                    Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
                    this.ioAutoresDAO.AtualizaAutor(loAutor);
                    this.gvGerenciamentoAutores.EditIndex = -1;
                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Autor atualizado com sucesso!');</script>");

                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do cadastro do autor.');</script>");
                    HttpContext.Current.Response.Write($"<script>console.log({ex.Message});</script>");
                    HttpContext.Current.Response.Write($"<script>console.log({ex.StackTrace});</script>");
                }
            }


        }
        protected void gvGerenciamentoAutores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loRGridViewRow = this.gvGerenciamentoAutores.Rows[e.RowIndex];
                decimal ldcIdAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("lblIdAutor") as
                Label).Text);
                Autores loAutor = this.ioAutoresDAO.BuscaAutores(ldcIdAutor).FirstOrDefault();
                if (loAutor != null)
                {
                    //Crie LivrosDAO e o método FindLivrosByAutor() que deve receber um Autor como parâmetro e retornar
                    //uma lista de livros.
                    LivrosDAO loLivrosDAO = new LivrosDAO();
                    if (loLivrosDAO.FindLivrosByAutor(loAutor).Count != 0)
                        HttpContext.Current.Response.Write("<script>alert('Não é possível remover o autor selecionado pois existem livros associados a ele.');</script>");
                    else
                    {
                        this.ioAutoresDAO.DeletaAutor(loAutor);
                        this.CarregaDados();
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do autor selecionado.');</script>");
                HttpContext.Current.Response.Write($"<script>console.log({ex.Message});</script>");
                HttpContext.Current.Response.Write($"<script>console.log({ex.StackTrace});</script>");
            }
        }

        protected void gvGerenciamentoAutores_RowCommand (object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "CarregaLivrosAutor":
                        int liRowIndex = Convert.ToInt32(e.CommandArgument);
                        string IdAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblEditIdAutor") as Label).Text;
                        decimal ldcIdAutor = Convert.ToDecimal(IdAutor);

                        string lsNomeAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("tbxEditNomeAutor") as TextBox).Text;
                        string lsSobrenomeAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("tbxEditSobrenomeAutor") as TextBox).Text;
                        string lsEmailAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("tbxEditEmailAutor") as TextBox).Text;

                        Autores loAutores = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
                        this.AutorSessao = loAutores;
                        Response.Redirect("/Livraria/GerenciamentoLivros");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na busca dos livros.');</script>");
                HttpContext.Current.Response.Write($"<script>console.log({ex.Message});</script>");
                HttpContext.Current.Response.Write($"<script>console.log({ex.StackTrace});</script>");
            }

        }
    }
}