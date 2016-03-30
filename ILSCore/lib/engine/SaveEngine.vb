Imports ILSCore.ilscore.lib.entities
Imports ILSCore.ilscore.lib.file
Imports System.Configuration

Namespace ilscore.lib.engine
    Public Class SaveEngine

        Private _dictionary As DictionaryWords

        Public Sub SaveMemory(ByVal dictionary As DictionaryWords)




            Dim _foldercontroler As New FileFolderControler
            Dim _xmlcontroler As New XMLControler
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
                Try
                    Dim srt_2ndlvltree As String = parentpath & "\" & Str.Substring(0, 2).ToUpper
                    _foldercontroler.CreateFolder(srt_2ndlvltree)
                    Dim filesfound = dictionary.WordsCollected.Where(Function(x) x.Key.Contains(Str.Substring(0, 2).ToUpper)).Select(Function(x) x.Value).ToList
                    Dim new_dic As New DictionaryWords
                    For Each i As WordFileCollection In filesfound
                        For Each item As WordFile In i.Items
                            new_dic.AddItem(item.Word, item)
                        Next
                    Next
                    Try
                        Dim xml_file_name As String = srt_2ndlvltree & "\" & Str.Substring(0, 2).ToUpper & ".xml"
                        _xmlcontroler.CreateXML(xml_file_name, new_dic)
                    Catch ex As Exception
                        Throw
                    End Try
                Catch ex As Exception
                    Console.WriteLine("Error: " & ex.Message)
                End Try

                Next
           
        End Sub


    End Class
End Namespace
