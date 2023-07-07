using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    // GameObject leftButton, rightButton;

    private void Awake() {
    }

    private void Start() {
    }

    public void OnClick(GameObject btn)
    {
        if (btn.name == "LeftButton") {
            Debug.Log("LeftButton clicked");
            // player.Step(false);
        } else if (btn.name == "RightButton") {
            Debug.Log("RightButton clicked");
            // player.Step(true);
        }
    }
}
