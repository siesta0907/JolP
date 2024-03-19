using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// 아이템 정보를 나타내는 클래스
public class Item : MonoBehaviour
{
    public int price; // 아이템 가격
    public SerializableDictionary<string, int> itemEffect; // 아이템 효과를 저장하는 딕셔너리
    public string itemName; // 아이템 이름
    private Text itemPriceText; // 아이템 가격을 표시하는 텍스트
    private Button itemBtn; // 아이템을 나타내는 버튼
    public Sprite itemImage; // 아이템 이미지
    public string itemType; // 아이템 종류

    //추가
    public AnimationClip Egg_Motion; // 알 먹는 모션
    public AnimationClip Animation_Motion;    // Child 먹는 모션


    // 시작 시 호출되는 함수
    private void Start()
    {
        // 아이템 효과 딕셔너리 초기화
        itemEffect = new SerializableDictionary<string, int>();

        // 아이템 이미지 할당
        itemImage = GetComponent<Image>().sprite;

        // 아이템 버튼 및 텍스트 컴포넌트 할당
        itemBtn = GetComponentInParent<Button>();
        itemPriceText = GetComponentInChildren<Text>();

        // 아이템 구매 버튼에 클릭 리스너 추가
        itemBtn.onClick.AddListener(PurchaseItem);

        // 아이템 가격 표시
        itemPriceText.text = string.Format($"{price} coin");

        // 아이템 효과 설정
        SetEffect();
    }

    // 아이템 효과를 설정하는 함수
    private void SetEffect()
    {
        // 아이템 이름에 따라 효과를 설정

        //음식 효과
        if (itemName == "Apple")
        {
            itemEffect.Add("full", 30);
        }
        if (itemName == "Steak")
        {
            itemEffect.Add("full", 100);

        }
        if (itemName == "Can")
        {
            itemEffect.Add("full", 30);
        }

        //장난감 효과
        if (itemName == "Block")
        {
            itemEffect.Add("playfulness", 30);
        }
        if (itemName == "Car")
        {
            itemEffect.Add("playfulness", 50);
        }
        if (itemName == "Game")
        {
            itemEffect.Add("playfulness", 90);
        }


    }

    // 아이템을 구매하는 함수
    private void PurchaseItem()
    {
        // 인벤토리가 가득 차 있고, 현재 아이템이 인벤토리에 없는 경우
        if (Inventory.instance.itemImages.Length == Inventory.instance.items.Count && !Inventory.instance.items.ContainsKey(this))
        {
            Debug.Log("인벤토리가 가득찼습니다.");
            return;
        }

        // 소지한 돈이 아이템 가격보다 많은 경우
        if (MoneyManager.money > price)
        {
            FrameManager.instance.OpenQuestionConfirm();
            QuestionConfirmController.instance.SetQuestion(string.Format($"{itemName}을 구매하시겠습니까?"));
            StartCoroutine(ConfirmPayItem(this));
        }
        else
        {
            // 돈이 부족한 경우
            Debug.Log("골드가 부족하여 구매에 실패하였습니다.");
        }
    }
    // 아이템 사용 확인 코루틴
    private IEnumerator ConfirmPayItem(Item item)
    {
        QuestionConfirmController.instance.buttonClickedTask = new TaskCompletionSource<bool>();
        Button YesButton = QuestionConfirmController.instance.YesBtn;
        Button NoButtom = QuestionConfirmController.instance.NoBtn;

        yield return new WaitUntil(() => QuestionConfirmController.instance.buttonClickedTask.Task.IsCompleted);

        if (QuestionConfirmController.isYes == true)
        {
            // 인벤토리에 아이템 추가 및 돈 차감
            Inventory.instance.GetItem(this);
            MoneyManager.money -= price;
        }
        else
        {

        }
        Inventory.instance.ClearInventory();
        Inventory.instance.InventoryUpdate();

        // 현재 캔버스 닫기
        FrameManager.instance.CloseModal(FrameManager.instance.Shop); // currentModalCanvas는 현재 열린 캔버스의 참조

        // 초기화
        QuestionConfirmController.isYes = null;
    }
}
