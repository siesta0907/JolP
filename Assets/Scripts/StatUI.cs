using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    public Slider health_bar, intellect_bar, likeability_bar, cleanliness_bar, full_bar, social_bar, playfulness_bar;
    private void Update()
    {
        health_bar.maxValue = EggMonStat.maxHealth;
        health_bar.value = EggMonStat.health;
        intellect_bar.value = EggMonStat.intellect;
        likeability_bar.value = EggMonStat.likeability;
        cleanliness_bar.value = EggMonStat.cleanliness;
        full_bar.value = EggMonStat.full;
        social_bar.value = EggMonStat.social;
        playfulness_bar.value = EggMonStat.playfulness;
    }
}
