using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Byn.Awrtc;
using Byn.Awrtc.Unity;
using Byn.Unity.Examples;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    //[SerializeField] bool timePaused;
    public bool timePaused = true;

    //implementations still needed for when time runs out. Possibly navigate to results scene.

    public void pauseTimer()
    {
        timePaused = true;
    }

    public void startTimer()
    {
        timePaused = false;
    }

    public void Update()
    {
        //Debug.Log("timePaused: " + timePaused + "\n");
        if (timePaused == false)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
