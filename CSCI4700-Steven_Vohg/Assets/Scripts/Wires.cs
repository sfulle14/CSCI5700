using UnityEngine;

public class Wires : MonoBehaviour
{
    public GameObject linePrefab; // Prefab for the LineRenderer object
    public GameObject pointPrefab; // Prefab for the draggable points

    private GameObject currentLine; // Reference to the current LineRenderer object
    private GameObject startPoint; // Reference to the start point of the wire
    private GameObject endPoint; // Reference to the end point of the wire
    private bool isDrawing = false; // Flag to indicate if the wire creation is in progress
    private bool isDragging = false; // Flag to indicate if the points are being dragged

    // Update is called once per frame
    void Update()
    {
        if (isDrawing && Input.GetMouseButtonDown(0))
        {
            if (startPoint != null && endPoint != null)
            {
                if (IsMouseOverPoint(startPoint) || IsMouseOverPoint(endPoint))
                {
                    isDragging = true;
                }
            }
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            DragPoint();
        }

        if (isDrawing && Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            isDragging = false;
            currentLine = null;
            startPoint = null;
            endPoint = null;
        }
    }

    // Method to create a new LineRenderer object
    public void CreateNewLine()
    {
        currentLine = Instantiate(linePrefab);
        startPoint = Instantiate(pointPrefab);
        endPoint = Instantiate(pointPrefab);
        currentLine.GetComponent<LineRenderer>().SetPosition(0, startPoint.transform.position);
        currentLine.GetComponent<LineRenderer>().SetPosition(1, endPoint.transform.position);
        isDrawing = true;
    }

    // Method to update the position of the LineRenderer object
    public void UpdateLine()
    {
        if (currentLine != null && startPoint != null && endPoint != null && !isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            endPoint.transform.position = mousePosition;
            currentLine.GetComponent<LineRenderer>().SetPosition(0, startPoint.transform.position);
            currentLine.GetComponent<LineRenderer>().SetPosition(1, endPoint.transform.position);
        }
    }

    // Method to check if the mouse is over a point
    private bool IsMouseOverPoint(GameObject point)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Collider2D collider = point.GetComponent<Collider2D>();
        return collider.bounds.Contains(mousePosition);
    }

    // Method to handle the dragging of points
    private void DragPoint()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        if (IsMouseOverPoint(startPoint))
        {
            startPoint.transform.position = mousePosition;
        }
        else if (IsMouseOverPoint(endPoint))
        {
            endPoint.transform.position = mousePosition;
        }
        currentLine.GetComponent<LineRenderer>().SetPosition(0, startPoint.transform.position);
        currentLine.GetComponent<LineRenderer>().SetPosition(1, endPoint.transform.position);
    }
}