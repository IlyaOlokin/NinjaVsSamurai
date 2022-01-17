using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StarsOnLevelButton : MonoBehaviour
{
    [SerializeField] private GameObject[] starObjects;
    [SerializeField] private Text levelNum;
    private int stars;

    private void Start()
    {
        SetStars(StarSystem.GetLevelStars(int.Parse(levelNum.text)));
        
    }
    
    private void SetStars(int starsCount)
    {
        for (int i = 0; i < starObjects.Length; i++)
        {
            if (i < starsCount)
            {
                starObjects[i].SetActive(true);

            }
            else
            {
                starObjects[i].SetActive(false);
            }
        }
    }
}
