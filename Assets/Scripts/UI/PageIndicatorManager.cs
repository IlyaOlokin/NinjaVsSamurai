using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageIndicatorManager : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject[] allPoints;

    private float indicatorTargetPositionX;
    private bool needToMove = false;
    private float swipeSpeed = 0.0083f;

    private void Start()
    {
        indicator.transform.position = allPoints[0].transform.position;
        indicatorTargetPositionX = allPoints[0].transform.position.x;
    }

    private void Update()
    {
        needToMove = indicator.transform.position.x != indicatorTargetPositionX;
    }
    
    private void FixedUpdate()
    {
        if (needToMove)
        {
            indicator.transform.position = Vector2.MoveTowards(indicator.transform.position,
                new Vector2(indicatorTargetPositionX, indicator.transform.position.y), swipeSpeed);
        }
    }
    
    public void SetIndicatorTargetPosition(int page)
    {
        indicatorTargetPositionX = allPoints[page - 1].transform.position.x;
    }
}
