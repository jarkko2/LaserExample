using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public bool drawDebugLine = true;
    public bool drawLaser = true;
    public LineController lineController;
    public List<Line> Lines = new List<Line>();
    public int maximumIterations = 100;
    public float raycastDistance = 10f;

    private void DrawLines()
    {
        lineController.ClearLaser();
        if (drawLaser)
        {
            lineController.UpdateLines(Lines);
        }
        if (drawDebugLine)
        {
            foreach (Line line in Lines)
            {
                Debug.DrawLine(line.StartPos, line.EndPos, Color.red);
            }
        }
        Lines.Clear();
    }

    private void SendLaser()
    {
        Vector3 position = transform.position;
        Vector3 direction = transform.forward;

        for (int i = 0; i < maximumIterations; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                Lines.Add(new Line(position, hit.point));
                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
            }
            else
            {
                Lines.Add(new Line(position, position + direction * raycastDistance));
                break;
            }
        }
    }

    private void Update()
    {
        SendLaser();
        DrawLines();
    }
}

[System.Serializable]
public class Line
{
    public Vector3 EndPos;
    public Vector3 StartPos;

    public Line(Vector3 startPos, Vector3 endPos)
    {
        StartPos = startPos;
        EndPos = endPos;
    }
}