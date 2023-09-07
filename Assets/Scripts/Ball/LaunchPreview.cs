using UnityEngine;

public class LaunchPreview : MonoBehaviour
{
    [SerializeField] private float maxLineLength = 10f;
    private LineRenderer lineRenderer;
    private Vector3 dragStartPoint;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Reset();
    }

    public void Reset()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    public void SetColor(Color color)
    {
        lineRenderer.material.color = color;
    }

    public void SetStartPoint(Vector3 worldPoint)
    {
        dragStartPoint = worldPoint;
        lineRenderer.SetPosition(0, dragStartPoint);
    }

    public void SetEndPoint(Vector3 worldPoint)
    {
        Vector3 pointOffset = worldPoint - dragStartPoint;
        Vector3 endPoint = transform.position + pointOffset;
        if (Vector3.Distance(endPoint, transform.position) > maxLineLength)
            endPoint = transform.position + pointOffset.normalized * maxLineLength;

        lineRenderer.SetPosition(1, endPoint);
    }
}