<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="C#" runat="server">
    void Page_Load(object sender, EventArgs e) {
		if (!IsPostBack)
		{
			BindExternalCalendarTitle();
		}
	}
	
	private void BindExternalCalendarTitle()
	{
		lCalendarTitle.Text = Calendar1.DateFirstMonth.ToString("MMMM");
	}
	
	protected void lnkCalendarLeft_Click(object sender, EventArgs e)
	{
		Calendar1.DateFirstMonth = Calendar1.DateFirstMonth.AddMonths(-1);
		BindExternalCalendarTitle();
	}
	
	protected void lnkCalendarRight_Click(object sender, EventArgs e)
	{
		Calendar1.DateFirstMonth = Calendar1.DateFirstMonth.AddMonths(1);
		BindExternalCalendarTitle();
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
			.lnkCalendar
			{
				text-decoration:none;
				color:black;
			}
			.extCalendarTitle
			{
				font-size:12px;
				font-weight:bold;
			}
		</style>
	</head>
	<body>
		<form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Calendar week select</b></span>
		    <br /><br />	
			<br /><br /> 
			
			    <table width="175px">
				    <tr>
					    <td>
						    <asp:LinkButton OnClick="lnkCalendarLeft_Click" id="lnkCalendarLeft" runat="server" Text="<<" CssClass="lnkCalendar"></asp:LinkButton>
					    </td>
					    <td width="80%" align="center">
						    <asp:Label id="lCalendarTitle" runat="server" CssClass="extCalendarTitle"></asp:Label>
					    </td>
					    <td>
						    <asp:LinkButton OnClick="lnkCalendarRight_Click" id="lnkCalendarRight" runat="server" Text=">>" CssClass="lnkCalendar"></asp:LinkButton>
					    </td>
				    </tr>
			    </table>
			    
			    <obout:Calendar 
				    StyleFolder="styles/lite"
				    ID="Calendar1"
				    runat="server" 
				    TextArrowLeft=""
				    TextArrowRight=""
				    ShowOtherMonthDays="false"
				    Columns="1" 
				    TitleText="">
			    </obout:Calendar>
		
		<br /><br /><br /><br /><br />
    	    
	    <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>