using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTracer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float opacityDecay = 2;
    Color normalStart, normalEnd;

    private void Awake()
    {
        normalStart = lineRenderer.startColor;
        normalEnd = lineRenderer.endColor;
    }

    public void Draw(Vector2 p1, Vector2 p2)
    {
        StopAllCoroutines();
        Vector3[] points = { new Vector3(p1.x, p1.y, -1), new Vector3(p2.x, p2.y, -1) };
        lineRenderer.SetPositions(points);
        StartCoroutine(Decay());
    }
    IEnumerator Decay()
    {
        lineRenderer.startColor = normalStart;
        lineRenderer.endColor = normalEnd;
        while (lineRenderer.startColor.a >= 0 && lineRenderer.startColor.a >= 0)
        {
            Color s = lineRenderer.startColor, e = lineRenderer.endColor;
            Color ns = new Color(s.r, s.g, s.b, s.a - opacityDecay * Time.deltaTime);
            Color ne = new Color(e.r, e.g, e.b, e.a - opacityDecay * Time.deltaTime);
            lineRenderer.startColor = ns;
            lineRenderer.endColor = ne;
            yield return null;
        }
    }
}
