<%@ Page Language="C#" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
	protected OboutInc.Calendar2.Calendar myCal;
	
	void Page_Init(object o, EventArgs e) {
		myCal = new OboutInc.Calendar2.Calendar();
		myCal.ID = "calendar";
        myCal.EnabledDateExpression = "this.currentDate.getDate() % 2 == 0";
		myCalPlaceholder.Controls.Add(myCal);
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
	        <span class="tdText"><b>ASP.NET Calendar - Enabled date expression</b></span>
		    <br /><br />	
			<br /><br /> 
		 
		    You may write complex expressions, which enable or disable certain dates:
		    <br /><br />
		    <ASP:PlaceHolder runat="server" id="myCalPlaceholder" />

		    <br />
    		
		    See also <a href="http://www.obout.com/calendar/tutorial_enableddateexpression2.aspx">enabled date expression tutorial</a>
    		
		    <br /><br /><br />
    		
		    <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=CSHARP">« Back to examples</a>
		    
        </form>
	</body>
</html>