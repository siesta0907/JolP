using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 인벤토리 아이템 클릭에 대한 동작을 처리하는 스크립트
public class inventoryItemClick : MonoBehaviour
{
    public Item item; // 해당 인벤토리 아이템에 연결된 아이템 객체
    private Button InventoryItem; // 인벤토리 아이템을 나타내는 버튼
    public Tamagotchi tamagotchi;

    // 시작 시 호출되는 함수
    void Start()
    {
        // 버튼 컴포넌트 할당
        InventoryItem = gameObject.GetComponent<Button>();

        // 클릭 리스너 추가
        InventoryItem.onClick.AddListener(InventoryClick);
    }

    // 인벤토리 아이템이 클릭되었을 때 호출되는 함수
    void InventoryClick()
    {
        // EGG 상태에서는 장난감 아이템 사용 금지
        if (Tamagotchi.instance.state == Tamagotchi.State.EGG && item.itemType == "Toy")
        {
            Debug.Log("EGG 상태에서는 장난감을 사용할 수 없습니다.");
            Tamagotchi.instance.PlayErrorPanel.SetActive(true);
        }
        if (Tamagotchi.instance.state == Tamagotchi.State.TEEN && item.itemType == "Toy")
        {
            Debug.Log("TEEN 상태에서는 장난감을 사용할 수 없습니다.");
            Tamagotchi.instance.PlayErrorPanel.SetActive(true);
        }
        if (Tamagotchi.instance.state == Tamagotchi.State.ADULT && item.itemType == "Toy")
        {
            Debug.Log("ADULT 상태에서는 장난감을 사용할 수 없습니다.");
            Tamagotchi.instance.PlayErrorPanel.SetActive(true);
        }

        // 인벤토리 아이템이 있는 경우에만 아이템 사용 함수 호출
        if (Inventory.instance.items.Count > 0)
        {
            Inventory.instance.UseItem(item);
        }
    }
}
