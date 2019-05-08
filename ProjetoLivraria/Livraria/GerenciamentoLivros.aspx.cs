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



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // carregar editores 

                // carregar categorias
                this.CarregaCategorias();
                CarregaDropDownListCategoria();

                // carrega lista de livros
               
            }

          
            
        }

        private void CarregaDados()
        {
            // carrega uma lista de livros
            this.ListaLivros = ioLivrosDAO.BuscaLivros();
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

        DataRow CreateRow(string Text, decimal Value, DataTable dt)
        {

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
                
                // Royalty
                // Editor (ID)

            }
            catch (Exception)
            {

                throw;
            }
        }

        // após criar a gridView, criar os métodos de edit, update e delete
    }
}