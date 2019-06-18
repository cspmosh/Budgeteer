<%@ Page Language="C#" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			txtDate.Attributes.Add("onchange", "document.getElementById('" + hiddenDate.ClientID + "').value = this.value;");
		}
	}
	
	// method 1 - Get the date value using the "Request" object
	protected void btnSendRequest_Click(object sender, EventArgs e)
	{	
		lDate.Text = "The selected date is:" + Request["txtDate"];
	}
	
	// method 2 - Get the date value using aditional hidden object
	protected void btnSendHidden_Click(object sender, EventArgs e)
	{	
		lDate.Text = "The selected date is:" + hiddenDate.Value;
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
	        <span class="tdText"><b>ASP.NET Calendar - Date picker with readonly textbox</b></span>
		    <br /><br />	
			<br /><br />  
			 
			Date picker
			<ASP:TextBox ReadOnly="true" runat="server" id="txtDate" />
			<asp:HiddenField id="hiddenDate" runat="server" />
			<obout:Calendar runat="server"
							DatePickerMode = "true"
							TextBoxId = "txtDate"
							DatePickerImagePath = "styles/icon2.gif" />
							
							<br />
							<br />
							<b style="color:crimson"><asp:Literal id="lDate" runat="server" /></b>
							<br /><br /><br />
							Get the date value using the "Request" object:<br />
							<asp:Button Text="Get Date" id="btnSendRequest" runat="server" OnClick="btnSendRequest_Click" />
							<br /><br /><br />
							Get the date value using aditional hidden object:<br />
							<asp:Button Text="Get Date" id="btnSendHidden" runat="server" OnClick="btnSendHidden_Click" />
			
			<br /><br /><br /><br /><br />
    	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>