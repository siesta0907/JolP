using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class Inventory : MonoBehaviour
{
    // 인벤토리 싱글톤 인스턴스
    public static Inventory instance;
    public GameObject characterGameObject; // 애니메이션 실행을 위한 선언

    // 아이템과 수량을 저장하는 딕셔너리
    public SerializableDictionary<Item, int> items;

    // UI 요소 배열
    public GameObject[] inventoryItems;
    public GameObject[] itemImages;
    public GameObject[] itemNum;

    //서정 추가 
    public Animator anim;
    public Animator egganim;
    public GameObject skin1; // 이 변수는 Inspector에서 할당해야 합니다.
    public GameObject skin2;
    public GameObject skin3;
    public Tamagotchi tamagotchi;


    // 인벤토리를 특정 아이템 타입으로 열 때 사용하는 필드
    public string OpenWithItemType;

    // 초기화 함수
    public void Start()
    {
        OpenWithItemType = "All";
        instance = this;
        items = new SerializableDictionary<Item, int>();

        // UI 요소들을 태그로 찾아 배열에 할당
        itemImages = GameObject.FindGameObjectsWithTag("itemImage");
        itemNum = GameObject.FindGameObjectsWithTag("itemNum");
        inventoryItems = GameObject.FindGameObjectsWithTag("inventoryItem");

        // 초기화 시 모든 아이템 이미지를 비활성화
        foreach (var itemImage in itemImages)
        {
            itemImage.SetActive(false);
        }
    }

    // 인벤토리 초기화 함수
    public void ClearInventory()
    {
        int i = 0;
        foreach (var itemimage in itemImages)
        {
            itemimage.SetActive(true);
            itemimage.GetComponent<Image>().sprite = null;
            itemimage.SetActive(false);
            itemNum[i].GetComponent<Text>().text = "";
            inventoryItems[i].GetComponent<inventoryItemClick>().item = null;
            i += 1;
        }
    }

    // 인벤토리 업데이트 함수
    public void InventoryUpdate()
    {
        if (items.Count > 0)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (OpenWithItemType == "All" || OpenWithItemType == item.Key.itemType)
                {
                    if (itemImages.Length > i)
                    {
                        itemImages[i].SetActive(true);
                        itemImages[i].GetComponent<Image>().sprite = item.Key.itemImage;
                        itemNum[i].GetComponent<Text>().text = item.Value.ToString();
                        inventoryItems[i].GetComponent<inventoryItemClick>().item = item.Key;
                        i += 1;
                    }
                }
            }
        }
    }

    // 아이템 획득 함수
    public void GetItem(Item item)
    {
        if (items.ContainsKey(item))
        {
            items[item] += 1;
        }
        else
        {
            items.Add(item, 1);
        }
    }

    // 아이템 사용 함수
    public void UseItem(Item item)
    {
        if (items.ContainsKey(item) && items[item] > 0)
        {
            FrameManager.instance.OpenQuestionConfirm();
            QuestionConfirmController.instance.SetQuestion(string.Format($"{item.itemName}을 사용하시겠습니까?"));
            StartCoroutine(ConfirmUseItem(item));
        }
    }

    // 아이템 사용 확인 코루틴
    private IEnumerator ConfirmUseItem(Item item)
    {
        QuestionConfirmController.instance.buttonClickedTask = new TaskCompletionSource<bool>();
        Button YesButton = QuestionConfirmController.instance.YesBtn;
        Button NoButtom = QuestionConfirmController.instance.NoBtn;

        yield return new WaitUntil(() => QuestionConfirmController.instance.buttonClickedTask.Task.IsCompleted);

        if (QuestionConfirmController.isYes == true)
        {
            // 아이템 효과 적용
            foreach (var stat in item.itemEffect)
            {
                EggMonStat.IncreaseStat(stat.Key, stat.Value);
                Debug.Log(string.Format($"{stat.Key} 스탯이 {stat.Value}만큼 증가하였습니다."));
            }

            // 아이템 수량 감소
            items[item] -= 1;

            /// 아이템 사용 애니메이션 실행

            /// 알 상태일 때

            // 알 상태일 때 애니메이션 실행
            if (tamagotchi.state == Tamagotchi.State.EGG)
            {
                // "Apple", "Can", "Steak" 아이템에 대한 알 상태 애니메이션
                if (item.itemName == "Apple" && item.Egg_Motion != null)
                {
                    EggMonStat.IncreaseStat("health", 30);

                    StartCoroutine(PlayAnimationRepeat("Eggeat", 2));

                    // 에러 패널 비활성화
                    tamagotchi.errorMessagePanel.SetActive(false);
                }
                if (item.itemName == "Can" && item.Egg_Motion != null)
                {
                    EggMonStat.IncreaseStat("full", 30);

                    StartCoroutine(PlayAnimationRepeat("Eggeat", 2));                    // 에러 패널 비활성화
                    tamagotchi.errorMessagePanel.SetActive(false);
                }
                if (item.itemName == "Steak" && item.Egg_Motion != null)
                {
                    EggMonStat.IncreaseStat("health", 100);

                    StartCoroutine(PlayAnimationRepeat("Eggeat", 2));                    // 에러 패널 비활성화
                    tamagotchi.errorMessagePanel.SetActive(false);
                }
                if (item.itemName == "Skin Berry" || item.itemName == "Skin Bread" || item.itemName == "Skin Rabbit")
                {
                    tamagotchi.errorMessagePanel.SetActive(true);

                }
            }


            


            // Child 상태일 때
            if (tamagotchi.state == Tamagotchi.State.CHILD)
            {
                // CHILD 상태에서의 아이템 사용 및 애니메이션 로직
                // 예: "Apple" 아이템 사용 시
                if (item.itemName == "Apple" && item.Animation_Motion != null)
                {
                    EggMonStat.IncreaseStat("health", 30);

                    StartCoroutine(PlayAnimationRepeat("isEat", 3));
                    tamagotchi.errorMessagePanel.SetActive(false);
                }
                else if (item.itemName == "Can" && item.Animation_Motion != null)
                {
                    EggMonStat.IncreaseStat("full", 30);

                    StartCoroutine(PlayAnimationRepeat("EatCan", 3));
                }
                else if (item.itemName == "Steak" && item.Animation_Motion != null)
                {
                    EggMonStat.IncreaseStat("health", 100);

                    StartCoroutine(PlayAnimationRepeat("EatSteak", 3));
                }

            
            // 장난감
            if (item.itemName == "Block" && item.Animation_Motion != null)
                {
                    EggMonStat.IncreaseStat("playfulness", 30);

                    anim.SetBool("playMotion", true);


                }
                if (item.itemName == "Car" && item.Animation_Motion != null)
                {
                    EggMonStat.IncreaseStat("playfulness", 50);

                    anim.SetBool("playMotion", true);

                }
                if (item.itemName == "Game" && item.Animation_Motion != null)
                {
                    EggMonStat.IncreaseStat("playfulness", 90);

                    anim.SetBool("playMotion", true);

                }
                //스킨
                if (item.itemName == "Skin Berry")
                {
                    tamagotchi.ApplySkin(0); // ApplySkin 호출, 배열의 첫 번째 스킨
                    ShowSkinUI(item.itemName); // UI 업데이트
                    Debug.Log("스킨1");
                }
                else if (item.itemName == "Skin Bread")
                {
                    tamagotchi.ApplySkin(1); // 배열의 두 번째 스킨
                    ShowSkinUI(item.itemName); // UI 업데이트

                }
                else if (item.itemName == "Skin Rabbit")
                {
                    tamagotchi.ApplySkin(2); // 배열의 세 번째 스킨
                    ShowSkinUI(item.itemName); // UI 업데이트

                }
            }

            // TEEN 상태일때
            else if (tamagotchi.state == Tamagotchi.State.TEEN || tamagotchi.state == Tamagotchi.State.ADULT)
            {
                if (item.itemName == "Skin Berry" || item.itemName == "Skin Bread" || item.itemName == "Skin Rabbit")
                {
                    tamagotchi.errorMessagePanel.SetActive(true);

                }

                if (item.itemName == "Block" || item.itemName == "Car" || item.itemName == "Game")
                {
                    tamagotchi.errorMessagePanel.SetActive(true);

                }
            }

            
                


            // 수량이 0 이하이면 딕셔너리에서 아이템 제거
            if (items[item] <= 0)
            {
                items.Remove(item);
            }

            // 인벤토리 클리어 후 업데이트
            ClearInventory();
            InventoryUpdate();
        }
        
        // 현재 캔버스 닫기 (예: 인벤토리 캔버스)
        FrameManager.instance.CloseModal(FrameManager.instance.inventory); // currentModalCanvas는 현재 열린 캔버스의 참조
        

        // 초기화
        QuestionConfirmController.isYes = null;
    }

    // 애니메이션을 지정된 횟수만큼 반복한 후 비활성화하는 Coroutine
    IEnumerator PlayAnimationRepeat(string animationBoolName, int repeatCount)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            anim.SetBool(animationBoolName, true);
            egganim.SetBool(animationBoolName, true);
            tamagotchi.EGGTalkPanel.SetActive(true);
            // 애니메이션이 완전히 끝날 때까지 기다림 (애니메이션 길이를 조정하세요)
            yield return new WaitForSeconds(1.5f); // 예제로 1초 설정

            anim.SetBool(animationBoolName, false);
            tamagotchi.EGGTalkPanel.SetActive(false);
            egganim.SetBool(animationBoolName, false);
            // 다음 반복 전에 짧은 대기 시간을 넣음 (필요에 따라 조절)
            //yield return new WaitForSeconds(0.1f);
        }
    }
    // 스킨 UI 활성화 함수
    public void ShowSkinUI(string skinName)
    {
        // 모든 스킨 UI를 먼저 비활성화
        skin1.SetActive(false);
        skin2.SetActive(false);
        skin3.SetActive(false);

        // 스킨 이름에 따라 해당하는 스킨 UI를 활성화
        switch (skinName)
        {
            case "Skin Berry":
                skin1.SetActive(true);
                break;
            case "Skin Bread":
                skin2.SetActive(true);
                break;
            case "Skin Rabbit":
                skin3.SetActive(true);
                break;
        }
    }

}
