Imports ILSCore.ilscore.lib.entities
Imports System.IO


Namespace ilsserver.server

    Public Class Commands
        Public hilo As System.Threading.Thread
        Private _files As FileShowedCollection
        Private _path As String

        Public WriteOnly Property SetPath() As String
            Set(ByVal value As String)
                _path = value
            End Set
        End Property

        Public WriteOnly Property SetFiles() As FileShowedCollection
            Set(ByVal value As FileShowedCollection)
                _files = value
            End Set
        End Property

        Public Sub AddPathToSearch(ByVal path As String)

            Dim o_configuration As New ilsserver.server.Configuration
            Try
                o_configuration.AddNewConfigPathToSearch(path)
            Catch ex As Exception
                Throw ex
            End Try


        End Sub

        Public Sub RemovePathToSearch(ByVal key As String)
            Dim o_configuration As New ilsserver.server.Configuration
            Try
                o_configuration.DeleteConfiguration(key)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub StartManualSearch(ByVal FilesCollected As FileShowedCollection)
            Dim l_paths As List(Of String)
            Dim o_configuration As New ilsserver.server.Configuration
            l_paths = o_configuration.GetPaths

            For Each item As String In l_paths
                Console.WriteLine("Lookin in " & item & " .....")
                Dim values() As String = item.Split("?")
                _files = FilesCollected
                _path = values(1)
                hilo = New Threading.Thread(AddressOf ProcessDirectory)
                hilo.Start()
            Next
            hilo.Join()
            Console.WriteLine("Files Found :" & _files.Count.ToString)
        End Sub

        Public Sub StopManualSearch()

        End Sub

        Public Sub SetAutomaticSearch()

        End Sub

        Public Sub StopAutomaticSearch()

        End Sub

        Public Sub ShowPaths()
            Dim l_paths As New List(Of String)
            Dim o_configuration As New ilsserver.server.Configuration
            l_paths = o_configuration.GetPaths()

            If l_paths.Count = 0 Then
                Console.WriteLine("No paths found")
            Else
                For Each item As String In l_paths
                    Console.WriteLine(item)
                Next
            End If


        End Sub

        Private Sub ProcessDirectory()
            ProcessDirectory(_files, _path)
        End Sub

        Private Sub ProcessDirectory(ByVal FilesCollected As FileShowedCollection, ByVal path As String)
            Try
                Dim FilesEntries() As String = Directory.GetFiles(path)
                Dim AcceptedExtencions() As String = ConfigurationManager.AppSettings("AcceptExtFiles").Split("|")
                For Each item As String In FilesEntries

                    Dim fileinfo As New FileInfo(item)
                    Dim validextention As Boolean = False

                    For Each i As String In AcceptedExtencions
                        If i.Equals(fileinfo.Extension) Then
                            validextention = True
                        End If
                    Next

                    If validextention Then
                        Dim filefound As New FileShowed()
                        filefound.Setfile(item)
                        FilesCollected.Add(filefound)
                    End If

                Next

                Dim subdirectoryEntries() As String = Directory.GetDirectories(path)
                For Each item As String In subdirectoryEntries
                    ProcessDirectory(FilesCollected, item)
                Next

            Catch ex As Exception
                Console.WriteLine("Error trying to read the path : " & path)
            End Try
        End Sub

        Public Sub SearchWord(ByVal word As String, ByVal WordsCollected As WordFileCollection, ByVal DictionaryToSearch As ILSCore.ilscore.lib.entities.DictionaryWords)
            For Each item As KeyValuePair(Of String, WordFileCollection) In DictionaryToSearch.WordsCollected
                Dim s_itemkey As String = item.Key
                Dim wc_itemvalue As WordFileCollection = item.Value


                'Search word in dictionary
                If s_itemkey.ToUpper.Contains(word.ToUpper) Then
                    For Each i As WordFile In wc_itemvalue.Items
                        Dim newobj As New WordFile
                        newobj.FileRelated = New FileShowed
                        newobj.FileRelated.Setfile(i.FileRelated.GetFullActualPath)
                        newobj.Word = i.Word
                        newobj.Paragraphs = i.Paragraphs
                        WordsCollected.Add(newobj)
                    Next
                End If

                'Search the word in the name of the file
                For Each i_item As WordFile In wc_itemvalue.Items
                    If i_item.FileRelated.GetFileName.ToUpper.Contains(word.ToUpper) Then
                        Dim s_newobj As New WordFile
                        s_newobj.FileRelated = New FileShowed
                        s_newobj.FileRelated.Setfile(i_item.FileRelated.GetFullActualPath)
                        s_newobj.Word = word
                        s_newobj.Paragraphs = i_item.Paragraphs
                        WordsCollected.Add(s_newobj)
                    End If
                Next

            Next
        End Sub

        Public Sub SaveXmlFile(ByVal filename As String, ByVal DictionaryToSave As ILSCore.ilscore.lib.entities.DictionaryWords)
            Try
                Dim o_xmlcontroller As New ILSCore.ilscore.lib.file.XMLControler
                o_xmlcontroller.CreateXML(filename, DictionaryToSave)
            Catch ex As Exception
                Throw ex
            End Try


        End Sub

        Public Sub LoadXMLFile(ByVal filename As String, ByVal DictionaryToSave As ILSCore.ilscore.lib.entities.DictionaryWords)
            Try
                Dim o_xmlcontroller As New ILSCore.ilscore.lib.file.XMLControler
                o_xmlcontroller.ReadXML(filename, DictionaryToSave)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub


        Public Sub reziseJPG(ByVal FilesCollected As FileShowedCollection)
            Dim _itemsaved As Integer = 0
            Console.Write(TotalofPhotos(FilesCollected) & " / ")
            For Each item As FileShowed In FilesCollected.Items
                If item.GetExtention.ToString.ToLower = ".jpg" And (item.SizeFile / 1024) > 999 Then
                    Try
                        Dim new_filename As String = item.GetFullActualPath.Replace(item.GetExtention.ToString, "-r" & item.GetExtention)
                        Dim ImageResizer As New ILSCore.ilscore.lib.file.ImageResizer
                        ImageResizer.ResizeImage(item.GetFullActualPath, new_filename, 0.5)
                        _itemsaved += 1
                        Dim backup_path As String = ConfigurationManager.AppSettings("BackupPathPhotos") & item.GetFullActualPath.Remove(0, 2)
                        If Not System.IO.Directory.Exists(Path.GetDirectoryName(backup_path)) Then
                            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(backup_path))
                        End If
                        File.Move(item.GetFullActualPath, backup_path)

                        Console.Write("")
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop)
                        Console.Write(_itemsaved)

                        If _itemsaved > 0 And _itemsaved <= 9 Then
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop)
                        End If

                        If _itemsaved > 9 And _itemsaved <= 99 Then
                            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop)
                        End If
                        If _itemsaved > 99 And _itemsaved <= 999 Then
                            Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop)
                        End If
                        If _itemsaved > 999 And _itemsaved <= 9999 Then
                            Console.SetCursorPosition(Console.CursorLeft - 4, Console.CursorTop)
                        End If
                        If _itemsaved > 9999 And _itemsaved <= 99999 Then
                            Console.SetCursorPosition(Console.CursorLeft - 5, Console.CursorTop)
                        End If
                        If _itemsaved > 99999 And _itemsaved <= 999999 Then
                            Console.SetCursorPosition(Console.CursorLeft - 6, Console.CursorTop)
                        End If
                    Catch ex As Exception
                        'Console.WriteLine("Error on file " & item.GetFullActualPath & " Error: " & ex.Message.ToString)
                    End Try


                End If
            Next
        End Sub

        Public Function TotalofPhotos(ByVal FilesCollected As FileShowedCollection) As Integer
            Dim total As Integer = 0
            For Each item As FileShowed In FilesCollected.Items
                Try
                    If item.GetExtention.ToString.ToLower = ".jpg" And (item.SizeFile / 1024) > 999 Then
                        total += 1
                    End If
                Catch ex As Exception

                End Try
            Next
            Return total
        End Function

        Public Function TotalofAVIMovies(ByVal FilesCollected As FileShowedCollection) As Integer
            Dim total As Integer = 0
            For Each item As FileShowed In FilesCollected.Items
                Try
                    If item.GetExtention.ToString.ToLower = ".avi" Then
                        total += 1
                    End If
                Catch ex As Exception

                End Try
            Next
            Return total
        End Function

        Public Function TotalMovies(ByVal FilesCollected As FileShowedCollection) As Integer
            Dim total As Integer = 0
            For Each item As FileShowed In FilesCollected.Items
                Try
                    If item.GetExtention.ToString.ToLower = ".avi" Or item.GetExtention.ToString.ToLower = ".mov" Or item.GetExtention.ToString.ToLower = ".mpg" Or
                        item.GetExtention.ToString.ToLower = ".wmv" Or item.GetExtention.ToString.ToLower = ".mts" Then
                        total += 1
                    End If
                Catch ex As Exception

                End Try
            Next
            Return total
        End Function

        Public Sub ResizeAVIMovies(ByVal FilesCollected As FileShowedCollection, Optional showlistdetail As Boolean = False)
            Dim ffmpegTool As String = ConfigurationManager.AppSettings("ffmpegToolPath").ToString
            Dim command As String = ""
            Dim totalofavis As Integer = TotalofAVIMovies(FilesCollected)
            Dim filenum As Integer = 0
            If Not ffmpegTool.Equals("") Then
                For Each item As FileShowed In FilesCollected.Items
                    If item.GetExtention.ToString.ToLower = ".avi" Then
                        filenum += 1
                        Dim outputfile As String = item.GetFullActualPath.Replace(item.GetExtention.ToString, "-r" & item.GetExtention)
                        command = ffmpegTool & " -i """ & item.GetFullActualPath & """ """ & outputfile & """"
                        If showlistdetail Then
                            Console.WriteLine("Resizing file " & filenum.ToString & " of " & totalofavis.ToString & " : " & item.GetFullActualPath)
                        End If
                        Try
                            Dim procStartInfo As New System.Diagnostics.ProcessStartInfo("cmd", "/c" & command)
                            procStartInfo.RedirectStandardOutput = True
                            procStartInfo.UseShellExecute = False
                            procStartInfo.CreateNoWindow = True
                            Dim proc As New Diagnostics.Process
                            proc.StartInfo = procStartInfo
                            proc.Start()
                            Dim result As String = proc.StandardOutput.ReadToEnd
                            Console.WriteLine(result)

                            Dim backup_path As String = ConfigurationManager.AppSettings("BackupPathPhotos") & item.GetFullActualPath.Remove(0, 2)
                            If Not System.IO.Directory.Exists(Path.GetDirectoryName(backup_path)) Then
                                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(backup_path))
                            End If
                            File.Move(item.GetFullActualPath, backup_path)
                        Catch ex As Exception
                            Console.WriteLine(ex.ToString)
                        End Try
                    End If
                Next
            End If
        End Sub

        Public Sub ResizeMovieFiles(ByVal FilesCollected As FileShowedCollection, Optional showlistdetail As Boolean = False)
            Dim ffmpegTool As String = ConfigurationManager.AppSettings("ffmpegToolPath").ToString
            Dim command As String = ""
            Dim totalofavis As Integer = TotalMovies(FilesCollected)
            Dim filenum As Integer = 0
            If Not ffmpegTool.Equals("") Then
                For Each item As FileShowed In FilesCollected.Items
                    If item.GetExtention.ToString.ToLower = ".avi" Or item.GetExtention.ToString.ToLower = ".mov" Or item.GetExtention.ToString.ToLower = ".mpg" Or
                        item.GetExtention.ToString.ToLower = ".wmv" Or item.GetExtention.ToString.ToLower = ".mts" Then
                        filenum += 1
                        Dim outputfile As String = item.GetFullActualPath.Replace(item.GetExtention.ToString, "-r.avi")
                        command = ffmpegTool & " -i """ & item.GetFullActualPath & """ """ & outputfile & """"
                        If showlistdetail Then
                            Console.WriteLine("Resizing file " & filenum.ToString & " of " & totalofavis.ToString & " : " & item.GetFullActualPath)
                        End If
                        Try
                            Dim procStartInfo As New System.Diagnostics.ProcessStartInfo("cmd", "/c" & command)
                            procStartInfo.RedirectStandardOutput = True
                            procStartInfo.UseShellExecute = False
                            procStartInfo.CreateNoWindow = True
                            Dim proc As New Diagnostics.Process
                            proc.StartInfo = procStartInfo
                            proc.Start()
                            Dim result As String = proc.StandardOutput.ReadToEnd
                            Console.WriteLine(result)

                            Dim backup_path As String = ConfigurationManager.AppSettings("BackupPathPhotos") & item.GetFullActualPath.Remove(0, 2)
                            If Not System.IO.Directory.Exists(Path.GetDirectoryName(backup_path)) Then
                                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(backup_path))
                            End If
                            File.Move(item.GetFullActualPath, backup_path)
                        Catch ex As Exception
                            Console.WriteLine(ex.ToString)
                        End Try
                    End If
                Next
            End If
        End Sub

    End Class
End Namespace