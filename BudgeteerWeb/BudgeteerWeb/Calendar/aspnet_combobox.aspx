<%@ Page Language="C#" EnableViewState="False" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
		<script type="text/javascript">
			//this function is called when date is selected in calendar
			function onDateChange(sender, selectedDate) {
				var month = selectedDate.getMonth();
				var day = selectedDate.getDate() - 1;
				var year = selectedDate.getFullYear() - 1999;
				
				document.getElementById("selMonth").options[month].selected = true;
				document.getElementById("selDay").options[day].selected = true;
				document.getElementById("selYear").options[year].selected = true;
			}
			
			//called when one of the comboboxes is changed
			function selectDate() {
				var month = document.getElementById("selMonth").selectedIndex;
				var day = document.getElementById("selDay").selectedIndex;
				var year = document.getElementById("selYear").selectedIndex;
				
				var selectedDate = new Date(year + 1999, month, day + 1);
				
				calDate.setDate(selectedDate, selectedDate);
			}
		</script>
	</head>
	<body>
	    <form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Date picker with example</b></span>
		    <br /><br />	
			<br /><br />  
		
		    <select id="selMonth" onChange="selectDate()">
			    <option>January</option>
			    <option>February</option>
			    <option>March</option>
			    <option>April</option>
			    <option>May</option>
			    <option selected="true">June</option>
			    <option>July</option>
			    <option>August</option>
			    <option>September</option>
			    <option>October</option>
			    <option>November</option>
			    <option>December</option>
		    </select>
    		
		    <select id="selDay" onChange="selectDate()">
			    <option>1</option>
			    <option>2</option>
			    <option>3</option>
			    <option>4</option>
			    <option>5</option>
			    <option>6</option>
			    <option>7</option>
			    <option>8</option>
			    <option>9</option>
			    <option>10</option>
			    <option>11</option>
			    <option>12</option>
			    <option>13</option>
			    <option>14</option>
			    <option selected="true">15</option>
			    <option>16</option>
			    <option>17</option>
			    <option>18</option>
			    <option>19</option>
			    <option>20</option>
			    <option>21</option>
			    <option>22</option>
			    <option>23</option>
			    <option>24</option>
			    <option>25</option>
			    <option>26</option>
			    <option>27</option>
			    <option>28</option>
			    <option>29</option>
			    <option>30</option>
			    <option>31</option>
		    </select>
    		
		    <select id="selYear" onChange="selectDate()">
			    <option>1999</option>
			    <option>2000</option>
			    <option>2001</option>
			    <option>2002</option>
			    <option>2003</option>
			    <option>2004</option>
			    <option>2005</option>
			    <option>2006</option>
			    <option>2007</option>
			    <option selected="true">2008</option>
			    <option>2009</option>
			    <option>2010</option>
		    </select>&#160;

		    <obout:Calendar runat="server" id="calDate"
						    DatePickerMode="true"
						    DatePickerImagePath ="styles/icon2.gif"
						    OnClientDateChanged="onDateChange" SelectedDate="6/15/2008" DateFirstMonth="6/15/2008"
						    DateMin="1/1/1999"
						    DateMax = "12/31/2010" />
    		
		    <br /><br /><br />
    		
		    See also <a href="http://www.obout.com/calendar/tutorial_clientside.aspx">client side functions tutorial</a>
    		
		    <br /><br /><br /><br /><br />
    	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>