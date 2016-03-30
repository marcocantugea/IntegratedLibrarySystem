Imports ILSCore.ilscore.lib.entities
Imports System.IO

Namespace ilsserver.server


    Public Class Commands

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
                ProcessDirectory(FilesCollected, values(1))
            Next
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

    End Class
End Namespace