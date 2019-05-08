<%@ Page Title="Gerenciamento de Livros" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoLivros.aspx.cs"
     Inherits="ProjetoLivraria.Livraria.GerenciamentoLivros" %>

<asp:Content ID="GerenciamentoLivros" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left;">
        <table style="width: 100%;">
            <tr style="display: grid;">
                <%-- Titulo Livro --%>
                <td>
                    <asp:Label ID="lblCadastroNomeLivro" Text="Título: " runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroNomeLivro" runat="server" CssClass="form-control" 
                        Height="35px" Width="400px"/>
                    <asp:RequiredFieldValidator ErrorMessage="Digite o título do livro!" ControlToValidate="tbxCadastroNomeLivro" runat="server" 
                        Style="color: red;"/>
                </td>
                
            </tr>
            <tr style="display: grid;">
                <%-- Resumo Livro --%>
                <td>
                    <asp:Label ID="lblCadastroResumoLivro" Text="Resumo do Livro: " runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroResumoLivro" runat="server" CssClass="form-control" 
                        Height="35px" Width="400px"/>
                    <asp:RequiredFieldValidator ErrorMessage="Escreva um resumo do livro" ControlToValidate="tbxCadastroResumoLivro" runat="server" 
                        Style="color: red;"/>
                </td>
                
            </tr>
            <tr style="display: grid;">
                <%-- Categoria Livro - DropDown List --%>
                <td>
                    <asp:Label ID="lblCadastroCategoriaLivro" Text="Categoria: " runat="server" />
                </td>
                <td>
                    <%-- carregar categorias existentes aqui quando a página carregar --%>
                    <asp:DropDownList ID="ddlCadastroCategoriaLivro" runat="server">
                        <asp:ListItem Text="categoria1" />
                        <asp:ListItem Text="categoria2" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Escolha uma categoria!" ControlToValidate="ddlCadastroCategoriaLivro" runat="server" 
                        Style="color: red;"/>
                </td>
                
            </tr>
            <tr style="display: grid;">
                <%-- Preço Livro --%>
                <td>
                    <asp:Label ID="lblCadastroPrecoLivro" Text="Preço do Livro: " runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroPrecoLivro" runat="server" CssClass="form-control" 
                        Height="35px" Width="400px"/>
                    <asp:RequiredFieldValidator ErrorMessage="Escreva o preço do livro!" ControlToValidate="tbxCadastroPrecoLivro" runat="server" 
                        Style="color: red;"/>
                </td>
                
            </tr>
            <tr style="display: grid;">
                <%-- Royalty Livro --%>
                <td>
                    <asp:Label ID="lblCadastroRoyaltyLivro" Text="Royalty do Livro: " runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroRoyaltyLivro" runat="server" CssClass="form-control" 
                        Height="35px" Width="400px"/>
                    <asp:RequiredFieldValidator ErrorMessage="Escreva o royalty do livro!" ControlToValidate="tbxCadastroRoyaltyLivro" runat="server" 
                        Style="color: red;"/>
                </td>
                
            </tr>
            <tr style="display: grid;">
                <%-- Editor Livro DropDown List --%>
                <td>
                    <asp:Label ID="lblCadastroEditorLivro" Text="Editor: " runat="server" />
                </td>
                <td>
                    <%-- carregar categorias existentes aqui quando a página carregar --%>
                    <asp:DropDownList ID="ddlCadastroEditorLivro" runat="server">
                        <asp:ListItem Text="editor1" />
                        <asp:ListItem Text="editor2" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Escolha um editor para o livro!" ControlToValidate="ddlCadastroEditorLivro" runat="server" 
                        Style="color: red;"/>
                </td>
                
            </tr>
            <tr style="display: grid;">
                <%-- Edição Numero Livro --%>
                <td>
                    <asp:Label ID="lblCadastroNumeroEdicaoLivro" Text="Numero de Edição do Livro: " runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroNumeroEdicaoLivro" runat="server" CssClass="form-control" 
                        Height="35px" Width="400px"/>
                    <asp:RequiredFieldValidator ErrorMessage="Escreva o numero da edicao do livro!" ControlToValidate="tbxCadastroNumeroEdicaoLivro" runat="server" 
                        Style="color: red;"/>
                </td>
                
            </tr>
            <tr style="display: grid;">
                <td>
                    <asp:Button ID="BtnNovoLivro" OnClick="BtnNovoLivro_Click" Text="Salvar" runat="server" />
                </td>
            </tr>            
        </table>
    </div>
</asp:Content>


