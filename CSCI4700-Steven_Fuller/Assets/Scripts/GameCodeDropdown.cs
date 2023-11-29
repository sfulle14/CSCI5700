using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCodeDropdown : MonoBehaviour
{
    //temp for testing
    // AnotherGame && LightFlashing
    public string game = "LightFlashing";

    /*
     * Creating Dropdown menus for each game type
     */
    public TMPro.TMP_Dropdown LightFlashingDropdown;
    public TMPro.TMP_Dropdown AnotherGameDropdown;

    public TMPro.TMP_Text text;

    /*
     * Creating lists for dropdown for each game type
     */
    public List<string> LightFlashingList = new List<string>
    {
        "pinMode(LED_BUILTIN, OUTPUT)"
        ,"digitalWrite(LED_BUILTIN, HIGH)"
        ,"digitalWrite(LED_BUILTIN, LOW)"
        ,"delay(1000)"
    };

    public List<string> AnotherGameList = new List<string>
    {
        "Nothing yet"
        ,"No data to see"
    };

    // Start is called before the first frame update
    void Start()
    {
        CreateDropDown();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.tag.Equals("EndCode"))
        {
            transform.SetAsLastSibling();
        }
    }

    //loads the data from the list into the dropdown attached
    void CreateDropDown()
    {
        /*
         * IF statements to to pick lists and dropdowns
         * depending on the game that is being played
         */
        if (game == "LightFlashing")  
        {
            foreach (string item in LightFlashingList)
            {
                LightFlashingDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = item });
            }
        }

        //if another activity
        if (game == "AnotherGame")
        {
            foreach (string item in AnotherGameList)
            {
                AnotherGameDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData() { text = item });
            }
        }
    }

}
