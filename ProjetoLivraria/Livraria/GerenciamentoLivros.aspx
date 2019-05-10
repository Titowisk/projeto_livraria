<%@ Page Title="Gerenciamento de Livros" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoLivros.aspx.cs"
     Inherits="ProjetoLivraria.Livraria.GerenciamentoLivros" %>

<asp:Content ID="GerenciamentoLivros" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left;">
        <table style="width: 100%;">
            <tr style="display: grid;">
                <%-- Titulo Livro --%>
                <td>
                    <asp:Label ID="lblCadastroTituloLivro" Text="Título: " runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroTituloLivro" runat="server" CssClass="form-control" 
                        Height="35px" Width="400px"/>
                    <asp:RequiredFieldValidator ErrorMessage="Digite o título do livro!" ControlToValidate="tbxCadastroTituloLivro" runat="server" 
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
                        <asp:ListItem Selected="True" Value="" Text="-" />
                        <%--<asp:ListItem Value="IdDaCategoria" Text="categoria2" />--%>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Escolha uma categoria!" ControlToValidate="ddlCadastroCategoriaLivro" runat="server" 
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
                        <asp:ListItem Selected="True" Value="" Text="-" />
                        <%--<asp:ListItem Text="editor1" />
                        <asp:ListItem Text="editor2" />--%>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Escolha um editor para o livro!" ControlToValidate="ddlCadastroEditorLivro" runat="server" 
                        Style="color: red;"/>
                </td>
                
            </tr>
             <tr style="display: grid;">
                <%-- Autor Livro DropDown List --%>
                <td>
                    <asp:Label ID="lblCadastroAutorLivro" Text="Autor: " runat="server" />
                </td>
                <td>
                    <%-- carregar categorias existentes aqui quando a página carregar --%>
                    <asp:DropDownList ID="ddlCadastroAutorLivro" runat="server">
                        <asp:ListItem Selected="True" Value="" Text="-" />
                        <%--<asp:ListItem Text="autor1" />
                        <asp:ListItem Text="autor2" />--%>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="Escolha um Autor para o livro!" ControlToValidate="ddlCadastroAutorLivro" runat="server" 
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
    <div class="row">
        <h2 style="text-align: center;">Lista de Livros Cadastrados</h2>
        <asp:GridView ID="gvGerenciamentoLivros" Width="100%" AutoGenerateColumns="false" Font-Size="14px" CellPadding="4" 
            ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoLivros_RowCancelingEdit" 
            OnRowEditing="gvGerenciamentoLivros_RowEditing" OnRowUpdating="gvGerenciamentoLivros_RowUpdating"
            OnRowDeleting="gvGerenciamentoLivros_RowDeleting" OnRowCommand="gvGerenciamentoLivros_RowCommand" runat="server">
            <Columns>
                <asp:TemplateField Visible="false">
                    <%-- ID livro --%>
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdLivro" Text='<%# Eval("liv_id_livro") %>' runat="server" />
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextIdLivro" runat="server" Text="ID"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdLivro" runat="server" Text='<%# Eval("liv_id_livro") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <%-- Titulo Livro --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditTituloLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="20" 
                            Text='<%# Eval("liv_nm_titulo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoTituloLivro" runat="server" Style="text-align: center;" Text="Título"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTituloLivro" runat="server" Style="text-align: left;" Text='<%# Eval("liv_nm_titulo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <%-- Id TipoLivro (oculto) --%>

                <%-- DropDownList Categoria (TipoLivro) --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEditCategoriaLivro" runat="server"  CssClass="form-control" Height="35px" MaxLength="20">
                            <%-- serão carregadas opções com texto= nome da categoria e valor = id da categoria --%>
                            <%--<asp:ListItem Text="Cat1" />
                            <asp:ListItem Text="Cat2" />--%>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoCategoriaLivro" Style="text-align: center;" Text="Categoria" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCategoriaLivro" Text='<%# Eval("liv_id_tipo_livro") %>' runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

                <%-- Id Editor (oculto) --%>

                <%-- DropDownList Editor --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEditEditorLivro" runat="server"  CssClass="form-control" Height="35px" MaxLength="20">
                            <%-- serão carregadas opções com texto= nome da categoria e valor = id da categoria --%>
                            <%--<asp:ListItem Text="Cat1" />
                            <asp:ListItem Text="Cat2" />--%>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoEditorLivro" Style="text-align: center;" Text="Editor" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEditorLivro" Text='<%# Eval("liv_id_editor") %>' runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

                <%-- Id Autor (oculto) --%>

                <%-- DropDownList Autor --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEditAutorLivro" runat="server"  CssClass="form-control" Height="35px" MaxLength="20">
                            <%-- serão carregadas opções com texto= nome da categoria e valor = id da categoria --%>
                            <%--<asp:ListItem Text="Cat1" />
                            <asp:ListItem Text="Cat2" />--%>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoAutorLivro" Style="text-align: center;" Text="Autor" runat="server" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAutorLivro" Text='ToDo' runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

                <%-- Preço --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditPrecoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="20" 
                            Text='<%# Eval("liv_vl_preco") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoPrecoLivro" runat="server" Style="text-align: center;" Text="Preço"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPrecoLivro" runat="server" Style="text-align: left;" Text='<%# Eval("liv_vl_preco") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

                <%-- PC Royalty --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditRoyaltyLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="20" 
                            Text='<%# Eval("liv_pc_royalty") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoRoyaltyLivro" runat="server" Style="text-align: center;" Text="Royalties"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRoyaltyLivro" runat="server" Style="text-align: left;" Text='<%# Eval("liv_pc_royalty") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

                <%-- Resumo --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditResumoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="20" 
                            Text='<%# Eval("liv_ds_resumo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoResumoLivro" runat="server" Style="text-align: center;" Text="Resumo"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblResumoLivro" runat="server" Style="text-align: left;" Text='<%# Eval("liv_ds_resumo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="300px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

                <%-- Numero Edição --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditNumeroEdicaoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="20" 
                            Text='<%# Eval("liv_nu_edicao") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoNumeroEdicaoLivro" runat="server" Style="text-align: center;" Text="Nº Edição"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNumeroEdicaoLivro" runat="server" Style="text-align: left;" Text='<%# Eval("liv_nu_edicao") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success" Text="Atualizar"
                            CausesValidation="false"/>
                        <asp:Button Text="Cancelar" ID="btnCancelar" CommandName="Cancel" CssClass="btn btn-danger" runat="server" 
                            CausesValidation="false"/>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button Text="Editar" ID="Button1" CommandName="Edit" CausesValidation="false" runat="server" CssClass="btn btn-success"/>
                        <asp:Button Text="Deletar" ID="btnDeletarLivro" CommandName="Delete" CausesValidation="false" runat="server" CssClass="btn btn-danger"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
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


