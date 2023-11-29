using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCanvases : MonoBehaviour
{
    //all diffrent canvases the follower uses
    public GameObject WaitScreen;
    public GameObject MenuCode;
    public GameObject ArduinoAssembly;
    public GameObject MistakeScreen;

    //used for mistake screen to go back to scene that was worked on.
    public bool Arduino;

    // Start is called before the first frame update
    public void Start()
    {
        WaitScreen.SetActive(false);
        MenuCode.SetActive(false);
        ArduinoAssembly.SetActive(false);
        MistakeScreen.SetActive(false);
        Arduino = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void follower_join()
    {
        //Join button has been pressed. Enable wait screen for follower.
        WaitScreen.SetActive(true);
    }

    public void begin_exercise()
    {
        //Leader has started the exercise. Enable Arduino canvas for follower.
        WaitScreen.SetActive(false);
        ArduinoAssembly.SetActive(true);
    }
    public void next_canvas()
    {
        //validation has been processed and passed. Call the next canvas
        //check which canvas is active
        if (ArduinoAssembly.active == true)
        {
            ArduinoAssembly.SetActive(false);
            Arduino = false;
            MenuCode.SetActive(true);
        }
        else
        {
            //MenuCode validation processed and passed. move to results scene
            SceneManager.LoadScene(5);
        }
    }

    public void mistake()
    {
        //validation has been processed and fails. call mistake canvas
        if (ArduinoAssembly.active == true)
        {
            ArduinoAssembly.SetActive(false);
        }
        else
        {
            //MenuCode
            MenuCode.SetActive(false);
        }

        MistakeScreen.SetActive(true);
    }

    public void fix_mistake()
    {
        //follower wants to fix mistake.
        MistakeScreen.SetActive(false);
        if (Arduino == true)
        {
            ArduinoAssembly.SetActive(true);
        }
        else
        {
            MenuCode.SetActive(true);
        }
    }

    public void start_over()
    {
        //follower wants to start over. Go back to Arduino assembly.
        ArduinoAssembly.SetActive(true);
        MistakeScreen.SetActive(false);
        Arduino = true;
    }
}
