using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinObject : MonoBehaviour
{
    public enum PinType
    {
        Digital,
        Analog,
        Led,
        Ground
    }

    public PinType pinType;
    public int pinNumber;
    public bool isHigh; // For digital pins
    public float analogValue; // For analog pins


   
    public bool CheckState()
    {
        return isHigh;
    }

    // Method to toggle the state of the digital pin
    public void ToggleState()
    {
        isHigh = !isHigh;
        Debug.Log("Pin " + pinNumber + " is now " + (isHigh ? "HIGH" : "LOW"));
        // Perform any other necessary actions here
    }

    // Method to set the value of the analog pin
    public void SetAnalog(float value)
    {
        analogValue = value;
        Debug.Log("Analog value of pin " + pinNumber + " set to " + value);
        // Perform any other necessary actions here

    }
}