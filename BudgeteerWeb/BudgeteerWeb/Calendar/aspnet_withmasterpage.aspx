<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>
<%@ Page Language="C#" MasterPageFile="aspnet_withmasterpage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   Default style example
	
	<br /><br />
	
	<obout:Calendar runat="server" />
	
	<br />
	Date picker
	<ASP:TextBox runat="server" id="txtDate" />
	<obout:Calendar runat="server"
					DatePickerMode = "true"
					TextBoxId = "txtDate"
					DatePickerImagePath = "styles/icon2.gif" />
	
</asp:Content>
