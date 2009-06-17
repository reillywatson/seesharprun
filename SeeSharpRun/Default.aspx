<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SeeSharp._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frmCodeInput" runat="server">
    <div>
		<asp:TextBox ID="txtCode" TextMode="MultiLine" runat="server" Width="500" Height="300" />
		<br />
		<asp:Button id="btnTestCode" text="label" OnClick="btnTestCode_Click" runat="server" />
		<br />
		<asp:TextBox ID="txtRunResults" runat="server" TextMode="MultiLine" ReadOnly="true"  Width="500" Height="300" />
    </div>
    </form>
</body>
</html>
