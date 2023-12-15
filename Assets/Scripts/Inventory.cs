using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public SerializableDictionary<Item, int> items;
    public GameObject InventoryItemUI;

    public GameObject[] inventoryItems;
    public GameObject[] itemImages;
    public GameObject[] itemNum;

    

    public void Start()
    {
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

    private void Update()
    {
        if (items.Count > 0)
        {
            int i = 0;
            foreach (var item in items)
            {
                if(itemImages.Length > i)
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
            QuestionConfirmController.instance.SetQuestion(string.Format($"{item.itemName}�� ����Ͻðڽ��ϱ�?"));
            
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
                Debug.Log(string.Format($"{stat.Key} ������ {stat.Value}��ŭ �����Ͽ����ϴ�."));
            }
            items[item] -= 1;
        }
        QuestionConfirmController.isYes = null;
    }

}