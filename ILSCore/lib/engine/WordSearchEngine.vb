Imports ILSCore.ilscore.lib.entities


Namespace ilscore.lib.engine

    Public Class WordSearchEngine

        Private _Text As String

        Public Sub FindWords(ByVal Dictorianry As DictionaryWords, ByVal Sentence As String, ByVal FilePath As String)

            Dim words() As String
            Dim space() As Char = {" "c}

            words = Sentence.Split(space)

            For Each word As String In words

                Dim b_validcarac As Boolean = False
                If System.Text.RegularExpressions.Regex.IsMatch(word, "([A-Z]|.|\?)") Then
                    b_validcarac = True
                End If

                If b_validcarac Then
                    Dim o_wordfile As New WordFile
                    o_wordfile.Word = word
                    o_wordfile.FileRelated = New FileShowed()
                    o_wordfile.FileRelated.Setfile(FilePath)
                    o_wordfile.Paragraphs = Sentence
                    Dictorianry.AddItem(word, o_wordfile)
                    'If Dictorianry.WordsCollected.ContainsKey(word) Then
                    '    Dim o_wordfile As New WordFile
                    '    o_wordfile.Word = word
                    '    o_wordfile.FileRelated = New FileShowed()
                    '    o_wordfile.FileRelated.Setfile(FilePath)
                    '    Dictorianry.AddItem(word, o_wordfile)

                    'Else
                    '    Dim o_wordcollected As New WordFileCollection
                    '    Dim o_wordfile As New WordFile
                    '    o_wordfile.Word = word
                    '    o_wordfile.FileRelated = New FileShowed()
                    '    o_wordfile.FileRelated.Setfile(FilePath)
                    '    o_wordcollected.Add(o_wordfile)

                    '    Dictorianry.AddItem(word, o_wordfile)
                    'End If
                End If

            Next

        End Sub


    End Class
End Namespace