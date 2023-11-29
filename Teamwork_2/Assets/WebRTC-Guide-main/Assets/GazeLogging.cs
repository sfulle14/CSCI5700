using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming; /*GetGazePoint*/
using System.IO; /*StreamWriter, File*/
using System; /*DateTime*/

public class GazeLogging : MonoBehaviour
{
    private string path, date;
    public string currentlyViewedObject = "";
    public bool isPaused;

    void Start ()
    {
        isPaused = false;
        //Path for csv file
        date = DateTime.Now.ToString().Replace("/", "-");
        date = date.Replace(":", "_");
        path = Path.Combine(Application.streamingAssetsPath, "Data", "EyeTracker files", $"Tobii_{date}_GazeData.csv");
    }

    void Update() {
        if (!isPaused) {
            LogGaze(TobiiAPI.GetGazePoint());
        }
    }
    
    //Logs gaze data to csv file named after Date and time.
    void LogGaze(GazePoint gazePoint)
    {
        if (gazePoint.IsValid)
        {
            string X, Y;

            //Format x and y pos to only go to three decimal places
            X = (gazePoint.Screen.x / Screen.width).ToString();
            Y = (gazePoint.Screen.y / Screen.height).ToString();

            GameObject hitObject = TobiiAPI.GetFocusedObject();
            Debug.Log($"hit object = {hitObject}");

            //If an object is being hit
            if (hitObject != null)
            {
                currentlyViewedObject = hitObject.name;
                //Names objects that are more specific to more general
                // if (currentlyViewedObject == "Example")
                // {
                //     currentlyViewedObject = "ReplacementExample";
                // }

                //Create file if not already there
                if (!File.Exists(path))
                {
                    //Writing using streamwriter
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("ts,X,Y,viewedObjects");
                        writeFunc(sw, X, Y, currentlyViewedObject);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        writeFunc(sw, X, Y, currentlyViewedObject);
                    }
                }
            }
            else {
                    if (!File.Exists(path))
                    {
                        //Writing using streamwriter
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine("ts,X,Y,viewedObjects");
                            writeFunc(sw, X, Y);
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            writeFunc(sw, X, Y);
                        }
                    }
            }
        }
    }
    
    private string CurrentTime()
    {
        return DateTime.UtcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK");
    }
    
    //Function for writing the file contents aside from header
     private void writeFunc(StreamWriter sw, string X, string Y, string objectViewed = "No Object Tracked") {
        sw.WriteLine(
            CurrentTime() + ","
                      + X + ","
                      + Y + ","
                      + objectViewed);
        sw.Flush();
    }
}