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
    public Button button;


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        // set the partent in unity so nest line is not needed
        // but leaving for testings
        //parentAfterDrag = transform.parent.parent.parent.parent.parent;
        childID = transform.GetSiblingIndex();
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");

        //collision has to be with an object of tag
        // trash for this function to trigger
        if (collision.gameObject.tag.Equals("trash")){
            Destroy(this.transform.gameObject);
        }
    }

    public void ValidateCode()
    {
        if (game == "LightFlashing")
        {
           if(transform.GetChild(2).Equals("digitalWrite(LED_BUILTIN, HIGH)"))
            {
                if (transform.GetChild(3).Equals("delay(1000)"))
                {
                    if (transform.GetChild(4).Equals("digitalWrite(LED_BUILTIN, LOW)"))
                    {
                        if (transform.GetChild(5).Equals("delay(1000)"))
                        {
                            
                        }
                    }
                }
            }
        }
    }

}
