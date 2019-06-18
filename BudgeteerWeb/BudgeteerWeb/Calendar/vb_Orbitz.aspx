<%@ Page Language="VB" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

	Protected calDefault As OboutInc.Calendar2.Calendar
	Protected calDatePicker As OboutInc.Calendar2.Calendar
	
	Public Sub Page_Load(o As Object, e As EventArgs)
		calDefault.StyleFolder = "styles/orbitz"
		calDefault.MonthWidth = 155
		calDefault.MonthHeight = 140
		calDefault.TextArrowLeft = ""
		calDefault.TextArrowRight = ""
		
		calDatePicker.StyleFolder = "styles/orbitz"
		calDatePicker.DatePickerMode = true
		calDatePicker.TextBoxId = "txtDate"
		calDatePicker.DatePickerImagePath = "styles/icon2.gif"
		calDatePicker.MonthWidth = 155
		calDatePicker.MonthHeight = 140
		calDatePicker.TextArrowLeft = ""
		calDatePicker.TextArrowRight = ""
		
		DefaultPlaceholder.Controls.Add(calDefault)
		DatePickerPlaceholder.Controls.Add(calDatePicker)
	End Sub

	Public Sub Page_Init(o As Object, e As EventArgs)
		calDefault = new OboutInc.Calendar2.Calendar()
		calDatePicker = new OboutInc.Calendar2.Calendar()
		
		calDefault.ID = "calDefault"
		calDatePicker.ID = "calDatePicker"
	End Sub
</script>
<html>
	<head>
		<title>obout ASP.NET Calendar examples</title>

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
	        <span class="tdText"><b>ASP.NET Calendar - Orbitz style</b></span>
		    <br /><br />	
			<br /><br /> 
			
			<ASP:PlaceHolder runat="server" id="DefaultPlaceholder"></ASP:PlaceHolder>
			
			<br />
			Date picker
			<ASP:TextBox runat="server" id="txtDate"></ASP:TextBox>
			
			<ASP:PlaceHolder runat="server" id="DatePickerPlaceholder"></ASP:PlaceHolder>
			
			<br /><br /><br />
			
			<a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=VBNET">« Back to examples</a>
			
		</form>
	</body>
</html>