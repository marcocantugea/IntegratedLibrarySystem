Imports System.IO


Namespace ilscore.lib.entities

    Public Class FileShowed

        Private _FileProp As FileInfo

        Public Property FileProp() As FileInfo
            Get
                Return _FileProp

            End Get
            Set(ByVal value As FileInfo)
                _FileProp = value
            End Set
        End Property

        Public ReadOnly Property SizeFile() As Long
            Get
                Return _FileProp.Length
            End Get
        End Property

        Public ReadOnly Property GetFileName() As String
            Get
                Return _FileProp.Name
            End Get
        End Property

        Public ReadOnly Property GetFullActualPath() As String
            Get
                Return _FileProp.FullName
            End Get
        End Property

        Public ReadOnly Property GetExtention()
            Get
                Return _FileProp.Extension
            End Get
        End Property

        Public Sub Setfile(ByVal Path As String)
            _FileProp = New FileInfo(Path)
        End Sub

    End Class
End Namespace
