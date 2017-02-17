<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="Lemo.Admin.Controls.Login" %>
<script type="text/javascript" src="../../../UI/jquery-1.8.3.js"></script>
<script src="../../Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    $(function () {
        $(".cbCorporate input").removeAttr("checked");
        $(".cbCorporate").click(function () {
            if ($(".cbCorporate input").attr("checked"))
                $(".signupCompany").hide();  // unchecked
            else
                $(".signupCompany").show();  // checked          
        });
    });
    </script>
<div style="padding: 5px; margin: 5px;">    
    <asp:Panel style="text-align: left;" runat="server" ID="panelMain" DefaultButton="butLogin">
        <fieldset style="width: 90%;">
            <legend>
                <asp:Label runat="server" ID="lblTitle" Text="Affiliate/Corporate" CssClass="TitleInnerPage"></asp:Label></legend>
                <asp:Label runat="server" ID="lblError" Text="Incorrect username or password or your company not active"
        CssClass="TitleError" Visible="false"></asp:Label>
            <table>
                 <tr>
                    <td class="style1">
                    <asp:CheckBox runat="server" ID="cbCorporate" CssClass="cbCorporate" />
                        <asp:Label ID="lbl" runat="server" Text="Is Corporate?"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtUsername" runat="server" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtUsername" ErrorMessage="Required" ID="RequiredFieldValidator0"
                            ValidationGroup="vgLoginAdmin" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPassword" runat="server" Width="150px" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
                            ControlToValidate="txtPassword" ErrorMessage="Required" ID="RequiredFieldValidator1"
                            ValidationGroup="vgLoginAdmin" runat="server" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton runat="server" ID="butSignUp" ImageUrl="~/Image/signupbtn.png" OnClick="butSignUp_Click" CssClass="signupCompany" />
                        <asp:ImageButton runat="server" ID="butLogin" ValidationGroup="vgLoginAdmin" ImageUrl="~/Image/loginbtn.png"
                            OnClick="butLogin_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    </div>