using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class led : MonoBehaviour
{
    
    public UnityEngine.Rendering.Universal.Light2D led1; // 2D spotlight component to enable when connected
    public UnityEngine.Rendering.Universal.Light2D led2; // 2D spotlight component to enable when connected

    //public PinObject positive;
    public PinObject ground;
    public PinObject positive;

    public PinObject [] positivePins;

    //public PinObject ledPositive;
    //public PinObject ledNegative;

    public bool blink = true;
    public bool coding = false;
    public bool wiring = false;
    public bool lightOn = false;

    public float blinkFrequency1 = 1.0f; // Frequency for LED 1 in Hz
    public float blinkFrequency2 = 0.5f; // Frequency for LED 2 in Hz

    private float timeElapsed1 = 0.0f;
    private float timeElapsed2 = 0.0f;

    void Update()
    {
        // Update timeElapsed for each LED based on frequencies
        timeElapsed1 += Time.deltaTime * blinkFrequency1;
        timeElapsed2 += Time.deltaTime * blinkFrequency2;

        // Calculate intensity based on sine function to create blinking effect
        float intensity1 = Mathf.PingPong(timeElapsed1, 1.0f) * 5.0f;
        float intensity2 = Mathf.PingPong(timeElapsed2, 1.0f) * 5.0f;

        // Set intensity for each LED
        led1.intensity = intensity1;
        led2.intensity = intensity2;
    }




    // Method to check the LED's connection and enable the spotlight
    public void CheckConnection()
    {
        bool isGrounded = ground.CheckState();
        bool isPowered = positive.CheckState();


        if (isGrounded && isPowered && !lightOn)
        {
            Debug.Log("LED is connected and powered.");
            EnableSpotlight();
        }
        else if((!isGrounded || !isPowered) && lightOn)
        {
            Debug.Log("LED is not connected or not powered.");
            DisableSpotlight();
        }
    }


    private void DisableSpotlight()
    {
        led1.gameObject.SetActive(false);
        led2.gameObject.SetActive(false);
        lightOn = false;
    }


    // Method to enable the 2D spotlight
    private void EnableSpotlight()
    {
        led1.gameObject.SetActive(true);
        led2.gameObject.SetActive(true);
        lightOn = true;

    }


}
