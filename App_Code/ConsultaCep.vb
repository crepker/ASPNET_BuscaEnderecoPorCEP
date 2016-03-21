Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Net
Imports System.IO


Public Class ConsultaCep

    Public Class DadosEndereco
        Private _logradouro As String
        Private _endereco As String
        Private _bairro As String
        Private _cidade As String
        Private _uf As String
        Private _cep As String
        Private _resultado As Boolean
        'Private _resultado_txt As String

        Public Sub New()
            Logradouro = ""
            Bairro = ""
            Cidade = ""
            Uf = ""
            Cep = ""
            Resultado = False
        End Sub

        Public Property Logradouro() As String
            Get
                Return _logradouro
            End Get
            Set(ByVal Valor As String)
                _logradouro = Valor
            End Set
        End Property

        Public Property Endereco() As String
            Get
                Return _endereco
            End Get
            Set(ByVal Valor As String)
                _endereco = Valor
            End Set
        End Property

        Public Property Bairro() As String
            Get
                Return _bairro
            End Get
            Set(ByVal Valor As String)
                _bairro = Valor
            End Set
        End Property

        Public Property Cidade() As String
            Get
                Return _cidade
            End Get
            Set(ByVal Valor As String)
                _cidade = Valor
            End Set
        End Property

        Public Property Uf() As String
            Get
                Return _uf
            End Get
            Set(ByVal Valor As String)
                _uf = Valor
            End Set
        End Property

        Public Property Cep() As String
            Get
                Return _cep
            End Get
            Set(ByVal Valor As String)
                _cep = Valor
            End Set
        End Property

        Public Property Resultado() As Boolean
            Get
                Return _resultado
            End Get
            Set(ByVal Valor As Boolean)
                _resultado = Valor
            End Set
        End Property
    End Class

    Public Shared Function Consulta(ByVal _CEP As String) As DadosEndereco
        Dim requisicao As WebRequest = WebRequest.Create("http://cep.republicavirtual.com.br/web_cep.php?cep=" + _CEP + "&formato=xml")
        Dim resposta As WebResponse = Nothing

        Dim listaResult As New DadosEndereco

        Dim logradouro As String = ""
        Dim endereco As String = ""
        Dim bairro As String = ""
        Dim cidade As String = ""
        Dim uf As String = ""
        Dim cep As String = ""
        Dim resultado As Boolean = False

        Dim falhaWService As Boolean = False
        Try
            resposta = requisicao.GetResponse()
        Catch ex As Exception
            falhaWService = True
        End Try

        If falhaWService Then
            Return ConsultaCorreios(_CEP)
        Else

            Dim cont As Integer
            Dim buffer(1000) As Byte
            Dim sb As New StringBuilder
            Dim temp As String

            Dim stream As Stream = resposta.GetResponseStream()

            Do
                cont = stream.Read(buffer, 0, buffer.Length)
                temp = Encoding.Default.GetString(buffer, 0, cont).Trim
                sb.Append(temp)
            Loop While cont > 0

            Dim xmlString = sb.ToString
            Dim xmlElem = XElement.Parse(xmlString)

            If xmlElem.Descendants("resultado").Value = "0" Then
                Return ConsultaCorreios(_CEP)
            ElseIf xmlElem.Descendants("resultado").Value = "1" Then
                Try
                    logradouro = xmlElem.Descendants("tipo_logradouro").Value
                    endereco = xmlElem.Descendants("logradouro").Value
                    bairro = xmlElem.Descendants("bairro").Value
                    cidade = xmlElem.Descendants("cidade").Value
                    uf = xmlElem.Descendants("uf").Value
                    resultado = True
                Catch ex As Exception
                    '
                End Try
            End If
        End If

        listaResult.Logradouro = logradouro
        listaResult.Endereco = endereco
        listaResult.Bairro = bairro
        listaResult.Cidade = cidade
        listaResult.Uf = uf
        listaResult.Cep = cep
        listaResult.Resultado = resultado

        Return listaResult
    End Function

    Shared Function ConsultaCorreios(ByVal _CEP As String) As DadosEndereco
        Dim requisicao As WebRequest = WebRequest.Create("http://www.buscacep.correios.com.br/servicos/dnec/consultaLogradouroAction.do?Metodo=listaLogradouro&CEP=" + _CEP + "&TipoConsulta=cep")
        Dim resposta As WebResponse

        Dim listaResult As New DadosEndereco

        Try
            resposta = requisicao.GetResponse()
        Catch ex As Exception
            listaResult.Resultado = False
            Return listaResult
        End Try

        Dim logradouro As String = ""
        Dim endereco As String = ""
        Dim bairro As String = ""
        Dim cidade As String = ""
        Dim uf As String = ""
        Dim cep As String = ""
        Dim resultado As Boolean = False

        Dim cont As Integer
        Dim buffer(1000) As Byte
        Dim sb As New StringBuilder
        Dim temp As String

        Dim stream As Stream = resposta.GetResponseStream()

        Do
            cont = stream.Read(buffer, 0, buffer.Length)
            temp = Encoding.Default.GetString(buffer, 0, cont).Trim
            sb.Append(temp)
        Loop While cont > 0

        Dim pagina As String = sb.ToString

        If Not pagina.IndexOf("<font color=""black"">CEP NAO ENCONTRADO</font>") > 0 Then
            Try
                logradouro = Regex.Match(pagina, "<td width=""268"" style=""padding: 2px"">(.*)</td>").Groups(1).Value
                bairro = Regex.Matches(pagina, "<td width=""140"" style=""padding: 2px"">(.*)</td>")(0).Groups(1).Value
                cidade = Regex.Matches(pagina, "<td width=""140"" style=""padding: 2px"">(.*)</td>")(1).Groups(1).Value
                uf = Regex.Match(pagina, "<td width=""25"" style=""padding: 2px"">(.*)</td>").Groups(1).Value
            Catch ex As Exception
                '
            End Try
        End If
        Dim aux As String() = logradouro.Split(" ")
        For i = 0 To aux.Count - 1
            If i = 0 Then
                logradouro = aux(i)
            Else
                endereco += " " + aux(i)
            End If
        Next
        listaResult.Logradouro = logradouro
        listaResult.Endereco = endereco.TrimStart
        listaResult.Bairro = bairro
        listaResult.Cidade = cidade
        listaResult.Uf = uf
        listaResult.Cep = cep
        listaResult.Resultado = resultado

        Return listaResult
    End Function

End Class
