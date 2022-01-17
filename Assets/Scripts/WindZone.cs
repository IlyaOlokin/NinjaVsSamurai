using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField] private Vector2 windVector;
    [SerializeField] private float windForce;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity += windVector.normalized * windForce;
        }
    }
}
