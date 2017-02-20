Namespace ilscore.lib.entities
    Public Class WordFileSorter
        Implements System.Collections.Generic.IComparer(Of WordFile)

        Public Function Compare(ByVal x As WordFile, ByVal y As WordFile) As Integer Implements System.Collections.Generic.IComparer(Of WordFile).Compare
            Return String.Compare(x.Word, y.Word, True)
        End Function
    End Class
End Namespace
