using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Code : MonoBehaviour
{
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }


    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);

    }





}
