<%@ Page Language="C#"%>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="C#" runat="server">

	void Page_Load(object sender, EventArgs e) {

		if(!Page.IsPostBack) {
			CreateGrid();	
		}
	}
	void CreateGrid()
	{
		OleDbConnection myConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("../App_Data/Northwind.mdb"));

		OleDbCommand myComm = new OleDbCommand("SELECT TOP 25 OrderID, ShipName, ShipCity, ShipPostalCode, ShipCountry, OrderDate, Format(OrderDate, 'dd-MM-yyyy') AS OrderDateValue FROM Orders ORDER BY OrderID DESC", myConn);
		myConn.Open();		
		OleDbDataReader myReader = myComm.ExecuteReader();

		grid1.DataSource = myReader;
		grid1.DataBind();

		myConn.Close();	
	}	
		
	void DeleteRecord(object sender, GridRecordEventArgs e)
	{
	}
	void UpdateRecord(object sender, GridRecordEventArgs e)
	{
	}
	void InsertRecord(object sender, GridRecordEventArgs e)
	{
	}
	void RebindGrid(object sender, EventArgs e)
	{
		CreateGrid();
	}
	
	DataTable getData() {

		if(Session["calendar_data"] == null) {
			DataRow dr;
			DataTable data = new DataTable();

			data.Columns.Add(new DataColumn("OrderID", typeof(int)));
			data.Columns.Add(new DataColumn("ShipName", typeof(string)));
			data.Columns.Add(new DataColumn("OrderDate", typeof(DateTime)));
			Session["calendar_data"] = data;

			for(int i=0; i<10; i++) {
				dr = data.NewRow();
				dr[0] = i;
				dr[1] = "some name " + i;
				dr[2] = DateTime.Now;
				data.Rows.Add(dr);
			}
		}

		return (DataTable) Session["calendar_data"];			
	}

</script>

