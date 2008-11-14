<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="AspNetApplication.Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        Welcome</h2>
    <p>
        There are 11 dials and gauges in the Codeplex.Dashboarding library. This tutorial
        will show you how to embed the Silverlight guages and dials into your own silverlight
        applications.
    </p>get the latest version of the controls from the Codeplx project 
    <asp:HyperLink ID="HyperLink2" NavigateUrl="http://www.codeplex.com/dashboarding" runat="server">David's Silverlight Dashboards</asp:HyperLink>.

    <p>
        Before we start here is a little gratuitous eye-candy to show you where we are going
        :-) (Hover over a control to see the control name)</p>
    <br />
    <object type="application/x-silverlight" height="200" width="800">
        <param name="source" value="ClientBin/SilverlightApplication.xap" />
        <!-- startPage key can have values Page1 or Page2 -->
        <param name="initParams" value="demo=TopStrip" />
        <param name="background" value="Transparent" />
        <param name="windowless" value="True" />
    </object>   
    <br />
    <p>
        The above example shows the following controls:</p>
    <ul>
        <li>Dial360</li>
        <li>DecadeVuMeter</li>
        <li>RoundLed</li>
        <li>TickCross</li>
        <li>PerformanceMonitor</li>
        <li>FiveStarRanking </li>
        <li>SixteenSegmentLed</li>
        <li>Odometer</li>
        <li>Thermometer</li>
        <li>ProgressBar</li>
        
    </ul>
    <p>
        New dials and gauges will be added as time goes by, so check back here ocassionally
        or visit the <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://www.codeplex.com/dashboarding">Codeplex project</asp:HyperLink> to see whats new.</p>
    <h2>
        Getting started</h2>
    <p>
        Before we wade in to the XAML for declaring dashboard controls, we need to look
        at the requirements for creating a project using them. The controls are declared
        in an assembly named <code>Codeplex.Dashboarding.dll</code> you can find the assembly
        in the xap file shiped with the example .NET solution (rename.xap to .zip and extract).
    </p>
    <p>
        As time goes by we may move the assembly into an easier to find location! Once you
        have the assembly from the xap file, add it as a reference to your Silverlight application</p>
    <p>
        To add controls to a XAML file you need to create a namespace, to make the samples
        consistant we will use the following:</p>
    <p>
        <code>xmlns:db="clr-namespace:Codeplex.Dashboarding;assembly=Codeplex.Dashboarding"
 
        </code>
    </p>
    <p>
        The namespace with the prefix <code>db</code> contains all dashboard controls.
    <p>
        If you prefer using Expression Blend over hacking XAML then Blend will add a namespace
        declaration automatically for the <code>Codeplex.Dashboarding</code>
        namespace when you drag a control on to the design surface. 
    <h3>
        First steps
    </h3>
    <p>
        Once you have added the reference to <code>Codeplex.Dashboarding</code> to your
        project and added the two namespace declarations to the top of a control try declaring
        a single dial ( <code>&lt;db:Dial360/&gt;</code>) and see what happens:
    </p>
    <table height="180">
        <tr>
            <td>
                <pre class="csharpcode">
