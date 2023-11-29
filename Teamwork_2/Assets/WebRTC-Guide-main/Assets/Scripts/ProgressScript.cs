using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressScript : MonoBehaviour
{
    public GameObject Progress1;
    public GameObject Progress2;
    public GameObject Progress3;
    public GameObject Progress4;
    public GameObject Progress5;
    // Start is called before the first frame update
    void Start()
    {
        Progress1.SetActive(false);
        Progress2.SetActive(false);
        Progress3.SetActive(false);
        Progress4.SetActive(false);
        Progress5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Update_progress1()
    {
        Progress1.SetActive(true);
        Progress2.SetActive(true);
    }

    public void Update_progress2()
    {
        Progress3.SetActive(true);
        Progress4.SetActive(true);
    }

    public void Update_progress3()
    {
        Progress5.SetActive(true);
    }
}
