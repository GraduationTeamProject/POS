using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public Button RemoveButton;
    public GameObject[] Items;
    public string ItemName;
    public Transform playerPos;

    private void Start()
    {
        playerPos = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        ItemName = GetComponentInChildren<Text>().text;

    }
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        //item = null;
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void TakeOut()
    {
        
        Debug.Log("꺼내기함수 !!");
        foreach (var item in Items)
        {
            Debug.Log("누른 아이템 이름 :" + ItemName.ToString() + "비교한아이템이름:" + item.name.ToString());
            //이름이 같으면
            if (item.gameObject.name.ToString().Contains(ItemName.ToString()))
                {
                    GameObject InstanItem = Instantiate(item);
                    InstanItem.GetComponent<MeshRenderer>().material.SetVector("_DissolveOffest", new Vector3(0, 1, 0));
                    InstanItem.GetComponent<ItemPickUp>().Item.Count--;
                    ItemController itemController = InstanItem.GetComponent<ItemController>();
                    //나올떄 스르륵나오게하고싶은데..이상하네
                    itemController.Start = true;

                //(추가)개수 0 이면 아이템슬롯UI 안보이게 삭제
                if (InstanItem.GetComponent<ItemPickUp>().Item.Count==0)
                        RemoveItem();
                    else
                        InventoryManager.Instance.ListItems();
                InstanItem.transform.position = playerPos.position + new Vector3(0f, 0f, 1f);
            }
        }
       
    }

}
