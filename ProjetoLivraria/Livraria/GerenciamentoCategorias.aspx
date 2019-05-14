<%@ Page Title="Gerenciamento Categorias" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="GerenciamentoCategorias.aspx.cs" 
    Inherits="ProjetoLivraria.Livraria.GerenciamentoCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left;">
        <h2>Cadastro Nova Categoria</h2>
        <table>
            <tr style="display: grid;">
                <%-- ID TipoLivro --%>
                <td>
                    <asp:Label ID="lblCadastroNomeTipoLivro" runat="server" Font-size="16pt" Text="Nome: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroNomeTipoLivro" runat="server" CssClass="form-control" Height="35px"
                        Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCadastroNomeTipoLivro"
                        Style="color: red;" ErrorMessage="* Digite o nome da Categoria."></asp:RequiredFieldValidator>
                </td>
                <%-- Descrição TipoLivro --%>
                <td>
                    <asp:Label ID="lblCadastroCategoriaTipoLivro" runat="server" Font-Size="16pt" Text="Categoria: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroCategoriaTipoLivro" runat="server" CssClass="form-control" Height="35px"
                        Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="tbxCadastroCategoriaTipoLivro" Style="color: red;" ErrorMessage="* Digite o Categoria do
                        TipoLivro."></asp:RequiredFieldValidator>
                </td>
                <%-- Botão cadastro de nova categoria --%>
                <td>
                    <asp:Button ID="btnNovoTipoLivro" runat="server" CssClass="btn btn-success" Style="margin-top: 10px" Text="Salvar"
                        OnClick="btnNovoTipoLivro_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="row">
        <h2 style="text-align: center">Lista de Categorias Cadastradas</h2>
        <asp:GridView ID="gvGerenciamentoCategorias" runat="server" Width="100%" AutoGenerateColumns="False" Font-Size="14px" CellPadding="4" 
            ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoCategorias_RowCancelingEdit" 
            OnRowEditing="gvGerenciamentoCategorias_RowEditing" OnRowUpdating="gvGerenciamentoCategorias_RowUpdating" 
            OnRowDeleting="gvGerenciamentoCategorias_RowDeleting" OnRowCommand="gvGerenciamentoCategorias_RowCommand">
            <Columns>
                <%-- ID --%>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdTipoLivro" runat="server" Text='<%# Eval("aaaaaaaaaaa") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextIdTipoLivro" runat="server" Text="ID"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdTipoLivro" runat="server" Text='<%# Eval("aaaaaaaaaaa") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <%-- Categoria --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditNomeTipoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="15" 
                            Text='<%# Eval("aaaaaaaaaaa") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoNomeTipoLivro" runat="server" Style="text-align: center;" Text="Nome"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNomeTipoLivro" runat="server" Style="text-align: left;" Text='<%# Eval("aaaaaaaaaaa") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="900px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

               
                <%-- Botões Editar, atualizar, deletar e Livros --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success" Text="Atualizar" CausesValidation="false"
                            />&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false"/>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="btnEditarTipoLivro" runat="server" CssClass="btn btn-success" Text="Editar" CommandName="Edit" CausesValidation="false"
                            />&nbsp;
                        <asp:Button ID="btnDeletarTipoLivro" runat="server" CssClass="btn btn-danger" Text="Deletar" CommandName="Delete"
                            CausesValidation="false" />
                        <asp:Button ID="btnCarregaLivrosTipoLivro" runat="server" CssClass="btn btn-primary" Text="Livros" CommandName="CarregaLivrosTipoLivro"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" />&nbsp;
                    </ItemTemplate>
                    
                    <HeaderStyle HorizontalAlign="Right" Width="150px"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" Font-Size="14px"/>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle HorizontalAlign="Center" Wrap="True" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Center" BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Font-Size="14px"/>
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
</asp:Content>

