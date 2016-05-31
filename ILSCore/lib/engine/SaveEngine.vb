Imports ILSCore.ilscore.lib.entities
Imports ILSCore.ilscore.lib.file
Imports System.Configuration

Namespace ilscore.lib.engine
    Public Class SaveEngine

        Private _dictionary As DictionaryWords
        Private _tsaved As Threading.Thread

        Public WriteOnly Property Dictionary() As DictionaryWords
            Set(ByVal value As DictionaryWords)
                _dictionary = value
            End Set
        End Property

        Public Sub SaveMemory()
            SaveMemory(_dictionary)
        End Sub

        Public Sub SaveMemory(ByVal dictionary As DictionaryWords)

            Dim _foldercontroler As New FileFolderControler

            _dictionary = dictionary
            'get the initial path 
            Dim parentpath As String = ConfigurationSettings.AppSettings("MemoryFilePathStorage")
            'Create a root path of memory
            parentpath = parentpath & "SavedMemory" & Date.Now.ToString("MMddyyyymmss")

            _foldercontroler.CreateFolder(parentpath)

            'sort directory
            Dim keys As List(Of String) = dictionary.WordsCollected.Keys.ToList

            keys.Sort()

            'create directory levels
            For Each Str As String In keys
                'Dim srt_1lvltree As String = parentpath & "\" & Str.Substring(0, 1).ToUpper
                '_foldercontroler.CreateFolder(srt_1lvltree)
                Dim s_str As String = Str.Trim()
                Dim s_name As String = s_str.Substring(0, 3).ToUpper
                Dim rgx As New System.Text.RegularExpressions.Regex("[^a-zA-Z ]")
                s_name = rgx.Replace(s_name, "")

                If Not s_name.Equals("") And s_name.Length = 3 Then
                    Try
                        Dim srt_2ndlvltree As String
                        Dim filesfound = dictionary.WordsCollected.Where(Function(x) x.Key.Contains(s_name)).Select(Function(x) x.Value).ToList
                        Dim new_dic As New DictionaryWords
                        For Each i As WordFileCollection In filesfound
                            For Each item As WordFile In i.Items
                                new_dic.AddItem(item.Word, item)
                            Next
                        Next
                        If new_dic.WordsCollected.Count > 0 Then
                            srt_2ndlvltree = parentpath & "\" & s_name
                            _foldercontroler.CreateFolder(srt_2ndlvltree)
                        End If
                        Try
                            'If Str.Substring(0, 3).Contains("") Then
                            If new_dic.WordsCollected.Count > 0 Then
                                Dim xml_file_name As String = srt_2ndlvltree & "\" & s_name & ".xml"
                                Dim _xmlcontroler As New XMLControler
                                _xmlcontroler.Dictionaryy = new_dic
                                _xmlcontroler.Filename = xml_file_name
                                _tsaved = New Threading.Thread(AddressOf _xmlcontroler.CreateXML)
                                _tsaved.Start()
                                '_tsaved.Join()

                                '_xmlcontroler.CreateXML(xml_file_name, new_dic)
                                'End If
                            End If
                        Catch ex As Exception
                            Throw
                        End Try
                    Catch ex As Exception
                        Console.WriteLine("Error: " & ex.Message)
                    End Try
                End If
            Next
            _tsaved.Join()

        End Sub


    End Class
End Namespace
