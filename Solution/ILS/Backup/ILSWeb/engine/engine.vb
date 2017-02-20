Imports ILSCore.ilscore.lib.file.XMLControler

Public Class engine

    Public Sub LoadILSMemory(ByVal filename As String, ByVal DictionaryToSave As ILSCore.ilscore.lib.entities.DictionaryWords)
        Try
            Dim o_xmlcontroller As New ILSCore.ilscore.lib.file.XMLControler
            o_xmlcontroller.ReadXML(filename, DictionaryToSave)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
