using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform StartPoint;
    public Transform EndPoint;
    private LineRenderer Line;

    private Vector3 startPos, endPos;

    void Start()
    {
        Line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        startPos = StartPoint.position;
        endPos = EndPoint.position;

        Vector3 center = (startPos + endPos) * 0.5f;

        center.y -= 3;

        startPos = startPos - center;
        endPos = endPos - center;

        //นÝบน
        for(int i=0;i<Line.positionCount;++i)
        {
            Vector3 point = Vector3.Slerp(startPos, endPos, i / (float)(Line.positionCount - 1));
            point += center;

            Line.SetPosition(i, point);
        }
    }
}
