using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDConnectionValidator : MonoBehaviour
{
    public GameObject positivePin;
    public GameObject negativePin;

    public PinObject [] digitalPins;
    public PinObject [] analogPins;
    public PinObject [] ardGround;

    private bool isConnectedToPositive = false;
    private bool isConnectedToNegative = false;
    private bool digitalConnection = false;
    private bool analogConnection = false;
    private bool arduinoGroundConnection = false;

    private bool dcFlag = false;
    private bool acFlag = false;
    private bool agFlag = false;
    
    bool wires = false;

    private PinObject connectedDigital;
    private PinObject connectedAnalog;

    void Update()
    {
        CheckDigitalPins();
        CheckGroundPins();
        //CheckConnection();
        checkCorrectness();
    }


    public bool CorrectWires() 
    {

        wires = true;
        /*
        if (digitalConnection && arduinoGroundConnection) {
            wires = true;
        }
        */

        return wires;
    }

    public void checkCorrectness() {
        //if (digitalConnection == true && arduinoGroundConnection == true) 
        //isConnectedToPositive  == true && isConnectedToNegative == true) 

       // Debug.Log("digital = " + digitalConnection);
       // Debug.Log("ground = " + arduinoGroundConnection);
       // Debug.Log("led pos = " + isConnectedToPositive);
       // Debug.Log("led neg = " + isConnectedToNegative);

        
        if (digitalConnection == true) 
        {
  
            Debug.Log("DIGTIAL GOOD");
            dcFlag = true;
            Debug.Log("dcFlag = " + dcFlag);
        } 

        if (arduinoGroundConnection == true) 
        {
            agFlag = true;
            Debug.Log("GROUND GOOD");
            Debug.Log("agFlag = " + agFlag);
        }

        if ((dcFlag == true) && (agFlag == true)) 
        {
            Debug.Log("CORRECT!");
        }
        //else {
           // Debug.Log("INCORRECT!");
       // }


    }

    void CheckConnection()
    {

        if (!digitalConnection)
        {
            CheckDigitalPins();
        }
        
        if (!arduinoGroundConnection)
        {
            CheckGroundPins();
        }

         if (!analogConnection)
        {
            CheckAnalogPins();
        }


        if (!isConnectedToPositive)
        {
            CheckPositiveConnection();
        }

        if (!isConnectedToNegative)
        {
            CheckNegativeConnection();
        }

        // Additional logic as needed
    }

    void CheckPositiveConnection()
    {
        Collider[] colliders = Physics.OverlapSphere(positivePin.transform.position, 0.1f); // Adjust the radius as needed

        foreach (Collider col in colliders)
        {
            if (col.gameObject == gameObject)
            {
                isConnectedToPositive = true;
                Debug.Log("Connected to positive pin!");
                // Additional actions when connected to positive pin
            }

        }
    }

    void CheckNegativeConnection()
    {
        Collider[] colliders = Physics.OverlapSphere(negativePin.transform.position, 0.1f); // Adjust the radius as needed

        foreach (Collider col in colliders)
        {
            if (col.gameObject == gameObject)
            {
                isConnectedToNegative = true;
                Debug.Log("Connected to negative pin!");
                // Additional actions when connected to negative pin
            }
  
        }
    }

    void CheckDigitalPins()
    {
   
        // Check Digital Pins
        foreach (PinObject pin in digitalPins)
        {
            Collider[] pinColliders = Physics.OverlapSphere(pin.transform.position, 0.1f);

            foreach (Collider col in pinColliders)
            {
                if (col.gameObject == gameObject)
                {
                    connectedDigital = pin;
                    digitalConnection = true;

                    Debug.Log("Connected to Arduino pin: " + connectedDigital.pinNumber); // Assuming PinObject has a PinNumber property
                    // Additional actions when connected to Arduino pin
                }
                /*
                else if(connectedDigital = null)
                {
                    digitalConnection = false;
                    Debug.Log("Disconnected from Arduino pin");
                }
                */
            }
        }  

    }

    void CheckGroundPins() {
        // Check arduino ground pins
        foreach (PinObject pin in ardGround)
        {
            Collider[] pinColliders = Physics.OverlapSphere(pin.transform.position, 0.1f);

            foreach (Collider col in pinColliders)
            {
                if (col.gameObject == gameObject)
                {

                    arduinoGroundConnection = true;
                    //Debug.Log("Connected to Ground pin: " + connectedPin.pinNumber); // Assuming PinObject has a PinNumber property
                    // Additional actions when connected to Arduino pin
                }
            }
        }
    }

    void CheckAnalogPins() {
    // Check Analog Pins
        foreach (PinObject pin in analogPins)
        {
            Collider[] pinColliders = Physics.OverlapSphere(pin.transform.position, 0.1f);

            foreach (Collider col in pinColliders)
            {
                if (col.gameObject == gameObject)
                {
                    connectedAnalog = pin;
                    analogConnection = true;

                    //Debug.Log("Connected to Analog pin: " + connectedPin.pinNumber); // Assuming PinObject has a PinNumber property
                    // Additional actions when connected to Arduino pin
                }
            }
        }
    }


}