<span class="kwrd">&lt;</span><span class="html">UserControl</span> <span class="attr">x:Class</span><span
    class="kwrd">="SilverlightApplication.SingleGuages.FirstStep"</span>
    <span class="attr">xmlns</span><span class="kwrd">="http://schemas.microsoft.com/winfx/2006/xaml/presentation"</span> 
    <span class="attr">xmlns:x</span><span class="kwrd">="http://schemas.microsoft.com/winfx/2006/xaml"</span> 
    <span class="attr">xmlns:db</span><span class="kwrd">="clr-namespace:Codeplex.Dashboarding;assembly=Codeplex.Dashboarding"</span>
        class="kwrd">&gt;</span>
    <span class="kwrd">&lt;</span><span class="html">Grid</span> <span class="attr">x:Name</span><span
        class="kwrd">="LayoutRoot"</span> <span class="attr">Background</span><span class="kwrd">="Transparent"</span><span
            class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">db:Dial360</span> <span class="kwrd">/&gt;</span>
    <span class="kwrd">&lt;/</span><span class="html">Grid</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">UserControl</span><span class="kwrd">&gt;</span></pre>
            </td>
            <td>
                <object type="application/x-silverlight" height="160">
                    <param name="source" value="ClientBin/SilverlightApplication.xap" />
                    <param name="initParams" value="demo=FirstStep" />
                    <param name="background" value="Transparent" />
                    <param name="windowless" value="True" />
                </object>
            </td>
        </tr>
    </table>
    <p>
        Quite unsurprisingly you get a single dial with the default color and no value set.
        We can set a value two ways, using the value attribute in XAML or by setting the
        Value property in the code behind. The value should be in the range 0..100 for now,
        in a subsequent release we will more add Minimum, Maximum properties to allow any
        range to be set..</p>
    <p>
        In markup the value can be set as</p>
    <table height="180">
        <tr>
            <td>
        <pre class="csharpcode">
    <span class="kwrd">&lt;</span><span class="html">UserControl</span> <span class="attr">x:Class</span><span
        class="kwrd">="SilverlightApplication.SingleGuages.SecondStep"</span>
       <span class="attr">xmlns</span><span class="kwrd">="http://schemas.microsoft.com/winfx/2006/xaml/presentation"</span> 
        <span class="attr">xmlns:x</span><span class="kwrd">="http://schemas.microsoft.com/winfx/2006/xaml"</span> 
        <span class="attr">xmlns:db</span><span class="kwrd">="clr-namespace:Codeplex.Dashboarding;assembly=Codeplex.Dashboarding"</span>
            class="kwrd">&gt;</span>
        <span class="kwrd">&lt;</span><span class="html">Grid</span> <span class="attr">x:Name</span><span
            class="kwrd">="LayoutRoot"</span> <span class="attr">Background</span><span class="kwrd">="Transparent"</span><span
                class="kwrd">&gt;</span>
            <span class="kwrd">&lt;</span><span class="html">db:Dial360</span> <span class="attr">Value</span><span
                class="kwrd">="66"</span> <span class="kwrd">/&gt;</span>
        <span class="kwrd">&lt;/</span><span class="html">Grid</span><span class="kwrd">&gt;</span>
    <span class="kwrd">&lt;/</span><span class="html">UserControl</span><span class="kwrd">&gt;</span></pre>
            </td>
            <td>
                <object type="application/x-silverlight" height="160">
                    <param name="source" value="ClientBin/SilverlightApplication.xap" />
                    <param name="initParams" value="demo=SecondStep" />
                    <param name="background" value="Transparent" />
                    <param name="windowless" value="True" />
                </object>
            </td>
        </tr>
    </table>
    <p>
        Doing this in the code behind is hardly difficult (except this time we added an
        x:Name attribute to the dial)</p>
    <table height="180">
        <tr>
            <td>
                <pre class="csharpcode">
 <span class="kwrd">public</span> <span class="kwrd">partial</span> <span class="kwrd">class</span> ThirdStep : UserControl
    {
        <span class="kwrd">public</span> ThirdStep()
        {
            InitializeComponent();
            Loaded += <span class="kwrd">new</span> RoutedEventHandler(IamLoaded);
        }

        <span class="kwrd">void</span> IamLoaded(<span class="kwrd">object</span> sender, RoutedEventArgs e)
        {
            Random random = <span class="kwrd">new</span> Random();
            _dial.Value = random.Next(100);
        }
    }</pre>
            </td>
            <td>
                <object type="application/x-silverlight" height="160">
                    <param name="source" value="ClientBin/SilverlightApplication.xap" />
                    <param name="initParams" value="demo=ThirdStep" />
                    <param name="background" value="Transparent" />
                    <param name="windowless" value="True" />
                </object>
            </td>
        </tr>
    </table>
    
    <p>So far we have set the value for the gauge directy in markup and then in code. The
    final appreach to setting the value is data binding. In the next and final demo
    we data bind the value property to the <code>MilesPerHour</code> property of a
    <code>Car</code> class.</p>
    <p>I don't know the <code>Car</code> class says about its author, but the only interesting
    item about a car appears to be how fast it is going :-)</p>
    
    <pre class="csharpcode"> <span class="kwrd">            public</span> <span class="kwrd">class</span> Car
            {
                <span class="kwrd">public</span> <span class="kwrd">double</span> MilesPerHour { get; set; }
            }</pre>

<p>Next we need to crate a new control in mark up and declarativly bind the
<code>Value</code> property to the cars <code>MilesPerHour</code> property</p>
    
<pre class="csharpcode">
            <span class="kwrd">&lt;</span><span class="html">UserControl</span> <span class="attr">x:Class</span><span class="kwrd">="SilverlightApplication.SingleGuages.FourthStep"</span>
               <span class="attr">xmlns</span><span class="kwrd">="http://schemas.microsoft.com/winfx/2006/xaml/presentation"</span> 
                <span class="attr">xmlns:x</span><span class="kwrd">="http://schemas.microsoft.com/winfx/2006/xaml"</span> 
                <span class="attr">xmlns:db</span><span class="kwrd">="clr-namespace:Codeplex.Dashboarding;assembly=Codeplex.Dashboarding"</span>
                <span class="kwrd">&gt;</span>
                <span class="kwrd">&lt;</span><span class="html">Grid</span> <span class="attr">x:Name</span><span class="kwrd">="LayoutRoot"</span> <span class="attr">Background</span><span class="kwrd">="Transparent"</span><span class="kwrd">&gt;</span>
                    <span class="kwrd">&lt;</span><span class="html">db:Dial360</span> <span class="attr">x:Name</span><span class="kwrd">="_dial"</span>  <span class="attr">Value</span><span class="kwrd">="{Binding MilesPerHour, Mode=OneWay}"</span>  <span class="kwrd">/&gt;</span>
                <span class="kwrd">&lt;/</span><span class="html">Grid</span><span class="kwrd">&gt;</span>
            <span class="kwrd">&lt;/</span><span class="html">UserControl</span><span class="kwrd">&gt;</span></pre>    
                
<p>Finally in our code behind we set the controls DataContext to an instance of a new
car.</p>
    
    
  <pre class="csharpcode">
             Car porsche = <span class="kwrd">new</span> Car { MilesPerHour = 99 };
             _dial.DataContext = porsche;
             </pre>  
    
    <table height="180">
  
  
     
        <tr>
        <td>                <object type="application/x-silverlight" height="160">
                    <param name="source" value="ClientBin/SilverlightApplication.xap" />
                    <param name="initParams" value="demo=FourthStep" />
                    <param name="background" value="Transparent" />
                    <param name="windowless" value="True" />
                </object>
</td>
        <td>et viola! (Which is a French expression - I believe - about musical instruments)</td>

        </tr>


            

    </table>
    <p>
        That's the end of this general overview of the <code>Codeplex.Dashboarding</code>
        assembly. Please select a control from the list on the left to get a more detailed
        description.</p>
</asp:Content>
