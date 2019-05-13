using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoLivros : System.Web.UI.Page
    {
        LivrosDAO ioLivrosDAO = new LivrosDAO(); // comunicação com o BD
        EditoresDAO ioEditoresDAO = new EditoresDAO();
        TipoLivroDAO ioCategoriasDAO = new TipoLivroDAO();
        AutoresDAO ioAutoresDAO = new AutoresDAO();
        LivroAutorDAO ioLivroAutorDAO = new LivroAutorDAO();

        public BindingList<Livros> ListaLivros
        {
            get
            {
                if ((BindingList<Livros>)ViewState["ViewStateListaLivros"] == null) this.CarregaDados();
                return (BindingList<Livros>)ViewState["ViewStateListaLivros"];
            }
            set
            {
                ViewState["ViewStateListaLivros"] = value;
            }
        } 
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
        public BindingList<TipoLivro> ListaCategorias
        {
            get
            {
                if ((BindingList<TipoLivro>)ViewState["ViewStateListaCategorias"] == null) this.CarregaDados();
                return (BindingList<TipoLivro>)ViewState["ViewStateListaCategorias"];
            }
            set
            {
                ViewState["ViewStateListaCategorias"] = value;
            }
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // carregar editores 
                this.CarregaEditores();
                //CarregaDropDownListEditor();
                CarregaDropDownList(ddlCadastroEditorLivro, "EditorNome", "EditorId", CreateEditorDataSource);

                // carregar categorias
                this.CarregaCategorias();
                //CarregaDropDownListCategoria();
                CarregaDropDownList(ddlCadastroCategoriaLivro, "TipoLivroDescricao", "TipoLivroId", CreateCategoriaDataSource);

                // carregar autores
                this.CarregaAutores();
                //CarregaDropDownListAutor();
                CarregaDropDownList(ddlCadastroAutorLivro, "AutorNome", "AutorId", CreateAutorDataSource);

                // carrega lista de livros
                this.CarregaDados();
            }
          
            
        }

        private void CarregaDados()
        {
            // carrega uma lista de livros
            this.ListaLivros = ioLivrosDAO.BuscaLivros();

            // carrega o gridview de livros
            gvGerenciamentoLivros.DataSource = ListaLivros.OrderBy(livro => livro.liv_nm_titulo);
            gvGerenciamentoLivros.DataBind();

        }

        private void CarregaCategorias ()
        {
            // CarregaCategorias uma lista das categorias disponíveis (com seus IDs de forma oculta)
            this.ListaCategorias = ioCategoriasDAO.BuscaCategorias();
        }

        private void CarregaEditores ()
        {
            // Carrega uma lista de editores disponíveis (com seus IDs de forma oculta)
            this.ListaEditores = ioEditoresDAO.BuscaEditores();
        }

        private void CarregaAutores()
        {
            // carrega uma lista de autores disponíveis (com seus IDs de forma oculta)
            this.ListaAutores = ioAutoresDAO.BuscaAutores();
        }

        private void CarregaDropDownList(DropDownList dropDownListObj, string TextFieldName, string ValueFieldName, Func<ICollection> CreateDataSource)
        {
            // cria o data source que irá alimentar o DropDownList de Editores
            dropDownListObj.DataSource = CreateDataSource();
            dropDownListObj.DataTextField = TextFieldName; // valor mostrado
            dropDownListObj.DataValueField = ValueFieldName; // valor oculto

            dropDownListObj.DataBind();

            dropDownListObj.SelectedIndex = 0;
        }
        

        private ICollection CreateEditorDataSource()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("EditorNome", typeof(string)));
            dt.Columns.Add(new DataColumn("EditorId", typeof(decimal)));

            foreach (var editor in this.ListaEditores)
            {
                dt.Rows.Add(CreateRow(editor.edi_nm_editor, editor.edi_id_editor, dt));
            }

            DataView dv = new DataView(dt);
            return dv;
        }


        private ICollection CreateCategoriaDataSource ()
        {
            // Fonte: https://docs.microsoft.com/pt-br/dotnet/api/system.web.ui.webcontrols.dropdownlist?view=netframework-4.8#exemplos

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("TipoLivroDescricao", typeof(string))); // Text
            dt.Columns.Add(new DataColumn("TipoLivroId", typeof(decimal))); // Value

            foreach (var categoria in this.ListaCategorias)
            {
                dt.Rows.Add(CreateRow(categoria.til_ds_descricao, categoria.til_id_tipo_livro, dt));
            }

            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);
            return dv;
        }

        private ICollection CreateAutorDataSource()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("AutorNome", typeof(string))); // Text
            dt.Columns.Add(new DataColumn("AutorId", typeof(decimal))); // Value

            foreach (var autor in this.ListaAutores)
            {
                dt.Rows.Add(CreateRow(autor.aut_nm_nome, autor.aut_id_autor, dt));
            }

            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);
            return dv;
        }

        DataRow CreateRow(string Text, decimal Value, DataTable dt)
        {
            // Essa função serve tanto para TipoLivro, como para Editor e também Autor
            // pois as informações que pego deles, possuem o mesmo tipo

            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();

            // This DataRow contains the ColorTextField and ColorValueField 
            // fields, as defined in the CreateDataSource method. Set the 
            // fields with the appropriate value. Remember that column 0 
            // is defined as ColorTextField, and column 1 is defined as 
            // ColorValueField.
            dr[0] = Text;
            dr[1] = Value;

            return dr;

        }

        public void BtnNovoLivro_Click (object sender, EventArgs e)
        {
            // botão para submeter o formulário e assim criar um novo livro
            try
            {
                // ID
                decimal ldcIdLivro = this.ListaLivros.OrderByDescending(livro => livro.liv_id_livro).First().liv_id_livro + 1;
                // Titulo
                string lsTituloLivro = this.tbxCadastroTituloLivro.Text;
                // Resumo
                string lsResumoLivro = this.tbxCadastroResumoLivro.Text;
                // Categoria (ID)
                decimal ldcIdTipoLivro = Convert.ToDecimal( this.ddlCadastroCategoriaLivro.SelectedItem.Value );
                // Preço
                decimal lsPrecoLivro = Convert.ToDecimal(this.tbxCadastroPrecoLivro.Text);
                // Royalty
                decimal lsRoyaltyLivro = Convert.ToDecimal(this.tbxCadastroRoyaltyLivro.Text);
                // Editor (ID)
                decimal ldcIdEditor = Convert.ToDecimal(this.ddlCadastroEditorLivro.SelectedItem.Value);
                // Numero Edicao
                int lsNumeroEdicaoLivro = Convert.ToInt32(this.tbxCadastroNumeroEdicaoLivro.Text);

                // autor (não pode ser criado um livro sem autor)
                decimal ldcIdAutor = Convert.ToDecimal(this.ddlCadastroAutorLivro.SelectedItem.Value);

                // Cria o livro no banco de dados (com editor e tipolivro)
                Livros loLivro = new Models.Livros(ldcIdLivro, ldcIdTipoLivro, ldcIdEditor, lsTituloLivro,
                    lsPrecoLivro, lsRoyaltyLivro, lsResumoLivro, lsNumeroEdicaoLivro);
                ioLivrosDAO.InsereLivro(loLivro);
                // cria a relação de autor com livro em LIA_AUTOR_LIVRO
                LivroAutor loLivroAutor = new LivroAutor(ldcIdAutor, ldcIdLivro, lsRoyaltyLivro);
                ioLivroAutorDAO.InsereLivroAutor(loLivroAutor);

                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Livro cadastrado com sucesso!');</script>");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>console.log('Erro no cadastro do Livro!');</script>");
                throw;
            }
        }

        protected void gvGerenciamentoLivros_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoLivros.EditIndex = -1;
            this.CarregaDados();
        }

        protected void gvGerenciamentoLivros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGerenciamentoLivros.EditIndex = e.NewEditIndex;
            CarregaDados();
            // carregar as dropdown lists de cada modelo (autor, categoria e editor) para edição
            // categoria
            DropDownList gvDropDownListCategoria = (gvGerenciamentoLivros.Rows[e.NewEditIndex].FindControl("ddlEditCategoriaLivro") as DropDownList);
            CarregaDropDownList(gvDropDownListCategoria, "TipoLivroDescricao", "TipoLivroId", CreateCategoriaDataSource);

            // autor
            DropDownList gvDropDownListAutor = (gvGerenciamentoLivros.Rows[e.NewEditIndex].FindControl("ddlEditAutorLivro") as DropDownList);
            CarregaDropDownList(gvDropDownListAutor, "AutorNome", "AutorId", CreateAutorDataSource);

            // editor
            DropDownList gvDropDownListEditor = (gvGerenciamentoLivros.Rows[e.NewEditIndex].FindControl("ddlEditEditorLivro") as DropDownList);
            CarregaDropDownList(gvDropDownListEditor, "EditorNome", "EditorId", CreateEditorDataSource);

        }

        protected void gvGerenciamentoLivros_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var currentRow = gvGerenciamentoLivros.Rows[e.RowIndex];
            // pegar os valores colocados pelo usuário
            //liv_id_livro
            string idLivro = (currentRow.FindControl("lblEditIdLivro") as Label).Text;
            decimal ldcIdLivro = Convert.ToDecimal(idLivro);
            //liv_id_tipo_livro
            decimal ldcIdTipoLivro = Convert.ToDecimal((currentRow.FindControl("ddlEditCategoriaLivro") as DropDownList).SelectedItem.Value);
            //liv_id_editor
            decimal ldcIdEditor = Convert.ToDecimal((currentRow.FindControl("ddlEditEditorLivro") as DropDownList).SelectedItem.Value);
            // autor
            decimal ldcIdAutor = Convert.ToDecimal((currentRow.FindControl("ddlEditAutorLivro") as DropDownList).SelectedItem.Value);
            //liv_nm_titulo
            string lsTituloLivro = (currentRow.FindControl("tbxEditTituloLivro") as TextBox).Text;
            //liv_vl_preco 
            decimal lsPrecoLivro = Convert.ToDecimal((currentRow.FindControl("tbxEditPrecoLivro") as TextBox).Text);
            //liv_pc_royalty 
            decimal lsRoyaltyLivro = Convert.ToDecimal((currentRow.FindControl("tbxEditRoyaltyLivro") as TextBox).Text);
            //liv_ds_resumo 
            string lsResumoLivro = (currentRow.FindControl("tbxEditResumoLivro") as TextBox).Text;
            //liv_nu_edicao
            int lsNuEdicaoLivro = Convert.ToInt32((currentRow.FindControl("tbxEditNumeroEdicaoLivro") as TextBox).Text);
            
            // verificar se há algum valor nulo
            if (String.IsNullOrWhiteSpace(lsTituloLivro))
                HttpContext.Current.Response.Write("<script>alert('Digite o titulo do Livro.')</script>");
            else if (String.IsNullOrWhiteSpace(lsResumoLivro))
                HttpContext.Current.Response.Write("<script>alert('Digite o resumo do Livro.')</script>");
            else if (lsNuEdicaoLivro.Equals(0))
                HttpContext.Current.Response.Write("<script>alert('Numero de edição do Livro não pode ser 0')</script>");
            else
            {
                // atualizar o objeto no banco de dados
                try
                {
                    Livros loLivro = new Livros(ldcIdLivro, ldcIdTipoLivro, ldcIdEditor, lsTituloLivro, lsPrecoLivro, lsRoyaltyLivro, lsResumoLivro, lsNuEdicaoLivro);
                    LivroAutor loLivroAutor = new LivroAutor(ldcIdAutor, ldcIdLivro, lsRoyaltyLivro);
                    ioLivrosDAO.AtualizaLivro(loLivro);
                    ioLivroAutorDAO.AtualizaLivroAutor(loLivroAutor);
                    gvGerenciamentoLivros.EditIndex = -1;
                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Livro atualizado com sucesso!');</script>");
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do livro.')</script>");
                    HttpContext.Current.Response.Write($"<script>console.log('${ex.Message}')</script>");
                    HttpContext.Current.Response.Write($"<script>console.log('${ex.StackTrace}')</script>");
                    
                }
            }



        }

        protected void gvGerenciamentoLivros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var currentRow = gvGerenciamentoLivros.Rows[e.RowIndex];
            decimal ldcIdLivro = Convert.ToDecimal((currentRow.FindControl("lblIdLivro") as Label).Text);
            Livros ioLivro = ioLivrosDAO.BuscaLivros(ldcIdLivro).FirstOrDefault();

            if (ioLivro != null)
            {
                try
                {
                    // apagar o livro
                    ioLivrosDAO.DeletaLivro(ioLivro);

                    // apagar a relação do livro com outros autores
                    ioLivroAutorDAO.DeletaLivroAutor(ioLivro);

                    gvGerenciamentoLivros.EditIndex = -1;
                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Livro removido com sucesso!');</script>");

                }
                catch (Exception ex)
                {

                    HttpContext.Current.Response.Write("<script>alert('Erro na remoção do livro.')</script>");
                    HttpContext.Current.Response.Write($"<script>console.log('${ex.Message}')</script>");
                    HttpContext.Current.Response.Write($"<script>console.log('${ex.StackTrace}')</script>");
                }

            }
        }

        protected void gvGerenciamentoLivros_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        // após criar a gridView, criar os métodos de edit, update e delete
    }
}