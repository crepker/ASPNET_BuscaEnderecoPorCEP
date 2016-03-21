<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PesquisarPorCEP.aspx.vb" Inherits="PesquisarPorCEP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Consulta de endereço por CEP</title>
    <link href="styles/style.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-2.2.1.min.js"></script>
    <script src="scripts/jquery.maskedinput.js"></script>
    <script src="scripts/main.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrap">
            <hgroup>
                <h1>Pesquisar endereço por CEP</h1>
                <h2>
                    Para pesquisar algum endereço, digite o CEP e clique no botão ao lado.
                </h2>
            </hgroup>
            <span class="recLinha">CEP:<asp:TextBox ID="TextBoxCEP" runat="server"></asp:TextBox>
            <asp:ImageButton ID="ImageButtonConsultar" ImageUrl="~/imagens/Extract object Mini.png" runat="server" /></span>

            <asp:Label ID="LabelMensagem" runat="server" ForeColor="#990000" Text="Cep não encontrado" Visible="False"></asp:Label>
            <br />
            <table id="tblDados">
<%--                <colgroup>
                    <col style="text-wrap:none;">
                    <col style="width: 100%;">
                </colgroup>--%>
                <tr>
                    <td>Tipo de logradouro: </td>
                    <td>
                        <asp:TextBox ID="TextBoxLogradouro" runat="server" CssClass="txtdados" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Endereço:</td>
                    <td>
                        <asp:TextBox ID="TextBoxEndereco" runat="server" CssClass="txtdados" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Bairro:</td>
                    <td>
                        <asp:TextBox ID="TextBoxBairro" runat="server" CssClass="txtdados" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Cidade:</td>
                    <td>
                        <asp:TextBox ID="TextBoxCidade" runat="server" CssClass="txtdados" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Estado:</td>
                    <td>
                        <asp:TextBox ID="TextBoxUF" runat="server" CssClass="txtdados" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
