using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EggMonStat 
{
    public static float health, maxHealth, intellect, likeability, cleanliness, full, social, playfulness; 

    public static void InitializeStat()
    {
        maxHealth = 100f;
        health = maxHealth;
        intellect = 0f; 
        likeability = 0f;
        playfulness = 0f;
        cleanliness = 100f;
        full = 100f;
        social = 50f;
    }

    public static void IncreaseStat(string stat, int num)
    {
        switch (stat)
        {
            case "health": health += num; break;
            case "intellect": intellect += num; break;
            case "likeability": likeability += num; break;
            case "cleanliness": cleanliness += num; break;
            case "full": full += num; break;
            case "social": social += num; break;
            default: Debug.Log($"{stat}이라는 스탯은 존재하지 않습니다."); break;

        }
    }
    public static void DecreaseStat(string stat, int num)
    {
        switch (stat)
        {
            case "health": health -= num; break;
            case "intellect": intellect -= num; break;
            case "likeability": likeability -= num; break;
            case "cleanliness": cleanliness -= num; break;
            case "full": full -= num; break;
            case "social": social -= num; break;
            default: Debug.Log($"{stat}이라는 스탯은 존재하지 않습니다."); break;

        }
    }
}
