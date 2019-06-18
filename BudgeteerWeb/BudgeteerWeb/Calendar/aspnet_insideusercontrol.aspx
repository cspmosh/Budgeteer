<%@ Register Src="aspnet_insideusercontrol.ascx" TagName="WebUserControl" TagPrefix="uc1" %>

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
            .text
            {
	            font-size:11px;
	            background-color:#cccccc;
	            text-align:center;
            }
            .textContent
            {
	            font-size:11px;
	            text-align:center;
            }
        </style>
    </head>
    <body style="width:100%;height:100%">
        <form id="Form1" runat="server">
	        <span class="tdText"><b>ASP.NET Calendar - Inside User Control</b></span>
		    <br /><br />	
			<br /><br />  
	
	        <uc1:WebUserControl ID="WebUserControl1" runat="server" />
	        <br /><br /><br />
	        Inside User Control
	        <uc1:WebUserControl ID="WebUserControl2" runat="server" />
	        <br /><br /><br />
	        Inside User Control
	        <uc1:WebUserControl ID="WebUserControl3" runat="server" />
         
            <br /><br /><br /><br /><br />
    	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
    </body>
</html>
