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
            public string username;
            public string profilePic;
            public int bestScore;
        }

        public class req_GetAll {
        }
        public class res_GetAll {
            public user[] result;   
        }


        public class req_GetScore {
            public string username;
        }
        public class res_GetScore {
            public int bestScore;
        }


        public class req_InsertUser {
            public string username;
            public string profilePic;
            public string bestScore;
        }
        public class res_InsertUser {
        }


        public class req_UpdateUser {
            public string username;
            public int bestScore;
        }
        public class res_UpdateUser {
        }
    }
}