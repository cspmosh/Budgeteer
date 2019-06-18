<%@ Page Language="C#" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>
<%@ Register TagPrefix="owd" Namespace="OboutInc.Window" Assembly="obout_Window_NET"%>

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
	        <span class="tdText"><b>ASP.NET Calendar - Inside ASP.NET Window</b></span>
		    <br /><br />	
			<br /><br />
			
			<center>
				<table style="width:400px;">
				<tr>
					<td align="center"><input type="button" value="Open Window" onclick="myWindow.Open();myWindow.screenCenter();" /> </td>
				</tr>
				</table>
			</center>
			<owd:Window runat="server" ID="myWindow" Status="Status" Title="Calendar inside Window"
				ShowCloseButton="true"  VisibleOnLoad="false" IconPath="../window/images/outlook.png" 
				Left="350" Top="200" ShowMaximizeButton="true" Overflow="HIDDEN" Width="420" Height="300"  
				StyleFolder="../window/wdstyles/default">
					<table width="100%" height="100%">
						<tr>
							<td align="center" valign="middle">
								Pick date: <asp:TextBox id="txt1" runat="server" />
								<obout:Calendar ID="Calendar1" runat="server" DatePickerImagePath="styles/icon2.gif"
									DatePickerMode="true" TextBoxId="txt1" ShowTimeSelector="False" ShowYearSelector="False" StyleFolder="styles/blue"
									TitleText="Wednesday, June 25, 2008" YearMaxScroll="1" YearMinScroll="1" YearMonthFormat="MMMM, yyyy">
								</obout:Calendar>
							</td>
						</tr>
					</table>
			</owd:Window>
			
		    <br /><br /><br /><br /><br />
    	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>