Imports ILSCore.ilscore.lib.entities
Imports System.IO

Module Module1

    Sub Main()
        Dim o_commands As New ilsserver.server.Commands
        Dim doprogram As Boolean = True
        Dim l_filesfound As New FileShowedCollection
        Dim d_MainDictionary As New ILSCore.ilscore.lib.entities.DictionaryWords
        Dim threat_opener As System.Threading.Thread

        While doprogram = True
            Dim command As String = Console.ReadLine()

            If command = "help" Then
                Console.WriteLine("Commands available")
                Console.WriteLine("showpaths          Show the paths that are configure to search.")
                Console.WriteLine("addpath            Add path to search.")
                Console.WriteLine("startsearch        Star server to search files.")
                Console.WriteLine("showfilelist       show the list of files in memory.")
                Console.WriteLine("showfilestotal     show the total of files in memory.")
                Console.WriteLine("lookinfiles        start manually the search of words in documents.")
                Console.WriteLine("findword           find a word in the memory index")
                Console.WriteLine("countwords         count the total of words in the index")
                Console.WriteLine("savememory         save the actual state of the index")
                Console.WriteLine("loadsavedmemory    load in memory the index saved")
                Console.WriteLine("exit               quit the ILC server.")
                Console.WriteLine(" ")
                Console.WriteLine(" ")
            End If
            If command = "showpaths" Then
                o_commands.ShowPaths()
            End If

            If command.Contains("addpath") Then
                If command.Contains("""") Then
                    Dim v_command() As String
                    v_command = command.Split("""")
                    Dim o_command As New ilsserver.server.Commands
                    o_command.AddPathToSearch(v_command(1))

                Else
                    Console.WriteLine("Command to register a new path to search files")
                    Console.WriteLine("how to use")
                    Console.WriteLine(" ")
                    Console.WriteLine("addpath ""C:\Documents and Settings\Administrator\Desktop""")
                    Console.WriteLine(" ")
                    Console.WriteLine(" ")
                End If
            End If

            If command.Equals("removepath") Then
                Dim o_configuration As New ilsserver.server.Configuration
                Dim opt As Integer = 0
                Dim a_optiones() As String

                If o_configuration.GetPaths.Count > 0 Then
                    Console.WriteLine("Select the option to erase.")
                    Console.WriteLine("")
                    ReDim a_optiones(o_configuration.GetPaths.Count)
                    For Each item As String In o_configuration.GetPaths
                        Console.WriteLine(opt.ToString & " - " & item)
                        a_optiones(opt) = item
                        opt = opt + 1
                    Next
                    Console.WriteLine("")
                    Console.WriteLine("")
                    Console.Write("option: ")
                    Dim selec_opt As String = Console.ReadLine
                    If IsNumeric(selec_opt) Then
                        Dim i_opt As Integer = Integer.Parse(selec_opt)
                        If i_opt <= opt Then
                            Console.WriteLine("Are you sure to delete the path :" & a_optiones(selec_opt))
                            Console.Write("Type Yes to confirm :")
                            Dim confirm As String = Console.ReadLine
                            If confirm.ToUpper.Equals("YES") Then
                                Dim values() As String = a_optiones(selec_opt).Split("?")
                                Try
                                    o_commands.RemovePathToSearch(values(0))
                                    Console.WriteLine("Path erased.")
                                    Console.WriteLine("")
                                    Console.WriteLine("")
                                Catch ex As Exception
                                    Console.WriteLine("Error : " & ex.Message)
                                End Try
                            Else
                                Console.WriteLine("Operation cancelled")
                                Console.WriteLine("")
                                Console.WriteLine("")
                            End If
                            'Console.WriteLine("erase option option " & a_optiones(selec_opt))
                        Else
                            Console.WriteLine("Invalid option, try the command again.")
                        End If
                    Else
                        Console.WriteLine("Invalid option, try the command again.")
                    End If
                Else
                    Console.WriteLine("No paths found")
                End If

               
            End If

            If command.Contains("startsearch") Then

                Dim o_command As New ilsserver.server.Commands
                o_command.StartManualSearch(l_filesfound)
                'Console.WriteLine("Files Found : " & l_filesfound.Count.ToString)
                Console.WriteLine("")
                Console.WriteLine("")
            End If


            If command.Contains("showfilelist") Then
                For Each item As FileShowed In l_filesfound.Items
                    Console.WriteLine(item.GetFileName)
                Next
            End If
            If command.Contains("showfilestotal") Then
                Console.WriteLine("files in memory : " & l_filesfound.Items.Count)
            End If


            If command.Contains("lookinfiles") Then
                Dim o_t As New ILSCore.ilscore.lib.file.OpenerTextFile
                Dim o_w As New ILSCore.ilscore.lib.file.OpenerWordFile
                Dim o_wx As New ILSCore.ilscore.lib.file.OpenerWordXFile
                Dim o_pdft As New ILSCore.ilscore.lib.file.OpenerPDFText

                If Not command.Contains("""") Then
                    Dim count_processedfiles As Integer = 0
                    Console.WriteLine("Reading Files please wait..")
                    For Each item As ILSCore.ilscore.lib.entities.FileShowed In l_filesfound.Items
                        If item.GetExtention.Equals(".doc") Then
                            'create threat
                            o_w.Dictionary = d_MainDictionary
                            o_w.FilePath = item.GetFullActualPath
                            threat_opener = New Threading.Thread(AddressOf o_w.OpenerWordFile)
                            threat_opener.Start()
                            threat_opener.Join()

                            'o_w.OpenerWordFile(d_MainDictionary, item.GetFullActualPath)
                        End If
                        If item.GetExtention.Equals(".docx") Then
                            Try
                                'create threat
                                o_wx.Dictionary = d_MainDictionary
                                o_wx.FilePath = item.GetFullActualPath
                                threat_opener = New Threading.Thread(AddressOf o_wx.OpenerWordXFile)
                                threat_opener.Start()
                                threat_opener.Join()

                                'o_wx.OpenerWordXFile(d_MainDictionary, item.GetFullActualPath)
                            Catch ex As Exception
                                Console.WriteLine("Error Reading file : " & item.GetFileName)
                            End Try

                        End If
                        If item.GetExtention.Equals(".pdf") Then
                            Try
                                Console.WriteLine("Opening : " & item.GetFileName)
                                o_pdft.Dictionary = d_MainDictionary
                                o_pdft.FilePath = item.GetFullActualPath
                                threat_opener = New Threading.Thread(AddressOf o_pdft.OpenPDFTxt)
                                threat_opener.Start()
                                threat_opener.Join()

                                'o_pdft.OpenPDFTxt(d_MainDictionary, item.GetFullActualPath)

                            Catch ex As Exception
                                Console.WriteLine("Error Reading file : " & item.GetFileName)
                            End Try
                        End If
                        count_processedfiles = count_processedfiles + 1
                    Next
                    Console.WriteLine("Files Processed: " & count_processedfiles.ToString)
                Else

                    Dim v_command() As String
                    Dim b_vallidparam As Boolean = False
                    v_command = command.Split("""")
                    'valid the command parameter
                    If v_command(1).Contains(".") Then
                        b_vallidparam = True
                    End If
                    Dim AcceptedExtencions() As String = ConfigurationManager.AppSettings("AcceptExtFiles").Split("|")
                    For Each i As String In AcceptedExtencions
                        If i.Equals(v_command(1)) Then
                            b_vallidparam = True
                        End If
                    Next
                    Dim count_processedfiles As Integer = 0
                    If b_vallidparam Then
                        Console.WriteLine("Reading Files please wait..")
                        For Each item As ILSCore.ilscore.lib.entities.FileShowed In l_filesfound.Items
                            If item.GetExtention.Equals(v_command(1)) Then
                                If item.GetExtention.Equals(".doc") Then
                                    'create threat
                                    o_w.Dictionary = d_MainDictionary
                                    o_w.FilePath = item.GetFullActualPath
                                    threat_opener = New Threading.Thread(AddressOf o_w.OpenerWordFile)
                                    threat_opener.Start()
                                    'o_w.OpenerWordFile(d_MainDictionary, item.GetFullActualPath)
                                End If
                                If item.GetExtention.Equals(".docx") Then
                                    Try
                                        'create threat
                                        o_wx.Dictionary = d_MainDictionary
                                        o_wx.FilePath = item.GetFullActualPath
                                        threat_opener = New Threading.Thread(AddressOf o_wx.OpenerWordXFile)
                                        threat_opener.Start()
                                        'o_wx.OpenerWordXFile(d_MainDictionary, item.GetFullActualPath)
                                    Catch ex As Exception
                                        Console.WriteLine("Error Reading file : " & item.GetFileName)
                                    End Try

                                End If
                                If item.GetExtention.Equals(".pdf") Then
                                    Try
                                        Console.WriteLine("Opening : " & item.GetFileName)
                                        o_pdft.Dictionary = d_MainDictionary
                                        o_pdft.FilePath = item.GetFullActualPath
                                        threat_opener = New Threading.Thread(AddressOf o_pdft.OpenPDFTxt)
                                        threat_opener.Start()
                                        threat_opener.Join()

                                        'o_pdft.OpenPDFTxt(d_MainDictionary, item.GetFullActualPath)

                                    Catch ex As Exception
                                        Console.WriteLine("Error Reading file : " & item.GetFileName)
                                    End Try
                                End If

                                count_processedfiles = count_processedfiles + 1
                            End If
                        Next
                    End If

                    For Each item As KeyValuePair(Of String, WordFileCollection) In d_MainDictionary.WordsCollected
                        'Console.WriteLine("Word -- " & item.Key)
                        Dim v_item As WordFileCollection = item.Value
                        If v_item.Count > 0 Then
                            'For Each sitem As WordFile In v_item.Items
                            'Console.WriteLine("       word= " & sitem.Word)
                            'Next
                        End If
                    Next

                    Console.WriteLine("Files Processed: " & count_processedfiles.ToString)
                End If
            End If


            If command.Contains("findword") Then
                Dim wc_wordscollected As New WordFileCollection

                If Not command.Contains("""") Then
                Else
                    Dim v_command() As String
                    v_command = command.Split("""")
                    o_commands.SearchWord(v_command(1), wc_wordscollected, d_MainDictionary)

                    For Each item As ILSCore.ilscore.lib.entities.WordFile In wc_wordscollected.Items
                        Console.WriteLine("WORD : " & item.Word & "  FILE:" & item.FileRelated.GetFullActualPath)
                        Console.WriteLine("Paragraph")
                        Console.WriteLine(item.Paragraphs)

                    Next

                    If wc_wordscollected.Count = 0 Then
                        Console.WriteLine("No Matchs Found")
                        Console.WriteLine("")
                        Console.WriteLine("")
                    End If

                End If
            End If

            'If command.Contains("savememory") Then
            '    Try
            '        Console.WriteLine("Saving memory please wait...")
            '        o_commands.SaveXmlFile(ConfigurationManager.AppSettings("MemoryFilePath"), d_MainDictionary)
            '        Console.WriteLine("Memory saved.")
            '    Catch ex As Exception
            '        Console.WriteLine("Error: " & ex.Message.ToString)
            '    End Try
            'End If

            If command.Contains("savememory") Then
                Try
                    Console.WriteLine("Saving memory please wait...")
                    'o_commands.SaveXmlFile(ConfigurationManager.AppSettings("MemoryFilePath"), d_MainDictionary)
                    Dim Saveengine As New ILSCore.ilscore.lib.engine.SaveEngine
                    Saveengine.SaveMemory(d_MainDictionary)
                    Console.WriteLine("Memory saved.")
                Catch ex As Exception
                    Console.WriteLine("Error: " & ex.Message.ToString)
                End Try
            End If

            If command.Contains("loadsavedmemory") Then
                Try
                    Console.WriteLine("Loading saved memory please wait...")
                    o_commands.LoadXMLFile(ConfigurationManager.AppSettings("MemoryFilePath"), d_MainDictionary)
                    Console.WriteLine("Memory loaded successfully")
                Catch ex As Exception
                    Console.WriteLine("Error: " & ex.Message.ToString)
                End Try
            End If

            If command.Contains("countwords") Then
                Console.WriteLine(d_MainDictionary.WordsCollected.Count)
            End If

            If command = "exit" Then
                doprogram = False
            End If

            If command.Contains("resizephotos") Then
                Console.WriteLine("Resizing Photos .........")
                Console.Write("Total Of Photos resized.......:")
                o_commands.reziseJPG(l_filesfound)
                Console.WriteLine("")
                Console.WriteLine("Resizing Process Finished")
            End If


        End While
    End Sub

End Module
