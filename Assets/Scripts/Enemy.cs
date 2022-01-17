using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private ParticleSystem particle;
    [SerializeField] private int lives;
    [SerializeField]private GameObject[] healthPoints;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            TakeDamage(collision.transform.GetComponent<Player>().dmg);
        }

        if (collision.transform.CompareTag("Shuriken"))
        {
            TakeDamage(collision.transform.GetComponent<Shuriken>().Dmg);
        }
        
        if (collision.transform.CompareTag("Explosion"))
        {
            TakeDamage(collision.transform.GetComponent<BarrelExlosion>().Dmg);
        }
    }

    private void TakeDamage(int dmg)
    {
        particle.Play();
        lives -= dmg;
        SetHealthPoints(lives);
        FindObjectOfType<AudioManager>().Play("EnemyHit");
        if (lives <= 0)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject, 1);
        }
           
    }

    private void SetHealthPoints(int lives)
    {
        for (int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].SetActive(i < lives);
        }
    }
}
