Namespace ilscore.lib.entities
    Public Class WordFile

        Private _Word As String
        Private _FileShowed As FileShowed
        Private _Paragraphs As String
        Private _size_paragrhap As Integer = 150

        Public Property Paragraphs() As String
            Get
                If _Paragraphs.Length > _size_paragrhap Then
                    Try
                        Dim rgx As New System.Text.RegularExpressions.Regex("[^a-zA-Z ]")
                        Dim f_char As Integer = _Paragraphs.IndexOf(_Word)

                        Dim p1 = rgx.Replace(_Paragraphs, " ")
                        Dim p As String

                        If f_char <= 15 Then
                            If f_char = 0 Then
                                p = p1.Substring(f_char, _size_paragrhap)
                            Else
                                p = Strings.Mid(p1, f_char, _size_paragrhap)
                            End If

                        Else
                            p = Strings.Mid(p1, f_char - 15, _size_paragrhap)
                        End If

                        Return p
                    Catch ex As Exception
                        Console.WriteLine("Error pharagraph: " & ex.Message)
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
