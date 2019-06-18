<%@ Page Language="C#" Inherits="OboutInc.oboutAJAXPage" %>
<%@ Register TagPrefix="oajax" Namespace="OboutInc" Assembly="obout_AJAXPage" %> 
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="C#" runat="server">

	private void Button1_OnClick(object sender, EventArgs e)
	{
		Calendar1.StyleFolder = "../calendar/styles/graphite";
	}
	
</script>
<html>
	<head runat="server">
	<title>obout ASP.NET Calendar examples</title>

	    <style type="text/css">
			
			.tdText {
                font:11px Verdana;
                color:#333333;
            }
	    </style>
	</head>
	<body>
	    <form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Inside AJAX Page panel</b></span>
		    <br /><br />	
			<br /><br /> 
			
		    <table>
			    <tr>
				    <td class="tdText">
					    Change the calendar style without page reload.<br /><br />
					    AJAXPage panel
					    <br />
					    <div style="border:1px solid gray;width:400px">
					    <oajax:CallbackPanel id="callbackPanel1" runat="server" ShowLoading="false">
						    <Content>
							    <div style="padding:25px">
							    Calendar
							    <obout:Calendar runat="server" 
									    ID="Calendar1"
									    StyleFolder="../calendar/styles/default" 
									    DatePickerImagePath ="../calendar/icon2.gif"/>
							    <br /><br /><br />
							    <asp:Button ID="Button1" runat="server" Text="Change style" OnClick="Button1_OnClick" />
							    </div>
						    </Content>
					    </oajax:CallbackPanel>
					    </div>
				    </td>
			    </tr>
		    </table>
		    
		    <br /><br /><br /><br /><br />
    	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>