<%@ Page Language="C#" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Web.UI" %>

<script runat="server" Language="C#">
	void Page_load(object sender, EventArgs e)
	{
		try
		{
			XmlTextReader oXMLReader = null;
			oXMLReader = new XmlTextReader(Server.MapPath("examples.xml"));
			while (oXMLReader.Read())
			{
				if (oXMLReader.NodeType == XmlNodeType.Element && oXMLReader.Name == "path")
				{
					lblPath.Text = "<b>Path to folder with Calendar examples: </b>" + oXMLReader.ReadString();
				}
			}
			oXMLReader.Close();
		}
		catch 
		{
		}

		if (Request.QueryString["type"] != null)
		{
			ExamplesType.Value = Request.QueryString["type"].ToString();
		}
	}
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>obout ASP.NET Calendar examples</title>
    <style type="text/css">
		td.link{
			padding-left:30px;
			width:250px;			
		}
		
		td.header {
			padding-top:20px;
			border-bottom:1px solid #eeeeee;
			color:#555555;
		}
		
		.tdText {
			font:11px Verdana;
			color:#333333;
		}
		.option2{
			font:11px Verdana;
			color:#0033cc;
			background-color___:#f6f9fc;
			padding-left:4px;
			padding-right:4px;
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
		function showexamples() {
			var exSelect = document.forms[0].exSelect;
			for(var i=0; i<exSelect.length; i++) {
				if(exSelect[i].checked == true) {
					document.getElementById(exSelect[i].value).style.display = "block";
				} else {
					document.getElementById(exSelect[i].value).style.display = "none";
				}
			}
		}
		function showExamplesFromSpan(index) {		   
		    document.forms[0].exSelect[index].checked = true;
		    showexamples(index);
		}
		window.onload = function() {
			var oExamplesType = document.getElementById("ExamplesType");
			if(oExamplesType.value == "ASPNET") {
				document.forms[0].exSelect[0].checked = true;
			} else if(oExamplesType.value == "CSHARP") {
				document.forms[0].exSelect[1].checked = true;
			} else if(oExamplesType.value == "VBNET") {
				document.forms[0].exSelect[2].checked = true;
			} else {
			    document.forms[0].exSelect[0].checked = true;
			}
			showexamples();
		}
	</script>
</head>
<body style="margin-left:30px;">
    <form name="form1" id="form1" runat="server">
    	
	<input id="ExamplesType" type="hidden" runat="server" />
	
    <span class="tdText">
        <a href="../Default.aspx">« Back to Suite Examples</a>
        <br /><br /><br />
        <span style="font-weight:bold;">obout ASP.NET Calendar examples</span>(version 2.9.2, released on February 19, 2008) 
        <br />
        <br />
        Select the examples you want to see:&#160;&#160;&#160;
        <input type="radio" name="exSelect" onclick="showexamples(0)" value="divASPNET" class="tdText"/><span style="cursor: pointer" onclick="showExamplesFromSpan(0)">ASP.NET</span> &#160;&#160;&#160;
        <input type="radio" name="exSelect" onclick="showexamples(1)" value="divCSharp" class="tdText"/><span style="cursor: pointer" onclick="showExamplesFromSpan(1)">C#</span> &#160;&#160;&#160;
        <input type="radio" name="exSelect" onclick="showexamples(2)" value="divVBNET" class="tdText"/><span style="cursor: pointer" onclick="showExamplesFromSpan(2)">VB.NET</span> &#160;&#160;&#160;
        <br /><br />
        <asp:Label runat="server" ID="lblPath"></asp:Label>
        <br /> 
        <br />
		<div id="divASPNET">
			<table width="1024">
				<tr>
					<td valign="top">
						<table>
							<tr>
								<td class="header" colspan="2"><b>Styles</b></td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_default.aspx">Default</a>
								</td>
								<td>aspnet_default.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_graphite.aspx">Graphite</a>
								</td>
								<td>aspnet_graphite.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_desert.aspx">Desert</a>
								</td>
								<td>aspnet_desert.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_style10.aspx">Style 10</a>
								</td>
								<td>aspnet_style10.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_blue.aspx">Blue</a>
								</td>
								<td>aspnet_blue.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_simple.aspx">Simple</a>
								</td>
								<td>aspnet_simple.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_dark.aspx">Dark</a>
								</td>
								<td>aspnet_dark.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_expedia.aspx">Expedia</a>
								</td>
								<td>aspnet_expedia.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_orbitz.aspx">Orbitz</a>
								</td>
								<td>aspnet_orbitz.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_blocky_revert.aspx">Blocky revert</a>
								</td>
								<td>aspnet_blocky_revert.aspx</td>
							</tr>


							<tr>
								<td class="header" colspan="2"><b>Functionality</b></td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_special_dates.aspx">Special dates</a>
								</td>
								<td>aspnet_special_dates.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_date_range.aspx">Date range</a>
								</td>
								<td>aspnet_date_range.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_localization.aspx">Localization</a>
								</td>
								<td>aspnet_localization.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_multiple_months.aspx">Multiple months</a>
								</td>
								<td>aspnet_multiple_months.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_combobox.aspx">With comboboxes</a>
								</td>
								<td>aspnet_combobox.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_date_expression.aspx">Enabled date expression</a>
								</td>
								<td>aspnet_date_expression.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_print.aspx">Print</a>
								</td>
								<td>aspnet_print.aspx, aspnet_printpopup.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_weekselect.aspx">Week select</a>
								</td>
								<td>aspnet_weekselect.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_withexternalmonthselector.aspx">With external month selector</a>
								</td>
								<td>aspnet_withexternalmonthselector.aspx</td>
							</tr>
							
							
							<tr>
								<td class="header" colspan="2"><b>Date picker</b></td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_datepicker_customimage.aspx">Date picker custom image</a>
								</td>
								<td>aspnet_datepicker_customimage.aspx</td>
							</tr>
							
							<tr>
								<td class="header" colspan="2"><b>Integration</b></td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_multiple_calendars_inside_grid.aspx">Multiple calendars inside a grid</a>
								</td>
								<td>aspnet_multiple_calendars_inside_grid.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_withreadonly.aspx">Date picker with readonly textbox</a>
								</td>
								<td>aspnet_withreadonly.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_insideusercontrol.aspx">Inside User Control</a>
								</td>
								<td>aspnet_insideusercontrol.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_withmasterpage.aspx">With Master Page</a>
								</td>
								<td>aspnet_withmasterpage.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_insideoboutgrid.aspx">Inside Obout Grid</a>
								</td>
								<td>aspnet_insideoboutgrid.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_msgridexample.aspx">Inside MS DataGrid</a>
								</td>
								<td>aspnet_msgridexample.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_calendar_inside_window.aspx">Inside ASP.NET Window</a>
								</td>
								<td>aspnet_calendar_inside_window.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_calendarinsideAJAXPagepanel.aspx">Inside AJAX Page Panel</a>
								</td>
								<td>aspnet_calendarinsideAJAXPagepanel.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_task_list.aspx">Task list</a>
								</td>
								<td>aspnet_task_list.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_calendar_for_multiple_textboxes.aspx">Calendar for multiple Textboxes</a>
								</td>
								<td>aspnet_calendar_for_multiple_textboxes.aspx</td>
							</tr>
							<tr>
								<td class="header" colspan="2"><b>Other</b></td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_disablescrolling.aspx">Disable Month Scrolling</a>
								</td>
								<td>aspnet_disablescrolling.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="aspnet_specialdates_selection.aspx">Select only Special Dates</a>
								</td>
								<td>aspnet_specialdates_selection.aspx</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div>  
		
		<div id="divCSharp">
			<table width="1024">
				<tr>
					<td valign="top">
						<table>
							<tr>
								<td class="header" colspan="2"><b>Styles</b></td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_default.aspx">Default</a>
								</td>
								<td>cs_default.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_graphite.aspx">Graphite</a>
								</td>
								<td>cs_graphite.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_desert.aspx">Desert</a>
								</td>
								<td>cs_desert.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_style10.aspx">Style 10</a>
								</td>
								<td>cs_style10.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_blue.aspx">Blue</a>
								</td>
								<td>cs_blue.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_simple.aspx">Simple</a>
								</td>
								<td>cs_simple.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_dark.aspx">Dark</a>
								</td>
								<td>cs_dark.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_expedia.aspx">Expedia</a>
								</td>
								<td>cs_expedia.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_orbitz.aspx">Orbitz</a>
								</td>
								<td>cs_orbitz.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_blocky_revert.aspx">Blocky revert</a>
								</td>
								<td>cs_blocky_revert.aspx</td>
							</tr>


							<tr>
								<td class="header" colspan="2"><b>Functionality</b></td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_special_dates.aspx">Special dates</a>
								</td>
								<td>cs_special_dates.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_date_range.aspx">Date range</a>
								</td>
								<td>cs_date_range.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_localization.aspx">Localization</a>
								</td>
								<td>cs_localization.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_multiple_months.aspx">Multiple months</a>
								</td>
								<td>cs_multiple_months.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_combobox.aspx">With comboboxes</a>
								</td>
								<td>cs_combobox.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="cs_date_expression.aspx">Enabled date expression</a>
								</td>
								<td>cs_date_expression.aspx</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div> 
		
		<div id="divVBNET">
			<table width="1024">
				<tr>
					<td valign="top">
						<table>
							<tr>
								<td class="header" colspan="2"><b>Styles</b></td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_default.aspx">Default</a>
								</td>
								<td>vb_default.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_graphite.aspx">Graphite</a>
								</td>
								<td>vb_graphite.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_desert.aspx">Desert</a>
								</td>
								<td>vb_desert.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_style10.aspx">Style 10</a>
								</td>
								<td>vb_style10.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_blue.aspx">Blue</a>
								</td>
								<td>vb_blue.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_simple.aspx">Simple</a>
								</td>
								<td>vb_simple.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_dark.aspx">Dark</a>
								</td>
								<td>vb_dark.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_expedia.aspx">Expedia</a>
								</td>
								<td>vb_expedia.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_orbitz.aspx">Orbitz</a>
								</td>
								<td>vb_orbitz.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_blocky_revert.aspx">Blocky revert</a>
								</td>
								<td>vb_blocky_revert.aspx</td>
							</tr>


							<tr>
								<td class="header" colspan="2"><b>Functionality</b></td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_special_dates.aspx">Special dates</a>
								</td>
								<td>vb_special_dates.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_date_range.aspx">Date range</a>
								</td>
								<td>vb_date_range.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_localization.aspx">Localization</a>
								</td>
								<td>vb_localization.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_multiple_months.aspx">Multiple months</a>
								</td>
								<td>vb_multiple_months.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_combobox.aspx">With comboboxes</a>
								</td>
								<td>vb_combobox.aspx</td>
							</tr>
							<tr>
								<td class="link">
									<a href="vb_date_expression.aspx">Enabled date expression</a>
								</td>
								<td>vb_date_expression.aspx</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</div> 
		<br />
		<br />
		<div class="tdText" style="width: 525px;">
		 </div>
        <br />
        <br />
        <a href="http://www.obout.com/calendar">obout ASP.NET Calendar home</a>
        <br />
        <a href="http://www.obout.com">obout inc home</a>
       </span>                             
    </form>
</body>
</html>
