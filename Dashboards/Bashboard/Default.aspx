<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Dashboard.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Silverlight Project Test Page </title>
    
    <script type="text/javascript" src="Silverlight.js"></script>
    <script type="text/javascript" >
    
    function createSilverlight()
    {
	    Silverlight.createObjectEx({
		    source: "Page.xaml",
		    parentElement: document.getElementById("SilverlightControlHost"),
		    id: "SilverlightControl",
		    properties: {
			    width: "800px",
			    height: "600px",
			    version: "1.1",
			    enableHtmlAccess: "true"
		    },
		    events: {}
	    });
    	   
	    // Give the keyboard focus to the Silverlight control by default
        document.body.onload = function() {
          var silverlightControl = document.getElementById('SilverlightControl');
          if (silverlightControl)
          silverlightControl.focus();
        }

    }

    </script>
    <style type="text/css">
        .silverlightHost { width: 800px; height: 600px; }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div id="SilverlightControlHost" class="silverlightHost" >
        <script type="text/javascript">
            createSilverlight();
        </script>
    </div>    
    </form>
</body>
</html>
