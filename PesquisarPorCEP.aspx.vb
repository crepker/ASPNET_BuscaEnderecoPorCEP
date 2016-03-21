Imports ConsultaCep

Partial Class PesquisarPorCEP
    Inherits System.Web.UI.Page

    Protected Sub ImageButtonConsultar_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButtonConsultar.Click
        Dim consulta As New DadosEndereco()
        consulta = ConsultaCep.Consulta(TextBoxCEP.Text)

        If consulta.Resultado Then
            TextBoxLogradouro.Text = consulta.Logradouro
            TextBoxEndereco.Text = consulta.Endereco
            TextBoxBairro.Text = consulta.Bairro
            TextBoxCidade.Text = consulta.Cidade
            TextBoxUF.Text = consulta.Uf

        Else
            LabelMensagem.Visible = True
            TextBoxLogradouro.Text = ""
            TextBoxEndereco.Text = ""
            TextBoxBairro.Text = ""
            TextBoxCidade.Text = ""
            TextBoxUF.Text = ""
        End If






    End Sub
End Class
