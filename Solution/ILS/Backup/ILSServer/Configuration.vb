Imports System.Configuration
Imports System.Xml


Namespace ilsserver.server
    Public Class Configuration

        Public Function GetPaths() As List(Of String)
            Dim l_paths As New List(Of String)

            Dim XmlDoc As New XmlDocument()
            XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
            For Each xElement As XmlElement In XmlDoc.DocumentElement
                If xElement.Name = "appSettings" Then
                    For Each xNode As XmlNode In xElement.ChildNodes
                        If Not IsNothing(xNode.Attributes) Then
                            If xNode.Attributes(0).Value.Contains("PathToSearch") Then
                                l_paths.Add(xNode.Attributes(0).Value & "?" & xNode.Attributes(1).Value)
                            End If
                        End If
                    Next
                End If
            Next


            Return l_paths
        End Function

        Public Sub DeleteConfiguration(ByVal key As String)
            Dim e_config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            e_config.AppSettings.Settings.Remove(key)
            e_config.Save(ConfigurationSaveMode.Modified)
            ConfigurationManager.RefreshSection("appSettings")
        End Sub

        Private Sub AddNewConfigurationParameter(ByVal Key As String, ByVal Value As String)
            Dim new_config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            new_config.AppSettings.Settings.Add(Key, Value)
            new_config.Save(ConfigurationSaveMode.Modified)

            ConfigurationManager.RefreshSection("appSettings")

        End Sub

        Public Sub AddNewConfigPathToSearch(ByVal value As String)
            Dim l_paths As New List(Of String)
            l_paths = GetPaths()

            If l_paths.Count > 0 Then
                Dim index_path As Integer = -1

                For Each item As String In l_paths
                    Dim param_val() As String = item.Split("?")
                    Dim var_indx() As String
                    Dim index As Integer
                    Try

                        var_indx = param_val(0).Split("-")
                        index = var_indx(1)

                    Catch ex As Exception
                        Throw ex
                    End Try

                    If index_path < index Then
                        index_path = index
                    End If

                Next

                Dim name As String
                index_path = index_path + 1
                name = "PathToSearch-" & index_path.ToString
                AddNewConfigurationParameter(name, value)
            Else
                AddNewConfigurationParameter("PathToSearch-0", value)

            End If
        End Sub


        Public Shared Sub UpdateAppSettings(ByVal KeyName As String, ByVal KeyValue As String)
            '  AppDomain.CurrentDomain.SetupInformation.ConfigurationFile 
            ' This will get the app.config file path from Current application Domain
            Dim XmlDoc As New XmlDocument()
            ' Load XML Document
            XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
            ' Navigate Each XML Element of app.Config file
            For Each xElement As XmlElement In XmlDoc.DocumentElement
                If xElement.Name = "appSettings" Then
                    ' Loop each node of appSettings Element 
                    ' xNode.Attributes(0).Value , Mean First Attributes of Node , 
                    ' KeyName Portion
                    ' xNode.Attributes(1).Value , Mean Second Attributes of Node,
                    ' KeyValue Portion
                    For Each xNode As XmlNode In xElement.ChildNodes
                        If Not IsNothing(xNode.Attributes) Then
                            If xNode.Attributes(0).Value = KeyName Then
                                xNode.Attributes(1).Value = KeyValue
                            End If
                        End If
                    Next
                End If
            Next
            ' Save app.config file
            XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
            XmlDoc = Nothing
        End Sub

    End Class
End Namespace
