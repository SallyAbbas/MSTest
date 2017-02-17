<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/HomeMaster.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Lemo._Default" %>

<%@ Register Src="~/Controls/BookEngine.ascx" TagName="BookEngine" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/SignupControl.ascx" TagName="SignupControl" TagPrefix="uc2" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="padding: 10px; background-image: url('../Image/HomeImage.jpg');">
        <uc1:BookEngine ID="BookEngine1" runat="server" />
        <div>
            <br />
            <br />
            Limo all over is a limo service with a full range of luxury private cars you can
            count on because we do the best to provide each and every costumer with the safest,
            most comfortable and enjoyable trip, while our experienced drivers are committed
            to get you to your destination on time. Customer satisfaction is always our first
            priority. Through delivering punctuality and performance in our service we have
            earned the trust of our clients and we are sure we can earn yours as well. Limoallover
            has been providing transportation services in and around the New York and New Jersey
            land area. Limo all over has now a worldwide affiliate network, offering limo service
            in the US and worldwide We appreciate your comments and feedback on how we have
            served you in the past and how we can make it better for you in the future. We are
            here to assist you with all transportation needs you may have. If you have any questions
            or concerns, please do not hesitate to contact us . Let limo all over make your
            next trip quick and comfortable experience. Call us today at <span style="color: Red;">
                646-688-5000</span>. You can also make your arrangements online with our e-reservations
            service.</div>
    </div>
    <div style="padding: 10px;">
        <table width="100%">
            <tr>
                <td style="width: 33%;">
                    <div class="DivFotter">
                        <div class="FotterTitle">
                            Tumblr Car Cam</div>
                        <br />
                        <div>
                            We’re making some changes here at LimoAllOver – not just with a new logo – though
                            we’re pretty excited about that. LimoAllOver the next generation in car service;
                            bringing you more of what you want (and less of what you don’t). We know traveling
                            can be rough, so here’s how we’re making it easier: Reliable: When we say your driver
                            is here, we mean it. And he’ll be on time guaranteed – or your next ride is free.
                            Affordable: Get a personal driver for the price of your last cab ride</div>
                    </div>
                </td>
                <td style="width: 33%;">
                    <div class="DivFotter">
                        <div class="FotterTitle">
                            Heard on Twitter</div>
                        <br />
                        <div>
                            <a class="twitter-timeline" href="https://twitter.com/limoallover" data-widget-id="285466263688445953">
                                Tweets by @limoallover</a>
                            <script>                                !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");</script>
                        </div>
                    </div>
                </td>
                <td style="width: 33%;">
                    <div class="DivFotter">
                        <div class="FotterTitle">
                            Testimonials</div>
                        <br />
                        <div>
                            I have recommended limallover to many of my friends. Excellent service. Thanks limoallover!
                            <div style="font-weight: bold;">
                                N.D NYC</div>
                            My favorite thing about using limoallover is that i can book online and its a piece
                            of cake !
                            <div style="font-weight: bold;">
                                J.L NYC</div>
                            i love limoallover. They are on time and reliable !
                            <div style="font-weight: bold;">
                                K.K NYC</div>
                            The website is really easy to use no instructions need. Drivers are always on time
                            which i love<div style="font-weight: bold;">
                                A.A NYC</div>
                            Fairly priced and reliable thanks!
                            <div style="font-weight: bold;">
                                M.N NYC</div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
