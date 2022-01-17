using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Barrel : MonoBehaviour
{
    [SerializeField] private Color blinkColor;
    
    private bool isActivated;
    
    [SerializeField] private float explodeDelay;
    [SerializeField] private float blinkPeriod;

    [SerializeField] private GameObject explosionEffect;
    
    private float blinkTimer;
    private float explodeTimer;
    
    private int blinkCount;
    private Color startColor;
    private Rigidbody2D rb;
    private Collider2D collider;
    private Image img;

    private void Start()
    {
        img = transform.GetComponent<Image>();
        startColor = img.color;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (isActivated)
        {
            blinkTimer += Time.deltaTime;
            explodeTimer += Time.deltaTime;
            if (explodeTimer >= explodeDelay)
            {
                Explode();
            }
        
            if (blinkTimer >= blinkPeriod)
            {
                img.color = blinkCount % 2 == 0 ? blinkColor : startColor;
                blinkTimer = 0;
                blinkCount++;
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Activate();
        }
            
    }

    private void Activate()
    {
        isActivated = true;
    }

    private void Explode()
    {
        explosionEffect.SetActive(true);
        rb.velocity = Vector2.zero;
        collider.enabled = false;
        GetComponent<Image>().enabled = false;
    }
}
