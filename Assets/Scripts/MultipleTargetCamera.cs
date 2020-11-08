using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public static MultipleTargetCamera Instance { get; private set; }
    public List<Transform> targets;
    public List<Transform> externalTargets;
    private Vector3 velocity;
    public float smoothTime;
    public Vector3 offset;
    //public float maxZoom, minZoom, zoomLimiter;
    void Start()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        if (targets.Count == 0) return;
        Move();
        Zoom();
        //Debug.Log(GetGreatestDistance());
    }

    private void Move()
    {
        float CameraZ = Camera.main.transform.position.z;
        Vector3 centerPoint = getCenterPoint();
        centerPoint.z = CameraZ;
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private void Zoom()
    {
        //float newZoom = Mathf.Lerp(maxZoom, minZoom,  GetGreatestDistance() / zoomLimiter);
        float newZoom = 2*GetGreatestDistance() / 5;
        if (newZoom < 5f) newZoom = 5f;
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newZoom, Time.deltaTime);
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (var target in targets)
        {
            bounds.Encapsulate(target.position);
        }
        foreach(var target in externalTargets)
        {
            bounds.Encapsulate(target.position);
        }
        return bounds.size.x;
    }

    private Vector3 getCenterPoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach(var target in targets)
        {
            bounds.Encapsulate(target.position);
        }
        foreach (var target in externalTargets)
        {
            bounds.Encapsulate(target.position);
        }
        return bounds.center;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
