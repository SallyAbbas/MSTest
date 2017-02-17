<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainControl.ascx.cs"
    Inherits="Lemo.Scripts.MainControl" %>
<script type="text/javascript" src="../Scripts/crawler.js">
    /*
    Text and/or Image Crawler Script ©2009 John Davenport Scheuer
    as first seen in http://www.dynamicdrive.com/forums/ username: jscheuer1
    This Notice Must Remain for Legal Use
    */
</script>
<script type="text/javascript">
    marqueeInit({
        uniqueid: 'mycrawler',
        style: {
            'padding': '5px',
            'width': '100%',
            'background': 'white',
            'border': '1px solid #ffffff'
        },
        inc: 8, //speed - pixel increment for each iteration of this marquee's movement
        mouse: 'cursor driven', //mouseover behavior ('pause' 'cursor driven' or false)
        moveatleast: 3,
        neutral: 150,
        savedirection: true
    });
</script>
<div width="100%" class="marquee" id="mycrawler2">
    <img width="100" height="100" src="../Image/Cars/BD.png" alt="" />
    <img width="100" height="100" src="../Image/Cars/LX.png" alt="" />
    <img width="100" height="100" src="../Image/Cars/LX.png" alt="" />
    <img width="100" height="100" src="../Image/Cars/ME.png" alt="" />
    <img width="100" height="100" src="../Image/Cars/S6.png" alt="" />
    <img width="100" height="100" src="../Image/Cars/SUV6.png" alt="" />
    <img width="100" height="100" src="../Image/Cars/VIP.png" alt="" />
    <img width="100" height="100" src="../Image/Cars/VN.png" alt="" />
    </div>
<script type="text/javascript">
    marqueeInit({
        uniqueid: 'mycrawler2',
        style: {
            'padding': '2px',
            'width': '100%',
            'height': '100%'
        },
        inc: 5, //speed - pixel increment for each iteration of this marquee's movement
        mouse: 'cursor driven', //mouseover behavior ('pause' 'cursor driven' or false)
        moveatleast: 2,
        neutral: 150,
        savedirection: true
    });
</script>
