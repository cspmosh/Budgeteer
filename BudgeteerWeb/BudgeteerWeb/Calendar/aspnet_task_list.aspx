<%@ Page EnableEventValidation="false" Language="C#" Inherits="OboutInc.oboutAJAXPage"%>

<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Flyout2" Assembly="obout_Flyout2_NET"%>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="oajax" Namespace="OboutInc" Assembly="obout_AJAXPage" %> 

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ import Namespace="System.Globalization"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="C#" runat="server">
    DateTime selectedDate = DateTime.Now;
    
    void Page_load(object sender, EventArgs e)
    {
		calDate.DateFirstMonth = new DateTime(2007, 8, 1);
		
        if (Page.IsPostBack)
        {
            if (Session["SelectedDate"] != null)
            {
				IFormatProvider culture = new CultureInfo("en-US", true);
                selectedDate = DateTime.Parse(Session["SelectedDate"].ToString(), culture, DateTimeStyles.NoCurrentDateDefault);
            }

        }
        else
        {
            // set the tooltip and CSS for scheduled date
            SetScheduledDate();
            
        }
        CreateGrid();
        
    }
    void SetScheduledDate()
    {
        OleDbConnection myConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("../App_Data/tasklist.mdb"));

        OleDbCommand myComm = new OleDbCommand("SELECT * FROM Tasks ORDER BY `Date` ASC", myConn);
        myConn.Open();

        myComm.Parameters.Add("@Date", OleDbType.Date).Value = selectedDate;
        OleDbDataReader myReader = myComm.ExecuteReader();
        DateTime pre = DateTime.MinValue;
        DateTime curr;
        String tooltip = "Tasks List: ";
        
        while ( myReader.Read() ){
            curr = myReader.GetDateTime(4);
            if (DateTime.Compare(curr, pre) == 0)
            {
                // pre date is equals to current date
                //get task name
                tooltip = tooltip + myReader.GetString(1) + "; ";
            }
            else
            {
                if (pre != DateTime.MinValue)
                {
                    calDate.AddSpecialDate(pre, tooltip, "taskDate");
                }
                //get task name
                tooltip = "Tasks List: " + myReader.GetString(1) + "; ";
                pre = curr;                
            }
                
        }
        if (pre != DateTime.MinValue)
        {
            calDate.AddSpecialDate(pre, tooltip, "taskDate");
        }

        grid1.DataSource = myReader;
        grid1.DataBind();        
    }

    public void LoadTaskList(String SelectedDate)
    {
        Session["SelectedDate"] = SelectedDate;
    }

    void CreateGrid()
    {
        OleDbConnection myConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("../App_Data/tasklist.mdb"));

        OleDbCommand myComm = new OleDbCommand("SELECT TOP 25 TaskID, TaskName, TaskDescription, Owner FROM Tasks WHERE Date = @Date ORDER BY TaskID ASC", myConn);
        myConn.Open();

        myComm.Parameters.Add("@Date", OleDbType.Date).Value = selectedDate;
        OleDbDataReader myReader = myComm.ExecuteReader();

        grid1.DataSource = myReader;
        grid1.DataBind();

        myConn.Close();
    }

    void DeleteRecord(object sender, GridRecordEventArgs e)
    {
        OleDbConnection myConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("../App_Data/tasklist.mdb"));
        myConn.Open();

        OleDbCommand myComm = new OleDbCommand("DELETE FROM Tasks WHERE TaskID = @TaskID", myConn);

        myComm.Parameters.Add("@TaskID", OleDbType.Integer).Value = e.Record["TaskID"];

        myComm.ExecuteNonQuery();
        myConn.Close();
    }
    void UpdateRecord(object sender, GridRecordEventArgs e)
    {
        OleDbConnection myConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("../App_Data/tasklist.mdb"));
        myConn.Open();

        OleDbCommand myComm = new OleDbCommand("UPDATE Tasks SET TaskName = @TaskName, TaskDescription = @TaskDescription, Owner=@Owner WHERE TaskID = @TaskID", myConn);

        myComm.Parameters.Add("@TaskName", OleDbType.VarChar).Value = e.Record["TaskName"];
        myComm.Parameters.Add("@TaskDescription", OleDbType.VarChar).Value = e.Record["TaskDescription"];
        myComm.Parameters.Add("@Owner", OleDbType.VarChar).Value = e.Record["Owner"];
        myComm.Parameters.Add("@TaskID", OleDbType.Integer).Value = e.Record["TaskID"];

        myComm.ExecuteNonQuery();
        myConn.Close();
    }
    void InsertRecord(object sender, GridRecordEventArgs e)
    {
        OleDbConnection myConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("../App_Data/tasklist.mdb"));
        myConn.Open();

        OleDbCommand myComm = new OleDbCommand("INSERT INTO Tasks (TaskName, TaskDescription, Owner, `Date`) VALUES(@TaskName, @TaskDescription, @Owner, @Date)", myConn);

        myComm.Parameters.Add("@TaskName", OleDbType.VarChar).Value = e.Record["TaskName"];
        myComm.Parameters.Add("@TaskDescription", OleDbType.VarChar).Value = e.Record["TaskDescription"];
        myComm.Parameters.Add("@Owner", OleDbType.VarChar).Value = e.Record["Owner"];
        myComm.Parameters.Add("@Date", OleDbType.VarChar).Value = Session["SelectedDate"].ToString();

        myComm.ExecuteNonQuery();
        myConn.Close();
    }
    void RebindGrid(object sender, EventArgs e)
    {
        CreateGrid();
    }
	</script>		

