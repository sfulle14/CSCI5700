using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public PinObject [] pin; // Reference to the PinObject script attached to the pin GameObject

    // Update is called once per frame
    void Update()
    {

        // Simulate toggling the digital state when the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (pin != null && pin[0].pinType == PinObject.PinType.Digital)
            {
                pin[0].ToggleState();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (pin != null && pin[1].pinType == PinObject.PinType.Digital)
            {
                pin[1].ToggleState();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (pin != null && pin[2].pinType == PinObject.PinType.Digital)
            {
                pin[2].ToggleState();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (pin != null && pin[3].pinType == PinObject.PinType.Digital)
            {
                pin[3].ToggleState();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (pin != null && pin[4].pinType == PinObject.PinType.Digital)
            {
                pin[4].ToggleState();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (pin != null && pin[5].pinType == PinObject.PinType.Digital)
            {
                pin[5].ToggleState();
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (pin != null && pin[14].pinType == PinObject.PinType.Digital)
            {
                pin[14].ToggleState();
            }
        }

    }
}
