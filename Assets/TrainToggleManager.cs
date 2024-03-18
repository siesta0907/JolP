using UnityEngine;
using System.Collections; // Coroutine을 사용하기 위해 필요
using UnityEngine.UI; // UI 관련 기능을 사용하기 위해 필요

public class TrainToggleManager : MonoBehaviour
{
    public GameObject firstPanel;
    public GameObject secondPanel;
    public GameObject thirdPanel;
    public GameObject fourthPanel;

    public GameObject trainMenuPanel;

    // 각 패널에 대응되는 이미지 GameObjects
    public GameObject firstImage;
    public GameObject secondImage;
    public GameObject thirdImage;
    public GameObject fourthImage;



    void Start()
    {
        DisableAllPanelsAndImages();

    }

    public void ShowFirstPanel()
    {
        ActivatePanel(firstPanel);

        EggMonStat.DecreaseStat("health", 60); // 특정 상태 감소 -> 검술 훈련

        StartCoroutine(ShowRewardImageAfterDelay(firstImage, 3f));
    }

    public void ShowSecondPanel()
    {
        ActivatePanel(secondPanel);

        EggMonStat.DecreaseStat("health", 40); // 특정 상태 감소 -> 무용
        StartCoroutine(ShowRewardImageAfterDelay(secondImage, 3f));


    }

    public void ShowThirdPanel()
    {
        ActivatePanel(thirdPanel);
        EggMonStat.DecreaseStat("health", 20); // 특정 상태 감소 -> 예절

        StartCoroutine(ShowRewardImageAfterDelay(thirdImage, 3f));

    }


    public void ShowFourthPanel()
    {
        ActivatePanel(fourthPanel);
        EggMonStat.DecreaseStat("health", 30); // 특정 상태 감소 -> 사고력

        StartCoroutine(ShowRewardImageAfterDelay(fourthImage, 3f));

    }
    private void ActivatePanel(GameObject panel)
    {
        // 모든 패널 비활성화
        firstPanel.SetActive(false);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(false);
        fourthPanel.SetActive(false);

        // 선택한 패널 활성화
        panel.SetActive(true);
    }


    IEnumerator ShowRewardImageAfterDelay(GameObject rewardImage, float delay)
    {
        // 모든 보상 이미지 비활성화
        firstImage.SetActive(false);
        secondImage.SetActive(false);
        thirdImage.SetActive(false);
        fourthImage.SetActive(false);

        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // 해당 보상 이미지 활성화
        rewardImage.SetActive(true);
    }

    public void ShowExitPanel()
    {
        DisableAllPanelsAndImages();
        trainMenuPanel.SetActive(true);
    }
    private void DisableAllPanelsAndImages()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(false);
        fourthPanel.SetActive(false);

        firstImage.SetActive(false);
        secondImage.SetActive(false);
        thirdImage.SetActive(false);
        fourthImage.SetActive(false);
    }

    public void HideRewardImage()
    {
        // 모든 리워드 이미지 비활성화
        firstImage.SetActive(false);
        secondImage.SetActive(false);
        thirdImage.SetActive(false);
        fourthImage.SetActive(false);



    }
}
