<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoEditores.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoEditores" %>

<asp:Content>
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
                    <asp:Label ID="lblCadastroUrlEditor" runat="server" Font-Size="16pt" Text="E-mail: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroUrlEditor" runat="server" CssClass="form-control" Height="35px"
                        Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxCadastroUrlEditor"
                        Style="color: red;" ErrorMessage="* Digite a Url do editor."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Button ID="btnNovoEditor" runat="server" CssClass="btn btn-success" Style="margin-top: 10px" Text="Salvar"
                        OnClick="BtnNovoEditor_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
