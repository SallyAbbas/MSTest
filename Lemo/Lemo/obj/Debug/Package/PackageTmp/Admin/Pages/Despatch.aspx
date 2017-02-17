<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="Despatch.aspx.cs" Inherits="Lemo.Admin.Pages.Despatch" %>

<%@ Register Src="../../Controls/BookEngine.ascx" TagName="BookEngine" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Refresh" content="60">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td valign="top">
                <div runat="server" id="divAddDespatch">
                    <uc1:BookEngine ID="BookEngine1" runat="server" />
                </div>
            </td>
            <td valign="top">
                <div>
                    <br />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow"
                        OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="True" BorderColor="Tan"
                        BorderWidth="1px" CellPadding="2" GridLines="None" ForeColor="Black">
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        <Columns>
                            <asp:TemplateField HeaderText="Action">
                                <ItemStyle Width="30px" />
                                <ItemTemplate>
                                    <%-- <asp:DropDownList runat="server" ID="ddlCars" Width="200px" Height="20px" DataValueField="CarID"
                                        DataSource='<%# Eval("CarsList") %>' DataTextField="CarName">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlCars" ErrorMessage="*" InitialValue="0"
                                        ID="RequiredFieldValidator12" ValidationGroup="vgAccept" runat="server" ForeColor="Red"
                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    <asp:Button Text="Accept" runat="server" ID="butAccept" CommandName='<%# Eval("JobID") %>'
                                        OnClick="butAccept_onClick" ValidationGroup="vgAccept" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--       <asp:TemplateField HeaderText="Limo Conf">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLimoConfirmNumber" Text='<%# Eval("jobWrapperDespatch.LimoConfirmNumber") %>'></asp:Label>
                                <%# Eval("LimoConfirmNumber") %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Date/Time">
                                <ItemStyle Width="75px" />
                                <ItemTemplate>
                                    <%# Eval("JobDate")%>
                                    <br />
                                    <%# Eval("JobTime")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Passenger">
                                <ItemStyle Width="150px" />
                                <ItemTemplate>
                                    <%# Eval("PassengerFullName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PU Location">
                                <ItemStyle Width="250px" />
                                <ItemTemplate>
                                    <%# Eval("FromAddress")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DO Location">
                                <ItemStyle Width="250px" />
                                <ItemTemplate>
                                    <%# Eval("ToAddress")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Price">
                                <ItemStyle Width="75px" />
                                <ItemTemplate>
                                    <%--<asp:Label runat="server" ID="lblTotalPrice" Text='<%# Eval("TotalPrice") %>'></asp:Label>--%>
                                    <%# Eval("TotalPrice")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cancel">
                                <ItemStyle Width="30px" />
                                <ItemTemplate>
                                    <asp:Button Text="Cancel" runat="server" ID="butCancel" CommandName='<%# Eval("JobID") %>'
                                        OnClick="butCancel_onClick" />
                                </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="Tan" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <SortedAscendingCellStyle BackColor="#FAFAE7" />
                        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                        <SortedDescendingCellStyle BackColor="#E1DB9C" />
                        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
