<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="ManageCompany.aspx.cs" Inherits="Lemo.Admin.Pages.ManageCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; padding: 10px;">
        <table width="100%">
            <tr>
                <td>
                    <asp:Label runat="server" ID="Label1" Text="Manage Companies" CssClass="TitleInnerPage"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:TextBox runat="server" ID="txtFilter"></asp:TextBox>
                    <asp:Button runat="server" ID="butSearch" Text="Search by Phone" 
                        onclick="butSearch_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="butClear" Text="Clear Search" 
                        onclick="butClear_Click" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br />
                    <div runat="server" id="divProblem" visible="false" style="font-weight: bold; font-size: 30px;
                        color: Red;">
                        You can not delete this car.
                        <br />
                    </div>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                        AllowPaging="true" PageSize="10" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" GridLines="Vertical" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:TemplateField HeaderText="Company">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCompany" Text='<%# Eval("CompanyName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="State">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStateName" Text='<%# Eval("stateName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="City">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCityName" Text='<%# Eval("CityName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Available">
                                <ItemTemplate>
                                    <%--<asp:Label runat="server" ID="lblAvailable" Text='<%# Eval("IsAvailable") %>'></asp:Label>--%>
                                    <asp:CheckBox runat="server" ID="cbISAvaliable" Checked='<%# (bool) Eval("IsAvailable") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Active">
                                <ItemTemplate>
                                    <%--<asp:Label runat="server" ID="lblActive" Text='<%# Eval("IsActive") %>'></asp:Label>--%>
                                    <asp:CheckBox runat="server" ID="cbISActive" Checked='<%# (bool) Eval("IsActive") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Top">
                                <ItemTemplate>
                                    <%--<asp:Label runat="server" ID="lblTop" Text='<%# Eval("IsTop") %>'></asp:Label>--%>
                                    <asp:CheckBox runat="server" ID="cbISTop" Checked='<%# (bool) Eval("IsTop") %>' Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblEmail" Text='<%# Eval("Email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Phone">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPhone" Text='<%# Eval("Phone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUserName" Text='<%# Eval("UserName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Password">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPassword" Text='<%# Eval("Password") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:Button Text="Edit" runat="server" ID="butEdit" CommandName='<%# Eval("CompanyID") %>'
                                        OnClick="butEdit_onClick" />
                                </ItemTemplate>                               
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Delete">
                                    <itemtemplate>
                                    <asp:Button Text="Delete" runat="server" ID="butDelete" CommandName='<%# Eval("CompanyID") %>'
                                        OnClick="butDelete_onClick" />
                                </itemtemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Login">
                                <ItemTemplate>
                                    <asp:Button Text="Login as Affiliate" runat="server" ID="butLoginAfflite" CommandName='<%# Eval("CompanyID") %>'
                                        OnClick="butLoginAfflite_onClick" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
