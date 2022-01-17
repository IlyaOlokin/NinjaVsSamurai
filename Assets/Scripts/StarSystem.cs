using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class StarSystem
{
    public const int TotalStars = 54;
    private static int[] LevelStars = new int[18];
    private static int[] WorldBarriers =  {13, 28, 54};

    public static int GetCurrentStarsAmount()
    {
        return LevelStars.Sum();
    }

    public static void SetLevelStars(int levelNum, int starsAmount)
    {
        if (LevelStars[levelNum - 1] < starsAmount)
            LevelStars[levelNum - 1] = starsAmount;
    }

    public static int GetLevelStars(int levelNum)
    {
        return LevelStars[levelNum - 1];
    }

    public static int GetNextWorldBarrier()
    {
        foreach (var barrier in WorldBarriers)
        {
            if (GetCurrentStarsAmount() < barrier)
            {
                return barrier;
            }
        }

        return WorldBarriers[WorldBarriers.Length - 1];
    }

    public static int[] GetAllWorldBarriers()
    {
        return WorldBarriers;
    }

    public static int[] GetAllLevelStars()
    {
        return LevelStars;
    }
    
    public static void SetAllLevelStars(string stars)
    {
        if(stars.Length != LevelStars.Length) return;
        for (int i = 0; i < LevelStars.Length; i++)
        {
            LevelStars[i] = int.Parse(stars[i].ToString());
        }
    }
    
    public static String SaveAllLevelStars()
    {
        var stars = new StringBuilder();
        for (int i = 0; i < LevelStars.Length; i++)
        {
            stars.Append(LevelStars[i].ToString());
        }

        return stars.ToString();
    }
}
