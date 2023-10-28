using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Button RetryButton; // 여기에 전환 버튼을 할당하세요.
    public string ingame; // 여기에 로드할 씬의 이름을 입력하세요.

    void Start()
    {
        RetryButton.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(ingame);
    }
}
