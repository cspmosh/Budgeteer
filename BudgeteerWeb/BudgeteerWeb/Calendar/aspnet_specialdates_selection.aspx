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
		    .mySpecialDate {
                    font:bold 12px Tahoma;
                    color:green;
                    text-align:center;
                    padding:1px;
                    border:0px solid #993766;
            }
		    .mySpecialDateOver {
                    font:bold 12px Tahoma;
                    color:green;
                    text-align:center;
                    padding:1px;
                    border:0px solid #993766;
            }
		    .specialDateJuly4 {
			    font-size:1px;
			    color:#F6F6F6;
			    text-align:right;
			    vertical-align:bottom;
			    padding:1px;
			    border:0px solid black;
			    background-image:url('styles/flag_usa.gif');
			    background-position:bottom center;
			    background-repeat:no-repeat;
		    }
		    .specialDateJuly4Over {
			    font-size:1px;
			    color:#F6F6F6;
			    text-align:right;
			    vertical-align:bottom;
			    padding:1px;
			    border:0px solid black;
			    background-image:url('styles/flag_usa.gif');
			    background-position:bottom center;
			    background-repeat:no-repeat;
		    }
		    .tdText {
				    font:11px Verdana;
				    color:#333333;
			    }
	</style>
</head>
<body>
	 <form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Select only special dates</b></span>
		    <br /><br />	
			<br /><br /> 
			
		    <obout:Calendar runat="server" 
					    MonthWidth="175"
					    MonthHeight="140"
					    ShortDayNames="Su,Mo,Tu,We,Th,Fr,Sa" MonthMarginWidth="4"
					    AllowSelectSpecial="true"
					    EnabledDateExpression="false"
					    >
			    <obout:SpecialDate Year="-1" Month="7" Day="4" Tooltip="Independence day" CssClass="specialDateJuly4" CssClassOver="specialDateJuly4Over" />
			    <obout:SpecialDate Year="-1" Month="10" Day="20" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="1" Day="22" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="1" Day="14" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="2" Day="3" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="2" Day="26" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="3" Day="16" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="3" Day="18" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="4" Day="20" ToolTip="My Special Day" CssClass="mySpecialDate" CssClassOver="mySpecialDateOver" />
			    <obout:SpecialDate Year="-1" Month="4" Day="8" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="5" Day="1" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="3" Day="20" ToolTip="Dentist appointment" />
			    <obout:SpecialDate Year="-1" Month="6" Day="23" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="6" Day="6" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="7" Day="19" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="8" Day="29" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="8" Day="14" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="9" Day="9" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="9" Day="23" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="10" Day="4" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="10" Day="27" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="11" Day="1" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="11" Day="6" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="12" Day="11" ToolTip="Hello world!  This is a tooltip" />
			    <obout:SpecialDate Year="-1" Month="12" Day="24" ToolTip="Hello world!  This is a tooltip" />
		    </obout:Calendar>
    		
		    <br /><br /><br /><br /><br />
        	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>