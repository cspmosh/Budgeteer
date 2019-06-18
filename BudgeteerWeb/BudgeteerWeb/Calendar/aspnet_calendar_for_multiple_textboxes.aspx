<%@ Page Language="C#" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2"  TagPrefix="obout" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="OboutInc.Calendar2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="C#" runat="server">

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
	
	}		

	public int UpdateRecord2(string SupplierID, string CompanyName, string Address, string Country)
	{
		return 1;
	}

	protected void OnGridRowCreated(object sender, GridRowEventArgs args)
    {
           
    }	
	
	protected void OnGridRowDataBound(object sender, GridRowEventArgs args)
    {
	 
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    
</script>
    <html>
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
			.tdText {
				font:11px Verdana;
				color:#333333;
			}
		</style>
		
		
	<script type="text/javascript">



	    function displayCalendar(txtBoxId, e) {

            if(Calendar1.isVisible==true)
            {
                Calendar1.displayCalendar(e);
            }
	        Calendar1.textBoxId = txtBoxId.id;
            Calendar1.displayCalendar(e);
            
	    }

	    function onSelect(sender, selectedDate) {

	        if (sender != null) {
	            document.getElementById(sender.textBoxId).value = sender.formatDate(selectedDate, 'M/d/yyyy');
	        }
	        
	        Calendar1.textBoxId = null;
	    }
	    
           

	function OnDelete(record) {		
		alert("The supplier with ID " + record.SupplierID + " (for " + record.CompanyName + ") was deleted. In a real application, the database will be updated.");
	}
	function OnUpdate(record) {						
		alert("The supplier with ID " + record.SupplierID + " (for " + record.CompanyName + ") was modified. In a real application, the database will be updated.");		
	}
	function OnInsert(record) {		
		alert("A new supplier was created. In a real application, the database will be updated.");					
	}
	
	function doSaveGrid(){
		alert("The grid data was modified. In a real application, the database will be updated.");	
		
	}	
	
    </script>
	</head>
	<body>
		<form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Single Calendar for multiple Textboxes</b></span>
		    <br /><br />	
			<br /><br /> 
			
	        <div style="display:none">
	        <obout:Calendar ID="Calendar1" runat="server" 
									            StyleFolder="styles/default" 
									            DatePickerMode="true"
									            DatePickerImagePath ="styles/icon2.gif" OnClientDateChanged="onSelect" DateFormat="M/d/yyyy"></obout:Calendar>
		        </div>
		        <br /><br />
		        <asp:SqlDataSource runat="server" ID="sds1" SelectCommand="SELECT *, Now() as Today FROM Suppliers"
		         ConnectionString="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|Northwind.mdb;" ProviderName="System.Data.OleDb"></asp:SqlDataSource>
        		 
		        <obout:Grid id="grid1" runat="server" DataSourceID="sds1" AllowAddingRecords="false" CallbackMode="false" Serialize="true" AllowRecordSelection="false"
			        AllowColumnResizing="true" AutoGenerateColumns="false" 
			         FolderStyle="../grid/styles/grand_gray" EnableTypeValidation="false"
			        OnRebind="RebindGrid" OnRowCreated="OnGridRowCreated" OnInsertCommand="InsertRecord" OnDeleteCommand="DeleteRecord" OnUpdateCommand="UpdateRecord">
			        <ClientSideEvents OnClientDelete="OnDelete" OnClientUpdate="OnUpdate" OnClientInsert="OnInsert"/>

			        <Columns>
				        <obout:Column ID="Column1" DataField="SupplierID" ReadOnly="true" HeaderText="ID" Width="45" runat="server">				
				            <TemplateSettings TemplateID="TmplSupplierID"/>
				        </obout:Column>				
				        <obout:Column ID="Column2" DataField="CompanyName" HeaderText="Company Name" Width="160" runat="server">				
				            <TemplateSettings TemplateID="TmplCompanyName"/>
				        </obout:Column>
				        <obout:Column ID="Column3" DataField="Address" HeaderText="Address" Width="190" runat="server">
				            <TemplateSettings TemplateID="TmplAddress"/>
				        </obout:Column>				
				        <obout:Column ID="Column4" DataField="Country" HeaderText="Country" Width="80" runat="server">
				            <TemplateSettings TemplateID="TmplCountry"/>
				        </obout:Column>				
				        <obout:Column ID="Column5" DataField="Today" HeaderText="Date" Width="130" runat="server">
					        <TemplateSettings TemplateID="TmplDatePicker" />
				        </obout:Column>
			        </Columns>			
                    <Templates>
				        <obout:GridTemplate runat="server" ID="TmplDatePicker" ControlID="txtDate" ControlPropertyName="value">
			                <Template>                   
			                   <input id="txtDate" runat="server" class="ob_gEC" onclick="displayCalendar(this,event)"
                                   style="width: 80px;" type="text" />
			                   <%--<obout:OboutTextBox ID="txtDate" runat="server" onclick="displayCalendar(this,event)"
                                   WatermarkText="click to open calender">
                               </obout:OboutTextBox>--%></Template>
			            </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="TmplSupplierID">
					        <Template>
						        <input type="text" id="txtSupplierID" class="ob_gEC" value="<%# Container.Value %>" disabled/>
					        </Template>
				        </obout:GridTemplate>
				        <obout:GridTemplate runat="server" ID="TmplCompanyName">
					        <Template>
						        <input type="text" id="txtCompanyName" class="ob_gEC" value="<%# Container.Value %>"/>
					        </Template>
				        </obout:GridTemplate>
                        <obout:GridTemplate runat="server" ID="TmplAddress">
					        <Template>
						        <input type="text" id="txtAddress" class="ob_gEC" value="<%# Container.Value %>"/>
					        </Template>
				        </obout:GridTemplate>
				        <obout:GridTemplate runat="server" ID="TmplCountry">
					        <Template>
						        <input type="text" id="txtCountry" class="ob_gEC" value="<%# Container.Value %>"/>
					        </Template>
				        </obout:GridTemplate>
			        </Templates>
		        </obout:Grid>	
       
            <br /><br /><br /><br /><br />
    	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>
