Option Infer On

Imports TodoPagoConnector
Imports TodoPagoConnector.Utils
Imports TodoPagoConnector.Model
Imports TodoPagoConnector.Exceptions
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security

Module Module1

    Sub Main()
        Console.WriteLine("init TodoPagoConnectorSample")

        Dim tpcs As New TodoPagoConnectorSample()

        'Console.WriteLine("----------------------------------------------------------------------");
        'Console.WriteLine("getCredentials");
        'tpcs.getCredentials();

        Console.WriteLine("----------------------------------------------------------------------")
        Console.WriteLine("GetEndpointForm")
        Dim endpointForm As String = tpcs.GetEndpointForm()
        Console.WriteLine("Response: {0}", endpointForm)

        Console.WriteLine("------------------------------------------------------------------------")
        Console.WriteLine("initSendAuthorizeRequestParams")
        tpcs.initSendAuthorizeRequestParams()
        Console.WriteLine("Call SendAuthorizeRequest")
        tpcs.sendAuthorizeRequest()

        Console.WriteLine("------------------------------------------------------------------------")
        Console.WriteLine("initGetAuthorizeAnswer")
        tpcs.initGetAuthorizeAnswer()
        Console.WriteLine("sendGetAuthorizeAnswer")
        tpcs.sendGetAuthorizeAnswer()

        Console.WriteLine("------------------------------------------------------------------------")
        Console.WriteLine("initGetStatus")
        tpcs.initGetStatus()
        Console.WriteLine("sendGetStatus")
        tpcs.sendGetStatus()

        Console.WriteLine("------------------------------------------------------------------------")
        Console.WriteLine("PaymentMethods")
        tpcs.getAllPaymentMethods()

        Console.WriteLine("------------------------------------------------------------------------")
        Console.WriteLine("VoidRequest")
        tpcs.voidRequest()

        Console.WriteLine("------------------------------------------------------------------------")
        Console.WriteLine("ReturnRequest")
        tpcs.returnRequest()

        'Console.WriteLine("----------------------------------------------------------------------");
        'Console.WriteLine("initGetByRangeDateTime");
        'tpcs.getByRangeDateTime();

        Console.Read()
    End Sub

    Private Class TodoPagoConnectorSample

        'Connector
        Private connector As TPConnector

        'SendAuthorizeRequest
        Private sendAuthorizeRequestParams As New Dictionary(Of String, String)()

        Private sendAuthorizeRequestPayload As New Dictionary(Of String, String)()

        'GetAuthorizeAnswer
        Private getAuthorizeAnswerParams As New Dictionary(Of String, String)()

        'GetStatus
        Private getStatusMerchant As String

        Private getStatusOperationId As String

        'Authentification and Endpoint
        Private authorization As String = "PRISMA f3d8b72c94ab4a06be2ef7c95490f7d3"

        'Constructor
        Public Sub New()
            'connector = initConnectorForCredetials();
            connector = initConnector()
        End Sub

        Private Function initConnector() As TPConnector
            Dim headers = New Dictionary(Of [String], [String])()
            headers.Add("Authorization", authorization)

            'Override SSL security - must be removed on PRD
            ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(AddressOf ValidateCertificate)

            Return New TPConnector(TPConnector.developerEndpoint, headers)
        End Function

        Private Function initConnectorForCredetials() As TPConnector
            Return New TPConnector(TPConnector.developerEndpoint)
        End Function

        Public Sub initSendAuthorizeRequestParams()
            sendAuthorizeRequestParams.Add(ElementNames.SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3")
            sendAuthorizeRequestParams.Add(ElementNames.SESSION, "ABCDEF-1234-12221-FDE1-00000200")
            sendAuthorizeRequestParams.Add(ElementNames.MERCHANT, "2153")
            sendAuthorizeRequestParams.Add(ElementNames.URL_OK, "http://someurl.com/ok")
            sendAuthorizeRequestParams.Add(ElementNames.URL_ERROR, "http://someurl.com/fail")
            sendAuthorizeRequestParams.Add(ElementNames.ENCODING_METHOD, "XML")

            sendAuthorizeRequestPayload.Add(ElementNames.MERCHANT, "2153")
            sendAuthorizeRequestPayload.Add(ElementNames.OPERATIONID, "2121")
            sendAuthorizeRequestPayload.Add(ElementNames.CURRENCYCODE, "032")
            sendAuthorizeRequestPayload.Add(ElementNames.AMOUNT, "1.00")
            sendAuthorizeRequestPayload.Add(ElementNames.EMAILCLIENTE, "email_cliente@dominio.com")
            sendAuthorizeRequestPayload.Add(ElementNames.MAXINSTALLMENTS, "12")
            'NO MANDATORIO, MAXIMA CANTIDAD DE CUOTAS, VALOR MAXIMO 12
            sendAuthorizeRequestPayload.Add(ElementNames.MININSTALLMENTS, "3")
            'NO MANDATORIO, MINIMA CANTIDAD DE CUOTAS, VALOR MINIMO 1
            sendAuthorizeRequestPayload.Add(ElementNames.TIMEOUT, "300000")
            'NO MANDATORIO, TIEMPO DE ESPERA DE 300000 (5 minutos) a 21600000 (6hs)

            sendAuthorizeRequestPayload.Add("CSBTCITY", "Villa General Belgrano")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTCOUNTRY", "AR")
            'MANDATORIO. Código ISO.
            sendAuthorizeRequestPayload.Add("CSBTEMAIL", "todopago@hotmail.com")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTFIRSTNAME", "Juan")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTLASTNAME", "Perez")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTPHONENUMBER", "541161988")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTPOSTALCODE", "1010")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTSTATE", "B")
            'MANDATORIO
            sendAuthorizeRequestPayload.Add("CSBTSTREET1", "Cerrito 740")
            'MANDATORIO.
            'sendAuthorizeRequestPayload.Add("CSBTSTREET2", "");//NO MANDATORIO
            sendAuthorizeRequestPayload.Add("CSBTCUSTOMERID", "453458")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSBTIPADDRESS", "192.0.0.4")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSPTCURRENCY", "ARS")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSPTGRANDTOTALAMOUNT", "1.00")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD6", "")
            'NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD7", "")
            'NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD8", "")
            'NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD9", "")
            'NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD10", "")
            'NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD11", "")
            'NO MANDATORIO.
            'retail
            sendAuthorizeRequestPayload.Add("CSSTCITY", "Villa General Belgrano")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSSTCOUNTRY", "AR")
            'MANDATORIO. Código ISO.
            sendAuthorizeRequestPayload.Add("CSSTEMAIL", "todopago@hotmail.com")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSSTFIRSTNAME", "Juan")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSSTLASTNAME", "Perez")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSSTPHONENUMBER", "541160913988")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSSTPOSTALCODE", "1010")
            'MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSSTSTATE", "B")
            'MANDATORIO
            sendAuthorizeRequestPayload.Add("CSSTSTREET1", "Cerrito 740")
            'MANDATORIO.
            'sendAuthorizeRequestPayload.Add("CSSTSTREET2", "");//NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSITPRODUCTCODE", "electronic_good#electronic_good#electronic_good#electronic_good")
            'CONDICIONAL
            sendAuthorizeRequestPayload.Add("CSITPRODUCTDESCRIPTION", "Prueba desde net#Prueba desde net#Prueba desde net#tttt")
            'CONDICIONAL.
            sendAuthorizeRequestPayload.Add("CSITPRODUCTNAME", "netsdk#netsdk#netsdk#netsdk")
            'CONDICIONAL.
            sendAuthorizeRequestPayload.Add("CSITPRODUCTSKU", "nsdk123#nsdk123#nsdk123#nsdk123")
            'CONDICIONAL.
            sendAuthorizeRequestPayload.Add("CSITTOTALAMOUNT", "1.00#1.00#1.00#1.00")
            'CONDICIONAL.
            sendAuthorizeRequestPayload.Add("CSITQUANTITY", "1#1#1#1")
            'CONDICIONAL.
            sendAuthorizeRequestPayload.Add("CSITUNITPRICE", "1.00#1.00#1.00#1.00")

            sendAuthorizeRequestPayload.Add("CSMDD12", "")
            'NO MADATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD13", "")
            'NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD14", "")
            'NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD15", "")
            'NO MANDATORIO.
            sendAuthorizeRequestPayload.Add("CSMDD16", "")
            'NO MANDATORIO.
        End Sub

        Public Sub sendAuthorizeRequest()
            Dim output As String = ""
            Try
                Dim res = connector.SendAuthorizeRequest(sendAuthorizeRequestParams, sendAuthorizeRequestPayload)


                'string response = res["StatusCode"].ToString() + "-" + res["StatusMessage"].ToString();
                'string detail = "URL_Request = " + res["URL_Request"] + "\r\nRequestKey = " + res["RequestKey"] + "\r\nPublicRequestKey = " + res["PublicRequestKey"];

                ' output += "\r\n- " + res["StatusCode"].ToString();
                ' output += "\r\n- " + res["StatusMessage"].ToString();

                ' output += "\r\n- URL_Request = " + res["URL_Request"];
                'output += "\r\n- RequestKey = " + res["RequestKey"];
                'output += "\r\n- PublicRequestKey = " + res["PublicRequestKey"];

                'Console.WriteLine(response);
                'Console.WriteLine(detail);
                printDictionary(res, "")
            Catch ex As WebException
                If ex.Status = WebExceptionStatus.ProtocolError Then
                    Dim resp As WebResponse = ex.Response
                    Using sr As New StreamReader(resp.GetResponseStream())
                        output += vbCr & vbLf + sr.ReadToEnd() + " - " + ex.Message
                    End Using
                End If
            Catch ex As Exception
                output += vbCr & vbLf + ex.Message
            End Try

            Console.WriteLine(output)
        End Sub

        Public Sub initGetAuthorizeAnswer()
            getAuthorizeAnswerParams.Add(ElementNames.SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3")
            getAuthorizeAnswerParams.Add(ElementNames.SESSION, Nothing)
            getAuthorizeAnswerParams.Add(ElementNames.MERCHANT, "2153")
            getAuthorizeAnswerParams.Add(ElementNames.REQUESTKEY, "710268a7-7688-c8bf-68c9-430107e6b9da")
            getAuthorizeAnswerParams.Add(ElementNames.ANSWERKEY, "693ca9cc-c940-06a4-8d96-1ab0d66f3ee6")
        End Sub

        Public Sub sendGetAuthorizeAnswer()
            Dim output As String = ""
            Try
                Dim res = connector.GetAuthorizeAnswer(getAuthorizeAnswerParams)

                For Each key In res.Keys
                    Console.WriteLine("- " + key + ": " + Convert.ToString(res(key)))

                    If key.Equals("Payload") Then
                        Dim aux As System.Xml.XmlNode() = DirectCast(res("Payload"), System.Xml.XmlNode())
                        If aux IsNot Nothing Then
                            For i As Integer = 0 To aux.Count() - 1
                                Dim inner As System.Xml.XmlNodeList = aux(i).ChildNodes
                                For j As Integer = 0 To inner.Count - 1
                                    Console.WriteLine("     " + inner.Item(j).Name + " : " + inner.Item(j).InnerText)
                                Next
                            Next
                        End If
                    End If
                Next
            Catch ex As WebException
                If ex.Status = WebExceptionStatus.ProtocolError Then
                    Dim resp As WebResponse = ex.Response
                    Using sr As New StreamReader(resp.GetResponseStream())
                        'Response.Write(sr.ReadToEnd());
                        output += vbCr & vbLf + sr.ReadToEnd() + vbCr & vbLf + ex.Message
                    End Using
                End If
            Catch ex As Exception
                output += vbCr & vbLf + ex.Message + vbCr & vbLf + ex.InnerException.Message + vbCr & vbLf + ex.HelpLink
            End Try

            Console.WriteLine(output)
        End Sub

        Public Sub initGetStatus()
            getStatusOperationId = "8000"
            getStatusMerchant = "2658"
        End Sub

        Public Sub sendGetStatus()
            Dim res As New List(Of Dictionary(Of String, Object))()

            Try
                res = connector.GetStatus(getStatusMerchant, getStatusOperationId)
            Catch ex As ResponseException
                Console.WriteLine(ex.Message)
            End Try

            For i As Integer = 0 To res.Count - 1
                Dim dic As Dictionary(Of String, Object) = res(i)
                For Each aux As Dictionary(Of String, Object) In dic.Values
                    For Each k As String In aux.Keys
                        If aux(k).[GetType]().IsInstanceOfType(aux) Then
                            Dim a As Dictionary(Of String, Object) = DirectCast(aux(k), Dictionary(Of String, Object))
                            Console.WriteLine((Convert.ToString("- ") & k) + ": ")
                            For Each aux2 As Dictionary(Of String, Object) In a.Values
                                Console.WriteLine("- REFUND: ")
                                For Each b As String In aux2.Keys

                                    Console.WriteLine((Convert.ToString("- ") & b) + ": " + aux2(b))
                                Next
                            Next
                        Else
                            Console.WriteLine((Convert.ToString("- ") & k) + ": " + aux(k))
                        End If
                    Next
                Next
            Next
        End Sub

        Public Sub getAllPaymentMethods()
            Dim res As Dictionary(Of String, Object) = connector.GetAllPaymentMethods("2153")
            printDictionary(res, "")
        End Sub

        Public Sub voidRequest()
            Dim gbrdt As New Dictionary(Of String, String)()

            gbrdt.Add(ElementNames.MERCHANT, "2153")
            gbrdt.Add(ElementNames.SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3")
            gbrdt.Add(ElementNames.REQUESTKEY, "bb25d589-52bc-8e21-fc5d-47d677b0995c")

            Dim res As Dictionary(Of String, Object) = connector.VoidRequest(gbrdt)
            printDictionary(res, "")
        End Sub

        Public Sub returnRequest()
            Dim gbrdt As New Dictionary(Of String, String)()

            gbrdt.Add(ElementNames.MERCHANT, "2153")
            gbrdt.Add(ElementNames.SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3")
            gbrdt.Add(ElementNames.REQUESTKEY, "0db2e848-b0ab-6baf-f9e1-b70a82ed5844")
            gbrdt.Add(ElementNames.AMOUNT, "10")

            Dim res As Dictionary(Of String, Object) = connector.ReturnRequest(gbrdt)
            printDictionary(res, "")
        End Sub

        Public Sub getByRangeDateTime()
            Dim gbrdt As New Dictionary(Of String, String)()

            gbrdt.Add(ElementNames.MERCHANT, "2153")
            gbrdt.Add(ElementNames.STARTDATE, "2015-01-01")
            gbrdt.Add(ElementNames.ENDDATE, "2015-12-10")
            gbrdt.Add(ElementNames.PAGENUMBER, "1")

            Dim res As Dictionary(Of String, [Object]) = connector.getByRangeDateTime(gbrdt)
            printDictionary(res, "")
        End Sub


        Public Sub getCredentials()
            Dim user As New User()

            Try
                user = connector.getCredentials(getUser())
                connector.setAuthorize(user.getApiKey())
            Catch ex As EmptyFieldException
                Console.WriteLine(ex.Message)
            Catch ex As ResponseException
                Console.WriteLine(ex.Message)
            End Try
            Console.WriteLine(user.toString())
        End Sub

        Private Function getUser() As User
            Dim mail As [String] = "test@Test.com.ar"
            Dim pass As [String] = "pass1234"
            Dim user As New User(mail, pass)
            Return user
        End Function


        'Utils

        ''' <summary>
        ''' Permite emular la validación del Certificado SSL devolviendo true siempre
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="certificate"></param>
        ''' <param name="chain"></param>
        ''' <param name="sslPolicyErrors"></param>
        ''' <returns>bool true</returns>
        Private Function ValidateCertificate(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
            Return True
        End Function

        Public Function GetEndpointForm() As String
            Return connector.GetEndpointForm()
        End Function

        Private Sub printDictionary(p As Dictionary(Of String, Object), tab As String)
            For Each k As String In p.Keys
                If p(k) IsNot Nothing AndAlso p(k).[GetType]().ToString().Contains("System.Collections.Generic.Dictionary") Then
                    '.ToString().Contains("string"))
                    Console.WriteLine(Convert.ToString(tab & Convert.ToString("- ")) & k)
                    Dim n As Dictionary(Of String, Object) = DirectCast(p(k), Dictionary(Of String, Object))
                    printDictionary(n, tab & Convert.ToString("  "))
                Else
                    Console.WriteLine((Convert.ToString(tab & Convert.ToString("- ")) & k) + ": " + Convert.ToString(p(k)))
                End If
            Next
        End Sub
    End Class
End Module
