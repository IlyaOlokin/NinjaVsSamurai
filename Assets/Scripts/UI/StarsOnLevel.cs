using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StarsOnLevel : MonoBehaviour
{
    [SerializeField] private GameObject[] starObjects;
    private int stars;
    
    void Start()
    {
        SetInputAmount(0);
    }


    public int GetStars()
    {
        return stars;
    }

    public void SetInputAmount(int inputAmount)
    {
        stars = 4 - inputAmount;
        if (stars <= 0) stars = 1;
        SetStars(stars);
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
