using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{

    private bool wires = false;

    public UnityEngine.Rendering.Universal.Light2D led1; // 2D spotlight component to enable when connected
    public UnityEngine.Rendering.Universal.Light2D led2; // 2D spotlight component to enable when connected
    public GameObject wireCanvas;
    public GameObject on;
    public GameObject off;

    private bool ledOn = false;
    public LEDConnectionValidator validator;

    public void TurnOnLED()
    {
        
        if(validator.CorrectWires() == true && !ledOn) {
            led1.gameObject.SetActive(true);
            led2.gameObject.SetActive(true);
            ledOn = true;
        }
        else if(validator.CorrectWires() == false || ledOn) {
            led1.gameObject.SetActive(false);
            led2.gameObject.SetActive(false);
            ledOn = false;
        }
        
        
    }



    public void toggleWire()
    {
        if (!wires) 
        {
            wireCanvas.gameObject.SetActive(true);
            on.gameObject.SetActive(true);
            off.gameObject.SetActive(false);
            wires = true;
        }
        else if (wires) {
            wireCanvas.gameObject.SetActive(false);
            on.gameObject.SetActive(false);
            off.gameObject.SetActive(true);
            wires = false;
        }
    }



  

}
