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
                CarregaDropDownListEditor();

                // carregar categorias
                this.CarregaCategorias();
                CarregaDropDownListCategoria();

                // carregar autores
                this.CarregaAutores();
                CarregaDropDownListAutor();
                // carrega lista de livros
                               
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

        private void CarregaDropDownListEditor()
        {
            // cria o data source que irá alimentar o DropDownList de Editores
            ddlCadastroEditorLivro.DataSource = CreateEditorDataSource();
            ddlCadastroEditorLivro.DataTextField = "EditorNome"; // valor mostrado
            ddlCadastroEditorLivro.DataValueField = "EditorId"; // valor oculto

            ddlCadastroEditorLivro.DataBind();

            ddlCadastroEditorLivro.SelectedIndex = 0;
        }

        

        // cria uma tabela que armazena valores que serão usados na DropDownList de Categoria
        private void CarregaDropDownListCategoria()
        {
            // Specify the data source and field names for the Text 
            // and Value properties of the items (ListItem objects) 
            // in the DropDownList control.
            ddlCadastroCategoriaLivro.DataSource = CreateCategoriaDataSource();
            ddlCadastroCategoriaLivro.DataTextField = "TipoLivroDescricao";
            ddlCadastroCategoriaLivro.DataValueField = "TipoLivroId";

            // Bind the data to the control.
            ddlCadastroCategoriaLivro.DataBind();

            // Set the default selected item, if desired.
            ddlCadastroCategoriaLivro.SelectedIndex = 0;

        }

        private void CarregaDropDownListAutor()
        {
            // cria o data source que irá alimentar o DropDownList de Autores
            ddlCadastroAutorLivro.DataSource = CreateAutorDataSource();
            ddlCadastroAutorLivro.DataTextField = "AutorNome";
            ddlCadastroAutorLivro.DataValueField = "AutorId";

            // Bind the data to the control.
            ddlCadastroAutorLivro.DataBind();

            // Set the default selected item, if desired.
            ddlCadastroAutorLivro.SelectedIndex = 0;
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
                this.CarregaDados();
                ioLivrosDAO.InsereLivro(loLivro);
                // cria a relação de autor com livro em LIA_AUTOR_LIVRO
                LivroAutor loLivroAutor = new LivroAutor(ldcIdAutor, ldcIdLivro, lsRoyaltyLivro);
                ioLivroAutorDAO.InsereLivroAutor(loLivroAutor);

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
            
        }

        protected void gvGerenciamentoLivros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGerenciamentoLivros.EditIndex = e.NewEditIndex;
            CarregaDados();
            // carregar as dropdown lists de cada modelo (autor, categoria e editor) para edição
            
        }

        protected void gvGerenciamentoLivros_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvGerenciamentoLivros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvGerenciamentoLivros_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        // após criar a gridView, criar os métodos de edit, update e delete
    }
}