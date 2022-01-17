using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialPlayerInput : MonoBehaviour
{
    private Vector3 startPoint;
    private Vector3 forceVector;

    [SerializeField] private Player player;
    [SerializeField] private Joystick joystickPref;
    private GameObject playerCanvas;
    
    
    private Joystick joystick;

    private int inputAmount;

    private void Start()
    {
        Time.timeScale = 1f;
        playerCanvas = GameObject.Find("Player");
        joystick = Instantiate(joystickPref);
        joystick.transform.SetParent(playerCanvas.transform);
        joystick.transform.gameObject.SetActive(false);
    }

    public void StartInput(Vector3 point)
    {
        startPoint = point;
        joystick.transform.position = (Vector2) startPoint;
    }
    
    public void WhileInput(Vector3 point)
    {
        forceVector = startPoint - point;
        joystick.transform.gameObject.SetActive(true);
        player.SetArrowActive(true);
        player.DrawVectorArrow(forceVector);
        joystick.MoveInnerCircle(-forceVector);
       
    }
    
    public void EndInput()
    {
        player.GetBoost(forceVector);
        player.SetArrowActive(false);
        joystick.transform.gameObject.SetActive(false);
    }
}