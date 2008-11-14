<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigator.ascx.cs" Inherits="AspNetApplication.Navigation.Navigator" %>
<div id="left">
    <h2>
        Controls</h2>
    <ul>
        <asp:Repeater ID="_controls" runat="server">
            <ItemTemplate>
                <li>
                    <a href="<%# DataBinder.Eval(Container.DataItem,"Href")  %>" ><%# DataBinder.Eval(Container.DataItem,"Title") %> </a>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <h2>
        Bits and pieces</h2>
    <ul>
        <asp:Repeater ID="_extras" runat="server">
            <ItemTemplate>
                <li>
                    <a href="<%# DataBinder.Eval(Container.DataItem,"Href")  %>" ><%# DataBinder.Eval(Container.DataItem,"Title") %> </a>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
