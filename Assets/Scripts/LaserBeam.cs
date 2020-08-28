using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint;

    [SerializeField]
    private Transform endPoint;

    private LineRenderer lineRenderer;

    private bool isEnabled;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.25f;

        isEnabled = this.gameObject.activeInHierarchy;

        InvokeRepeating("LaserRoutine", 0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(1, endPoint.position);
    }

    void LaserRoutine()
    {
        isEnabled = !isEnabled;
        this.gameObject.SetActive(isEnabled);
    }
}
