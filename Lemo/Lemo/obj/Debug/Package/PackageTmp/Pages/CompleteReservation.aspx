<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CompleteReservation.aspx.cs" Inherits="Lemo.Pages.CompleteReservation" %>

<%@ Register Src="../Controls/FeesDetais.ascx" TagName="FeesDetais" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td style="width: 72%; padding: 15px;" valign="top">
                    <table>
                        <tr>
                            <td>
                                <div id="divCompanyName" style="font-size: 50px; font-weight: bold;">
                                <div style="float: left;">
                                        <asp:Image runat="server" ID="imgLogo" /></div>
                                    <div style="text-align: center;">
                                        <asp:Label runat="server" ID="lblCompanyName" Font-Bold="true" Font-Size="50"></asp:Label></div>                                    
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="text-align: left;">
                                    <asp:Label runat="server" ID="lblAboutUs" Font-Bold="true" Font-Size="26" Text="About Us"></asp:Label>
                                </div>
                                <div id="divAboutUs" runat="server">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblDescription" Font-Bold="true" Font-Size="26" Text="Description"></asp:Label>
                                <div id="divDescription" runat="server">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblOurService" Font-Bold="true" Font-Size="26" Text="Our Service"></asp:Label>
                                <div id="divOurService" runat="server">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblOurFleet" Font-Bold="true" Font-Size="26" Text="Our Fleet"></asp:Label>
                                <div id="divOurFleet" runat="server">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblContactUs" Font-Bold="true" Font-Size="26" Text="Contact Us"></asp:Label>
                                <div id="divContactUs" runat="server">
                                </div>
                            </td>
                        </tr>
                    </table>
                    <%-- <div style="font-size: 16px;">   
                    <div style="font-size: 26px;font-weight:bold;">Operator Description</div> 
                    <br />              
                       <span style="font-weight:bold;">LIMO ALLOVER</span>  is a limo service you can count on, because we do the best we can to
                        accommodate to your schedule. We provide you with a full range of luxury private
                        cars, and drivers who are committed to making your trip to and from as pleasant
                        and as comfortable as possible. We would like to welcome the opportunity to discuss
                        with you offers and packages that may benefit you. Whether a personal account of
                        your business account, we are here to do what is best for you. As always, we invite
                        your comments and feedback on how we have served you in the past and how we can
                        make it better for you in the future. we are here to assist you with all transportation
                        needs you may have. If you have any questions or concerns, please do not hesitate
                        to contact us. Thank you for your business and we look forward to being of serves
                        to you on your next trip.
                        <br />
                        <br />
                        <span style="font-weight:bold;">LIMO ALLOVER</span> is the place that you can count on .for a full range of luxury private
                        transportation, and people who are committed to help you achieve getting to your
                        destination on time .we will do everything possible to make ZamZone Service relationship
                        rewarding and convenient for you. We are here to assist you with wherever needs
                        you may have. If you have any questions or concerns, please do not hesitate to contact
                        us at the number listed .we would also welcome the opportunity to meet personally
                        with you to learn more about how we can be more of a service to you and to discuss
                        offers and packages that may be immediate benefit to you. By working together, we
                        can get you where you want to go, and offers you a great value along the way. As
                        always, we invite your comments and feedback on how we can continue to serve our
                        customers along with a personal thank-you for making our jobs at ZamZone Service
                        Service more enjoyable.
                    </div>--%>
                </td>
                <td valign="top">
                    <uc1:FeesDetais ID="FeesDetais1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
