using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KakaoLogin : MonoBehaviour
{
    private AndroidJavaObject ajo;
    
    void Start()
    {
        ajo = new AndroidJavaObject( "com.example.madcamp_week2.UKakao" );
    }

    public void login()
    {
        ajo.Call( "KakaoLogin" );
    }
}
