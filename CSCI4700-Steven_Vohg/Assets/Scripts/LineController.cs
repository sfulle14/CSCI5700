using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private List<DotController> dots;
 

    private void Awake() {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;

        dots = new List<DotController>();
    }


    public void AddDot(DotController dot) {

        dot.SetLine(this);
        dot.SetIndex(dots.Count);

        lr.positionCount++;
        dots.Add(dot);
    }


    public void SplitPointsAtIndex(int index, out List<DotController> beforePoints, out List<DotController> afterPoints) {
        List<DotController> before = new List<DotController>();
        List<DotController> after = new List<DotController>();

        int i = 0;
        for (; i < index; i++) {
            before.Add(dots[i]);
        }
        i++;
        for (; i < dots.Count; i++) {
            after.Add(dots[i]);
        }

        beforePoints = before;
        afterPoints = after;

        dots.RemoveAt(index);
    }


    private void LateUpdate() {
            if (dots.Count >= 2) {
                for (int i = 0; i < dots.Count; i++) {
                    Vector3 position = dots[i].transform.position;
                    position += new Vector3(0, 0, 5);

                    lr.SetPosition(i, position);
                }
            }
        } 

/*

    public DotController GetFirstPoint() {
        return dots[0];
    }

    

    

    public void SetColor(Color color) {
        lr.startColor = color;
        lr.endColor = color;
    }

    public void ToggleLoop() {
        lr.loop = !lr.loop;
    }

    public bool isLooped() {
        return lr.loop;
    }
*
    private void LateUpdate() {
        if (dots.Count >= 2) {
            for (int i = 0; i < dots.Count; i++) {
                Vector3 position = dots[i].transform.position;
                position += new Vector3(0, 0, 5);

                lr.SetPosition(i, position);
            }
        }
    } */
}
