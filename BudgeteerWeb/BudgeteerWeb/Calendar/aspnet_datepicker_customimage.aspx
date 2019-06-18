<%@ Page Language="C#" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
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
	        <span class="tdText"><b>ASP.NET Calendar - Date picker custom image</b></span>
		    <br /><br />	
			<br /><br /> 
			
		    <span class="tdText">Click on the calendar image near the textbox. </span><br /><br />
		    <table cellspacing="0" cellpadding="0" border="0" width="100%">
			    <tr><td valign="top">
				    <table border="0" cellspacing="0" cellpadding="0">
					    <tr>
						    <td align="left" valign="middle">
							    <input readonly="true" type="text" ID="txtDate1" style="height:20px;width:70px" />
						    </td>
						    <td align="left" valign="middle">
							    <obout:Calendar runat="server" 
								    StyleFolder="styles/default" 
								    DatePickerMode="true"
								    TextBoxId="txtDate1" 
								    DatePickerImagePath ="styles/icon2.gif"/>
						    </td>
					    </tr>
					    <tr><td style="padding-top:3px;">&nbsp;</td></tr>
					    <tr>
						    <td align="left" valign="middle">
							    <input readonly="true" type="text" ID="txtDate2" style="height:20px;width:70px" />
						    </td>
						    <td align="left" valign="middle">
							    <obout:Calendar runat="server" 
								    StyleFolder="styles/default" 
								    DatePickerMode="true"
								    TextBoxId="txtDate2" 
								    DatePickerImagePath ="styles/date_picker1.gif"/>
						    </td>
					    </tr>
					    <tr><td style="padding-top:3px;">&nbsp;</td></tr>
					    <tr>
						    <td align="left" valign="middle">
							    <input readonly="true" type="text" ID="txtDate3" style="height:20px;width:70px" />
						    </td>
						    <td  align="left" valign="middle">
							    <obout:Calendar runat="server" 
								    StyleFolder="styles/default" 
								    DatePickerMode="true"
								    TextBoxId="txtDate3" 
								    DatePickerImagePath ="styles/date_picker2.gif"/>
						    </td>
					    </tr>
				    </table>
			    </td></tr>
		    </table>
		    
		    <br /><br /><br /><br /><br />
    	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>