using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DotController : MonoBehaviour, IDragHandler, IPointerClickHandler, IBeginDragHandler {

    public LineController line;
    public int index;

    public Action<DotController> OnDragEvent;
    public void OnDrag(PointerEventData eventData) {
        OnDragEvent?.Invoke(this);
    }

    public Action<DotController> OnRightClickEvent;
    public Action<LineController> OnLeftClickEvent;
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.pointerId == -2) {
            //Right Click

            OnRightClickEvent?.Invoke(this);
        }
        else if (eventData.pointerId == -1) {
            //Left Click

            OnLeftClickEvent?.Invoke(line);
        }
    }

    public void SetLine(LineController line) {
        this.line = line;
    }

    public void SetIndex(int index) {
        this.index = index;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (eventData.pointerId == -1) {
            //Left Drag

            OnLeftClickEvent?.Invoke(line);
        }
    }
}
