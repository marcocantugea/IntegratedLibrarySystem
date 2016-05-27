Imports System.Drawing
Imports System.Drawing.Dimension2D

Namespace ilscore.lib.file
Public Class ImageResizer

        Public Sub ResizeImage(ByVal file As String, ByVal newfilename As String, ByVal percentreduce As Double)
            Dim picfile As New Bitmap(file)
            Dim width As Integer = picfile.Width - (picfile.Width * percentreduce)
            Dim height As Integer = picfile.Height - (picfile.Height * percentreduce)

            Dim newpic As New Bitmap(width, height)
            Dim g As Graphics = Graphics.FromImage(newpic)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(picfile, New Rectangle(0, 0, width, height), New Rectangle(0, 0, picfile.Width, picfile.Height), GraphicsUnit.Pixel)
            g.Dispose()
            picfile.Dispose()

            newpic.Save(newfilename, System.Drawing.Imaging.ImageFormat.Jpeg)
            newpic.Dispose()

        End Sub


    End Class
End Namespace
