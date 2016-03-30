Imports System.Configuration

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Dim d_MainDictionary As ILSCore.ilscore.lib.entities.DictionaryWords

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'LoadILSMemory()
        'Response.Write("Loaded Memory items: " & d_MainDictionary.WordsCollected.Count.ToString)
        'CreateTableDirectory()
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
        sb_table.Append("<th colspan=2>File Path</th>")
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
        Response.Write(sb_table.ToString)
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
        sb_table.Append("<th colspan=2>File Path</th>")
        sb_table.Append("</tr>")

        Dim _engine As New engine
        d_MainDictionary = New ILSCore.ilscore.lib.entities.DictionaryWords

        Dim path As String = ConfigurationManager.AppSettings("MemoryFilePath") & TextBox1.Text.Substring(0, 2).ToUpper & "\" & TextBox1.Text.Substring(0, 2).ToUpper & ".xml"
        'Response.Write(path & "</ br>")
        _engine.LoadILSMemory(path, d_MainDictionary)

        Dim filesfound = d_MainDictionary.WordsCollected.Where(Function(x) x.Key.Contains(TextBox1.Text.ToUpper)).Select(Function(x) x.Value).ToList

        For Each i As ILSCore.ilscore.lib.entities.WordFileCollection In filesfound

            For Each item As ILSCore.ilscore.lib.entities.WordFile In i.Items
                sb_table.Append("<tr>")
                sb_table.Append("<td><span style=""color: #0000ff;"">" & item.Word & "</span></td>")
                sb_table.Append("<td>" & item.FileRelated.GetFullActualPath & "</td>")
                sb_table.Append("<td>Phrase : " & item.Paragraphs & "</td>")
                sb_table.Append("</tr>")
            Next
        Next

        sb_table.Append("</table>")
        Response.Write(sb_table.ToString)


    End Sub
End Class