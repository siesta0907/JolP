using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        cleanliness = 50f;
        full = 50f;
        social = 0f;
    }

    public static void IncreaseStat(string stat, float num)
    {
        switch (stat)
        {
            case "health": health += num; break;
            case "intellect": intellect += num; break;
            case "likeability": likeability += num; break;
            case "cleanliness": cleanliness += num; break;
            case "full": full += num; break;
            case "social": social += num; break;
            case "playfulness": playfulness += num; break;
            default: Debug.Log($"{stat}이라는 스탯은 존재하지 않습니다."); break;

        }
    }
    public static void DecreaseStat(string stat, float num)
    {
        switch (stat)
        {
            case "health": health -= num; break;
            case "intellect": intellect -= num; break;
            case "likeability": likeability -= num; break;
            case "cleanliness": cleanliness -= num; break;
            case "full": full -= num; break;
            case "social": social -= num; break;
            case "playfulness": playfulness -= num; break;
            default: Debug.Log($"{stat}이라는 스탯은 존재하지 않습니다."); break;

        }
    }
}
