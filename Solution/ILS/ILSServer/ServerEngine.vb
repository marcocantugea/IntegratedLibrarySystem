Imports ILSCore.ilscore.lib.entities

Namespace ilsserver.server


    Public Class ServerEngine

        Dim aTimer As New System.Timers.Timer
        Private _l_filesfound As FileShowedCollection

        Public Property filesfound() As FileShowedCollection
            Get
                Return _l_filesfound
            End Get
            Set(ByVal value As FileShowedCollection)
                _l_filesfound = value
            End Set
        End Property

        Public Sub ONAutomaticSearch()

        End Sub
        Public Sub OFFAutomaticSearch()

        End Sub

        Private Sub Tick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
            If Not IsNothing(_l_filesfound) Then
                Dim o_command As New Commands
                o_command.StartManualSearch(_l_filesfound)
            End If

        End Sub



    End Class
End Namespace