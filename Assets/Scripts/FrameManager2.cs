using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FrameManager2 : MonoBehaviour
{

    public Canvas Shop, Stat, Inventory, TrainPopup, OnlinePlay, Play, Work;
    public Button HomeBtn, ShopBtn, StatBtn, BagBtn, TrainpopBtn, OnlinePlayBtn, PlayBtn, WorkBtn;

    private void Start()
    {
        Shop.gameObject.SetActive(false);
        Stat.gameObject.SetActive(false);
        Inventory.gameObject.SetActive(false);
        TrainPopup.gameObject.SetActive(false);
        OnlinePlay.gameObject.SetActive(false);
        Play.gameObject.SetActive(false);
        Work.gameObject .SetActive(false);

        if (HomeBtn != null)
        {
            HomeBtn.onClick.AddListener(GoHome);
        }
        if (ShopBtn != null)
        {
            ShopBtn.onClick.AddListener(OpenShop);
        }
        if (StatBtn != null)
        {
            StatBtn.onClick.AddListener(OpenStat);
        }
        if (BagBtn != null)
        {
            BagBtn.onClick.AddListener(OpenInventory);
        }
        if (TrainpopBtn != null)
        {
            TrainpopBtn.onClick.AddListener(OpenTrainPopup);
        }
        if (OnlinePlayBtn != null)
        {
            OnlinePlayBtn.onClick.AddListener(OpenOnlinePlay);
        }
        if (PlayBtn != null)
        {
            PlayBtn.onClick.AddListener(OpenPlay);
        }
        if (WorkBtn != null)
        {
            WorkBtn.onClick.AddListener(OpenWork);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Shop.gameObject.SetActive(false);
            Stat.gameObject.SetActive(false);
            Inventory.gameObject.SetActive(false);
            TrainPopup.gameObject.SetActive(false);
            OnlinePlay.gameObject.SetActive(false);
            Play.gameObject.SetActive(false);
            Work.gameObject.SetActive(false);
        }
    }


    public void GoHome()
    {
        SceneManager.LoadScene("Start");
    }

    public void OpenShop()
    {
        Shop.gameObject.SetActive(true);
    }
    public void CloseShop()
    {
        Shop.gameObject.SetActive(false);
    }

    public void OpenStat()
    {
        Stat.gameObject.SetActive(true);
    }

    public void CloseStat()
    {
        Stat.gameObject.SetActive(false);
    }

    public void OpenInventory()
    {
        Inventory.gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        Inventory.gameObject.SetActive(false);
    }

    public void OpenTrainPopup()
    {
        TrainPopup.gameObject.SetActive(true);
    }

    public void CloseTrainPopup()
    {
        Debug.Log("»∆∑√√¢ exit");
        TrainPopup.gameObject.SetActive(false);
    }
    
    public void OpenOnlinePlay()
    {
        OnlinePlay.gameObject.SetActive(true);
    }

    public void CloseOnlinePlay()
    {
        OnlinePlay.gameObject.SetActive(false);
    }
    
    public void OpenPlay()
    {
        Play.gameObject.SetActive(true);
    }

    public void ClosePlay()
    {
        Play.gameObject.SetActive(false);
    }
    
    public void OpenWork()
    {
        Work.gameObject.SetActive(true);
    }

    public void CloseWork()
    {
        Work.gameObject.SetActive(false);
    }
}
