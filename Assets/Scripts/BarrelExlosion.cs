using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelExlosion : MonoBehaviour
{
    [SerializeField] private float explosionTime;
    public int Dmg;
    private float vanishSpeed;
    private Image sprite;

    private void OnEnable()
    {
        sprite = GetComponent<Image>();
        vanishSpeed = 1f / explosionTime;
        Destroy(transform.parent.gameObject, explosionTime);
    }

    private void Update()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b,
            sprite.color.a - vanishSpeed * Time.deltaTime);
    }
}
