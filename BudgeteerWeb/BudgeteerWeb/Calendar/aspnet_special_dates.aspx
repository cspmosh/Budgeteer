<%@ Page Language="C#" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
		<title>obout ASP.NET Calendar examples</title>

	<style type="text/css">
			
			.tdText {
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
	        <span class="tdText"><b>ASP.NET Calendar - Special dates</b></span>
		    <br /><br />	
			<br /><br /> 

		    <obout:Calendar runat="server" DateFirstMonth="2007/6/1">
			    <obout:SpecialDate Year="2007" Month="6" Day="20" ToolTip="Dentist appointment" />
			    <obout:SpecialDate Year="2007" Month="6" Day="8" ToolTip="Some tooltip" />
			    <obout:SpecialDate Year="-1" Month="7" Day="4" ToolTip="Independence Day" CSSClass="indepDay" CSSClassOver="indepDayOver" />
			    <obout:SpecialDate Year="2007" Month="7" Day="18" ToolTip="A tooltip" />
		    </obout:Calendar>

		    <br />
    		
		    See also <a href="http://www.obout.com/calendar/tutorial_specialdates2.aspx">special dates tutorial</a>
    			
		    <br /><br />
        	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>