using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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

        // 캐릭터 애니메이션 실행
        Animator characterAnimator = characterGameObject.GetComponent<Animator>();
        if (characterAnimator != null)
        {
            characterAnimator.SetTrigger("useItem");
        }

        // 초기화
        QuestionConfirmController.isYes = null;
    }
}
