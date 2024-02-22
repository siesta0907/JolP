using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// EggMon(알몬)의 스탯을 관리하는 정적 클래스
public static class EggMonStat
{
    // 각 스탯의 초기값 정의
    public static float health, maxHealth, intellect, likeability, cleanliness, full, social, playfulness;

    // 스탯 초기화 함수
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

    // 스탯 증가시키는 함수
    public static void IncreaseStat(string stat, float num)
    {
        // 스탯 이름에 따라 적절한 스탯을 증가시킴
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

    // 스탯 감소시키는 함수
    public static void DecreaseStat(string stat, float num)
    {
        // 스탯 이름에 따라 적절한 스탯을 감소시킴
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
