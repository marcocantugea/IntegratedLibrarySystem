<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="ILSWeb._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        body
        {
        	font-family: Verdana;
        }
        #displayresult table
        {
        	font-family: Verdana;
        	font-size: medium;
        	font
        	
        }
        #displayresult table td
        {
        	border-bottom-color: gray;
        	border-bottom-style:dashed ;
        	border-bottom-width:thin;
        	margin:0;
        	
        }
        #displayresult table th
        {
        	background-color:GrayText;
        	color:Black;
        	font-weight:bold;
        }
        #displayresult table tr
        {
        	height:25px;
        }
        #SearchTool input
        {
        	font-size:medium;
        	
        }
        h1
        {
        	font-family: Verdana;
        }
        #tableindexcontrol tr td 
        {
        	border:solid thin GrayText;
        	
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center" id="SearchTool">
        <div style=" width:100%; text-align:center">
            <h1>Server File Searcher (SFS)</h1>
        </div>
        <asp:TextBox ID="TextBox1" runat="server" Width="393px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Search" />
        <div>Index</div>
        <div id="Indexdiv" runat="server">
            <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none;" />
        </div>
        <div id="Subindexdiv" runat="server"></div>
    </div>
    <p></p>
    <div id="displayresult" runat="server">
    </div>
    </form>
</body>
</html>
