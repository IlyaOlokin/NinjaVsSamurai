using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviour
{
    private Vector3 startPoint;
    private Vector3 forceVector;
    private bool aimingBreaked;

    [SerializeField] private Player player;
    [SerializeField] private Joystick joystickPref;
    [SerializeField] private GameObject inputIndicator;
    private Collider2D inputZone;
    private bool inputAllowed = true;
    private GameObject playerCanvas;
    private StarsOnLevel starsOnLevel;
    
    private Joystick joystick;

    private int inputAmount;

    public int GetInputAmount() => inputAmount;

    private void Start()
    {
        playerCanvas = GameObject.Find("Player");
        inputZone = GameObject.FindGameObjectWithTag("InputZone").GetComponent<BoxCollider2D>();
        joystick = Instantiate(joystickPref);
        joystick.transform.SetParent(playerCanvas.transform);
        joystick.transform.gameObject.SetActive(false);
        starsOnLevel = GameObject.FindGameObjectWithTag("StarsOnLevel").GetComponent<StarsOnLevel>();
    }

    void Update()
    {
        inputIndicator.SetActive(player.IsStopped() && !player.IsDead());
        
        if (!player.IsStopped())
        {
            if (Input.GetMouseButton(0)) inputAllowed = false;
            return;
        }
        if (Input.GetMouseButtonDown(0) && !InInputZone())
        {
            inputAllowed = false;
            return;
        }
        if (Input.GetMouseButtonUp(0) && !inputAllowed)
        {
            inputAllowed = true;
            return;
        }
        if (!Input.GetMouseButton(0) && !inputAllowed)
        {
            inputAllowed = true;
        }
        if (!inputAllowed) return;
        
        
        if (Input.GetMouseButtonDown(0) && player.IsStopped())
        {
            StartInput(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetMouseButton(0) && !aimingBreaked && player.IsStopped())
        {
            WhileInput(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetMouseButtonUp(0) && player.IsStopped())
        {
            EndInput();
        }
        if (Input.GetMouseButtonDown(1))
        {
            forceVector = Vector3.zero;
            player.SetArrowActive(false);
            aimingBreaked = true;
            joystick.transform.gameObject.SetActive(false);
        }
    }

    public void EndInput()
    {
        if (forceVector.magnitude > 0.15f)
        {
            player.GetBoost(forceVector);
            inputAmount++;
            starsOnLevel.SetInputAmount(inputAmount);
        }

        player.SetArrowActive(false);
        aimingBreaked = false;
        joystick.transform.gameObject.SetActive(false);
    }

    public void WhileInput(Vector3 point)
    {
        forceVector = startPoint - point;
        if (forceVector.magnitude > 0.15f)
        {
            joystick.transform.gameObject.SetActive(true);
            player.SetArrowActive(true);
            player.DrawVectorArrow(forceVector);
            joystick.MoveInnerCircle(-forceVector);
        }
        else
        {
            joystick.transform.gameObject.SetActive(false);
        }
    }

    public void StartInput(Vector3 point)
    {
        startPoint = point;

        joystick.transform.position = (Vector2) startPoint;
    }

    private bool InInputZone()
    {
        return inputZone.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}