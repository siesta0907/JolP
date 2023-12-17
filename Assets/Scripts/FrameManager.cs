using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FrameManager : MonoBehaviour
{
    public static FrameManager instance;
    public  Canvas Shop, Stat, inventory, QuestionConfirm, TrainPopup, OnlinePlay, Play, Work;
    public  Button HomeBtn, ShopBtn, StatBtn, BagBtn, TrainpopBtn, OnlinePlayBtn, PlayBtn, WorkBtn, FoodBtn;

    public Image BackgroundImage; // 배경 이미지
    private bool isModalOpen = false; // 모달 창이 열려 있는지 여부를 나타내는 플래그


    private void Awake()
    {

        inventory.gameObject.SetActive(true);
    }
    private void Start()
    {
        instance = this;
        Shop.gameObject.SetActive(false);
        Stat.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);
        QuestionConfirm.gameObject.SetActive(false);
        TrainPopup.gameObject.SetActive(false);
        OnlinePlay.gameObject.SetActive(false);
        Play.gameObject.SetActive(false);
        Work.gameObject.SetActive(false);

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
        if(FoodBtn != null)
        {
            FoodBtn.onClick.AddListener(OpenFood);
        }

        if (BackgroundImage != null)
        {
            BackgroundImage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Shop.gameObject.SetActive(false);
            Stat.gameObject.SetActive(false);
            inventory.gameObject.SetActive(false);
            TrainPopup.gameObject.SetActive(false);
            OnlinePlay.gameObject.SetActive(false);
            Play.gameObject.SetActive(false);
            Work.gameObject.SetActive(false);
        }
    }


    public  void GoHome()
    {
        SceneManager.LoadScene("Start");
    }

    public void OpenShop()
    {
        OpenModal(Shop);
    }
    public  void CloseShop()
    {
        CloseModal(Shop);
    }

    public void OpenStat()
    {
        OpenModal(Stat);
    }

    public void CloseStat()
    {
        CloseModal(Stat);
    }

    public void OpenInventory()
    {
        Inventory.instance.ClearInventory();
        Inventory.instance.InventoryUpdate();
        OpenModal(inventory);
    }

    public void CloseInventory()
    {
        CloseModal(inventory);
        Inventory.instance.OpenWithItemType = "All";
    }

    public void OpenQuestionConfirm()
    {
        OpenModal(QuestionConfirm);
    }
    public void CloseQuestionConfirm()
    {
        CloseModal(QuestionConfirm);
    }


    public void OpenTrainPopup()
    {
        OpenModal(TrainPopup);
    }

    public void CloseTrainPopup()
    {
        CloseModal(TrainPopup);
    }

    public void OpenOnlinePlay()
    {
        OpenModal(OnlinePlay);
    }

    public void CloseOnlinePlay()
    {
        CloseModal(OnlinePlay);
    }

    public void OpenPlay()
    {
        Inventory.instance.OpenWithItemType = "Toy";
        Inventory.instance.ClearInventory();
        Inventory.instance.InventoryUpdate();

        OpenModal(inventory);
    }

    public void ClosePlay()
    {
        CloseModal(inventory);
        Inventory.instance.OpenWithItemType = "All";
    }

    public void OpenFood()
    {
        Inventory.instance.OpenWithItemType = "Food";
        Inventory.instance.ClearInventory();
        Inventory.instance.InventoryUpdate();
        OpenModal(inventory);
        Debug.Log("인벤토리 음식만 활성화됨!");
    }

    public void CloseFood()
    {
        CloseModal(inventory);
        Inventory.instance.OpenWithItemType = "All";
    }

    public void OpenWork()
    {
        OpenModal(Work);
    }

    public void CloseWork()
    {
        CloseModal(Work);
    }


    private void OpenModal(Canvas modalCanvas)
    {
        if (!isModalOpen)
        {
            // 배경 이미지를 어둡게 만들고 활성화
            if (BackgroundImage != null)
            {
                BackgroundImage.gameObject.SetActive(true);
                BackgroundImage.color = new Color(0, 0, 0, 0.5f); // 어두운 색상
            }

            // 모달 창 활성화
            modalCanvas.gameObject.SetActive(true);

            isModalOpen = true;
        }
    }

    // 모달 창 닫기 함수 수정
    private void CloseModal(Canvas modalCanvas)
    {
        if (isModalOpen)
        {
            // 배경 이미지 비활성화
            if (BackgroundImage != null)
            {
                BackgroundImage.gameObject.SetActive(false);
                BackgroundImage.color = new Color(0, 0, 0, 0); // 투명한 색상
            }

            // 모달 창 비활성화
            modalCanvas.gameObject.SetActive(false);

            isModalOpen = false;
        }
    }
}
