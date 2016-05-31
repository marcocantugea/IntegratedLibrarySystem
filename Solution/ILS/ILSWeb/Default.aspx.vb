Imports System.Configuration

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Dim d_MainDictionary As ILSCore.ilscore.lib.entities.DictionaryWords

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'LoadILSMemory()
        'Response.Write("Loaded Memory items: " & d_MainDictionary.WordsCollected.Count.ToString)
        'CreateTableDirectory()

        Dim IndexControl As New IndexILS
        IndexControl.maindiv = Indexdiv
        IndexControl.subindex = Subindexdiv
        IndexControl.CreateMainIndexMenu()

        Dim ctrlname As String = Page.Request.Params("__EVENTTARGET")
        If IsPostBack Then
            If ctrlname.Contains("index_") Then
                Dim v() As String = ctrlname.Split("_")
                If v(0).Equals("index") Then
                    Dim leter As String = ctrlname.Substring(ctrlname.Length - 1, 1)
                    IndexControl.CreateSubMainIndex(leter)
                ElseIf v(0).Equals("subindex") Then
                    Dim leter As String = v(1).Substring(0, 1)
                    IndexControl.CreateSubMainIndex(leter)
                End If
            End If
        Else
            If Not IsNothing(Request.QueryString("q")) Then
                Dim qval As String = Request.QueryString("q")
                If Not qval.Equals("") Then
                    TextBox1.Text = qval
                    Button1_Click(Me, Nothing)
                End If
            End If
        End If
    End Sub


    Private Sub LoadILSMemory()
        Dim _engine As New engine
        If IsNothing(d_MainDictionary) Then
            d_MainDictionary = New ILSCore.ilscore.lib.entities.DictionaryWords
            _engine.LoadILSMemory(ConfigurationManager.AppSettings("MemoryFilePath"), d_MainDictionary)
        End If

    End Sub


    Public Sub CreateTableDirectory()
        Dim sb_table As New System.Text.StringBuilder
        sb_table.Append("<table style=""undefined;table-layout: fixed; width: 100%"">")
        sb_table.Append("<colgroup>")
        sb_table.Append("<col style=""width: 272px"">")
        sb_table.Append("<col style=""width: 100%px"">")
        sb_table.Append("</colgroup>")
        sb_table.Append("<tr>")
        sb_table.Append("<th>Word</th>")
        sb_table.Append("<th>File Path</th>")
        sb_table.Append("<th>Word Found</th>")
        sb_table.Append("</tr>")

        Dim keys As List(Of String) = d_MainDictionary.WordsCollected.Keys.ToList

        keys.Sort()

        For Each Str As String In keys
            sb_table.Append("<tr>")
            sb_table.Append("<td><span style=""color: #0000ff;"">" & Str & "</span></td>")
            sb_table.Append("<td colspan=2>&nbsp;</td>")
            sb_table.Append("</tr>")
            For Each i As ILSCore.ilscore.lib.entities.WordFile In d_MainDictionary.WordsCollected(Str).Items
                sb_table.Append("<tr>")
                sb_table.Append("<td>&nbsp;</td>")
                sb_table.Append("<td>" & i.FileRelated.GetFullActualPath & "</td>")
                sb_table.Append("<td>Phrase : " & i.Paragraphs & "</td>")
                sb_table.Append("</tr>")
            Next
        Next


        'For Each item As KeyValuePair(Of String, ILSCore.ilscore.lib.entities.WordFileCollection) In d_MainDictionary.WordsCollected
        '    Dim l_items As ILSCore.ilscore.lib.entities.WordFileCollection = item.Value
        '    For Each i As ILSCore.ilscore.lib.entities.WordFile In l_items.Items
        '        sb_table.Append("<tr>")
        '        sb_table.Append("<td>" & i.Word & "</td>")
        '        sb_table.Append("<td>" & i.FileRelated.GetFullActualPath & "</td>")
        '        sb_table.Append("</tr>")
        '    Next
        'Next
        sb_table.Append("<\table>")
        displayresult.InnerHtml = sb_table.ToString
    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click


        Dim sb_table As New System.Text.StringBuilder
        sb_table.Append("<table style=""undefined;table-layout: fixed; width: 100%"">")
        sb_table.Append("<colgroup>")
        sb_table.Append("<col style=""width: 272px"">")
        sb_table.Append("<col style=""width: 100%px"">")
        sb_table.Append("</colgroup>")
        sb_table.Append("<tr>")
        sb_table.Append("<th>Word</th>")
        sb_table.Append("<th>File</th>")
        sb_table.Append("<th>Word Found </th>")
        sb_table.Append("</tr>")

        Dim _engine As New engine
        d_MainDictionary = New ILSCore.ilscore.lib.entities.DictionaryWords

        If TextBox1.Text.Length >= 3 Then

            Dim path As String = ConfigurationManager.AppSettings("MemoryFilePath") & TextBox1.Text.Substring(0, 3).ToUpper & "\" & TextBox1.Text.Substring(0, 3).ToUpper & ".xml"
            'Response.Write(path & "</ br>")
            Try
                _engine.LoadILSMemory(path, d_MainDictionary)
            Catch ex As Exception
                sb_table.Append("<tr>")
                sb_table.Append("<td colspan=""3""> No match found.<td>")
                sb_table.Append("</tr>")
            End Try

            Dim filesfound = d_MainDictionary.WordsCollected.Where(Function(x) x.Key.Contains(TextBox1.Text.ToUpper)).Select(Function(x) x.Value).ToList

            Dim collecteditems As New ILSCore.ilscore.lib.entities.WordFileCollection

            For Each i As ILSCore.ilscore.lib.entities.WordFileCollection In filesfound
                i.SortItems()

                For Each item As ILSCore.ilscore.lib.entities.WordFile In i.Items
                    collecteditems.Add(item)
                    'sb_table.Append("<tr>")
                    'sb_table.Append("<td><span style=""color: #0000ff;"">" & item.Word & "</span></td>")
                    'sb_table.Append("<td><a href=""file://" & item.FileRelated.GetFullActualPath & """>" & item.FileRelated.GetFileName & "</td>")
                    'sb_table.Append("<td>Phrase : " & item.Paragraphs & "</td>")
                    'sb_table.Append("</tr>")
                Next
            Next

            collecteditems.Items.Sort(New ILSCore.ilscore.lib.entities.WordFileSorter)
            For Each item As ILSCore.ilscore.lib.entities.WordFile In collecteditems.Items
                sb_table.Append("<tr>")
                sb_table.Append("<td><span style=""color: #0000ff;"">" & item.Word & "</span></td>")
                sb_table.Append("<td><a href=""file://" & item.FileRelated.GetFullActualPath & """>" & item.FileRelated.GetFileName & "</td>")
                sb_table.Append("<td>Phrase : " & item.Paragraphs & "</td>")
                sb_table.Append("</tr>")
            Next

            sb_table.Append("</table>")
            'Response.Write(sb_table.ToString)
            displayresult.InnerHtml = sb_table.ToString
        End If

    End Sub

    
End Class