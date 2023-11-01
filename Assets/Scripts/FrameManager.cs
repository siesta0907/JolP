using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FrameManager : MonoBehaviour
{

    public Canvas Shop, Stat, Inventory;
    public Button HomeBtn, ShopBtn, StatBtn, BagBtn;

    private void Start()
    {
        Shop.gameObject.SetActive(false);
        Stat.gameObject.SetActive(false);
        Inventory.gameObject.SetActive(false);


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
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Shop.gameObject.SetActive(false);
            Stat.gameObject.SetActive(false);
            Inventory.gameObject.SetActive(false);
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
}
