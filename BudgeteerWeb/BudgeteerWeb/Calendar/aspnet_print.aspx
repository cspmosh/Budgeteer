<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
	    <title>obout ASP.NET Calendar examples</title>

		<script type="text/javascript">
			function OpenCalendarPopupForPrint()
			{
				window.open('aspnet_printpopup.aspx', 'CalendarPrint', config='toolbar=0,location=0,status=1,scrollbars=1,menubar=1,resizable=1,width=550,height=670');
			}
		</script>
		<style type="text/css">
			
			.tdText {
                font:11px Verdana;
                color:#333333;
            }
			body {
				font:11px Verdana;
				color:#333333;
			}
			a {
				font:11px Verdana;
				color:#315686;
				text-decoration:underline;
			}
			a:hover {
				color:crimson;
			}
		</style>
	</head>
	<body>
	    <form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Print</b></span>
		    <br /><br />	
			<br /><br /> 
			
			<table border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td align="center" valign="middle" style="width:300px;">
						<a href="javascript:OpenCalendarPopupForPrint()">Print Calendar</a>
					</td>
				</tr>
			</table>
			
		    <br /><br /><br /><br /><br />
    	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>