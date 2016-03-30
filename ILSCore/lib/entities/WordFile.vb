Namespace ilscore.lib.entities
    Public Class WordFile

        Private _Word As String
        Private _FileShowed As FileShowed
        Private _Paragraphs As String

        Public Property Paragraphs() As String
            Get
                If _Paragraphs.Length > 100 Then
                    Try
                        Dim f_char As Integer = _Paragraphs.IndexOf(_Word)
                        Dim p1 As String = _Paragraphs.Substring(0, f_char - 1)
                        Dim p2 As String = _Paragraphs.Substring(f_char, 100)
                        Dim p As String = p1 & " " & _Word & " " & p2
                        Return p
                    Catch ex As Exception
                        Return _Paragraphs
                    End Try

                Else
                    Return _Paragraphs
                End If
            End Get
            Set(ByVal value As String)
                _Paragraphs = value
            End Set
        End Property

        Public ReadOnly Property GetFirstLetter()
            Get
                Return _Word.Substring(0)
            End Get
        End Property

        Public ReadOnly Property GetTwoFirstLetters()
            Get
                Return _Word.Substring(0, 2)
            End Get
        End Property

        Public ReadOnly Property GetThreeFirstLetters()
            Get
                Return _Word.Substring(0, 3)
            End Get
        End Property

        Public Property FileRelated() As FileShowed
            Get
                Return _FileShowed

            End Get
            Set(ByVal value As FileShowed)
                _FileShowed = value
            End Set
        End Property

        Public Property Word() As String
            Get
                Return _Word
            End Get
            Set(ByVal value As String)
                _Word = value
            End Set
        End Property

    End Class
End Namespace
