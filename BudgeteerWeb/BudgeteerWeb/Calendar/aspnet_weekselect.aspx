<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="C#" runat="server">
	protected void Date_Changed(object o, EventArgs e) {
		DateTime selectedDate = Calendar1.SelectedDate;
		
		Calendar1.SpecialDates.Clear();
		for (int index = 0;index < 7;index++)
		{
			int addedDays = (int)(index - selectedDate.DayOfWeek);
			Calendar1.AddSpecialDate(selectedDate.AddDays(addedDays).Year, selectedDate.AddDays(addedDays).Month, selectedDate.AddDays(addedDays).Day, "Week select");
		}
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
		</style>
	</head>
	<body>
		<form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Calendar week select</b></span>
		    <br /><br />	
			<br /><br />  
			    <obout:Calendar
				    ID="Calendar1"
				    runat="server" 
				    Columns="1"
				    AutoPostBack="true"
				    TitleText="Week select"
				    OnDateChanged="Date_Changed"
				    StyleFolder = "styles/default">
			    </obout:Calendar>

		        <br /><br /><br /><br /><br />
        	    
	            <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>