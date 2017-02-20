Imports System.IO
Imports ILSCore.ilscore.lib.entities

Namespace ilscore.lib.file

    Public Class OpenerTextFile

        Private _TextFile As String
        Private _Stream As StreamReader

        Sub OpenerTextFile(ByVal Dictionary As DictionaryWords, ByVal FilePath As String)
            _TextFile = FilePath
            ReadFile(Dictionary, FilePath)
        End Sub

        Private Sub ReadFile(ByVal Dictionary As DictionaryWords, ByVal FilePath As String)
            Dim o_wordsearchengine As New ilscore.lib.engine.WordSearchEngine
            If Not _TextFile.Equals("") Then
                Try
                    _Stream = New StreamReader(_TextFile)
                    While Not _Stream.EndOfStream
                        Dim line As String = _Stream.ReadLine
                        o_wordsearchengine.FindWords(Dictionary, line, FilePath)
                    End While
                Catch ex As Exception
                    Console.WriteLine(ex)
                End Try

            End If

        End Sub



    End Class
End Namespace
