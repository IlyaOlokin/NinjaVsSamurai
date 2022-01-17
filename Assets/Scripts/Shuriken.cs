using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public int Dmg;
    public float Speed;
    //private float rotateSpeed = 500;
    [SerializeField]private GameObject sprite;

    [NonSerialized] public Vector2 velocityDir;

    private void Start()
    {
        float angle = Mathf.Atan2(velocityDir.y, velocityDir.x) * Mathf.Rad2Deg;
        sprite.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void FixedUpdate()
    {
        transform.Translate(velocityDir.normalized * (Speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
