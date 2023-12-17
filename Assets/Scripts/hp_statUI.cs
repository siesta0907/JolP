using UnityEngine;
using UnityEngine.UI;

// EggMon의 체력 스탯을 UI로 표시하는 스크립트
public class hp_statUI : MonoBehaviour
{
    // 체력을 나타내는 슬라이더와 이미지
    public Slider hpSlider;
    public Image hpBarImage;

    // 시작 시 호출되는 함수
    void Start()
    {
        // 스탯 초기화
        EggMonStat.InitializeStat();

        // 슬라이더 최대값 설정
        hpSlider.maxValue = 1; // 슬라이더의 값은 0과 1 사이로, 비율로 표현됩니다.
    }

    // 프레임마다 호출되는 함수
    void Update()
    {
        // UI 업데이트 함수 호출
        UpdateUI();
    }

    // UI를 업데이트하는 함수
    private void UpdateUI()
    {
        // 체력 비율 계산
        float healthRatio = EggMonStat.health / EggMonStat.maxHealth;

        // 슬라이더 값 설정
        hpSlider.value = healthRatio;

        // 디버그 로그 추가
        Debug.Log("Health: " + EggMonStat.health + " / Max Health: " + EggMonStat.maxHealth + " - Ratio: " + healthRatio);

        // HP 바 이미지 크기 조절
        hpBarImage.rectTransform.sizeDelta = new Vector2(hpSlider.value * hpBarImage.rectTransform.sizeDelta.x, hpBarImage.rectTransform.sizeDelta.y);
    }
}
