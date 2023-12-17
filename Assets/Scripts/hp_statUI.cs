using UnityEngine;
using UnityEngine.UI;

public class hp_statUI : MonoBehaviour
{
    public Slider hpSlider;

    void Start()
    {
        // 스탯 초기화
        EggMonStat.InitializeStat();

        // 슬라이더 최대값 설정
        hpSlider.maxValue = 1; // 슬라이더의 값은 0과 1 사이로, 비율로 표현됩니다.
    }

    void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        float healthRatio = EggMonStat.health / EggMonStat.maxHealth;
        hpSlider.value = healthRatio;

        // 디버그 로그 추가
        Debug.Log("Health: " + EggMonStat.health + " / Max Health: " + EggMonStat.maxHealth + " - Ratio: " + healthRatio);
    }

    /*private void UpdateUI()
    {
        // EggMonStat 클래스의 health 값을 maxHealth로 나누어 슬라이더의 값을 업데이트합니다.
        hpSlider.value = EggMonStat.health / EggMonStat.maxHealth;
    }*/
}
