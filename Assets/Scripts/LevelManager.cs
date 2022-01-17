using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [NonSerialized] public bool LvlCompleted;
    [SerializeField] private GameObject enemiesParent;
    private readonly List<Enemy> enemies = new List<Enemy>();
    public bool IsWinCondition() => enemies.Count == 0;

    private void Start()
    {
        for (var i = 0; i < enemiesParent.transform.childCount; i++)
        {
            enemies.Add(enemiesParent.transform.GetChild(i).GetComponent<Enemy>());
        }
    }

    private void Update()
    {
        enemies.RemoveAll(item => item == null);
        if (FindObjectOfType<HeroSelection>().heroSelected && FindObjectOfType<Player>().needToRestart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
