using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    /*이 스크립트는 아이템슬롯에 적용되는 스크립트로, 
     인벤토리 UI에 표시되는 아이템을 삭제하거나,
    아이템을 리스트에 더하거나, 아이템을 꺼내는 함수가 작성되어있습니다.*/
    public Item item;       
    public Button RemoveButton;
    public GameObject[] Items;
    public string ItemName;
    public Transform playerPos;

    private GameObject InstanItem;

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


                    InstanItem = Instantiate(item);
                    Debug.Log("InstanItem layer:" + InstanItem.layer);
                    if(item.layer==8)
                        BuildManager.Instance.OriginalPrefab = InstanItem;
                
                   

                    

                //설치가능 오브젝트가 아닐때만
                if (InstanItem.layer!=8)
                {
                    //쉐이더 효과
                    if (InstanItem.GetComponent<MeshRenderer>() != null)
                        InstanItem.GetComponent<MeshRenderer>().material.SetVector("_DissolveOffest", new Vector3(0, 1, 0));
                    else
                        InstanItem.GetComponentInChildren<MeshRenderer>().material.SetVector("_DissolveOffest", new Vector3(0, 1, 0));
                }
                   

                //꺼냈으면 아이템 개수를 줄여야 하기 위한 처리
                InstanItem.GetComponent<ItemPickUp>().Item.Count--;
                    ItemController itemController = InstanItem.GetComponent<ItemController>();
                    itemController.Start = true;

                //(추가)개수 0 이면 아이템슬롯UI 안보이게 삭제
                if (InstanItem.GetComponent<ItemPickUp>().Item.Count==0)
                        RemoveItem();
                    else
                        InventoryManager.Instance.ListItems();
                if(InstanItem.layer!=8)
                    InstanItem.transform.position = playerPos.position + new Vector3(0f, 0f, 1f);
            }
        }
       
    }

}
