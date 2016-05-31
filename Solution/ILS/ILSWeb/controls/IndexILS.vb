
Imports ILSCore.ilscore.lib.entities
Imports ILSCore.ilscore.lib.file
Imports System.IO

Public Class IndexILS
    Inherits System.Web.UI.Page

    Public maindiv As HtmlGenericControl
    Public subindex As HtmlGenericControl
    Public subindexlast As HtmlGenericControl

    Public Sub CreateMainIndexMenu()
        Dim filescollected As New FileShowedCollection
        Dim indexm As New Dictionary(Of String, Dictionary(Of String, String))
        Dim table As New HtmlTable
        table.ID = "tableindexcontrol"
        'get all files from memory
        ProcessDirectory(filescollected, ConfigurationManager.AppSettings("MemoryFilePath"))
        For Each item As FileShowed In filescollected.Items
            If Not indexm.ContainsKey(item.GetFileName.Substring(0, 1)) Then
                Dim subindex As String = item.GetFileName.Substring(0, 2)
                Dim lastindex As String = item.GetFileName.Substring(0, 3)
                Dim subindexdic As New Dictionary(Of String, String)
                subindexdic.Add(subindex, lastindex)
                indexm.Add(item.GetFileName.Substring(0, 1), subindexdic)
            End If
        Next

        Dim col As Integer = 0
        Dim tablerow As HtmlTableRow
        For Each item As KeyValuePair(Of String, Dictionary(Of String, String)) In indexm
            If col = 0 Then
                tablerow = New HtmlTableRow
            End If
            Dim tablecell As New HtmlTableCell
            Dim linkbtn As New LinkButton
            linkbtn.ID = "index_" & item.Key
            linkbtn.Text = item.Key
            linkbtn.Attributes.Add("AutoPostback", "true")
            linkbtn.Attributes.Add("runat", "server")
            'AddHandler linkbtn.Click, AddressOf LinkButton1_Click
            tablecell.Controls.Add(linkbtn)
            tablerow.Cells.Add(tablecell)
            If col = 7 Then
                table.Rows.Add(tablerow)
                tablerow = Nothing
                col = 0
            Else
                col += 1
            End If
        Next
        maindiv.Controls.Add(table)
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim linka As LinkButton = sender
        CreateSubMainIndex(linka.Text)
    End Sub
    Protected Sub SearchtextSelected(ByVal sender As Object, ByVal e As EventArgs)
        Dim linkselected As LinkButton = sender
        linkselected.Page.Response.Redirect("Default.aspx?q=" & linkselected.Text)
    End Sub

    Public Sub CreateSubMainIndex(ByVal leter As String)
        Dim filescollected As New FileShowedCollection
        Dim indexm As New Dictionary(Of String, List(Of String))
        Dim table As New HtmlTable
        table.ID = "tablesubindexcontrol"
        ProcessDirectory(filescollected, ConfigurationManager.AppSettings("MemoryFilePath"))
        For Each item As FileShowed In filescollected.Items
            If Not indexm.ContainsKey(item.GetFileName.Substring(0, 1)) Then
                'Dim subindex As String = item.GetFileName.Substring(0, 2)
                Dim lastindex As String = item.GetFileName.Substring(0, 3)
                Dim subindexdic As New List(Of String)
                subindexdic.Add(lastindex)
                indexm.Add(item.GetFileName.Substring(0, 1), subindexdic)
            Else
                Dim lastindex As String = item.GetFileName.Substring(0, 3)
                indexm.Item(item.GetFileName.Substring(0, 1)).Add(lastindex)
            End If
        Next
        Dim col As Integer = 0
        Dim tablerow As HtmlTableRow
        For Each item As KeyValuePair(Of String, List(Of String)) In indexm
            If item.Key.Equals(leter) Then

                Dim value As List(Of String)
                value = item.Value
                For Each i As String In value
                    Dim v As String = i
                    If col = 0 Then
                        tablerow = New HtmlTableRow
                    End If

                    Dim tablecell As New HtmlTableCell
                    Dim linkbtn As New LinkButton
                    linkbtn.ID = "subindex_" & v
                    linkbtn.Text = v
                    linkbtn.Attributes.Add("AutoPostback", "true")
                    linkbtn.Attributes.Add("runat", "server")
                    AddHandler linkbtn.Click, AddressOf SearchtextSelected
                    tablecell.Controls.Add(linkbtn)
                    tablerow.Cells.Add(tablecell)

                    If col = 15 Then
                        table.Rows.Add(tablerow)
                        tablerow = Nothing
                        col = 0
                    Else
                        col += 1
                    End If
                Next
            End If

        Next
        If Not IsNothing(tablerow) Then
            If tablerow.Cells.Count <= 15 Then
                For i = 0 To 15 - tablerow.Cells.Count
                    Dim tablecell As New HtmlTableCell
                    tablecell.InnerHtml = ""
                    tablerow.Cells.Add(tablecell)
                Next
                table.Rows.Add(tablerow)
            End If
        End If

        subindex.Controls.Add(table)
    End Sub

    Private Sub ProcessDirectory(ByVal FilesCollected As FileShowedCollection, ByVal path As String)
        Try
            Dim FilesEntries() As String = Directory.GetFiles(path)
            For Each item As String In FilesEntries

                Dim fileinfo As New FileInfo(item)
                Dim validextention As Boolean = False

                If fileinfo.Extension = ".xml" Then
                    validextention = True
                End If

                If validextention Then
                    Dim filefound As New FileShowed()
                    filefound.Setfile(item)
                    FilesCollected.Add(filefound)
                End If

            Next

            Dim subdirectoryEntries() As String = Directory.GetDirectories(path)
            For Each item As String In subdirectoryEntries
                ProcessDirectory(FilesCollected, item)
            Next

        Catch ex As Exception
            Console.WriteLine("Error trying to read the path : " & path)
        End Try
    End Sub

End Class
