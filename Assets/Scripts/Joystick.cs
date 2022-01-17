using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private GameObject distanceIndicator;
    [SerializeField] private GameObject innerCircle;
    private float dist;

    void Start()
    {
        dist = (transform.position - distanceIndicator.transform.position).magnitude;
    }
    
    public void MoveInnerCircle(Vector3 target)
    {
        innerCircle.transform.position = Vector3.MoveTowards(transform.position, target + transform.position, dist);
    }
}
