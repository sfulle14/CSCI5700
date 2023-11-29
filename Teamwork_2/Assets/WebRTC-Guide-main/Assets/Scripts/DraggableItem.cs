/*
 * Drag and drop: https://www.youtube.com/watch?v=kWRyZ3hb1Vc
 * Collisions: https://www.youtube.com/watch?v=Bc9lmHjqLZc
 * 
 */


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class DraggableItem : GameCodeDropdown, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    public Transform parentAfterDrag;
    public int childID;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin drag");
        // set the partent in unity so nest line is not needed
        // but leaving for testings
        //parentAfterDrag = transform.parent.parent.parent.parent.parent;
        
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag);
        childID = transform.GetSiblingIndex();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision has to be with an object of tag
        // trash for this function to trigger
        if (collision.gameObject.tag.Equals("trash")){
            Destroy(this.transform.gameObject);
        }
    }

    public void ValidateCode()
    {
        //bool added to check if all items in the list are valid
        bool code_correct = true;

        if (game == "LightFlashing")
        {
            //assign variables for first loop
            GameObject gob = GameObject.FindGameObjectWithTag("setup");
            Transform transform = gob.GetComponent<Transform>();
            if (transform.GetChild(2).name == "Item 1: pinMode(LED_BUILTIN, OUTPUT)")
            {
                Debug.Log("valid 2");
            }
            else
            {
                //Debug.Log("Invalid 2");
                //Debug.Log(transform.GetChild(2));
                code_correct = false;
            }

            if (transform.GetChild(3).GetComponent<Image>().name == "}")
            {
                //Debug.Log("Valid 3");
            }
            else
            {
                //Debug.Log("Invalid 3");
                //Debug.Log(transform.GetChild(3));
                code_correct = false;
            }


            //reasign variable for 2nd loop
            gob = GameObject.FindGameObjectWithTag("loop");
            transform = gob.GetComponent<Transform>();

            //check each child to see if the name matches what is expected
            if (transform.GetChild(2).name == "Item 2: digitalWrite(LED_BUILTIN, HIGH)")
            {
                Debug.Log("valid 2");
            }
            else
            {
                //Debug.Log("Invalid 2");
                //Debug.Log(transform.GetChild(2));
                code_correct = false;
            }

            if (transform.GetChild(3).name == "Item 4: delay(1000)")
            {
                Debug.Log("valid 3");
            }
            else
            {
                //Debug.Log("Invalid 3");
                //Debug.Log(transform.GetChild(3));
                code_correct = false;
            }

            if (transform.GetChild(4).name == "Item 3: digitalWrite(LED_BUILTIN, LOW)")
            {
                Debug.Log("valid 4");
            }
            else
            {
                //Debug.Log("Invalid 4");
                //Debug.Log(transform.GetChild(4));
                code_correct = false;
            }

            if (transform.GetChild(5).name == "Item 4: delay(1000)")
            {
                Debug.Log("Valid 5");
            }
            else
            {
                //Debug.Log("Invalid 5");
                //Debug.Log(transform.GetChild(5));
                code_correct = false;
            }

            if (transform.GetChild(6).name == "Item 2: digitalWrite(LED_BUILTIN, HIGH)")
            {
                Debug.Log("valid 6");
            }
            else
            {
                // Debug.Log("Invalid 6");
                // Debug.Log(transform.GetChild(6));
                code_correct = false;
            }

            if (transform.GetChild(7).name == "Item 5: delay(500)")
            {
                Debug.Log("Valid 7");
            }
            else
            {
                //Debug.Log("Invalid 7");
                //Debug.Log(transform.GetChild(7));
                code_correct = false;
            }

            if (transform.GetChild(8).name == "Item 3: digitalWrite(LED_BUILTIN, LOW)")
            {
                Debug.Log("valid 8");
            }
            else
            {
                //Debug.Log("Invalid 8");
                //Debug.Log(transform.GetChild(8));
                code_correct = false;
            }

            if (transform.GetChild(9).name == "Item 5: delay(500)")
            {
                Debug.Log("Valid 9");
            }
            else
            {
                //Debug.Log("Invalid 9");
                //Debug.Log(transform.GetChild(9));
                code_correct = false;
            }

            if (transform.GetChild(10).GetComponent<Image>().name == "}")
            {
                Debug.Log("Valid 10");
            }
            else
            {
                //Debug.Log("Invalid 10");
                //Debug.Log(transform.GetChild(10));
                code_correct = false;
            }
        }

        //event used to determine if follower has correctly assembled the code.
        if (code_correct == true)
        {
            //navigate to results scene
            Debug.Log("The follower did it!");
        }
        else
        {
            //navigate to mistake canvas
            Debug.Log("A mistake has been made");
        }
    }
}
