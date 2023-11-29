using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolCanvas : MonoBehaviour, IPointerClickHandler {

    public Action OnToolCanvasLeftClickEvent;
    public Action OnToolCanvasRightClickEvent;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
        if (eventData.pointerId == -1) {
            OnToolCanvasLeftClickEvent?.Invoke();
        }
        else if (eventData.pointerId == -2) {
            OnToolCanvasRightClickEvent?.Invoke();
        }
    }
}