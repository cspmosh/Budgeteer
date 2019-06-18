<%@ Page Language="C#" %>

<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>

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
		OboutInc.Calendar2.Calendar calendar = (OboutInc.Calendar2.Calendar)args.Row.FindControl("Calendar1");
        TextBox txtDate = (TextBox)args.Row.FindControl("txtDate");
		
		if (txtDate != null)
			Response.Write(txtDate.ClientID + "<br />");
		
		if (calendar != null)
			calendar.TextBoxId = txtDate.UniqueID;
            
    }	
	
	protected void OnGridRowDataBound(object sender, GridRowEventArgs args)
    {
		OboutInc.Calendar2.Calendar calendar = (OboutInc.Calendar2.Calendar)args.Row.FindControl("Calendar1");
        TextBox txtDate = (TextBox)args.Row.FindControl("txtDate");
		
		if (txtDate != null)
			Response.Write(txtDate.ClientID + "<br />");
		
		if (calendar != null)
			calendar.TextBoxId = txtDate.UniqueID;
            
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
	<script type="text/javascript">
	function OnDelete(record) {		
		alert("The supplier with ID " + record.SupplierID + " (for " + record.CompanyName + ") was deleted. In a real application, the database will be updated.");
	}
	function OnUpdate(record) {						
		alert("The supplier with ID " + record.SupplierID + " (for " + record.CompanyName + ") was modified. In a real application, the database will be updated.");		
	}
	function OnInsert(record) {		
		alert("A new supplier was created. In a real application, the database will be updated.");					
	}
	
	function doSaveGrid(){/*
		var tfArrays = document.getElementsByTagName("input");
		var rowCount = 0;
		for (var i=0; i< tfArrays.length; i++ ){
			var tf = tfArrays[i];
			
			if ( tf.type=="text" && tf.id=="txtSupplierID" ){
				// get txtSupplierID
				var sSupplierID = tf.value;
				
				// get txtCompanyName
				while ( i< tfArrays.length && tfArrays[i].id !="txtCompanyName" ) i++;
				var sCompanyName = tfArrays[i].value;
				
				// get txtAddress
				while ( i< tfArrays.length && tfArrays[i].id !="txtAddress" ) i++;
				var sAddress = tfArrays[i].value;
				
				// get txtCountry
				while ( i< tfArrays.length && tfArrays[i].id !="txtCountry" ) i++;
				var sCountry = tfArrays[i].value;
										
				try{

					if(typeof ob_post == "object")
					{
						// add the parameter needed on server-side
						// SupplierID, CompanyName, Address, Country will be a server-side argument for the UpdateRecord2 method
						
						ob_post.AddParam("SupplierID", sSupplierID);	
						ob_post.AddParam("CompanyName", sCompanyName);
						ob_post.AddParam("Address", sAddress);
						ob_post.AddParam("Country", sCountry);
						
						var result = ob_post.post(null, "UpdateRecord2");

						if (result <= 0)
						{
							alert("Update record error");
						}
					}
					else
					{
						alert("Please add obout_Postback ASP.NET control to your page to use the AjaxPage");
					} 
				}catch (ex){
					alert(ex);
				}
			}
		}
		grid1.refresh();*/
		alert("The grid data was modified. In a real application, the database will be updated.");	
		
	}	
	
</script>
	</head>
	<body>
		<form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Multiple Calendar controls inside Obout ASP.NET Grid</b></span>
		    <br /><br />	
			<br /><br />  
	
		    <asp:SqlDataSource runat="server" ID="sds1" SelectCommand="SELECT *, Now() as Today FROM Suppliers"
		     ConnectionString="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|Northwind.mdb;" ProviderName="System.Data.OleDb"></asp:SqlDataSource>
    		 
		    <obout:Grid id="grid1" runat="server" DataSourceID="sds1" AllowAddingRecords="false" CallbackMode="false" Serialize="true" AllowRecordSelection="false"
			    AllowColumnResizing="true" AutoGenerateColumns="false" 
			     FolderStyle="../grid/styles/style_9" EnableTypeValidation="false"
			    OnRebind="RebindGrid" OnRowCreated="OnGridRowCreated" OnInsertCommand="InsertRecord" OnDeleteCommand="DeleteRecord" OnUpdateCommand="UpdateRecord">
			    <ClientSideEvents OnClientDelete="OnDelete" OnClientUpdate="OnUpdate" OnClientInsert="OnInsert"/>

			    <Columns>
				    <obout:Column DataField="SupplierID" ReadOnly="true" HeaderText="ID" Width="45" runat="server">				
				        <TemplateSettings TemplateID="TmplSupplierID"/>
				    </obout:Column>				
				    <obout:Column DataField="CompanyName" HeaderText="Company Name" Width="160" runat="server">				
				        <TemplateSettings TemplateID="TmplCompanyName"/>
				    </obout:Column>
				    <obout:Column DataField="Address" HeaderText="Address" Width="190" runat="server">
				        <TemplateSettings TemplateID="TmplAddress"/>
				    </obout:Column>				
				    <obout:Column DataField="Country" HeaderText="Country" Width="80" runat="server">
				        <TemplateSettings TemplateID="TmplCountry"/>
				    </obout:Column>				
				    <obout:Column DataField="Today" HeaderText="Date" Width="130" runat="server">
					    <TemplateSettings TemplateID="TmplDatePicker" />
				    </obout:Column>
			    </Columns>			
                <Templates>
				    <obout:GridTemplate runat="server" ID="TmplDatePicker" ControlID="txtDate" ControlPropertyName="value">
			            <Template>
			                <table cellspacing="0" cellpadding="0" style="border-collapse:collapse;">
			                    <tr>
			                        <td valign="top"><asp:TextBox id="txtDate" runat="server" CssClass="ob_gEC" style="width: 80px;" />
			                        </td>
			                        <td valign="top">			                      
			                            <obout:Calendar ID="Calendar1" runat="server" 
									        StyleFolder="styles/default" 
									        DatePickerMode="true"
									        DatePickerImagePath ="styles/icon2.gif"/>
							        </td>
					            </tr>
						    </table>
			            </Template>
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
