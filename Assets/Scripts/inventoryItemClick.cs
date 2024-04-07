using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �κ��丮 ������ Ŭ���� ���� ������ ó���ϴ� ��ũ��Ʈ
public class inventoryItemClick : MonoBehaviour
{
    public Item item; // �ش� �κ��丮 �����ۿ� ����� ������ ��ü
    private Button InventoryItem; // �κ��丮 �������� ��Ÿ���� ��ư
    public Tamagotchi tamagotchi;

    // ���� �� ȣ��Ǵ� �Լ�
    void Start()
    {
        // ��ư ������Ʈ �Ҵ�
        InventoryItem = gameObject.GetComponent<Button>();

        // Ŭ�� ������ �߰�
        InventoryItem.onClick.AddListener(InventoryClick);
    }

    // �κ��丮 �������� Ŭ���Ǿ��� �� ȣ��Ǵ� �Լ�
    void InventoryClick()
    {
        // EGG ���¿����� �峭�� ������ ��� ����
        if (Tamagotchi.instance.state == Tamagotchi.State.EGG && item.itemType == "Toy")
        {
            Debug.Log("EGG ���¿����� �峭���� ����� �� �����ϴ�.");
            Tamagotchi.instance.PlayErrorPanel.SetActive(true);
        }
        if (Tamagotchi.instance.state == Tamagotchi.State.TEEN && item.itemType == "Toy")
        {
            Debug.Log("TEEN ���¿����� �峭���� ����� �� �����ϴ�.");
            Tamagotchi.instance.PlayErrorPanel.SetActive(true);
        }
        if (Tamagotchi.instance.state == Tamagotchi.State.ADULT && item.itemType == "Toy")
        {
            Debug.Log("ADULT ���¿����� �峭���� ����� �� �����ϴ�.");
            Tamagotchi.instance.PlayErrorPanel.SetActive(true);
        }

        // �κ��丮 �������� �ִ� ��쿡�� ������ ��� �Լ� ȣ��
        if (Inventory.instance.items.Count > 0)
        {
            Inventory.instance.UseItem(item);
        }
    }
}
