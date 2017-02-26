Imports ILSCore.ilscore.lib.entities
Imports System.IO

Module Module1
    Private o_commands As New ilsserver.server.Commands
    Private l_filesfound As New FileShowedCollection
    Private d_MainDictionary As New ILSCore.ilscore.lib.entities.DictionaryWords
    Sub Main()

        Dim doprogram As Boolean = True

        Dim _tigered_command As Boolean = False

        Dim arguments As String() = Environment.GetCommandLineArgs()

        If arguments.Length <= 1 Then
            Console.WriteLine("> Starting ILS Server. ")
            Console.WriteLine("> Type Help for view the commands.")
            While doprogram = True
                Dim command As String = Console.ReadLine()

                If command = "help" Then
                    _tigered_command = True
                    cmd_help()
                End If
                If command = "showpaths" Then
                    cmd_ShowPaths()
                    _tigered_command = True
                End If

                If command.Contains("addpath") Then
                    _tigered_command = True
                    cmd_addpath(command)
                End If

                If command.Equals("removepath") Then
                    _tigered_command = True
                    cmd_removepath()
                End If

                If command.Equals("removeallpaths") Then
                    _tigered_command = True
                    cmd_removeallpaths()
                End If

                If command.Contains("startsearch") Then
                    _tigered_command = True
                    cmd_startsearch()
                End If


                If command.Contains("showfilelist") Then
                    _tigered_command = True
                    cmd_showfilelist()
                End If

                If command.Contains("showfilestotal") Then
                    _tigered_command = True
                    cmd_showfilestotal()
                End If


                If command.Contains("lookinfiles") Then
                    _tigered_command = True
                    cmd_lookinfiles()
                End If


                If command.Contains("findword") Then
                    _tigered_command = True
                    cmd_findword(command)
                End If

                If command.Contains("savememory") Then
                    _tigered_command = True
                    cmd_savememory()
                End If

                If command.Contains("loadsavedmemory") Then
                    _tigered_command = True
                    cmd_loadsavedmemory()
                End If

                If command.Contains("countwords") Then
                    _tigered_command = True
                    cmd_countwords()
                End If

                If command = "exit" Then
                    _tigered_command = True
                    doprogram = False
                End If

                If command.Contains("resizephotos") Then
                    _tigered_command = True
                    cmd_resizephotos()
                End If

                If command.Contains("resizeavimovies") Then
                    _tigered_command = True
                    cmd_resizeavimovies()
                End If

                If command.Contains("resizemovies") Then
                    _tigered_command = True
                    cmd_resizemovies()
                End If

                If command.Contains("clearfilelist") Then
                    _tigered_command = True
                    cmd_clearfilelist()
                End If

                If command.Contains("addfile") Then
                    _tigered_command = True
                    cmd_addfile(command)
                End If

                If Not _tigered_command Then
                    Console.WriteLine("Command not found, type help for more info.")
                Else
                    _tigered_command = False
                End If

            End While
        Else
            If arguments.Contains("-c") Then
                'obtienes el comando
                Dim command As String = arguments(2)

                If command = "help" Then
                    _tigered_command = True
                    cmd_help()
                End If
                If command = "showpaths" Then
                    cmd_ShowPaths()
                    _tigered_command = True
                End If

                If command.Contains("addpath") Then
                    _tigered_command = True
                    cmd_addpath("""" & arguments(3) & """")
                End If

                If command.Equals("removepath") Then
                    _tigered_command = True
                    cmd_removepath()
                End If

                If command.Equals("removeallpaths") Then
                    _tigered_command = True
                    cmd_removeallpaths()
                End If

                If command.Contains("startsearch") Then
                    _tigered_command = True
                    cmd_startsearch()
                End If


                If command.Contains("showfilelist") Then
                    _tigered_command = True
                    cmd_showfilelist()
                End If

                If command.Contains("showfilestotal") Then
                    _tigered_command = True
                    cmd_showfilestotal()
                End If


                If command.Contains("lookinfiles") Then
                    _tigered_command = True
                    cmd_lookinfiles()
                End If


                If command.Contains("findword") Then
                    _tigered_command = True
                    cmd_findword("""" & arguments(3) & """")
                End If

                If command.Contains("savememory") Then
                    _tigered_command = True
                    cmd_savememory()
                End If

                If command.Contains("loadsavedmemory") Then
                    _tigered_command = True
                    cmd_loadsavedmemory()
                End If

                If command.Contains("countwords") Then
                    _tigered_command = True
                    cmd_countwords()
                End If

                If command = "exit" Then
                    _tigered_command = True
                    doprogram = False
                End If

                If command.Contains("resizephotos") Then
                    _tigered_command = True
                    cmd_resizephotos()
                End If

                If command.Contains("resizeavimovies") Then
                    _tigered_command = True
                    cmd_resizeavimovies()
                End If

                If command.Contains("resizemovies") Then
                    _tigered_command = True
                    cmd_resizemovies()
                End If

                If command.Contains("clearfilelist") Then
                    _tigered_command = True
                    cmd_clearfilelist()
                End If

                If command.Contains("addfile") Then
                    _tigered_command = True
                    cmd_addfile("""" & arguments(3) & """")
                End If

                If command.Contains("resizephoto") Then
                    _tigered_command = True
                    cmd_addfile("""" & arguments(3) & """")
                    cmd_resizephotos()
                End If

                If Not _tigered_command Then
                    Console.WriteLine("Command not found, type help for more info.")
                Else
                    _tigered_command = False
                End If

            End If
        End If
    End Sub

    'Command Help
    'Display the app help
    Private Sub cmd_help()
        Console.WriteLine("ILS Server, helps to find files and save into a memory for indexing, also help to reduce size of photos and movies.")
        Console.WriteLine("Commands available")
        Console.WriteLine("")
        Console.WriteLine("showpaths          Show the paths that are configure to search.")
        Console.WriteLine("addpath            Add path to search.")
        Console.WriteLine("addfile            Add file to the memory.")
        Console.WriteLine("removepath         Remove configured path.")
        Console.WriteLine("removeallpaths     Removes All configured paths.")
        Console.WriteLine("startsearch        Star server to search files.")
        Console.WriteLine("showfilelist       show the list of files in memory.")
        Console.WriteLine("showfilestotal     show the total of files in memory.")
        Console.WriteLine("clearfilelist      clear the list of files in memory.")
        Console.WriteLine("lookinfiles        start manually the search of words in documents.")
        Console.WriteLine("findword           find a word in the memory index")
        Console.WriteLine("countwords         count the total of words in the index")
        Console.WriteLine("savememory         save the actual state of the index")
        Console.WriteLine("loadsavedmemory    load in memory the index saved")
        Console.WriteLine("resizephotos       resize all images found on the actual list")
        Console.WriteLine("resizeavimovies    create a small version of a avi file")
        Console.WriteLine("resizemovies       create a small version of movies files with selected extention")
        Console.WriteLine("exit               quit the ILS server.")
        Console.WriteLine(" ")
        Console.WriteLine(" ")
    End Sub

    'Command showpaths
    'Display the configured paths of the app.
    Private Sub cmd_ShowPaths()
        o_commands.ShowPaths()
    End Sub
    'Command addpath
    'Add a path into the configuration file to be browse it.
    Private Sub cmd_addpath(argument As String)
        If argument.Contains("""") Then
            Dim v_command() As String
            v_command = argument.Split("""")
            Dim o_command As New ilsserver.server.Commands
            o_command.AddPathToSearch(v_command(1))
            Console.WriteLine("> Path Added.")
            Console.WriteLine(">")
        Else
            Console.WriteLine("Command to register a new path to search files")
            Console.WriteLine("how to use")
            Console.WriteLine(" ")
            Console.WriteLine("addpath ""C:\Documents and Settings\Administrator\Desktop""")
            Console.WriteLine(" ")
            Console.WriteLine(" ")
        End If
    End Sub

    'Command removepath
    'Removes a single configured path from the configuration file.
    Private Sub cmd_removepath()
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
    End Sub

    'Command removeallpaths
    'Removes all the collected paths from the configuration file.
    Private Sub cmd_removeallpaths()
        Dim o_configuration As New ilsserver.server.Configuration
        If o_configuration.GetPaths.Count > 0 Then
            For Each item As String In o_configuration.GetPaths
                Dim values() As String = item.Split("?")
                Console.WriteLine("> Removing Path : " & item)
                o_commands.RemovePathToSearch(values(0))
            Next
            Console.WriteLine("> Paths List Clear.")
            Console.WriteLine("")
        Else
            Console.WriteLine("> No Paths to Remove.")
        End If
    End Sub


    'Command startsearch
    'Star the engine to search files that are allowed in the configuration file.
    Private Sub cmd_startsearch()
        Dim o_command As New ilsserver.server.Commands
        o_command.StartManualSearch(l_filesfound)
        Console.WriteLine("")
        Console.WriteLine("")
    End Sub

    'Command showfilelist
    'Show the list of files that are on memory, this list is not saved.
    Private Sub cmd_showfilelist()
        If l_filesfound.Count > 0 Then
            For Each item As FileShowed In l_filesfound.Items
                Console.WriteLine(item.GetFileName)
            Next
        Else
            Console.WriteLine("File List is Empty.")
        End If
        Console.WriteLine("")
    End Sub

    'Command showfilelist
    'Shows the total of files that are on memory.
    Private Sub cmd_showfilestotal()
        Console.WriteLine("> files in memory : " & l_filesfound.Items.Count)
        Console.WriteLine("")
    End Sub

    'Command lookinfiles
    'Runs the search for words in the files for words and save it into memory for searchs.
    Private Sub cmd_lookinfiles()
        Dim threat_opener As System.Threading.Thread
        Dim o_t As New ILSCore.ilscore.lib.file.OpenerTextFile
        Dim o_w As New ILSCore.ilscore.lib.file.OpenerWordFile
        Dim o_wx As New ILSCore.ilscore.lib.file.OpenerWordXFile
        Dim o_pdft As New ILSCore.ilscore.lib.file.OpenerPDFText

        If Not Command.Contains("""") Then
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
                        Console.WriteLine("> Error Reading file : " & item.GetFileName)
                    End Try
                End If
                count_processedfiles = count_processedfiles + 1
            Next
            Console.WriteLine("> Files Processed: " & count_processedfiles.ToString)
        Else

            Dim v_command() As String
            Dim b_vallidparam As Boolean = False
            v_command = Command.Split("""")
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

            Console.WriteLine("> Files Processed: " & count_processedfiles.ToString)
        End If
    End Sub

    'Command findword
    'search for a word into the words that are in memory.
    Private Sub cmd_findword(argument As String)
        Dim wc_wordscollected As New WordFileCollection

        If Not argument.Contains("""") Then
        Else
            Dim v_command() As String
            v_command = argument.Split("""")
            o_commands.SearchWord(v_command(1), wc_wordscollected, d_MainDictionary)

            For Each item As ILSCore.ilscore.lib.entities.WordFile In wc_wordscollected.Items
                Console.WriteLine("------------------------------------------------")
                Console.WriteLine("> WORD : " & item.Word & "  FILE:" & item.FileRelated.GetFullActualPath)
                Console.WriteLine("> Paragraph")
                Console.WriteLine(item.Paragraphs)
                Console.WriteLine("------------------------------------------------")
                Console.WriteLine("")
            Next

            If wc_wordscollected.Count = 0 Then
                Console.WriteLine("No Matchs Found")
                Console.WriteLine("")
                Console.WriteLine("")
            End If

        End If
    End Sub

    'Command savememory
    'Store all the words in a xml document in the configured path.
    Private Sub cmd_savememory()
        Try
            Console.WriteLine("> Saving memory please wait...")
            Dim Saveengine As New ILSCore.ilscore.lib.engine.SaveEngine
            Saveengine.SaveMemory(d_MainDictionary)
            Console.WriteLine("> Memory saved.")
            Console.WriteLine("")
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message.ToString)
            Console.WriteLine("")
        End Try
    End Sub

    'Command loadsavedmemory
    'Load saved records into the memory
    Private Sub cmd_loadsavedmemory()
        Try
            Console.WriteLine("Loading saved memory please wait...")
            o_commands.LoadXMLFile(ConfigurationManager.AppSettings("MemoryFilePath"), d_MainDictionary)
            Console.WriteLine("Memory loaded successfully")
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message.ToString)
        End Try
    End Sub

    'Command countwords
    'Count the words that were found during the command lookinfiles.
    Private Sub cmd_countwords()
        Console.WriteLine(d_MainDictionary.WordsCollected.Count)
    End Sub

    'Command resizephotos
    'Resize all the photos found during the command startseach.
    Private Sub cmd_resizephotos()
        Console.WriteLine("Resizing Photos .........")
        Console.Write("Total Of Photos resized.......:")
        o_commands.reziseJPG(l_filesfound)
        Console.WriteLine("")
        Console.WriteLine("Resizing Process Finished")
        refreshfilesfound()
    End Sub

    'Command resizeavimovies
    'Resize the format movie avi into smaller file, the third app called ffmepg is need to properly configured
    Private Sub cmd_resizeavimovies()
        Console.WriteLine("Resizing AVI Movies .....")
        o_commands.ResizeAVIMovies(l_filesfound, True)
        Console.WriteLine("")
        Console.WriteLine("Resizing Process Finished")
        Console.WriteLine("Refreshing File List ...")
        refreshfilesfound()
    End Sub

    'Command resizemovies
    'Resize the movies into smaller file, the third app called ffmepg is need to properly configured.
    Private Sub cmd_resizemovies()
        Console.WriteLine("Resizing Movies .....")
        o_commands.ResizeMovieFiles(l_filesfound, True)
        Console.WriteLine("")
        Console.WriteLine("Resizing Process Finished")
        refreshfilesfound()
    End Sub

    'Command clearfilelist
    'Clear the content of the memory of all files stored.
    Private Sub cmd_clearfilelist()
        l_filesfound.Items.Clear()
        Console.WriteLine("Files List cleared.")
    End Sub

    'Command addfile
    'Adds a file into memory list
    Private Sub cmd_addfile(argument As String)
        If argument.Contains("""") Then
            Dim v_command() As String
            v_command = argument.Split("""")
            Dim o_command As New ilsserver.server.Commands
            o_command.ProcessFile(l_filesfound, v_command(1))
            
        Else
            Console.WriteLine("Command to add a new file to the memory list")
            Console.WriteLine("how to use")
            Console.WriteLine(" ")
            Console.WriteLine("addfile ""C:\Documents and Settings\Administrator\Desktop\document1.doc""")
            Console.WriteLine(" ")
            Console.WriteLine(" ")
        End If
    End Sub

    'Function to refresh the file list in memory.
    Private Sub refreshfilesfound()
        Dim o_configuration As New ilsserver.server.Configuration
        If o_configuration.GetPaths.Count > 0 Then
            Console.WriteLine("Refreshing File List ...")
            l_filesfound.Items.Clear()
            Dim o_command As New ilsserver.server.Commands
            o_command.StartManualSearch(l_filesfound)
            Console.WriteLine("")
            Console.WriteLine("Files List compleated ...")
        End If
    End Sub
End Module
