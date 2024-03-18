using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// UI 캔버스를 관리하는 스크립트
public class FrameManager : MonoBehaviour
{
    // 캔버스 및 버튼 변수
    public static FrameManager instance;
    public Canvas Shop, Stat, inventory, QuestionConfirm, TrainPopup, OnlinePlay, Play, Work;
    public Button HomeBtn, ShopBtn, StatBtn, BagBtn, TrainpopBtn, OnlinePlayBtn, PlayBtn, WorkBtn, FoodBtn;
    public Image BackgroundImage; // 배경 이미지
    public GameObject errorMessagePanel;

    private bool isModalOpen = false; // 모달 창이 열려 있는지 여부를 나타내는 플래그
    private Canvas currentModalCanvas; // 현재 열려 있는 모달 창을 추적하기 위한 변수

    // 초기화 함수
    private void Awake()
    {
        inventory.gameObject.SetActive(true);
    }

    // 시작 함수
    private void Start()
    {
        instance = this;

        // 각 캔버스 초기 비활성화
        Shop.gameObject.SetActive(false);
        Stat.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);
        QuestionConfirm.gameObject.SetActive(false);
        TrainPopup.gameObject.SetActive(false);
        OnlinePlay.gameObject.SetActive(false);
        Play.gameObject.SetActive(false);
        Work.gameObject.SetActive(false);
        

        // 각 버튼에 리스너 추가
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
        if (FoodBtn != null)
        {
            FoodBtn.onClick.AddListener(OpenFood);
        }

        // 배경 이미지 초기 비활성화
        if (BackgroundImage != null)
        {
            BackgroundImage.gameObject.SetActive(false);
        }
    }

    // 업데이트 함수
    private void Update()
    {
        // Escape 키를 누를 때 캔버스 비활성화
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

    // 홈으로 이동 함수
    public void GoHome()
    {
        SceneManager.LoadScene("Start");
    }

    // 상점 열기 함수
    public void OpenShop()
    {
        OpenModal(Shop);
    }

    // 상점 닫기 함수
    public void CloseShop()
    {
        CloseModal(Shop);
    }

    // 스탯 창 열기 함수
    public void OpenStat()
    {
        OpenModal(Stat);
    }

    // 스탯 창 닫기 함수
    public void CloseStat()
    {
        CloseModal(Stat);
    }

    // 인벤토리 열기 함수
    public void OpenInventory()
    {
        Inventory.instance.ClearInventory();
        Inventory.instance.InventoryUpdate();
        OpenModal(inventory);
    }

    // 인벤토리 닫기 함수
    public void CloseInventory()
    {
        CloseModal(inventory);
        Inventory.instance.OpenWithItemType = "All";
    }

    // 사용자에게 확인 문의 열기 함수
    public void OpenQuestionConfirm()
    {
        QuestionConfirm.gameObject.SetActive(true);
    }

    // 확인 문의 닫기 함수
    public void CloseQuestionConfirm()
    {
        QuestionConfirm.gameObject.SetActive(false);
    }

    // 훈련 팝업 열기 함수
    public void OpenTrainPopup()
    {
        OpenModal(TrainPopup);
    }

    // 훈련 팝업 닫기 함수
    public void CloseTrainPopup()
    {
        CloseModal(TrainPopup);
    }

    // 온라인 플레이 열기 함수
    public void OpenOnlinePlay()
    {
        OpenModal(OnlinePlay);
    }

    // 온라인 플레이 닫기 함수
    public void CloseOnlinePlay()
    {
        CloseModal(OnlinePlay);
    }

    // 놀이 창 열기 함수
    public void OpenPlay()
    {
        Inventory.instance.OpenWithItemType = "Toy";
        Inventory.instance.ClearInventory();
        Inventory.instance.InventoryUpdate();
        OpenModal(inventory);
    }

    // 놀이 창 닫기 함수
    public void ClosePlay()
    {
        CloseModal(inventory);
        Inventory.instance.OpenWithItemType = "All";
    }

    // 음식 창 열기 함수
    public void OpenFood()
    {
        Inventory.instance.OpenWithItemType = "Food";
        Inventory.instance.ClearInventory();
        Inventory.instance.InventoryUpdate();
        OpenModal(inventory);
    }

    // 음식 창 닫기 함수
    public void CloseFood()
    {
        CloseModal(inventory);
        Inventory.instance.OpenWithItemType = "All";
    }

    // 일하기 창 열기 함수
    public void OpenWork()
    {
        OpenModal(Work);
    }

    // 일하기 창 닫기 함수
    public void CloseWork()
    {
        CloseModal(Work);
    }


    
    // 에러 패널을 닫는 메서드
    public void CloseErrorMessagePanel()
    {
        errorMessagePanel.SetActive(false);
    }

    // 모달 창 열기 함수
    private void OpenModal(Canvas modalCanvas)
    {
        if (!isModalOpen)
        {
            // 배경 이미지를 어둡게 만들고 활성화
            if (BackgroundImage != null && !BackgroundImage.gameObject.activeSelf)
            {
                BackgroundImage.gameObject.SetActive(true);
            }

            // 모달 창 활성화
            modalCanvas.gameObject.SetActive(true);

            isModalOpen = true;
        }
    }

    // 모달 창 닫기 함수 수정
    public void CloseModal(Canvas modalCanvas)
    {
        if (isModalOpen)
        {
            // 배경 이미지가 비활성화된 경우에만 활성화하도록 수정
            if (BackgroundImage != null && BackgroundImage.gameObject.activeSelf)
            {
                BackgroundImage.gameObject.SetActive(false);
            }

            // 모달 창 비활성화
            modalCanvas.gameObject.SetActive(false);

            isModalOpen = false;
        }
    }
}
