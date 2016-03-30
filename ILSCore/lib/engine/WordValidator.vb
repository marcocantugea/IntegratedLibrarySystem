Imports System.Text.RegularExpressions


Namespace ilscore.lib.engine
    Public Class WordValidator

        Private _temp_word As String
        Private Function ValidateWord3orMoreLetters(ByVal word As String) As Boolean
            Dim result As Boolean = False
            If word.Length >= 3 Then
                result = True
            End If
            Return result
        End Function

        Private Function ValidateRegularCharacters(ByVal word As String) As Boolean
            Dim result As Boolean = False
            Dim _m As Match = Regex.Match(word, "^[a-zA-Z0-9\-]+$", RegexOptions.IgnoreCase)
            If _m.Success Then
                result = True
            End If
            Return result
        End Function

        Public Function ValidateWord(ByVal word As String) As Boolean
            _temp_word = word
            _temp_word = _temp_word.Trim
            _temp_word = _temp_word.Replace(vbCrLf, "")
            Dim result As Boolean = False

            If ValidateWord3orMoreLetters(_temp_word) Then
                If HasSpecialCharacters(_temp_word) Then
                    Dim s_word As String = EliminateEspecialChars(_temp_word)
                    If ValidateRegularCharacters(_temp_word) Then
                        result = True
                    End If
                Else
                    If ValidateRegularCharacters(_temp_word) Then
                        result = True
                    End If
                End If
            End If
            
            Return result
        End Function


        Public Function EliminateEspecialChars(ByVal word As String) As String
            Dim result As String = Regex.Replace(word, "[()]", "")
            Return result
        End Function

        Private Function HasSpecialCharacters(ByVal word As String) As Boolean
            Dim result As Boolean = False
            'Match(word, , RegexOptions.IgnoreCase)
            If Not Regex.IsMatch(word, "^.*[\[\]^$.|?*+()\\~`!@#%&\-_+={}'&quot;&lt;&gt;:;,\ ].*$") Then
                result = True
            End If
            Return result
        End Function

    End Class
End Namespace
