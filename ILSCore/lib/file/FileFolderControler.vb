
Imports System.IO
Imports ILSCore.ilscore.lib.entities

Namespace ilscore.lib.file
    Public Class FileFolderControler

        Public Sub CreateFolder(ByVal FolderNameAndPath As String)
            Try
                If Not Directory.Exists(FolderNameAndPath) Then
                    Directory.CreateDirectory(FolderNameAndPath)
                End If
            Catch ex As Exception
                Throw
            End Try
        End Sub

    End Class
End Namespace
