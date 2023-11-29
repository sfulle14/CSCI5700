using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderCanvases : MonoBehaviour
{
    // Start is called before the first frame update
    //canvas declarations
    public GameObject ArduinoSchematic;
    public GameObject CodeSchematic;
    public GameObject WaitCanvas;

    public void Start()
    {
        //leader will start with the arduino schematic first.
        ArduinoSchematic.SetActive(false);
        CodeSchematic.SetActive(false);
        WaitCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enable_wait()
    {
        //leader has pressed the join button. Enable the wait Canvas
        WaitCanvas.SetActive(true);
    }

    public void exercise_start()
    {
        //leader has pressed the start button. Exercise begins and the schematics are loaded.
        WaitCanvas.SetActive(false);
        ArduinoSchematic.SetActive(true);
    }
    //both buttons will do the same method since there is only 2 schematics. More can be added later on.
    public void next_image()
    {
        if (WaitCanvas.active == true)
        {
            //exercise has begun
            WaitCanvas.SetActive(false);
            ArduinoSchematic.SetActive(true);
        }
        if (ArduinoSchematic.active == true)
        {
            ArduinoSchematic.SetActive(false);
            CodeSchematic.SetActive(true);
        }
        else
        {
            ArduinoSchematic.SetActive(true);
            CodeSchematic.SetActive(false);
        }
    }
}
