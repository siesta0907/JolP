using UnityEngine;
using System.Collections; // Coroutine을 사용하기 위해 필요
using UnityEngine.UI; // UI 관련 기능을 사용하기 위해 필요



public class WorkToggleManager : MonoBehaviour
{
    public GameObject firstPanel;
    public GameObject secondPanel;
    public GameObject thirdPanel;
    public GameObject fourthPanel;

    public GameObject workMenuPanel;
    public Tamagotchi tamagotchi;


    // 각 패널에 대응되는 리워드 이미지
    public GameObject rewardImage1;
    public GameObject rewardImage2;
    public GameObject rewardImage3;
    public GameObject rewardImage4;


    public Text rewardText; // 보상 텍스트를 위한 변수


    void Start()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(false);
        fourthPanel.SetActive(false);

        // 모든 리워드 이미지를 초기에 비활성화
        rewardImage1.SetActive(false);
        rewardImage2.SetActive(false);
        rewardImage3.SetActive(false);
        rewardImage4.SetActive(false);
    }

    public void ShowFirstPanel()
    {
        ActivatePanel(firstPanel, rewardImage1, "과외를 마치고 200코인을 받았습니다!", 200, 3f);

        MoneyManager.AddMoney(200); // 첫 번째 패널(과외하기) 활성화 시 200 추가

        EggMonStat.DecreaseStat("full", 40f);

        if (EggMonStat.full <= 0) // full은 배고픔을 뜻함
        {
            tamagotchi.DecreaseHP(); // 배고픔이 0 이하이면 HP 감소
        }


        Debug.Log("돈 : 200코인 획득");

    }

    public void ShowSecondPanel()
    {
        ActivatePanel(secondPanel, rewardImage2, "백업 댄서로 활약하여 400코인을 받았습니다!", 400, 3f);

        MoneyManager.AddMoney(200); // 두 번째 패널(백업댄서 하기) 활성화 시 400 추가

        EggMonStat.DecreaseStat("full", 40f);

        if (EggMonStat.full <= 0) // full은 배고픔을 뜻함
        {
            tamagotchi.DecreaseHP(); // 배고픔이 0 이하이면 HP 감소
        }


        Debug.Log("돈 : 400코인 획득");
    }

    public void ShowThirdPanel()
    {
        ActivatePanel(thirdPanel, rewardImage3, "카페 일을 마치고 200코인을 받았습니다!", 200, 3f);

        MoneyManager.AddMoney(400); // 세 번째 패널(카페알바) 활성화 시 400 추가
        EggMonStat.DecreaseStat("full", 40f);

        if (EggMonStat.full <= 0) // full은 배고픔을 뜻함
        {
            tamagotchi.DecreaseHP(); // 배고픔이 0 이하이면 HP 감소
        }

    }

    public void ShowFourthPanel()
    {
        ActivatePanel(fourthPanel, rewardImage4, "바텐딩을 완료하고 300코인을 받았습니다!", 300, 3f);

        MoneyManager.AddMoney(300); // 네 번째 패널(바텐더) 활성화 시 300 추가

        EggMonStat.DecreaseStat("full", 40f);

        if (EggMonStat.full <= 0) // full은 배고픔을 뜻함
        {
            tamagotchi.DecreaseHP(); // 배고픔이 0 이하이면 HP 감소
        }


    }
    void ActivatePanel(GameObject panel, GameObject rewardImage, string rewardMessage, int moneyToAdd, float delay)
    {
        // 모든 패널 비활성화
        firstPanel.SetActive(false);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(false);
        fourthPanel.SetActive(false);

        // 선택한 패널 활성화
        panel.SetActive(true);

        // 돈 추가
        MoneyManager.AddMoney(moneyToAdd);

        // EggMonStat와 tamagotchi 관련 로직은 여기에 포함 (생략)

        // 보상 텍스트 업데이트 및 이미지 활성화
        UpdateRewardTextAndShowImage(rewardImage, rewardMessage, delay);
    }
    void UpdateRewardTextAndShowImage(GameObject rewardImage, string text, float delay)
    {
        StartCoroutine(ShowRewardImageAfterDelay(rewardImage, text, delay));
    }

    IEnumerator ShowRewardImageAfterDelay(GameObject rewardImage, string text, float delay)
    {
        // 모든 리워드 이미지 비활성화
        rewardImage1.SetActive(false);
        rewardImage2.SetActive(false);
        rewardImage3.SetActive(false);
        rewardImage4.SetActive(false);

        yield return new WaitForSeconds(delay);

        // 보상 텍스트 업데이트
        rewardText.text = text;

        // 해당 보상 이미지 활성화
        rewardImage.SetActive(true);
    }


    public void HideRewardImage()
    {
        // 모든 리워드 이미지 비활성화
        rewardImage1.SetActive(false);
        rewardImage2.SetActive(false);
        rewardImage3.SetActive(false);
        rewardImage4.SetActive(false);
        


}

public void ShowExitPanel()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(false);
        fourthPanel.SetActive(false);
        workMenuPanel.SetActive(true);

        // 모든 리워드 이미지를 비활성화할 때 사용
        HideRewardImage();
    }
}
