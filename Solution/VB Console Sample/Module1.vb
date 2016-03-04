Option Infer On

Imports TodoPagoConnector

Module Module1
    Dim SECURITY = "Security"
    Dim SESSION = "Session"
    Dim MERCHANT = "Merchant"
    Dim URL_OK = "URL OK"
    Dim URL_ERROR = "URL Error"
    Dim ENCODING_METHOD = "Encoding Method"

    Dim AUTHORIZATIONKEY = "AuthorizationKey"
    Dim AMOUNT = "Amount"
    Dim REQUESTCHANNEL = "requestChannel"
    Dim CURRENCYCODE = "currencyCode"

    Dim STARTDATE = "STARTDATE"
    Dim ENDDATE = "ENDDATE"
    Dim PAGENUMBER = "PAGENUMBER"

    'Requeridos para GetAuthorizeAnswer
    Dim REQUESTKEY = "RequestKey"
    Dim ANSWERKEY = "AnswerKey"

    Sub Main()
        sendAuthorizeRequest()
        getAuthorizeAnswer()
        getStatus()
        getByRangeDateTime()
        voidRequest()
        returnRequest()
        Console.Read()
    End Sub

    Private Sub sendAuthorizeRequest()

        Dim endPoint As String = "https://developers.todopago.com.ar/"
        Dim headers = New Dictionary(Of String, String)
        headers.Add("Authorization", "PRISMA f3d8b72c94ab4a06be2ef7c95490f7d3")
        Dim tpc As TPConnector = New TPConnector(endPoint, headers)

        Dim payload = New Dictionary(Of String, String)
        Dim request = New Dictionary(Of String, String)

        request.Add(SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3")
        request.Add(SESSION, "ABCDEF-1234-12221-FDE1-00000200")
        request.Add(MERCHANT, "2153")
        request.Add(URL_OK, "http:  //someurl.com/ok")
        request.Add(URL_ERROR, "http://someurl.com/fail")
        request.Add(ENCODING_METHOD, "XML")

        payload.Add("MERCHANT", "2153")
        payload.Add("OPERATIONID", "2121")
        payload.Add("CURRENCYCODE", "032")
        payload.Add("AMOUNT", "1.00")
        payload.Add("EMAILCLIENTE", "some@someurl.com")

        'Optionals
        payload.Add("AVAILABLEPAYMENTMETHODSIDS", "1#194#43#45")
        payload.Add("PUSHNOTIFYENDPOINT", "")
        payload.Add("PUSHNOTIFYMETHOD", "")
        payload.Add("PUSHNOTIFYSTATES", "")


        payload.Add("CSBTCITY", "Villa General Belgrano") 'MANDATORIO.
        payload.Add("CSBTCOUNTRY", "AR") 'MANDATORIO. Código ISO.
        payload.Add("CSBTEMAIL", "some@someurl.com") 'MANDATORIO.
        payload.Add("CSBTFIRSTNAME", "Juan") 'MANDATORIO.      
        payload.Add("CSBTLASTNAME", "Perez") 'MANDATORIO.
        payload.Add("CSBTPHONENUMBER", "541160913988") 'MANDATORIO.     
        payload.Add("CSBTPOSTALCODE", "1010") 'MANDATORIO.
        payload.Add("CSBTSTATE", "B") 'MANDATORIO
        payload.Add("CSBTSTREET1", "Some Street 2153") 'MANDATORIO.
        payload.Add("CSBTSTREET2", "") 'NO MANDATORIO

        payload.Add("CSBTCUSTOMERID", "453458") 'MANDATORIO.
        payload.Add("CSBTIPADDRESS", "192.0.0.4") 'MANDATORIO.       
        payload.Add("CSPTCURRENCY", "ARS") 'MANDATORIO.      
        payload.Add("CSPTGRANDTOTALAMOUNT", "1.00") 'MANDATORIO.        

        'retail
        payload.Add("CSSTCITY", "Villa General Belgrano") 'MANDATORIO.
        payload.Add("CSSTCOUNTRY", "AR") 'MANDATORIO. Código ISO.
        payload.Add("CSSTEMAIL", "some@someurl.com") 'MANDATORIO.
        payload.Add("CSSTFIRSTNAME", "Juan") 'MANDATORIO.      
        payload.Add("CSSTLASTNAME", "Perez") 'MANDATORIO.
        payload.Add("CSSTPHONENUMBER", "541160913988") 'MANDATORIO.     
        payload.Add("CSSTPOSTALCODE", "1010") 'MANDATORIO.
        payload.Add("CSSTSTATE", "B") 'MANDATORIO
        payload.Add("CSSTSTREET1", "Some Street 2153") 'MANDATORIO.
        payload.Add("CSSTSTREET2", "") 'NO MANDATORIO.

        payload.Add("CSITPRODUCTCODE", "electronic_good") 'CONDICIONAL
        payload.Add("CSITPRODUCTDESCRIPTION", "Prueba desde net") 'CONDICIONAL.     
        payload.Add("CSITPRODUCTNAME", "TestPrd") 'CONDICIONAL.  
        payload.Add("CSITPRODUCTSKU", "SKU1234") 'CONDICIONAL.      
        payload.Add("CSITTOTALAMOUNT", "10.01") 'CONDICIONAL.      
        payload.Add("CSITQUANTITY", "1") 'CONDICIONAL.       
        payload.Add("CSITUNITPRICE", "10.01")

        Dim res = tpc.SendAuthorizeRequest(request, payload)

        Console.WriteLine("--------------------------------------------------------------------")
        printDictionary(res)

    End Sub

    Private Sub getAuthorizeAnswer()
        Dim endPoint As String = "https://developers.todopago.com.ar/"
        Dim headers = New Dictionary(Of String, String)
        headers.Add("Authorization", "PRISMA f3d8b72c94ab4a06be2ef7c95490f7d3")
        Dim tpc As TPConnector = New TPConnector(endPoint, headers)

        Dim getAuthorizeAnswerParams = New Dictionary(Of String, String)
        getAuthorizeAnswerParams.Add(SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3")
        getAuthorizeAnswerParams.Add(SESSION, "")
        getAuthorizeAnswerParams.Add(MERCHANT, "2153")
        getAuthorizeAnswerParams.Add(REQUESTKEY, "710268a7-7688-c8bf-68c9-430107e6b9da")
        getAuthorizeAnswerParams.Add(ANSWERKEY, "693ca9cc-c940-06a4-8d96-1ab0d66f3ee6")


        Dim res = tpc.GetAuthorizeAnswer(getAuthorizeAnswerParams)

        Console.WriteLine("--------------------------------------------------------------------")
        printDictionary(res)

    End Sub

    Private Sub getByRangeDateTime()

        Dim endPoint As String = "https://developers.todopago.com.ar/"
        Dim headers = New Dictionary(Of String, String)
        headers.Add("Authorization", "PRISMA f3d8b72c94ab4a06be2ef7c95490f7d3")
        Dim tpc As TPConnector = New TPConnector(endPoint, headers)

        Dim param = New Dictionary(Of String, String)

        param.Add(MERCHANT, "2153")
        param.Add(STARTDATE, "2015-01-01")
        param.Add(ENDDATE, "2015-12-10")
        param.Add(PAGENUMBER, "1")

        Dim res = tpc.getByRangeDateTime(param)

        Console.WriteLine("--------------------------------------------------------------------")
        printDictionarys(res)

    End Sub


    Private Sub voidRequest()

        Dim endPoint As String = "https://developers.todopago.com.ar/"
        Dim headers = New Dictionary(Of String, String)
        headers.Add("Authorization", "PRISMA f3d8b72c94ab4a06be2ef7c95490f7d3")
        Dim tpc As TPConnector = New TPConnector(endPoint, headers)

        Dim param = New Dictionary(Of String, String)

        param.Add(MERCHANT, "2153")
        param.Add(SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3")
        param.Add(REQUESTKEY, "bb25d589-52bc-8e21-fc5d-47d677b0995c")

        Dim res = tpc.VoidRequest(param)

        Console.WriteLine("--------------------------------------------------------------------")
        Console.WriteLine("Result: " + res.ToString)

    End Sub

    Private Sub returnRequest()

        Dim endPoint As String = "https://developers.todopago.com.ar/"
        Dim headers = New Dictionary(Of String, String)
        headers.Add("Authorization", "PRISMA f3d8b72c94ab4a06be2ef7c95490f7d3")
        Dim tpc As TPConnector = New TPConnector(endPoint, headers)

        Dim param = New Dictionary(Of String, String)

        param.Add(MERCHANT, "2153")
        param.Add(SECURITY, "f3d8b72c94ab4a06be2ef7c95490f7d3")
        param.Add(REQUESTKEY, "bb25d589-52bc-8e21-fc5d-47d677b0995c")
        param.Add(AMOUNT, "10")

        Dim res = tpc.ReturnRequest(param)

        Console.WriteLine("--------------------------------------------------------------------")
        Console.WriteLine("Result: " + res.ToString)

    End Sub

    Private Sub getStatus()

        Dim endPoint As String = "https://developers.todopago.com.ar/"
        Dim headers = New Dictionary(Of String, String)
        headers.Add("Authorization", "PRISMA f3d8b72c94ab4a06be2ef7c95490f7d3")
        Dim tpc As TPConnector = New TPConnector(endPoint, headers)


        Dim getStatusOperationId = "02"
        Dim getStatusMerchant = "2153"

        Dim res = tpc.GetStatus(getStatusMerchant, getStatusOperationId)

        Console.WriteLine("--------------------------------------------------------------------")
        printList(res)

    End Sub

    Private Sub printDictionary(dic As Dictionary(Of String, Object))

        For Each kvp As KeyValuePair(Of String, Object) In dic
            Dim v1 As String = kvp.Key
            Dim v2 As Object = kvp.Value
            Console.WriteLine("Key: " + v1.ToString + " Value: " + v2.ToString)
        Next

    End Sub

    Private Sub printList(list As List(Of Dictionary(Of String, Object)))

        For Each ld In list

            Dim dic As Dictionary(Of String, Object) = ld

            For Each kvp As KeyValuePair(Of String, Object) In dic
                Dim v1 As String = kvp.Key
                Dim dic2 As Dictionary(Of String, String) = kvp.Value
                Console.WriteLine("opertion: " + v1.ToString)
                For Each kv2 As KeyValuePair(Of String, String) In dic2
                    Dim vk1 As String = kv2.Key
                    Dim vk2 As String = kv2.Value
                    Console.WriteLine("Key: " + vk1.ToString + " --- Value: " + vk2.ToString)
                Next
            Next
        Next

    End Sub

    Private Sub printDictionarys(list As Dictionary(Of String, Object))

        For Each kvp As KeyValuePair(Of String, Object) In list
            Dim v1 As String = kvp.Key
            Dim dic2 As Dictionary(Of String, Object) = kvp.Value
            Console.WriteLine("opertion: " + v1.ToString)
            For Each kv2 As KeyValuePair(Of String, Object) In dic2
                Dim vk1 As String = kv2.Key
                Dim vk2 As String = kv2.Value
                Console.WriteLine("Key: " + vk1.ToString + " --- Value: " + vk2.ToString)
            Next
        Next

    End Sub


End Module
