<%@ Page Language="C#" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

	protected OboutInc.Calendar2.Calendar calDefault;
	protected OboutInc.Calendar2.Calendar calDatePicker;
	
	void Page_Load(object o, EventArgs e) {
		calDefault.StyleFolder = "styles/blue";
		calDefault.MonthWidth = 160;
		calDefault.MonthHeight = 170;
		
		calDatePicker.StyleFolder = "styles/blue";
		calDatePicker.DatePickerMode = true;
		calDatePicker.TextBoxId = "txtDate";
		calDatePicker.DatePickerImagePath = "styles/icon2.gif";
		calDatePicker.MonthWidth = 160;
		calDatePicker.MonthHeight = 170;
		
		DefaultPlaceholder.Controls.Add(calDefault);
		DatePickerPlaceholder.Controls.Add(calDatePicker);
	}

	void Page_Init(object o, EventArgs e) {
		calDefault = new OboutInc.Calendar2.Calendar();
		calDatePicker = new OboutInc.Calendar2.Calendar();
		
		calDefault.ID = "calDefault";
		calDatePicker.ID = "calDatePicker";
	}
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
	        <span class="tdText"><b>ASP.NET Calendar - Blue style</b></span>
		    <br /><br />	
			<br /><br /> 
		
			<ASP:PlaceHolder runat="server" id="DefaultPlaceholder"></ASP:PlaceHolder>
			
			<br />
			Date picker
			<ASP:TextBox runat="server" id="txtDate"></ASP:TextBox>
			
			<ASP:PlaceHolder runat="server" id="DatePickerPlaceholder"></ASP:PlaceHolder>
			
			<br /><br /><br />
			
			<a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=CSHARP">« Back to examples</a>
			
		</form>
	</body>
</html>