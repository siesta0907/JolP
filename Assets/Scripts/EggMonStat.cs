using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EggMonStat 
{
    public static float health, intellect, likeability, cleanliness, full;

    public static void InitializeStat()
    {
        health = 0f;
        intellect = 0f;
        likeability = 0f;
        cleanliness = 0f;
        full = 0f;
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
            default: Debug.Log($"{stat}이라는 스탯은 존재하지 않습니다."); break;

        }
    }
}
