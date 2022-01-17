using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject arrowRotater;
    public int dmg;
    
    private bool isDead;
    [NonSerialized] public bool needToRestart = false;
    
    private float blinkTimer;
    [SerializeField]private float blinkPeriod;
    [SerializeField]private int blinksCount;
    private int currentBlinksCount;
    
    private Rigidbody2D rb;
    private Image image;
    
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        image = transform.Find("Body").GetComponent<Image>();
    }

    public bool IsStopped()
    {
        return rb.velocity == Vector2.zero;
    }

    private void Update()
    {
        if (isDead)
        {
            PlayDeath();
        }

        if (rb.velocity.magnitude <= 0.1f)
            rb.velocity = Vector2.zero;
    }

    public void GetBoost(Vector2 forceVector)
    {
        rb.AddForce(forceVector.normalized * speed, ForceMode2D.Impulse);
    }

    public void DrawVectorArrow(Vector2 vector)
    {
        arrowRotater.transform.right = vector;
    }

    public void SetArrowActive(bool isActive)
    {
        arrowRotater.SetActive(isActive);
    }

    public void Die()
    {
        isDead = true;
        blinkTimer = 0;
        rb.velocity = Vector2.zero;
    }

    private void PlayDeath()
    {
        blinkTimer += Time.deltaTime;
        
        if (currentBlinksCount == blinksCount)
        {
            needToRestart = true;
        }
        
        if (blinkTimer >= blinkPeriod)
        {
            image.enabled = !image.enabled;
            blinkTimer = 0;
            currentBlinksCount++;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemies"))
        {
            if (rb.velocity.magnitude <= 0.2f)
            {
                rb.velocity = (transform.position - other.transform.position).normalized * 0.2f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Wall"))
        {
            FindObjectOfType<AudioManager>().Play("Bounce");
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
