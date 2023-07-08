using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTimer : MonoBehaviour
{
    Slider slTimer;
    float fSliderBarTime, timerSpeed = 5f;
    float fSliderBarTimeMax;

    // Start is called before the first frame update
    void Start()
    {
        slTimer = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slTimer.value > 0.0f) {
            slTimer.value -= Time.deltaTime * timerSpeed;
        } else {
            // Debug.Log("Time Over");
            ResetTimer();
        }
    }

    public void ResetTimer() {
        slTimer.value = slTimer.maxValue;
    }
}

