using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMove : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
     private Vector3 _startPoint;
    [SerializeField] private Transform endPoint;
     private Vector3 _endPoint;
    [SerializeField] private float speed;
    void Start()
    {
        _startPoint = startPoint.position;
        _endPoint = endPoint.position;
        
        transform.position = _startPoint;
    }

    
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _endPoint, speed);
        if (transform.position == _endPoint)
        {
            transform.position = _startPoint;
        }
    }
}