<html>
	<head>
		<script type="text/javascript">
		
			function onBeforeDelete(record) {
				
			}
			function onBeforeUpdate(record) {	 
			   
				
			}
			function onBeforeInsert(record) {	    
				if(confirm("Are you sure you want to add the record?") == true) {
					return true
				} else {	    
					return false;
				}
			}
			
			function OnDelete(record) {		
				alert("The order with ID " + record.OrderID + " (for " + record.ShipName + ") was deleted. In a real application, the database will be updated.");
			}
			function OnUpdate(record) {						
				alert("The order with ID " + record.OrderID + " (for " + record.ShipName + ") was modified. In a real application, the database will be updated.");		
			}
			function OnInsert(record) {		
				alert("A new order was created. In a real application, the database will be updated.");					
			}
	
		</script>
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
				color:#CC033C;
			}
		</style>
	</head>
	<body>
		<form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Inside Obout ASP.NET Grid</b></span>
		    <br /><br />	
			<br /><br />  
		
		To see the calendar, edit a row and pick a date.
		
			<br /><br />			
			<obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" 
				 FolderStyle="../grid/styles/style_2" AutoGenerateColumns="false"
				OnRebind="RebindGrid" OnInsertCommand="InsertRecord"  OnDeleteCommand="DeleteRecord" OnUpdateCommand="UpdateRecord">
				<Columns>
					<obout:Column ID="Column1" DataField="OrderID" HeaderText="ORDER ID" Width="100" Visible="false" runat="server" />
					<obout:Column ID="Column2" DataField="ShipName" HeaderText="NAME" Width="190" runat="server"/>						
					<obout:Column ID="Column6" DataField="ShipCity" HeaderText="CITY" Width="115" runat="server"/>
					<obout:Column ID="Column3" DataField="ShipCountry" HeaderText="COUNTRY" Width="100" runat="server" />
					<obout:Column ID="Column4" DataField="OrderDateValue" HeaderText="ORDER DATE" Width="165" runat="server">
					    <TemplateSettings EditTemplateId="tplDatePicker" />
					</obout:Column>
					<obout:Column ID="Column5" DataField="" HeaderText="EDIT" Width="125" AllowEdit="true" AllowDelete="true" runat="server">
					    <TemplateSettings EditTemplateId="updateBtnTemplate" TemplateId="editBtnTemplate" />
					</obout:Column>
				</Columns>
				<ClientSideEvents OnClientDelete="OnDelete" OnClientUpdate="OnUpdate" OnClientInsert="OnInsert" OnBeforeClientDelete="onBeforeDelete" OnBeforeClientUpdate="onBeforeUpdate" OnBeforeClientInsert="onBeforeInsert" />
				<TemplateSettings NewRecord_TemplateId="addTemplate" NewRecord_EditTemplateId="saveTemplate"  />
				<Templates>								
					<obout:GridTemplate runat="server" ID="tplDatePicker" ControlID="txtDate" ControlPropertyName="value">
						<Template>
							<table cellspacing="0" cellpadding="0" style="border-collapse:collapse;">
								<tr>
									<td valign="top"><input type="text" id="txtDate" class="ob_gEC" style="width: 115px;" />
									</td>
									<td valign="top">			                      
										<obout:Calendar ID="cal1" runat="server" 
											StyleFolder="styles/default" 
											DatePickerMode="true" DateFormat="dd-MM-yyyy"
											TextBoxId="txtDate" 
											DatePickerImagePath ="styles/icon2.gif"/>
									</td>
								</tr>
							</table>
						</Template>
					</obout:GridTemplate>
					<obout:GridTemplate runat="server" ID="editBtnTemplate">
							<Template>
						<a class="ob_gAL" href="javascript: //" onclick="hideCalendar('<%# Container.DataItem["OrderDateValue"] %>');grid1.edit_record(this);return false;">Edit</a> 
							| 
							<a class="ob_gAL" href="javascript: //" onclick="hideCalendar();grid1.delete_record(this);return false;">Delete</a>
							</Template>
					</obout:GridTemplate>
					<obout:GridTemplate runat="server" ID="updateBtnTemplate">
							<Template>
							<a class="ob_gAL" href="javascript: //" onclick="hideCalendar();grid1.update_record(this);return false;">Update</a> 
							| 
							<a class="ob_gAL" href="javascript: //" onclick="hideCalendar();grid1.cancel_edit(this);return false;">Cancel</a>
							</Template>
					</obout:GridTemplate>
					<obout:GridTemplate runat="server" ID="addTemplate">
						<Template>                        
							<a class="ob_gAL" href="javascript: //" onclick="hideCalendar();grid1.addRecord();return false;">Add New</a>
						</Template>
					</obout:GridTemplate>
					 <obout:GridTemplate runat="server" ID="saveTemplate">
						<Template>
							<a class="ob_gAL" href="javascript: //" onclick="hideCalendar();grid1.insertRecord();return false;">Save</a> 
							| 
							<a class="ob_gAL" href="javascript: //" onclick="hideCalendar();grid1.cancelNewRecord();return false;">Cancel</a>
						</Template>
					</obout:GridTemplate>
				</Templates>
			</obout:Grid>	
		
            <br /><br /><br /><br /><br />
    	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>

<script type="text/javascript">

function hideCalendar(date) 
{	
    var ev;
    
    if(document.createEvent) {
        ev = document.createEvent("HTMLEvents");
        ev.initEvent("click", true, false);
    }
    
	var calendarContainer = document.getElementById("_grid1_tplDatePicker_ctl00_cal1Container");
	var calendarObject = grid1_tplDatePicker_ctl00_cal1;

	if (date != null)
	{
		var dateObj = date.split("-");
		var selectedDate = new Date(parseInt(dateObj[2]), parseInt(dateObj[1]) - 1, parseInt(dateObj[0]));
		calendarObject.setDate(selectedDate, selectedDate);
	}
	else
	{
		calendarObject.Clear();
		calendarObject.setDate(new Date(), null);
	}
	
    if(calendarContainer.style.display == "block") {
		calendarObject.displayCalendar(ev);
    }
}
</script>

