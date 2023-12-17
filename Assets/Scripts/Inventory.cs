using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public SerializableDictionary<Item, int> items;
   // public GameObject InventoryItemUI;

    public GameObject[] inventoryItems;
    public GameObject[] itemImages;
    public GameObject[] itemNum;
    public string OpenWithItemType;
    

    public void Start()
    {
        OpenWithItemType = "All";
        instance = this;
        items = new SerializableDictionary<Item, int>();

        itemImages = GameObject.FindGameObjectsWithTag("itemImage");
        itemNum = GameObject.FindGameObjectsWithTag("itemNum");
        inventoryItems = GameObject.FindGameObjectsWithTag("inventoryItem");
        foreach (var itemImage in itemImages)
        {
            itemImage.SetActive(false);
        }
    }

    public void ClearInventory()
    {
        if (items.Count > 0)
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
    }

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
public void UseItem(Item item)
    {
        if (items.ContainsKey(item) && items[item] > 0)
        {
            FrameManager.instance.OpenQuestionConfirm();
            QuestionConfirmController.instance.SetQuestion(string.Format($"{item.itemName}을 사용하시겠습니까?"));
            StartCoroutine(ConfirmUseItem(item));

        }
        
    }
    private IEnumerator ConfirmUseItem(Item item)
    {
        QuestionConfirmController.instance.buttonClickedTask = new TaskCompletionSource<bool>();
        Button YesButton = QuestionConfirmController.instance.YesBtn;
        Button NoButtom = QuestionConfirmController.instance.NoBtn;

        yield return new WaitUntil(() => QuestionConfirmController.instance.buttonClickedTask.Task.IsCompleted);

        if (QuestionConfirmController.isYes == true)
        {
            foreach (var stat in item.itemEffect)
            {
                EggMonStat.IncreaseStat(stat.Key, stat.Value);
                Debug.Log(string.Format($"{stat.Key} 스탯이 {stat.Value}만큼 증가하였습니다."));
            }
            items[item] -= 1;
            if(items[item] <= 0)
            {
                items.Remove(item);
            }
            ClearInventory();
            InventoryUpdate();
        }
        QuestionConfirmController.isYes = null;
    }

}
