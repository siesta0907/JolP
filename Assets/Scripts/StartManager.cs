using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 시작 화면을 관리하는 클래스
public class StartManager : MonoBehaviour
{
    public Button StartBtn, AlbumBtn, ExitBtn, GalaryExit; // UI 버튼들을 나타내는 변수들
    public Canvas Galary; // 앨범을 나타내는 캔버스

    // 시작 시 호출되는 함수
    void Start()
    {
        // 앨범 캔버스 비활성화
        Galary.gameObject.SetActive(false);

        // 버튼에 클릭 리스너 추가
        if (StartBtn != null)
        {
            StartBtn.onClick.AddListener(StartGame);
        }
        if (AlbumBtn != null)
        {
            AlbumBtn.onClick.AddListener(OpenAlbum);
        }
        if (ExitBtn != null)
        {
            ExitBtn.onClick.AddListener(ExitGame);
        }
    }

    // 프레임마다 호출되는 함수
    private void Update()
    {
        // 뒤로 가기 키를 누르면 앨범 캔버스 비활성화
        if (Input.GetKey(KeyCode.Escape))
        {
            Galary.gameObject.SetActive(false);
        }
    }

    // 게임 시작 함수
    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    // 앨범 열기 함수
    private void OpenAlbum()
    {
        Galary.gameObject.SetActive(true);
    }

    // 앨범 닫기 함수
    private void CloseAlbum()
    {
        Galary.gameObject.SetActive(false);
    }

    // 게임 종료 함수
    private void ExitGame()
    {
        Application.Quit();
    }
}
