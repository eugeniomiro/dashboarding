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
			    width: "1000px",
			    height: "440px",
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
        .silverlightHost { width: 1000px; height: 440px; }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <p>What you are seeing here are several  basic 
    dashboard controls. Half of them update client side when you click the white rectange  below,
    the others periodically update using a web service. All controls have configuarble properties to modify their appearance </p>
    
    <p>Web service are configurable and invoked asynchronously through reflection. Hopefully this project canmove forward from this basic
    framework. Next steps</p>
    <ul>
        <li>Put some of those comment thingies in the code. Apparently some people like em :-)</li>
        <li>Test and extend the web-service configuration. Is there an app.config in silverlight??</li>
        <li>Add more dashboard items, dials, thermometers, boolean displays, progress bars, you name it</li>
    </ul>
    
    <div id="SilverlightControlHost" class="silverlightHost" >
        <script type="text/javascript">
            createSilverlight();
        </script>
    </div>
    
    </div>
    </form>
    <p>
        That's the 'button' up there. No point doing anything better until Microsoft fuel our addictions with a new Beta release is there?</p>
</body>
</html>
