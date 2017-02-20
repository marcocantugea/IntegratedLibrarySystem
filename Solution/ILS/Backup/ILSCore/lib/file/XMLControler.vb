Imports ILSCore.ilscore.lib.entities
Imports System.Xml
Imports System.IO

Namespace ilscore.lib.file
    Public Class XMLControler

        Private _filenamea As String
        Private _Dictionaryy As DictionaryWords
        Public WriteOnly Property Dictionaryy() As DictionaryWords
            Set(ByVal value As DictionaryWords)
                _Dictionaryy = value
            End Set
        End Property
        Public WriteOnly Property Filename() As String
            Set(ByVal value As String)
                _filenamea = value
            End Set
        End Property

        Public Sub CreateXML()
            Dim fil As New System.IO.FileInfo(_filenamea)
            Try
                If Not System.IO.File.Exists(_filenamea) Then
                    CreateXML(_filenamea, _Dictionaryy)

                    Console.WriteLine("File Created : " & fil.Name)
                End If


            Catch ex As Exception
                Console.WriteLine("Warning : File " & fil.Name & " message:" & ex.Message)
            End Try

        End Sub

        Public Sub CreateXML(ByVal filename As String, ByVal Dictionary As DictionaryWords)
            Dim writer As New Xml.XmlTextWriter(filename, System.Text.Encoding.UTF8)
            writer.WriteStartDocument(True)
            writer.Formatting = Xml.Formatting.Indented
            writer.Indentation = 2
            writer.WriteStartElement("dictionary")
            For Each item As KeyValuePair(Of String, WordFileCollection) In Dictionary.WordsCollected
                If Not item.Key.Contains(vbCrLf) Then
                    writer.WriteStartElement("key")
                    writer.WriteString(item.Key)
                    writer.WriteEndElement()
                    writer.WriteStartElement("value")
                    writer.WriteStartElement("collection")
                    For Each it As WordFile In item.Value.Items
                        writer.WriteStartElement("itemcol")

                        writer.WriteStartElement("word")
                        writer.WriteString(it.Word)
                        writer.WriteEndElement()

                        writer.WriteStartElement("filepath")
                        writer.WriteString(it.FileRelated.GetFullActualPath)
                        writer.WriteEndElement()

                        writer.WriteStartElement("Paragraphs")
                        writer.WriteString(it.Paragraphs)
                        writer.WriteEndElement()

                        writer.WriteEndElement()
                    Next
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                End If
                
            Next
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()

        End Sub


        Public Sub ReadXML(ByVal filename As String, ByVal Dictionary As DictionaryWords)
            Dim xmldoc As New XmlDataDocument
            Dim xmlnode As XmlNodeList
            Dim fs As New FileStream(filename, FileMode.Open, FileAccess.Read)
            xmldoc.Load(fs)
            xmlnode = xmldoc.GetElementsByTagName("dictionary")
            Dim key As String
            For Each item As XmlNode In xmlnode
                For Each itm As XmlNode In item.ChildNodes
                    If itm.Name.Equals("key") Then
                        key = itm.InnerText.Trim
                        Dictionary.AddItem(key)
                    End If
                    'If itm.Name.Equals("key") Then
                    '    Console.WriteLine("Key: " & itm.InnerText.Trim)
                    'End If
                    If itm.Name.Equals("value") Then
                        'Console.WriteLine("value: ")
                        For Each it As XmlNode In itm.ChildNodes
                            Dim o_wordfile As New WordFile
                            For Each i As XmlNode In it.ChildNodes
                                For Each e As XmlNode In i.ChildNodes
                                    If e.Name.Equals("word") Then
                                        o_wordfile.Word = e.InnerText.Trim
                                    End If
                                    If e.Name.Equals("filepath") Then
                                        o_wordfile.FileRelated = New FileShowed
                                        o_wordfile.FileRelated.Setfile(e.InnerText.Trim)
                                    End If
                                    If e.Name.Equals("Paragraphs") Then
                                        o_wordfile.Paragraphs = e.InnerText.Trim
                                    End If
                                    'For Each e As XmlNode In i.ChildNodes
                                    '    Console.WriteLine("      " & e.Name)
                                Next
                                Dictionary.AddItem(key, o_wordfile)
                            Next

                            'Dictionary.WordsCollected(itm.InnerText.Trim).Items.Add(o_wordfile)
                            'Console.WriteLine("      " & it.Name)
                        Next
                    End If
                    'Console.WriteLine("      " & itm.Name)
                    ' Console.WriteLine(itm.InnerText.Trim())

                Next
            Next
            fs.Close()


        End Sub

    End Class
End Namespace
