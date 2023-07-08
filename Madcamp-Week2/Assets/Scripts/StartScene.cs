using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void OnClickStartButton() {
        // TODO: 로그인 처리

        SceneManager.LoadScene("GameScene");
    }
    
}
