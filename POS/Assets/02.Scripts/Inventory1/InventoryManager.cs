using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;   
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //Single Tone Scripte
    public static InventoryManager Instance;

    //인벤토리 리스트
    public List<Item> Items = new List<Item>();

    //인벤토리 슬롯 게임오브젝트리스트
    //public List<GameObject> Slot = new List<GameObject>();

    //인벤토리UI에 표시할 곳
    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;


    public InventoryItemController[] InventoryItems; 
    private void Awake()
    {
        Instance = this;


    }

    public void Update()
    {
        for (int i = Items.Count - 1; i >= 0; i--)
        {
            if (Items[i] != null && Items[i].Count == 0)
            {
                Remove(Items[i]);
            }
        }
    }
    //ItemList에 추가하는 함수
    public void Add(Item item)
    {
        //(추가)원래존재했던거면 리스트에 추가안하고 개수만 변경해준다.
        Item existingItem=Items.Find(i => i.itemName == item.itemName);

        if (existingItem!=null)
        {
            existingItem.Count += 1;
        }
        else
        {
            Items.Add(item);
            item.Count = 1;
        }
            
    }

    //ItemList에서 제거하는 함수
    public void Remove(Item item)
    {
        Items.Remove(item);
        
       
    }

    //인벤토리 리스트를 통해서 인벤토리에 표시하는 함수(인벤토리 새로고침)
    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        //Add한 오브젝트들을 담은 리스트를 모두 돌며
        foreach (var item in Items)
        {
            // (추가)수량이 0보다 큰 아이템만 표시
            if (item.Count>0)
            {
                //아이템슬롯 오브젝트를 Content라는 오브젝트 밑에 생성해준다.
                GameObject obj = Instantiate(InventoryItem, ItemContent);
                obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                //obj.transform.localPosition.z = 0.5f;
                //아이템 이름과 아이콘을 지정해준다.
                var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

                //(추가)개수추가
                var count = obj.transform.Find("ItemCount").GetComponent<Text>();


                itemName.text = item.itemName;
                itemIcon.sprite = item.icon;
                count.text = item.Count.ToString();

                if (EnableRemove.isOn)
                {
                    removeButton.gameObject.SetActive(true);
                }
            }
            

            

        }



        SetInventoryItems();


    }

    public void EnableItemsRemove()
    {
        if(EnableRemove.isOn)
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        //Content밑의 하위 오브젝트들의 InventoryItemController스크립트를 모두 가져온다.
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }

       

    }

    

    //public void CleanInventory()
    //{
    //    foreach(Transform item in ItemContent)
    //    {
    //        Destroy(item.gameObject);
    //    }
    //}

}
