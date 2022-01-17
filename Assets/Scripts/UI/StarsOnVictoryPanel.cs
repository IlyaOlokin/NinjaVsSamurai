using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsOnVictoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] starObjects;
    
    
    public void SetStars(int starsCount)
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
