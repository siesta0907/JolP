using UnityEngine;

public class TrainToggleManager : MonoBehaviour
{
    public GameObject firstPanel;
    public GameObject secondPanel;
    public GameObject thirdPanel;
    public GameObject fourthPanel;

    public GameObject trainMenuPanel;

    private Animator firstPanelAnimator;
    private Animator secondPanelAnimator;
    private Animator thirdPanelAnimator;
    private Animator fourthPanelAnimator;

    void Start()
    {
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(false);
        fourthPanel.SetActive(false);

        // 패널에 연결된 Animator 컴포넌트 가져오기
        firstPanelAnimator = firstPanel.GetComponent<Animator>();
        secondPanelAnimator = secondPanel.GetComponent<Animator>();
        thirdPanelAnimator = thirdPanel.GetComponent<Animator>();
        fourthPanelAnimator = fourthPanel.GetComponent<Animator>();
    }

    public void ShowFirstPanel()
    {
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(false);
        fourthPanel.SetActive(false);

        // 첫 번째 패널의 애니메이션 재생
        firstPanelAnimator.SetTrigger("Show");
    }

    public void ShowSecondPanel()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(true);
        thirdPanel.SetActive(false);
        fourthPanel.SetActive(false);

        // 두 번째 패널의 애니메이션 재생
        secondPanelAnimator.SetTrigger("Show");
    }

    public void ShowThirdPanel()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(true);
        fourthPanel.SetActive(false);

        // 세 번째 패널의 애니메이션 재생
        thirdPanelAnimator.SetTrigger("Show");
    }

    public void ShowFourthPanel()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(false);
        thirdPanel.SetActive(false);
        fourthPanel.SetActive(true);

        // 네 번째 패널의 애니메이션 재생
        fourthPanelAnimator.SetTrigger("Show");
    }

    public void ShowExitPanel()
    {
        firstPanel.SetActive(false);
        trainMenuPanel.SetActive(true);
    }
}
