using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenShot : MonoBehaviour
{
    [SerializeField] private GameObject shuriken;
    private GameObject playerCanvas;

    private void Start()
    {
        playerCanvas = GameObject.Find("Player");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Wall"))
        {
            ThrowShurikens(other.contacts[0].normal);
        }
    }

    private void ThrowShurikens(Vector2 normal)
    {
        var angle1 = 45;
        
        var angle2 = -45;
        

        
        var dir1 = (Vector2)(Quaternion.Euler(0, 0, angle1) * normal);
        var dir2 = (Vector2)(Quaternion.Euler(0, 0, angle2) * normal);


        ThrowShuriken(normal);
        ThrowShuriken(dir1);
        ThrowShuriken(dir2);
    }

    private void ThrowShuriken(Vector2 normal)
    {
        var newShuriken = Instantiate(shuriken);
        newShuriken.transform.SetParent(playerCanvas.transform);
        newShuriken.transform.position = transform.position;
        newShuriken.transform.rotation = Quaternion.identity;
        newShuriken.transform.localScale = Vector3.one;
        newShuriken.GetComponent<Shuriken>().velocityDir = normal;
    }
}
