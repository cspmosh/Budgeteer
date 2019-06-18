<%@ Page Language="C#" Debug="true" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="C#" runat="server">
	void Page_Load(object sender, EventArgs e) {
		if(!Page.IsPostBack) {
			myGrid.DataSource = getData();
			myGrid.DataBind();
		}
	}

	//build a temporary table for current session
	DataTable getData() {

		if(Session["data"] == null) {
			DataRow dr;
			DataTable data = new DataTable();

			data.Columns.Add(new DataColumn("OrderID", typeof(int)));
			data.Columns.Add(new DataColumn("ShipName", typeof(string)));
			data.Columns.Add(new DataColumn("OrderDate", typeof(DateTime)));
			Session["data"] = data;

			for(int i=0; i<10; i++) {
				dr = data.NewRow();
				dr[0] = i;
				dr[1] = "some name " + i;
				dr[2] = DateTime.Now;
				data.Rows.Add(dr);
			}
		}

		return (DataTable) Session["data"];			
	}

	void myGrid_Edit(object o, DataGridCommandEventArgs e) {
		myGrid.EditItemIndex = e.Item.ItemIndex;
		myGrid.DataSource = getData();
		myGrid.DataBind();
	}

	void myGrid_Cancel(object o, DataGridCommandEventArgs e) {
		myGrid.EditItemIndex = -1;
		myGrid.DataSource = getData();
		myGrid.DataBind();
	}

	void myGrid_Update(object o, DataGridCommandEventArgs e) {
		DataRow dr;

		TextBox txt1;
		TextBox txt2;
		OboutInc.Calendar2.Calendar cal;

		dr = getData().Rows[myGrid.EditItemIndex];
		txt1 = (TextBox) myGrid.Items[myGrid.EditItemIndex].FindControl("txtId");
		txt2 = (TextBox) myGrid.Items[myGrid.EditItemIndex].FindControl("txtName");
		cal = (OboutInc.Calendar2.Calendar) myGrid.Items[myGrid.EditItemIndex].FindControl("calDate");
		
		dr[0] = txt1.Text;
		dr[1] = txt2.Text;
		dr[2] = cal.SelectedDate;
		
		myGrid.EditItemIndex = -1;
		myGrid.DataSource = getData();
		myGrid.DataBind();
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
	        <span class="tdText"><b>ASP.NET Calendar - Inside MS DataGrid</b></span>
		    <br /><br />	
			<br /><br />   
	        <asp:DataGrid runat="server" id="myGrid" DataKeyField="OrderID" AutoGenerateColumns="false"
			        OnEditCommand="myGrid_Edit"
			        OnCancelCommand="myGrid_Cancel"
			        OnUpdateCommand="myGrid_Update"
			        CellPadding="2">

		        <Columns>
			        <asp:TemplateColumn HeaderText="Order ID">
				        <ItemTemplate>
					        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderID") %>' />
				        </ItemTemplate>
				        <EditItemTemplate>
					        <asp:TextBox runat="server" id="txtId" Text='<%# DataBinder.Eval(Container.DataItem, "OrderID") %>' />
				        </EditItemTemplate>
			        </asp:TemplateColumn>

			        <asp:TemplateColumn HeaderText="Ordered By">
				        <ItemTemplate>
					        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ShipName") %>' />
				        </ItemTemplate>
				        <EditItemTemplate>
					        <asp:TextBox runat="server" id="txtName" Text='<%# DataBinder.Eval(Container.DataItem, "ShipName") %>' />
				        </EditItemTemplate>
			        </asp:TemplateColumn>

			        <asp:TemplateColumn HeaderText="Date">
				        <ItemTemplate>
					        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:MMMM dd, yyyy}") %>' />
				        </ItemTemplate>
				        <EditItemTemplate>
					        <asp:TextBox runat="server" id="txtDate" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate") %>' />
					        <obout:Calendar runat="server" id="calDate" DatePickerMode="true" 
								        TextBoxId="txtDate"
								        StyleFolder="styles/default" 
								        SelectedDate='<%# DataBinder.Eval(Container.DataItem, "OrderDate") %>'
								        DatePickerImagePath="styles/icon2.gif"
								        AllowDeselect = "false" />
				        </EditItemTemplate>
			        </asp:TemplateColumn>

			        <asp:EditCommandColumn EditText="Edit" CancelText="Cancel" UpdateText="Update" ItemStyle-Wrap="false" HeaderText="" HeaderStyle-Wrap="false" />
        			
		        </Columns>
	        </asp:datagrid>
	        <br />
	        <br />
	        We intentionally didn't include the styles we used on the <a href="http://www.obout.com/calendar2/grid.aspx">example</a> page. <br />
	        It is done so as not to clutter the sample code.
    
            <br /><br /><br /><br /><br />
    	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
    </body>
</html>