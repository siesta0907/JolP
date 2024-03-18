using UnityEngine;

public class togglemanager : MonoBehaviour
{
    public GameObject firstPanel;   // Skin shop 패널
    public GameObject secondPanel;  // Food shop 패널
    public GameObject ThirdPanel;   // Play shop 패널

    // 게임이 시작할 때 첫 번째 패널을 활성화하고 두 번째 패널을 비활성화합니다.
    void Start()
    {
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
        ThirdPanel.SetActive(false);
    }

    // 첫 번째 패널의 버튼에 할당할 메서드
    public void ShowSecondPanel()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(true);
        ThirdPanel.SetActive(false);
    }
    public void ShowFirstPanel()
    {
        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
        ThirdPanel.SetActive(false);
    }

    // 두 번째 패널의 버튼에 할당할 메서드

    

    

    public void ShowThridPanel()
    {
        firstPanel.SetActive(false);
        secondPanel.SetActive(false);
        ThirdPanel.SetActive(true);
        
    }
}
