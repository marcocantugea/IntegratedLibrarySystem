Imports GetDocText.Doc
Imports ILSCore.ilscore.lib.entities

Namespace ilscore.lib.file

    Public Class OpenerWordFile

        Private _DocFile As String
        Private _dictionary As DictionaryWords
        Private _filepath As String

        Public WriteOnly Property FilePath() As String
            Set(ByVal value As String)
                _filepath = value
            End Set
        End Property

        Public WriteOnly Property Dictionary() As DictionaryWords
            Set(ByVal value As DictionaryWords)
                _dictionary = value
            End Set
        End Property

        Public Property DocFile() As String
            Get
                Return _DocFile
            End Get
            Set(ByVal value As String)
                _DocFile = value
            End Set
        End Property

        Public Sub OpenerWordFile()
            OpenerWordFile(_dictionary, _filepath)
        End Sub

        Sub OpenerWordFile(ByVal Dictionary As DictionaryWords, ByVal FilePath As String)
            _DocFile = FilePath
            ReadFile(Dictionary, FilePath)
        End Sub

        Private Sub ReadFile(ByVal Dictionary As DictionaryWords, ByVal FilePath As String)
            Dim text As String = ""
            Dim loader As New TextLoader(FilePath)
            Dim o_wordsearchengine As New ilscore.lib.engine.WordSearchEngine
            If loader.LoadText(text) Then
                o_wordsearchengine.FindWords(Dictionary, text, FilePath)
            End If
        End Sub


    End Class
End Namespace
