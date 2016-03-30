Namespace ilscore.lib.entities
    Public Class WordFileCollection
        Implements IEnumerable, IEnumerator, ICollection

        Private position As Integer = -1
        Private _Items As New List(Of WordFile)
        Private _WordValidator As New ilscore.lib.engine.WordValidator

        Public ReadOnly Property Items() As List(Of WordFile)
            Get
                Return _Items
            End Get
        End Property

        Public ReadOnly Property CurrentPosition() As Integer
            Get
                Return position
            End Get
        End Property

        Public Sub Add(ByVal Item As WordFile)
            If Not IsNothing(Item) Then
                If Not ExistWordAndFile(Item) Then
                    If _WordValidator.ValidateWord(Item.Word) Then
                        _Items.Add(Item)
                    End If
                End If

            End If
        End Sub

        Public Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo

        End Sub

        Public ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count
            Get
                Return _Items.Count
            End Get
        End Property

        Public ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
            Get
                Return Me
            End Get
        End Property

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return _Items.GetEnumerator.Current
        End Function

        Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return _Items.GetEnumerator.Current
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            position += 1
            Return (position <= _Items.Count)
        End Function

        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            position = -1
        End Sub

        Public Function ExistWordAndFile(ByVal Items As WordFile) As Boolean
            Dim exist As Boolean = False
            For Each it As WordFile In _Items
                If it.Word = Items.Word And it.FileRelated.GetFileName = Items.FileRelated.GetFileName Then
                    exist = True
                End If
            Next
            Return exist
        End Function


    End Class
End Namespace
