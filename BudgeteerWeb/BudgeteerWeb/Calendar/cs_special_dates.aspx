<%@ Page Language="C#" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
	protected OboutInc.Calendar2.Calendar myCal;
	
	void Page_Init(object o, EventArgs e) {
		myCal = new OboutInc.Calendar2.Calendar();
		myCal.ID = "calendar";
		myCalPlaceholder.Controls.Add(myCal);
	}

	void Page_Load(object o, EventArgs e) {
		myCal.DateFirstMonth = new DateTime(2007, 6, 1);
		
		myCal.AddSpecialDate(2007, 6, 20, "Dentist appointment");
		myCal.AddSpecialDate(2007, 6, 8, "Some tooltip");
		myCal.AddSpecialDate(-1, 7, 4, "Independence Day", "indepDay", "indepDayOver", "");
		myCal.AddSpecialDate(2007, 7, 18, "A tooltip");
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
			.indepDay {
				background-image:url('styles/flag_usa.gif');
				background-position:center center;
				background-repeat:no-repeat;
				font-size:1px;
				color:#F6F6F6;
			}
			.indepDayOver {
				background-image:url('styles/flag_usa.gif');
				background-position:center center;
				background-repeat:no-repeat;
				font-size:1px;
				color:#F6F6F6;
			}
		</style>
	</head>
	<body>
	    <form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Calendar week selectSpecial dates</b></span>
		    <br /><br />	
			<br /><br />  
		
		    <ASP:PlaceHolder runat="server" id="myCalPlaceholder" />

		    <br />
    		
		    See also <a href="http://www.obout.com/calendar/tutorial_specialdates2.aspx">special dates tutorial</a>
    		
		    <br /><br /><br />
		
		    <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=CSHARP">« Back to examples</a>
		    
	    </form>
	</body>
</html>