<html>
	<head runat="server">
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
			.taskDate{
                border:0px solid #993766;
                color:crimson;
                font-family:Tahoma;
                font-size:11px;
                font-size-adjust:none;
                font-stretch:normal;
                font-style:normal;
                font-variant:normal;
                font-weight:bold;
                line-height:normal;
                padding:1px;
                text-align:center;			
			}		
		</style>
		<script type="text/javascript">
		    var preSelectedDate;
			
			function UpdateTaskListPanel() {
				grid1.refresh();
			}			
		</script>
	</head>
	<body>
		<script type="text/javascript">
			//this function is called when date is selected in calendar
			function onDateChange(sender, selectedDate) {
				if (selectedDate == null){
					selectedDate = preSelectedDate;
				}else{
					preSelectedDate = selectedDate;
				}
				
				var month = selectedDate.getMonth() + 1;
				var day = selectedDate.getDate();
				var year = selectedDate.getFullYear();
				<%=Flyout1.getClientID()%>.Open();
				
				// set the flyout title
				document.getElementById("title").innerHTML = "Task list on: " + month + "/" + day + "/" + year; 
				// load tasks list detail
				ob_post.AddParam("SelectedDate", month + "/" + day + "/" + year);
				
				ob_post.post(null, "LoadTaskList", UpdateTaskListPanel);
				
			}
		</script>
		<br />
		<form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Task list</b></span>
		    <br /><br />	
			<br /><br />   
		
		    Select a date:
		    <obout:Calendar runat="server" id="calDate"
						    OnClientDateChanged="onDateChange"
						    DateMin="1/1/1999"
						    DateMax = "12/31/2010" />
		    <br />
		    <obout:Flyout runat="server" ID="Flyout1"  AttachTo="_calDateContainer"  OpenEvent="NONE" CloseEvent="NONE"
		     Position="ABSOLUTE" RelativeLeft="380" RelativeTop="0">
                <table cellpadding="0" cellspacing="0" border="0"  style="background: #E8E8D0;border:1px solid gray;">
                    <tr>
                        <td>
                            <obout:DragPanel runat="server" DraggingOpacity="50">
                                <span style="width:470px; height:21px;" class="tdtext">&nbsp;&nbsp;&nbsp;<b id="title"></title>&#160;</span>
                            </obout:DragPanel>            
                                               
                        </td>
                        <td align="right" style="width:22px;height:21px;background-image:url(../Flyout/images/wishlist_close.gif);"><span style="cursor:pointer;width:22px;height:21px;" onclick="<%=Flyout1.getClientID()%>.Close()">&#160;&#160;&#160;</span></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                                <obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true" EnableTypeValidation="true" 
	                                 FolderStyle="../grid/styles/style_2" AutoGenerateColumns="false" AllowColumnResizing="true" AllowRecordSelection="true" AllowPageSizeSelection="false"
	                                OnRebind="RebindGrid" OnInsertCommand="InsertRecord"  OnDeleteCommand="DeleteRecord" OnUpdateCommand="UpdateRecord">
	                                <Columns>
		                                <obout:Column DataField="TaskID" HeaderText="Task ID" Width="100" Visible="false" runat="server" />
		                                <obout:Column DataField="TaskName" HeaderText="Task Name" Width="100" runat="server"/>											
		                                <obout:Column DataField="TaskDescription" HeaderText="TaskDescription" Width="200" runat="server"/>
		                                <obout:Column DataField="Owner" HeaderText="Owner" Width="70" runat="server" />
		                                <obout:Column DataField="" HeaderText="EDIT" Width="125" AllowEdit="true" AllowDelete="true" runat="server" />
	                                </Columns>
                                </obout:Grid>  						      	    	                      
                        </td>
                    </tr>
                </table>
            </obout:Flyout>	
		<br />
		
        Try to click on the following dates: <b>15 Aug 2007</b>, <b>17 Aug 2007</b> or <b>22 Aug 2007</b>.<br>
        These dates were scheduled in task list database. <br><br>
        For each scheduled date you can set CSS class name and a ToolTip. <br><br>
        
        By clicking on any date, you also can edit/delete or add more tasks,<br>
        which are shown in a grid inside a draggable panel.
		
		<br /><br /><br /><br /><br />
    	    
	    <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	      
	    </form>  
	</body>
</html>