<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Logoff.aspx.cs" Inherits="WuKai1327SkySharkWebApplication.Logoff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    HOME
    <style type="text/css">
        #TextAreal {
            width: 524px;
            height: 151px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div style="height: 201px; width: 605px">
    <textarea id="TextAreal">
        Thank you for using SkyShark Airline.
        Looking forward for serving you again.
    </textarea>
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/default.aspx">Click here to logon.
</asp:HyperLink>
    </div>
    </form>
</asp:Content>
