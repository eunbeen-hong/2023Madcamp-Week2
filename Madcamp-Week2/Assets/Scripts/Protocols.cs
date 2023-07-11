using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protocols
{
    public class Packets
    {
        public class user
        {
            public string _id;
            public string id;
            public string username;
            public string univ;
            public int bestScore;
        }

        public class req_GetRank {
        }
        public class res_GetRank {
            public user[] result;   
        }


        public class req_PostById {
            public string id;
        }
        public class res_PostById {
            public int bestScore;
        }


        // public class req_InsertUser {
        //     public string username;
        //     public string univ;
        //     public string bestScore;
        // }
        // public class res_InsertUser {
        // }


        public class req_UpdateUser {
            public string id;
            public int bestScore;
        }
        public class res_UpdateUser {
        }
    }
}