using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//namespace Lemo.Helper
//{
    public static class Limo_Gloabel
    {
        public enum JobStatus
        {
            Cancelled_By_LimoAllOver=1,
            Cancelled_By_Customer = 2,
            Unassigned = 3,
            Assigned = 4,
            Dispatched = 5,
            On_The_Way=6,
            On_Location=7,
            Customer_In_Car=8,
            Done = 9,
            Pending = 10,
            Accepted = 11,
            Rejected = 12
        }

        public enum CarType
        {
            Sedan = 1,
            Suv = 2,
            Van = 3,
            Stretch_SUV = 4,
            Stretch_Limo = 5,
            Mini_Bus = 6,
            Bus = 7,
            BMW=8,
            Cadilac=9,
            Mercedes = 10
        }
    }
//}