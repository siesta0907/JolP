using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public Button StartBtn, AlbumBtn, ExitBtn;
    public Canvas Galary;

    void Start()
    {
        Galary.gameObject.SetActive(false);
        
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
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Galary.gameObject.SetActive(false);
        }
    }
    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    private void OpenAlbum()
    {
        Galary.gameObject.SetActive(true);
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
