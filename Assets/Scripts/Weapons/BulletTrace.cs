using System.Collections;
using UnityEngine;

public class BulletTrace : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private IEnumerator LifeLine(Vector3 originPosition, Vector3 endPosition)
    {
        _lineRenderer.SetPosition(0, originPosition);
        _lineRenderer.SetPosition(1, endPosition);
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.038f);
        _lineRenderer.enabled = false;
    }

    public void DrawLine(Vector3 originPosition, Vector3 endPosition)
    {
        if (_lineRenderer.enabled == true)
        {
            _lineRenderer.enabled = false;
            StopAllCoroutines();
        }
        
        StartCoroutine(LifeLine(originPosition, endPosition));
    }
}