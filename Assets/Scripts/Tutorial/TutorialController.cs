using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TutorialPlayerInput playerInput;
    [SerializeField] private float startDelay;
    [SerializeField] private float endInputDelay;
    [SerializeField] private Transform inputStartPoint;
    [SerializeField] private Transform inputEndPoint;
    
    private Transform inputStartPointTemp;
    private float dist;
    private bool inputStarted;

    private void Start()
    {
        inputStartPointTemp = inputStartPoint;
        dist = Vector2.Distance(inputStartPoint.position, inputEndPoint.position);
        StartCoroutine(StartInput(startDelay));
    }

    private void Update()
    {
        if (inputStarted)
        {
            playerInput.WhileInput(inputStartPointTemp.position);
            inputStartPointTemp.position = Vector3.MoveTowards(inputStartPointTemp.position, inputEndPoint.position, 
                dist / endInputDelay * Time.deltaTime);
        }
    }


    IEnumerator StartInput(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerInput.StartInput(inputStartPoint.position);
        inputStarted = true;
        StartCoroutine((EndInput(endInputDelay)));
    }
    
    IEnumerator EndInput(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerInput.EndInput();
        inputStarted = false;
    }
}
