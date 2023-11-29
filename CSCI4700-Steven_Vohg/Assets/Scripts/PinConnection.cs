using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinConnection : MonoBehaviour
{
    public float snapDistance = 0.2f; // Adjust this based on your scene scale
    public LayerMask pinLayer; // Layer mask to filter pins

    private bool isConnected = false; // Flag to track connection status
    private Transform connectedPin; // Reference to the connected pin
    

    void Update()
    {
        if (!isConnected)
        {
            SnapToPin();

        }
        else
        {
            // Optionally, you may want to check for disconnection or update the connection status here.
            CheckForDisconnection();
        }



    }

    void SnapToPin()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, snapDistance, pinLayer);

        if (colliders.Length > 0)
        {
            // Snap to the closest pin
            Transform closestPin = FindClosestPin(colliders);
            SnapToClosestPin(closestPin);
        }
    }

    Transform FindClosestPin(Collider[] colliders)
    {
        Transform closestPin = null;
        float closestDistance = float.MaxValue;

        foreach (Collider col in colliders)
        {
            float distance = Vector3.Distance(transform.position, col.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPin = col.transform;
            }
        }

        return closestPin;
    }

    void SnapToClosestPin(Transform pin)
    {
        // Align the dot with the pin
        transform.position = pin.position;
        transform.rotation = pin.rotation;

        // Set the connectedPin reference
        connectedPin = pin;
        

        // Optionally, you may want to perform additional actions when connected.
        OnConnected();
    }

    void CheckForDisconnection()
    {
        // Check if the dot is still close to the connected pin
        float distance = Vector3.Distance(transform.position, connectedPin.position);

        if (distance > snapDistance)
        {
            // Dot is no longer connected
            isConnected = false;
            connectedPin = null;

            // Optionally, you may want to perform additional actions when disconnected.
            OnDisconnected();
        }
    }

    void OnConnected()
    {
        isConnected = true;
        // Add any additional actions when connected
        //Debug.Log("Dot connected to pin!");
    }

    void OnDisconnected()
    {
        // Add any additional actions when disconnected
        //Debug.Log("Dot disconnected from pin!");
    }

 

}