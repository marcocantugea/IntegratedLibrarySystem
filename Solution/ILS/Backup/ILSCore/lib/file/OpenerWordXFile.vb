Imports System.IO
Imports System.Text
Imports System.Xml
Imports ILSCore.ilscore.lib.entities

Namespace ilscore.lib.file

    Public Class OpenerWordXFile

        Private _WordDoc As String
        Private _ResultText As String
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

        Sub OpenerWordXFile()
            Try
                OpenerWordXFile(_dictionary, _filepath)
            Catch ex As Exception
                Console.WriteLine("Error Reading file : " & _filepath)
            End Try

        End Sub
        Sub OpenerWordXFile(ByVal Dictionary As DictionaryWords, ByVal Filepath As String)
            Try
                Dim dtt As New DocxToTextDemo.DocxToText(Filepath)
                Dim o_wordsearchengine As New ilscore.lib.engine.WordSearchEngine
                _ResultText = dtt.ExtractText()
                o_wordsearchengine.FindWords(Dictionary, _ResultText, Filepath)

            Catch ex As Exception
                Console.WriteLine("Error Reading file : " & Filepath)
            End Try

        End Sub



    End Class
End Namespace