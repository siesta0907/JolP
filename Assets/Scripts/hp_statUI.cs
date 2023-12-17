using UnityEngine;
using UnityEngine.UI;

public class hp_statUI : MonoBehaviour
{
    public Slider hpSlider;
    public Image hpBarImage;

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

        hpBarImage.rectTransform.sizeDelta = new Vector2(hpSlider.value * hpBarImage.rectTransform.sizeDelta.x, hpBarImage.rectTransform.sizeDelta.y);
    }

}
