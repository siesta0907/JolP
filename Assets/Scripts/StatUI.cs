using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 에그몬의 스탯을 UI로 표시하는 클래스
public class StatUI : MonoBehaviour
{
    public Slider health_bar, intellect_bar, likeability_bar, cleanliness_bar, full_bar, social_bar, playfulness_bar;
    public Text playerName;
    public Image icon;

    private void Start()
    {
        online_play.LoadPlayerData();
    }

    // 프레임마다 호출되는 함수
    private void Update()
    {
        if (online_play.playerIcon != null)
        {
            Sprite iconSprite = Resources.Load<Sprite>(online_play.playerIcon);
            if (iconSprite != null)
                icon.sprite = iconSprite;
            else
                Debug.Log($"{online_play.playerIcon}을 찾을 수 없음!");
        }
        if (online_play.playerName != null) playerName.text = online_play.playerName;
        
        // 각 슬라이더의 최대값을 에그몬의 최대 스탯 값으로 설정
        health_bar.maxValue = EggMonStat.maxHealth;

        // 각 슬라이더에 현재 에그몬의 스탯 값을 적용
        health_bar.value = EggMonStat.health;
        intellect_bar.value = EggMonStat.intellect;
        likeability_bar.value = EggMonStat.likeability;
        cleanliness_bar.value = EggMonStat.cleanliness;
        full_bar.value = EggMonStat.full;
        social_bar.value = EggMonStat.social;
        playfulness_bar.value = EggMonStat.playfulness;
    }
}
