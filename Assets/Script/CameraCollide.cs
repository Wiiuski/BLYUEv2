using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollide : MonoBehaviour
{
    public float minDistance;
    public float maxDistance;
    public float smooth = 10;
    Vector3 dollyDir;
    public Vector3 dollyDirAdj;
    public float distance;
    public LayerMask objectLayers;

    public bool woke;

    

    public void Start()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;

        woke = true;
    }
   
    void Update()
    {
        if (woke)
        {
            Vector3 cameraPOSDesired = transform.parent.TransformPoint(dollyDir * maxDistance);
            RaycastHit hit;
            if (Physics.Linecast(transform.parent.position, cameraPOSDesired, out hit, objectLayers))
            {
                distance = Mathf.Clamp(hit.distance * 0.8f, minDistance, maxDistance);
            }
            else
            {
                distance = maxDistance;
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
        }
        
    }
}
