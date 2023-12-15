using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class inventoryItemClick : MonoBehaviour
{
    public Item item;
    private Button InventoryItem;

    void Start()
    {
        InventoryItem = gameObject.GetComponent<Button>();
        InventoryItem.onClick.AddListener(InventoryClick);
    }

    void InventoryClick()
    {
        if (Inventory.instance.items.Count > 0)
        {
            Inventory.instance.UseItem(item);
        }
    }
}
