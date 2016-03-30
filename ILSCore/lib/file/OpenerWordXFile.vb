Imports System.IO
Imports System.Text
Imports System.Xml
Imports ILSCore.ilscore.lib.entities

Namespace ilscore.lib.file

    Public Class OpenerWordXFile

        Private _WordDoc As String
        Private _ResultText As String

        Public ReadOnly Property ReturnText() As String
            Get
                Return _ResultText
            End Get
        End Property

        Public Property File() As String
            Get
                Return _WordDoc
            End Get
            Set(ByVal value As String)
                _WordDoc = value
            End Set
        End Property

        Sub OpenerWordXFile(ByVal Dictionary As DictionaryWords, ByVal Filepath As String)
            Dim dtt As New DocxToTextDemo.DocxToText(Filepath)
            Dim o_wordsearchengine As New ilscore.lib.engine.WordSearchEngine
            _ResultText = dtt.ExtractText()
            o_wordsearchengine.FindWords(Dictionary, _ResultText, Filepath)

        End Sub



    End Class
End Namespace