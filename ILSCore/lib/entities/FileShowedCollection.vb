Namespace ilscore.lib.entities


    Public Class FileShowedCollection
        Implements IEnumerable, IEnumerator, ICollection

        Private position As Integer = -1
        Private _Items As New List(Of FileShowed)

        Public ReadOnly Property Items() As List(Of FileShowed)
            Get
                Return _Items
            End Get
        End Property

        Public ReadOnly Property CurrentPosition() As Integer
            Get
                Return position
            End Get
        End Property

        Public Sub Add(ByVal Item As FileShowed)
            If Not IsNothing(Item) Then
                _Items.Add(Item)
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
    End Class
End Namespace