using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;   
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //Single Tone Scripte
    public static InventoryManager Instance;

    //�κ��丮 ����Ʈ
    public List<Item> Items = new List<Item>();

    //�κ��丮 ���� ���ӿ�����Ʈ����Ʈ
    //public List<GameObject> Slot = new List<GameObject>();

    //�κ��丮UI�� ǥ���� ��
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
    //ItemList�� �߰��ϴ� �Լ�
    public void Add(Item item)
    {
        //(�߰�)���������ߴ��Ÿ� ����Ʈ�� �߰����ϰ� ������ �������ش�.
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

    //ItemList���� �����ϴ� �Լ�
    public void Remove(Item item)
    {
        Items.Remove(item);
        
       
    }

    //�κ��丮 ����Ʈ�� ���ؼ� �κ��丮�� ǥ���ϴ� �Լ�(�κ��丮 ���ΰ�ħ)
    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        //Add�� ������Ʈ���� ���� ����Ʈ�� ��� ����
        foreach (var item in Items)
        {
            // (�߰�)������ 0���� ū �����۸� ǥ��
            if (item.Count>0)
            {
                //�����۽��� ������Ʈ�� Content��� ������Ʈ �ؿ� �������ش�.
                GameObject obj = Instantiate(InventoryItem, ItemContent);
                obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                //obj.transform.localPosition.z = 0.5f;
                //������ �̸��� �������� �������ش�.
                var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

                //(�߰�)�����߰�
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
        //Content���� ���� ������Ʈ���� InventoryItemController��ũ��Ʈ�� ��� �����´�.
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
