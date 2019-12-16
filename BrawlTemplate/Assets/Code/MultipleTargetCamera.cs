using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{

    public List<Transform> Targets;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    public float zoomLimiter = 50f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    private Vector3 velocity;


    private Camera cam;

    private void LateUpdate()
    {
        if (Targets.Count == 0)
            return;

        Move();
        Zoom();
       
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Zoom()
    {
     ///   Debug.Log(GetGreatestDistance());

        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() /  zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }


    float GetGreatestDistance()
    {
        var bounds = new Bounds(Targets[0].position, Vector3.zero);
        for(int i = 0; i < Targets.Count; i++)
        {
            bounds.Encapsulate(Targets[i].position);
        }

        return bounds.size.x;
    }


    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if(Targets.Count == 1)
        {
            return Targets[0].position;
        }

        var bounds = new Bounds(Targets[0].position, Vector3.zero);
        for(int i = 0; i < Targets.Count; i++)
        {
            bounds.Encapsulate(Targets[i].position);
        }
        return bounds.center;
    }
}
