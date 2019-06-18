<%@ Page Language="C#" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
	private void Page_Load(object sender, System.EventArgs e)
	{
		Calendar1.DateFirstMonth = new DateTime(DateTime.Now.Year + 1, 1, 1) ;
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
			@media screen
			{
				body
				{
					margin:0px;
					padding:0px;
				}
				a 
				{
					font:11px Verdana;
					color:#315686;
					text-decoration:underline;
				}
				a:hover 
				{
					color:crimson;
				}
			}
			@media print
			{
				a
				{
					display:none;
				}
			}
		</style>
	</head>
	<body>
		<form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Print pop-up</b></span>
		    <br /><br />	
			<br /><br />  
			
			<div align="center" style="padding-top:10px;padding-bottom:10px;">
			<a href="javascript:window.print()">Print</a>
			<a href="javascript:window.close()">Close</a>
			</div>
			<div align="center">
			<obout:Calendar runat="server" id="Calendar1"
				Rows="4"
				Columns="3"
			/>
			</div>
			
			<br /><br /><br /><br /><br />
    	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>