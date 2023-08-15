using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject PointLight;
    public List<GameObject> Lights = new List<GameObject>();
    public bool spawnLights = true;

    public void UpdateLines(List<Line> newLines)
    {
        lineRenderer.positionCount = newLines.Count * 2;

        for (int i = 0; i < newLines.Count; i++)
        {
            Line line = newLines[i];
            lineRenderer.SetPosition(i * 2, line.StartPos);
            lineRenderer.SetPosition(i * 2 + 1, line.EndPos);

            Vector3 direction = line.EndPos - line.StartPos;
            Vector3 lightPos = line.StartPos + direction * 0.95f;

            if (spawnLights)
            {
                GameObject light = Instantiate(PointLight, lightPos, Quaternion.identity);
                Lights.Add(light);
            }
        }
    }

    public void ClearLaser()
    {
        foreach (GameObject light in Lights)
        {
            Destroy(light.gameObject);
        }
        Lights.Clear();
        lineRenderer.positionCount = 0;
    }
}