using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 씬 전환을 담당하는 클래스
public class SceneChanger : MonoBehaviour
{
    public Button RetryButton; // 전환 버튼을 할당할 UI 버튼
    public string ingame; // 로드할 씬의 이름

    // 시작 시 호출되는 함수
    void Start()
    {
        // 전환 버튼에 클릭 리스너 추가
        RetryButton.onClick.AddListener(ChangeScene);
    }

    // 씬을 전환하는 함수
    void ChangeScene()
    {
        // 지정된 씬으로 전환
        SceneManager.LoadScene(ingame);
    }
}
