<%@ Page Language="C#" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
	        <span class="tdText"><b>ASP.NET Calendar - Disable month scolling</b></span>
		    <br /><br />	
			<br /><br />  
			
		    <obout:Calendar runat="server" 
			    StyleFolder="styles/default" 
			    TextArrowLeft=""
			    TextArrowRight=""
		       />
		       <br />
	        <span class="tdText">You can disable the month scrolling by set properties <b>TextArrowLeft</b> and <b>TextArrowRight</b> to empty.<br /><br />

            By default, it is set to "<<" respectively to ">>". </span>
            
	        <br /><br /><br /><br /><br />
    	    
	        <a style="font-size:10pt;font-family:Tahoma" href="Default.aspx?type=ASPNET">« Back to examples</a>
	        
	    </form>
	</body>
</html>
