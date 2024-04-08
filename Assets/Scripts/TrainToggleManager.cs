using UnityEngine;
using System.Collections; // Coroutine�� ����ϱ� ���� �ʿ�
using UnityEngine.UI; // UI ���� ����� ����ϱ� ���� �ʿ�

public class TrainToggleManager : MonoBehaviour
{
    public GameObject firstPanel;
    public GameObject secondPanel;
    public GameObject thirdPanel;
    public GameObject fourthPanel;

    public GameObject trainMenuPanel;

    // �� �гο� �����Ǵ� �̹��� GameObjects
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
        EggMonStat.DecreaseStat("health", 60); // Ư�� ���� ���� -> �˼� �Ʒ�
        EggMonStat.IncreaseStat("intellect", 50);
        ActivatePanel(firstPanel);

        

        StartCoroutine(ShowRewardImageAfterDelay(firstImage, 3f));
    }

    public void ShowSecondPanel()
    {
        ActivatePanel(secondPanel);

        EggMonStat.DecreaseStat("health", 40); // Ư�� ���� ���� -> ����
        EggMonStat.IncreaseStat("intellect", 30);

        StartCoroutine(ShowRewardImageAfterDelay(secondImage, 3f));



    }

    public void ShowThirdPanel()
    {
        ActivatePanel(thirdPanel);
        EggMonStat.DecreaseStat("health", 20); // Ư�� ���� ���� -> ����
        EggMonStat.IncreaseStat("intellect", 20);

        StartCoroutine(ShowRewardImageAfterDelay(thirdImage, 3f));

    }


    public void ShowFourthPanel()
    {
        ActivatePanel(fourthPanel);
        EggMonStat.DecreaseStat("health", 30); // Ư�� ���� ���� -> ����
        EggMonStat.IncreaseStat("intellect", 60);

        StartCoroutine(ShowRewardImageAfterDelay(fourthImage, 3f));

    }
    private void ActivatePanel(GameObject panel)
    {
        // ��� �г� ��Ȱ��ȭ
        firstPanel.SetActive(false);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(false);
        fourthPanel.SetActive(false);

        // ������ �г� Ȱ��ȭ
        panel.SetActive(true);
    }


    IEnumerator ShowRewardImageAfterDelay(GameObject rewardImage, float delay)
    {
        // ��� ���� �̹��� ��Ȱ��ȭ
        firstImage.SetActive(false);
        secondImage.SetActive(false);
        thirdImage.SetActive(false);
        fourthImage.SetActive(false);

        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(delay);

        // �ش� ���� �̹��� Ȱ��ȭ
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
        // ��� ������ �̹��� ��Ȱ��ȭ
        firstImage.SetActive(false);
        secondImage.SetActive(false);
        thirdImage.SetActive(false);
        fourthImage.SetActive(false);



    }
}
