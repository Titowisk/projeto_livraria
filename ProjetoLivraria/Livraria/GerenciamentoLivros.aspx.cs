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
        } // tem necessidade do ViewState?
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
            // carregar editores 

            // carregar categorias
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

        public void BtnNovoLivro_Click (object sender, EventArgs e)
        {
            // botão para submeter o formulário e assim criar um novo livro
            try
            {
                decimal ldcIdLivro = this.ListaLivros.OrderByDescending(livro => livro.liv_id_livro).First().liv_id_livro + 1;
                // Titulo
                // Resumo
                // Categoria (ID)
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