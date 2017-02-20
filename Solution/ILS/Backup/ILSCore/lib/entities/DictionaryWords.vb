
Namespace ilscore.lib.entities
Public Class DictionaryWords

        Private _DictionaryWords As New Dictionary(Of String, WordFileCollection)
        Private _WordValidator As New ilscore.lib.engine.WordValidator

        Public Property WordsCollected() As Dictionary(Of String, WordFileCollection)
            Get
                Return _DictionaryWords

            End Get
            Set(ByVal value As Dictionary(Of String, WordFileCollection))
                _DictionaryWords = value
            End Set
        End Property


        Public Sub AddItem(ByVal word As String, ByVal wordfile As WordFile)

            If _WordValidator.ValidateWord(word) Then
                If _DictionaryWords.ContainsKey(word) Then
                    _DictionaryWords(word).Add(wordfile)
                Else
                    Dim o_wordfilecol As New WordFileCollection
                    o_wordfilecol.Add(wordfile)
                    _DictionaryWords.Add(word, o_wordfilecol)
                End If
            End If

        End Sub

        Public Sub AddItem(ByVal word As String)
            If Not _DictionaryWords.ContainsKey(word) Then
                _DictionaryWords.Add(word, New WordFileCollection)
            End If
        End Sub



    End Class
End Namespace
