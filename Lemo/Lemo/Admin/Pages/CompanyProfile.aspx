<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="CompanyProfile.aspx.cs" Inherits="Lemo.Admin.Pages.CompanyProfile" %>

<%@ Register Src="../Controls/CompanyInformation.ascx" TagName="CompanyInformation"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../ui/jquery-1.8.3.js"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            $("#divChange").slideUp();
            $(".butShowChangePassword").click(function () {
                if ($('#divChange').is(":hidden")) {
                    $("#divChange").slideDown();
                } else {
                    $("#divChange").slideUp();
                }
                return false;
            });
        });
    </script>
    <asp:Button runat="server" ID="butShowChangePassword" CssClass="butShowChangePassword"
        Text="Change Password" />
    <div style="width: 50%;" id="divChange">
        <fieldset>
            <div runat="server" id="divChangePasswordConfirmation" visible="false" style="font-weight: bold;
                font-size: 30px; color: Green;">
                Your password has been changed.
            </div>
            <div runat="server" id="divProblemChangePasswordConfirmation" visible="false" style="font-weight: bold;
                font-size: 30px; color: Red;">
                Your old password is not correct.
            </div>
            <legend>
                <asp:Label runat="server" ID="Label1" Text="Change Password" CssClass="TitleInnerPage"></asp:Label></legend>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblOldPassword" runat="server" Text="Old Password"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtOldPassword" ErrorMessage="Required" ID="RequiredFieldValidator1"
                            Display="Dynamic" ValidationGroup="vgChangePassword" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtPassword" ErrorMessage="Required" ID="RequiredFieldValidator4"
                            Display="Dynamic" ValidationGroup="vgChangePassword" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtConfirmPassword" ErrorMessage="Required" ID="RequiredFieldValidator5"
                            Display="Dynamic" ValidationGroup="vgChangePassword" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords do not match."
                            ValidationGroup="vgChangePassword" ForeColor="Red" ControlToCompare="txtPassword"
                            ControlToValidate="txtConfirmPassword" Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Image/Save.png" OnClick="butSignUp_Click"
                            ValidationGroup="vgChangePassword" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div>
        <uc1:CompanyInformation ID="CompanyInformation1" runat="server" />
    </div>
</asp:Content>
