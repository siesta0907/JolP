using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 씬 전환을 담당하는 클래스
public class SceneChanger : MonoBehaviour
{

    // 씬을 전환하는 함수
    public void ChangeSceneToOnline()
    {
        // 지정된 씬으로 전환
        SceneManager.LoadScene("Multi");
    }

    public void ChangeSceneToMiniGame()
    {
        // 지정된 씬으로 전환
        SceneManager.LoadScene("MiniGame");
    }
}
