Imports PdfToText
Imports System.IO
Imports ILSCore.ilscore.lib.entities

Namespace ilscore.lib.file
    Public Class OpenerPDFText

        Private _TextFile As String
        Public _temp_file_created As String
        Private _Stream As StreamReader
        Private Sub ConvertPDFToText(ByVal pdf_file As String, ByVal temp_file As String)
            Try
                Dim pdfParse As New PDFParser
                Dim result As String = pdfParse.ExtractText(pdf_file, temp_file)
            Catch ex As Exception
                Throw
            End Try
        End Sub


        Public Sub OpenPDFTxt(ByVal Dictionary As DictionaryWords, ByVal FilePath As String)
            _TextFile = FilePath
            'Create a temporary file
            Dim temp_file As String = System.IO.Path.GetTempFileName()
            _temp_file_created = temp_file
            Try
                ConvertPDFToText(FilePath, temp_file)
                ReadFile(Dictionary, FilePath, _temp_file_created)

            Catch ex As Exception
                Throw
            End Try
        End Sub

        Private Sub ReadFile(ByVal Dictionary As DictionaryWords, ByVal FilePath As String, ByVal TempFilePath As String)
            Dim o_wordsearchengine As New ilscore.lib.engine.WordSearchEngine
            If Not _TextFile.Equals("") Then
                Try
                    _Stream = New StreamReader(TempFilePath)
                    While Not _Stream.EndOfStream
                        Dim line As String = _Stream.ReadLine
                        o_wordsearchengine.FindWords(Dictionary, line, FilePath)
                    End While
                Catch ex As Exception
                    Throw
                End Try
            End If

        End Sub

    End Class
End Namespace
