using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FrameManager : MonoBehaviour
{
    public static FrameManager instance;
    public  Canvas Shop, Stat, Inventory, QuestionConfirm, TrainPopup, OnlinePlay, Play, Work;
    public  Button HomeBtn, ShopBtn, StatBtn, BagBtn, TrainpopBtn, OnlinePlayBtn, PlayBtn, WorkBtn;

    public Image BackgroundImage; // 배경 이미지
    private bool isModalOpen = false; // 모달 창이 열려 있는지 여부를 나타내는 플래그


    private void Start()
    {
        instance = this;
        Shop.gameObject.SetActive(false);
        Stat.gameObject.SetActive(false);
        Inventory.gameObject.SetActive(false);
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

        // 1217 추가 팝업 열렸을 시 배경 어둡게
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
            Inventory.gameObject.SetActive(false);
            TrainPopup.gameObject.SetActive(false);
            OnlinePlay.gameObject.SetActive(false);
            Play.gameObject.SetActive(false);
            Work.gameObject.SetActive(false);
        }
    }

    
    /*public  void GoHome()
    {
        SceneManager.LoadScene("Start");
    }

    public void OpenShop()
    {
        Shop.gameObject.SetActive(true);
    }
    public  void CloseShop()
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

    public void OpenQuestionConfirm()
    {
        QuestionConfirm.gameObject.SetActive(true);
    }
    public void CloseQuestionConfirm()
    {
        QuestionConfirm.gameObject.SetActive(false);
    }


    public void OpenTrainPopup()
    {
        TrainPopup.gameObject.SetActive(true);
    }

    public void CloseTrainPopup()
    {
        Debug.Log("훈련창 exit");
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
    }*/

    // 모달 창 열기 함수 수정
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
    public void GoHome()
    {
        SceneManager.LoadScene("Start");
    }

    public void OpenShop()
    {
        OpenModal(Shop);
    }

    public void CloseShop()
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
        OpenModal(Inventory);
    }

    public void CloseInventory()
    {
        CloseModal(Inventory);
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
        OpenModal(Play);
    }

    public void ClosePlay()
    {
        CloseModal(Play);
    }

    public void OpenWork()
    {
        OpenModal(Work);
    }

    public void CloseWork()
    {
        CloseModal(Work);
    }
}
