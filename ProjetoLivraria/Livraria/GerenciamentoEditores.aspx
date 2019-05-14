<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoEditores.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoEditores" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="ContentEditores">
    <div class="row" style="text-align: left;">
        <h2>Cadastro Novo Editor</h2>
        <table>
            <tr style="display: grid;">
                <%-- Nome Editor --%>
                <td>
                    <asp:Label ID="lblCadastroNomeEditor" runat="server" Font-size="16pt" Text="Nome: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroNomeEditor" runat="server" CssClass="form-control" Height="35px"
                        Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCadastroNomeEditor"
                        Style="color: red;" ErrorMessage="* Digite o nome do editor."></asp:RequiredFieldValidator>
                </td>
                <%-- Email Editor --%>
                <td>
                    <asp:Label ID="lblCadastroEmailEditor" runat="server" Font-Size="16pt" Text="E-mail: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroEmailEditor" runat="server" CssClass="form-control" Height="35px"
                        Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbxCadastroEmailEditor"
                        Style="color: red;" ErrorMessage="* Digite o E-mail do editor."></asp:RequiredFieldValidator>
                </td>
                <%-- URL Editor --%>
                <td>
                    <asp:Label ID="lblCadastroUrlEditor" runat="server" Font-Size="16pt" Text="Url: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroUrlEditor" runat="server" CssClass="form-control" Height="35px"
                        Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxCadastroUrlEditor"
                        Style="color: red;" ErrorMessage="* Digite a Url do editor."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Button ID="btnNovoEditor" runat="server" CssClass="btn btn-success" Style="margin-top: 10px" Text="Salvar"
                        OnClick="btnNovoEditor_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="row">
        <h2 style="text-align: center">Lista de Editores Cadastrados</h2>
        <asp:GridView ID="gvGerenciamentoEditores" runat="server" Width="100%" AutoGenerateColumns="False" Font-Size="14px" CellPadding="4" 
            ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoEditores_RowCancelingEdit" 
            OnRowEditing="gvGerenciamentoEditores_RowEditing" OnRowUpdating="gvGerenciamentoEditores_RowUpdating" 
            OnRowDeleting="gvGerenciamentoEditores_RowDeleting" OnRowCommand="gvGerenciamentoEditores_RowCommand">
            <Columns>
                <%-- Id Editor --%>
                <asp:TemplateField Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditIdEditor" runat="server" Text='<%# Eval("EDI_ID_EDITOR") %>'></asp:Label>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextIdEditor" runat="server" Text="ID"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdEditor" runat="server" Text='<%# Eval("EDI_ID_EDITOR") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <%-- Nome Editor --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditNomeEditor" runat="server" CssClass="form-control" Height="35px" MaxLength="15" 
                            Text='<%# Eval("EDI_NM_EDITOR") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoNomeEditor" runat="server" Style="text-align: center;" Text="Nome"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNomeEditor" runat="server" Style="text-align: left;" Text='<%# Eval("EDI_NM_EDITOR") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <%-- Email Editor --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditEmailEditor" runat="server" CssClass="form-control" Height="35px" MaxLength="50" 
                            Text='<%# Eval("EDI_DS_EMAIL") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoEmailEditor" runat="server" Style="text-align: center;" Text="E-mail"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEmailEditor" runat="server" Style="text-align: center;" Text='<%# Eval("EDI_DS_EMAIL") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="450px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <%-- Url Editor --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbxEditUrlEditor" runat="server" CssClass="form-control" Height="35px" MaxLength="45" 
                            Text='<%# Eval("EDI_DS_URL") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lblTextoUrlEditor" runat="server" Style="text-align: center;" Text="Url"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUrlEditor" runat="server" Style="text-align: center;" Text='<%# Eval("EDI_DS_URL") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="400px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <%-- Botões da parte de editar --%>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success" Text="Atualizar" CausesValidation="false"
                            />&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false"/>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="btnEditarEditor" runat="server" CssClass="btn btn-success" Text="Editar" CommandName="Edit" CausesValidation="false"
                            />&nbsp;
                        <asp:Button ID="btnDeletarEditor" runat="server" CssClass="btn btn-danger" Text="Deletar" CommandName="Delete"
                            CausesValidation="false" />
                        <asp:Button ID="btnCarregaLivrosEditor" runat="server" CssClass="btn btn-primary" Text="Livros" CommandName="CarregaLivrosEditor"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" />&nbsp;
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
