using UnityEngine;

public class togglemanager : MonoBehaviour
{
    public GameObject firstPanel; // 첫 번째 패널의 참조
    public GameObject secondPanel; // 두 번째 패널의 참조

    // 게임이 시작할 때 첫 번째 패널을 활성화하고 두 번째 패널을 비활성화합니다.
    void Start()
    {
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
    }

    // 첫 번째 패널의 버튼에 할당할 메서드
    public void ShowSecondPanel()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(true);
    }

    // 두 번째 패널의 버튼에 할당할 메서드
    public void ShowFirstPanel()
    {
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
    }
}
